using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Vfi.Ui.Mvc.Vfi.Areas.Factory.Models;
using Vfi.Ui.Mvc.Vfi.Areas.Inv.Models;
using Vfi.Ui.Mvc.Vfi.Models;
using Vfi.Ui.Mvc.Vfi.Models.Production;
using System.Web;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core;
using System.Text;
using System.Security.Cryptography;
using System.Drawing;

namespace Vfi.Ui.Mvc.Vfi.Utilities {

    public static class MyUtilities {

        #region my system
        public static class MySystem {
            public static string CultureEN = "en-EN";
            public static string CultureVN = "vi-VN";
            public static string LotNumber_Weekly(DateTime date) {
                DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                Calendar cal = dfi.Calendar;
                return string.Format("{0:00}", date.Year % 100) + string.Format("{0:00}", cal.GetWeekOfYear(date, dfi.CalendarWeekRule, dfi.FirstDayOfWeek));
            }
            public static string LotNumber_Monthly(DateTime date) {
                return string.Format("{0:00}", date.Year % 100) + string.Format("{0:00}", date.Month);
            }

            public static string HelloUser(string name) {
                string a = "Xin chào ";
                string now = DateTime.Now.ToString("t");
                string morning = DateTime.Parse("5:30 AM").ToString("t");
                string noon = DateTime.Parse("10:00 AM").ToString("t");
                string evening = DateTime.Parse("3:00 PM").ToString("t");
                string night = DateTime.Parse("5:00 PM").ToString("t");
                if (DateTime.Parse(now) >= DateTime.Parse(morning) && DateTime.Parse(now) < DateTime.Parse(noon))
                    a += "buổi sáng: ";
                else if (DateTime.Parse(now) >= DateTime.Parse(noon) && DateTime.Parse(now) < DateTime.Parse(evening))
                    a += "buổi trưa: ";
                else if (DateTime.Parse(now) >= DateTime.Parse(evening) && DateTime.Parse(now) < DateTime.Parse(night))
                    a += "buổi chiều: ";
                else
                    a += "buổi tối: ";
                a += name;
                return a;
            }

            public static PageConfigModel GetPageConfig(string username) {
                var model = new WorkGroupModel() {
                    Theme = "office2007",
                    BackgroundImage = "bg_body.jpg",
                    ImagePath = "/vfi/Content/Images"
                }; // set default
                using (var vfi = new tammaContext()) {
                    var user = vfi.Users.FirstOrDefault(x => x.Username.Equals(username));
                    if (user != null) {
                        model.Description = user.FullName;
                    }

                    var workgroup = vfi.WorkGroups.FirstOrDefault(x => x.Active);
                    if (workgroup != null) {
                        model.WorkGroupName = "- " + workgroup.WorkGroupName;
                        model.WorkGroupCode = workgroup.WorkGroupCode;
                        if (!string.IsNullOrWhiteSpace(workgroup.Theme)) {
                            model.Theme = workgroup.Theme;
                        }
                        if (!string.IsNullOrWhiteSpace(workgroup.BackgroundImage)) {
                            model.BackgroundImage = workgroup.BackgroundImage;
                        }
                        if (!string.IsNullOrWhiteSpace(workgroup.LogoImage)) {
                            model.LogoImage = workgroup.LogoImage;
                        }
                        if (!string.IsNullOrWhiteSpace(workgroup.LogoImage)) {
                            model.LogoImage = workgroup.LogoImage;
                        }
                        if (!string.IsNullOrWhiteSpace(workgroup.ImagePath)) {
                            model.ImagePath = workgroup.ImagePath;
                        }
                        if (!string.IsNullOrWhiteSpace(workgroup.PageTitleColor)) {
                            model.PageTitleColor = workgroup.PageTitleColor;
                        }
                    }
                }

                return new PageConfigModel {
                    PageName = model.WorkGroupName,
                    PageTheme = model.ThemeCss,
                    BackgroundImage = model.BackgroundImage,
                    LogoImage = model.LogoImage,
                    ImagePath = model.ImagePath,
                    PageTitleColor = model.PageTitleColor,
                    UserLoginFullName = model.Description
                };
            }

            public static string GetContentPath() {
                if (System.Web.HttpContext.Current != null)
                    return System.Web.HttpContext.Current.Server.MapPath("~/Content");
                return HttpRuntime.AppDomainAppPath + "Content";
            }
            public static string GetUtilityPath() {
                if (System.Web.HttpContext.Current != null)
                    return System.Web.HttpContext.Current.Server.MapPath("~/Utilities");
                return HttpRuntime.AppDomainAppPath + "Utilities";
            }
            public static string GetLogPath() {
                if (System.Web.HttpContext.Current != null)
                    return System.Web.HttpContext.Current.Server.MapPath("~/Logs");
                return HttpRuntime.AppDomainAppPath + "Logs";
            }

            public static string FetchExceptionMessage(Exception ex) {
                if (ex.GetType() == typeof(DbEntityValidationException)) {
                    var exType = (DbEntityValidationException)ex;
                    return exType.EntityValidationErrors.First().Entry.Entity.ToString() + ": "
                        + exType.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage ?? ex.Message;
                }
                else if (ex.GetType() == typeof(DbUpdateException)) {
                    var exType = (DbUpdateException)ex;
                    return exType.InnerException.InnerException.Message ?? ex.Message;
                }
                else if (ex.InnerException != null) {
                    return ex.InnerException.InnerException.Message ?? ex.Message;
                }
                //else if (ex.GetType() == typeof(EntityCommandExecutionException)) {
                //    var exType = (EntityCommandExecutionException)ex;
                //    return exType.InnerException.InnerException.Message ?? ex.Message;
                //}
                //else if (ex.GetType() == typeof(DbUpdateException)) {
                //    var exType = (DbUpdateException)ex;
                //    return exType.InnerException.InnerException.Message ?? ex.Message;
                //}
                return ex.Message;
            }
            public class MyStatusModel {
                public int Value { get; set; }
                public string Text { get; set; }
            }

            public static string MyHash(string text) {
                Encoding enc = Encoding.ASCII;
                byte[] buffer = enc.GetBytes(text);

                var crypto = new SHA256CryptoServiceProvider();
                byte[] hash = crypto.ComputeHash(buffer);

                return Encoding.Unicode.GetString(hash);
            }
            const long MUST_BE_LESS_THAN = 10000000000; // 10 decimal digits

            public static long GetStableHash(string s) {
                uint hash = 0;
                // if you care this can be done much faster with unsafe 
                // using fixed char* reinterpreted as a byte*
                foreach (byte b in System.Text.Encoding.Unicode.GetBytes(s)) {
                    hash += b;
                    hash += (hash << 10);
                    hash ^= (hash >> 6);
                }
                // final avalanche
                hash += (hash << 3);
                hash ^= (hash >> 11);
                hash += (hash << 15);
                // helpfully we only want positive integer < MUST_BE_LESS_THAN
                // so simple truncate cast is ok if not perfect
                return (long)(hash % MUST_BE_LESS_THAN);
            }

            public static string Base64Encode(string plainText) {
                var plainTextBytes = System.Text.Encoding.Unicode.GetBytes(plainText);
                return System.Convert.ToBase64String(plainTextBytes);
            }
            public static string Base64Decode(string base64EncodedData) {
                //var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                //return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

                byte[] base64SingleBytes = Convert.FromBase64String(base64EncodedData);
                return System.Text.Encoding.Unicode.GetString(base64SingleBytes);
            }
            //public static string GetStableDeHash(long s) {
            //    uint hash = 0;
            //    // if you care this can be done much faster with unsafe 
            //    // using fixed char* reinterpreted as a byte*
            //    foreach (byte b in System.Text.Decoder.GetBytes(s)) {
            //        hash += b;
            //        hash += (hash << 10);
            //        hash ^= (hash >> 6);
            //    }
            //    // final avalanche
            //    hash += (hash << 3);
            //    hash ^= (hash >> 11);
            //    hash += (hash << 15);
            //    // helpfully we only want positive integer < MUST_BE_LESS_THAN
            //    // so simple truncate cast is ok if not perfect
            //    return (long)(hash % MUST_BE_LESS_THAN);
            //}
        }

        #endregion

        #region user
        public static class UserRole {
            /// <summary>
            /// quyền nhập sx1
            /// </summary>
            public static int ImportSx1 = 43;
            /// <summary>
            /// quyền nhập phay CNC
            /// </summary>
            public static int ImportCNC = 63;
            /// <summary>
            /// quyền xuất gia công ngoài
            /// </summary>
            public static int ExportPlating = 44;
            /// <summary>
            /// quyền nhập gia công ngoài
            /// </summary>
            public static int ImportPlating = 48;
            /// <summary>
            /// quyền xuất thành phẩm
            /// </summary>
            public static int ExportFinish = 25;
            /// <summary>
            /// xem đơn giá
            /// </summary>
            public static int SeePrice = 49;
            /// <summary>
            /// duyet phe pham
            /// </summary>
            public static int ApproveDefect = 50;
            /// <summary>
            /// Chỉnh sửa mã sản phẩm
            /// </summary>
            public static int EditProductCode = 62;
            /// <summary>
            /// Có thể chọn ngày chênh lệch (5 ngày)
            /// </summary>
            public static int SetTransactionDate = 75;
            /// <summary>
            /// Chỉnh sửa trọng lượng SX1
            /// </summary>
            public static int EditProductSx1 = 77;
            /// <summary>
            /// Chỉnh sửa trọng lượng GCN
            /// </summary>
            public static int EditProductGcnWeight = 78;
            /// <summary>
            /// Chỉnh SP
            /// </summary>
            public static int EditProductCommon = 98;
            /// <summary>
            /// Là quản lý sản xuất
            /// </summary>
            public static int ProductionManagement = 111;
            /// <summary>
            /// Là quản lý kinh doanh
            /// </summary>
            public static int SaleManagement = 123;
            /// <summary>
            /// Là quản lý kinh doanh cấp 2
            /// </summary>
            public static int SaleManagementLv2 = 231;
            /// <summary>
            /// Là quản lý sản xuất 2
            /// </summary>
            public static int Production2Management = 150;
            /// <summary>
            /// Là quản lý mua hàng
            /// </summary>
            public static int PurchasingManagement = 140;
            /// <summary>
            /// Là quản lý kho
            /// </summary>
            public static int InvManagement = 151;
            /// <summary>
            /// Là quản lý kho lv 2
            /// </summary>
            public static int InvManagementLv2 = 242;
            /// <summary>
            /// Là quản lý kỹ thuật
            /// </summary>
            public static int TechicalManagerLv1 = 237;
            /// <summary>
            /// Là quản lý kỹ thuật lv2
            /// </summary>
            public static int TechicalManagerLv2 = 238;
            /// <summary>
            /// Là quản lý kỹ thuật lv2
            /// </summary>
            public static int ApproveInternalProduct = 285;
            public static int ApproveInternalMaterial = 286;
            public static int ApproveInternalFuel = 287;
            public static int ApproveInternalTool = 288;
            /// <summary>
            /// Là Trưởng bộ phận duyet YC MH
            /// </summary>
            public static int ManagementApprovedPurchase = 383;

            public static string ManagementApprovedPurchase2 = "Duyệt Yêu cầu mua hàng";


            public static bool CheckRole2(string userName, string functionName) {
                if (string.IsNullOrWhiteSpace(userName)) 
                    return false;
                var check = false;
                using (var vfi = new vfiContext()){
                    var user = vfi.Users.FirstOrDefault(t => t.Username.Equals(userName));
                    var permission = vfi.Permissions.FirstOrDefault(t => t.UserID == user.UserId && t.Function.FunctionName == functionName);
                    if (permission != null){
                        if (permission.Execution ?? false) {
                            check = true;
                        }
                    }
                }
                return check;
            }

            public static bool CheckRole(string userName, int type) {
                if (string.IsNullOrWhiteSpace(userName)) return false;
                var check = false;
                using (var vfi = new vfiContext()) {
                    var user = vfi.Users.FirstOrDefault(u => u.Username.Equals(userName));
                    var permission =
                        vfi.Permissions.FirstOrDefault(
                            p => p.UserID == user.UserId && p.FunctionID == type);
                    if (permission != null)
                        if (permission.Execution ?? false)
                            check = true;
                }
                return check;
            }

            private static int DeadlineDay = 5;
            public static bool CheckTransaction(string userName, DateTime date) {
                if (date > DateTime.Now.AddDays(DeadlineDay)) {
                    return !CheckRole(userName, SetTransactionDate);
                }
                var beginDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                if (date >= beginDate)
                    return !true;
                var lastMonth = beginDate.AddMonths(-1);
                var deadlineDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DeadlineDay);
                if (DateTime.Now >= deadlineDate || date < lastMonth) {
                    return !CheckRole(userName, InvManagementLv2);
                }
                return !true;
            }
        }
        #endregion

        #region function utilities
        public static class Function {

            public static Bitmap ResizeBitmap(Bitmap b, int nWidth, int nHeight) {
                Bitmap result = new Bitmap(nWidth, nHeight);
                using (Graphics g = Graphics.FromImage((Image)result))
                    g.DrawImage(b, 0, 0, nWidth, nHeight);
                return result;
            }

            private static char SplitChar = ';';
            public static double GetTimeStamp() {
                return (DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds;
            }
            public static string StringsJoin(List<string> ids) {
                var str = "";
                if (!ids.Any()) return str;
                foreach (var id in ids) {
                    str += (id + SplitChar.ToString());
                }
                str.Remove(str.Length - 2);
                return str;
            }
            public static List<string> StringsSplit(string ids) {
                var list = new List<string>();
                if (string.IsNullOrWhiteSpace(ids)) return list;
                var strIds = ids.Split(SplitChar);
                try {
                    list = strIds.ToList();
                }
                catch (Exception ex) { }
                return list;
            }

            public static string IdsToString(List<int> ids) {
                var str = "";
                if (!ids.Any()) return str;
                foreach (var id in ids) {
                    str += (id + SplitChar.ToString());
                }
                str.Remove(str.Length - 2);
                return str;
            }

            public static string IdsToString(List<int> ids, Char splitChar) {
                var str = "";
                if (!ids.Any()) return str;
                foreach (var id in ids) {
                    str += (id + splitChar.ToString());
                }
                str.Remove(str.Length - 2);
                return str;
            }

            public static List<int> StringToIds(string ids) {
                var list = new List<int>();
                if (string.IsNullOrWhiteSpace(ids)) return list;
                var strIds = ids.Split(SplitChar);
                try {
                    foreach (var strId in strIds) {
                        list.Add(Convert.ToInt32(strId));
                    }
                }
                catch (Exception ex) { throw ex; }
                return list;
            }

            public static List<int> StringToIds(string ids, Char splitChar) {
                var list = new List<int>();
                if (string.IsNullOrWhiteSpace(ids)) return list;
                var strIds = ids.Split(splitChar);
                try {
                    foreach (var strId in strIds) {
                        list.Add(Convert.ToInt32(strId));
                    }
                }
                catch (Exception ex) { throw ex; }
                return list;
            }
            public static List<long> StringToBigIds(string ids, Char splitChar) {
                var list = new List<long>();
                if (string.IsNullOrWhiteSpace(ids)) return list;
                var strIds = ids.Split(splitChar);
                try {
                    foreach (var strId in strIds) {
                        list.Add(Convert.ToInt64(strId));
                    }
                }
                catch (Exception ex) { throw ex; }
                return list;
            }

            public static void SaveLog(string actionName, string msg) {

                //string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                var fileName = "LogFile.txt";
                //var destinationPath = Path.Combine(Server.MapPath("~/Content/Logs"), fileName);
                var destinationPath = Path.Combine(MySystem.GetLogPath(), fileName);
                //var sw = new System.IO.StreamWriter(destinationPath, true);
                using (var sw = new System.IO.StreamWriter(destinationPath, true)) {
                    sw.WriteLine(DateTime.Now.ToString("dd/MM/yy hh:mm:ss") + ": " + actionName + ": " + msg);
                }
                //sw.Close();
            }

            public static int Round(double value) {
                return Convert.ToInt32(Math.Round(value));
            }
            public static int Round10(double value) {
                var temp = value % 10;
                if (temp > 5)
                    value = value - temp + 10;
                else
                    value = value - temp;
                return Convert.ToInt32(value);
            }

            public static double Round5(double value) {
                var temp = value % 5;
                if (temp > 0)
                    value = value - temp + 5;
                return value;
            }

            public static double RoundUp(double value, int digit) {
                var temp = value % Math.Pow(10, digit);
                if (temp > 0)
                    value = value - temp + Math.Pow(10, digit);
                return value;
            }

            public static int RoundUp(double value) {
                var temp = value % Math.Pow(10, 0);
                if (temp > 0)
                    value = value - temp + Math.Pow(10, 0);
                return Convert.ToInt32(value);
            }

            public static int RoundDown(double value) {
                var temp = value % Math.Pow(10, 0);
                return Convert.ToInt32(value - temp);
            }

            public static int Days(DayOfWeek day, DateTime start, DateTime end) {
                TimeSpan ts = end - start; // Total duration
                int count = (int)Math.Floor(ts.TotalDays / 7); // Number of whole weeks
                int remainder = (int)(ts.TotalDays % 7); // Number of remaining days
                int sinceLastDay = (int)(end.DayOfWeek - day); // Number of days since last [day]
                if (sinceLastDay < 0) sinceLastDay += 7; // Adjust for negative days since last [day]

                // If the days in excess of an even week are greater than or equal to the number days since the last [day], then count this one, too.
                if (remainder >= sinceLastDay) count++;

                return count;
            }

            /// <summary>
            /// tính ngày bắt đầu từ ngày kết thúc (không tính CN)
            /// </summary>
            /// <param name="toDate"></param>
            /// <param name="days"></param>
            /// <returns></returns>
            public static DateTime FromDate(DateTime toDate, int addDays) {
                var fromDate = toDate.AddDays(addDays * -1);
                while (fromDate < toDate) {
                    if (fromDate.DayOfWeek == DayOfWeek.Sunday) {
                        fromDate = fromDate.AddDays(-1);
                        break;
                    }
                    toDate = toDate.AddDays(-1);
                }
                fromDate = fromDate.AddDays(RoundDown((toDate - fromDate).TotalDays / 7) * -1);
                return fromDate;
            }

            /// <summary>
            /// tính ngày kết thúc từ ngày bắt đầu (không tính CN)
            /// </summary>
            /// <param name="fromDate"></param>
            /// <param name="days"></param>
            /// <returns></returns>
            public static DateTime ToDate(DateTime fromDate, int addDays) {
                var toDate = fromDate.AddDays(addDays);
                while (fromDate <= toDate) {
                    if (fromDate.DayOfWeek == DayOfWeek.Sunday) {
                        toDate = toDate.AddDays(1);
                        break;
                    }
                    fromDate = fromDate.AddDays(1);
                }
                toDate = toDate.AddDays(RoundDown((toDate - fromDate).TotalDays / 7));
                return toDate;
            }
            /// <summary>
            /// tinh ngay tu bat dau toi ket thuc ko tinh chu nhat
            /// </summary>
            /// <param name="start"></param>
            /// <param name="end"></param>
            /// <returns></returns>
            public static int Days(DateTime start, DateTime end) {
                var day = Convert.ToInt32((end - start).TotalDays);
                var start2 = start;
                while (start2 < end) {
                    if (start2.DayOfWeek == DayOfWeek.Sunday) {
                        day++;
                        break;
                    }
                    start2 = start2.AddDays(1);
                }
                //day += Convert.ToInt32((end - start).TotalDays);
                day += RoundDown((end - start2).TotalDays / 7);
                return day;
            }

            public static int DaysNoSunDay(DateTime start, DateTime end) {
                var day = Convert.ToInt32((end - start).TotalDays) + 1;
                var start2 = start;
                while (start2 <= end) {
                    if (start2.DayOfWeek == DayOfWeek.Sunday) {
                        day--;
                        break;
                    }
                    start2 = start2.AddDays(1);
                }
                day -= RoundDown((end - start2).TotalDays / 7);
                return day;
            }

            public static List<int> ListDaysNoSunday(DateTime start, DateTime end) {
                var list = new List<int>();
                while (start < end) {
                    if (start.DayOfWeek != DayOfWeek.Sunday) {
                        list.Add(start.Day);
                    }
                    start = start.AddDays(1);
                }
                list.Add(end.Day);
                return list;
            }

            public static DateTime StartOfWeekDate(DateTime date, DayOfWeek startOfWeek = DayOfWeek.Monday) {
                int diff = (7 + (date.DayOfWeek - startOfWeek)) % 7;
                return date.AddDays(-1 * diff).Date;
            }

            public static DateTime EndOfWeekDate(DateTime date, DayOfWeek startOfWeek = DayOfWeek.Monday) {
                var start = StartOfWeekDate(date, startOfWeek);
                return start.AddDays(7).Date;
            }
            public static DateTime EndTimeOfWeekDate(DateTime date, DayOfWeek startOfWeek = DayOfWeek.Monday) {
                var start = StartOfWeekDate(date, startOfWeek);
                return start.AddDays(7).AddSeconds(-1);
            }
            
            /// <summary>
            /// Convert string to date with vi-VN timezone.
            /// </summary>
            /// <param name="date">IsNullOrWhiteSpace return ToDay.</param>
            /// <returns></returns>
            public static DateTime ParseDate(string date) {
                return string.IsNullOrWhiteSpace(date) ? DateTime.Today : Convert.ToDateTime(date, new CultureInfo("vi-VN"));
            }
            public static DateTime ParseDateTime(string date) {
                return string.IsNullOrWhiteSpace(date) ? DateTime.Now : Convert.ToDateTime(date, new CultureInfo("vi-VN"));
            }

            public static DateTime ParseLastDateTime(string date) {
                return ParseDate(date).AddDays(1).AddSeconds(-1);
            }
            public static DateTime ParseLastMonthTime(string date) {
                var d = ParseDate(date);
                return new DateTime(d.Year, d.Month, 1).AddMonths(1).AddSeconds(-1);
            }

            public static string GetDayOfWeek(DateTime date) {
                if (date.DayOfWeek == DayOfWeek.Sunday) return "Chủ nhật";
                return "Thứ " + date.DayOfWeek;
            }

            public static void GetQuaterDateTime(out DateTime fromDateTime, out DateTime toDateTime, out int quater,
                                                 DateTime date) {
                fromDateTime = date;
                toDateTime = date;
                quater = 0;
                if (date.Month <= 3) {
                    fromDateTime = new DateTime(date.Year, 1, 1).AddSeconds(-1);
                    toDateTime = new DateTime(date.Year, 4, 1).AddSeconds(-1);
                    quater = 1;
                }
                else if (date.Month <= 6) {
                    fromDateTime = new DateTime(date.Year, 4, 1).AddSeconds(-1);
                    toDateTime = new DateTime(date.Year, 7, 1).AddSeconds(-1);
                    quater = 2;
                }
                else if (date.Month <= 9) {
                    fromDateTime = new DateTime(date.Year, 7, 1).AddSeconds(-1);
                    toDateTime = new DateTime(date.Year, 10, 1).AddSeconds(-1);
                    quater = 3;
                }
                else if (date.Month <= 12) {
                    fromDateTime = new DateTime(date.Year, 10, 1).AddSeconds(-1);
                    toDateTime = new DateTime(date.Year, 1, 1).AddYears(1).AddSeconds(-1);
                    quater = 4;
                }
            }
        }
        #endregion

        #region report
        public static class Report {

            public enum Show {
                All = 1,
                InPeriod = 2,
            }

            public enum Calculate {
                All = 1,
                InPeriod = 2,
            }
        }

        public static class Monitor {
            public class MyJsonResult {
                public int Code { get; set; }
                public string Message { get; set; }
                public Object Data { get; set; }
                public MyJsonResult( int code, string message, Object data ) {
                    this.Code = code;
                    this.Message = message;
                    if (data != null) this.Data = data;
                }
            }
            public enum ErrorCode {
                NotImplement = -1,
                NoThing = 0,
                NoError = 1,
                StatusChanged = 7,
                ReferenceError = 8,
                NotFound = 9,
                Exception = 10,
            }

            public enum Color {
                None = 0,
                Red = 1,
            }

            public static string ErrorMessage(int code) {
                switch (code) {
                    case 1:
                        return "Sản phẩm thiếu năng suất sản xuất 1";
                    case 2:
                        return "Sản phẩm thiếu năng suất Sản xuất 2";
                    case 3:
                        return "Sản phẩm thiếu năng suất Cnc";
                    case 4:
                        return "Sản phẩm thiếu ngày xi mạ";
                    case 5:
                        return "Sản phẩm sản xuất không đơn giá";
                    case 6:
                        return "Sản phẩm thiếu định mức sản xuất 1";
                    case 7:
                        return "Sản phẩm chưa xác định công cụ";
                    case 8:
                        return "Sản phẩm chưa xác định bao bì";
                    case 9:
                        return "Sản phẩm thiếu quy trình kho";
                    case 10:
                        return "Sản phẩm chưa xác định nguyên liệu";
                    case 11:
                        return "Sản phẩm thiếu thông tin thiết kế";
                    case 12:
                        return "Sản phẩm thiếu trọng lượng";
                    case 13:
                        return "Sản phẩm thiếu tên - mã khách hàng";
                    default:
                        return "";
                }
            }

            public enum Type {
                Production1 = 1,
                Production2 = 2,
                Cnc = 3,
                Plating = 4,
                ProductionNoUnitPrice = 5,
                ProductionRate = 6,
                ProductionTool = 7,
                ProductionFuel = 8,
                ProductionProcesses = 9,
                ProductionMaterial = 10,
                ProductDesign = 11,
                ProductWeight = 12,
                ProductName = 13
            }

            public static string GetText(int type) {
                switch (type) {
                    case 1:
                        return "Sản phẩm thiếu năng suất sản xuất 1";
                    case 2:
                        return "Sản phẩm thiếu năng suất Sản xuất 2";
                    case 3:
                        return "Sản phẩm thiếu năng suất Cnc";
                    case 4:
                        return "Sản phẩm thiếu ngày xi mạ";
                    case 5:
                        return "Sản phẩm sản xuất không đơn giá";
                    case 6:
                        return "Sản phẩm thiếu định mức sản xuất 1";
                    case 7:
                        return "Sản phẩm chưa xác định công cụ";
                    case 8:
                        return "Sản phẩm chưa xác định bao bì";
                    case 9:
                        return "Sản phẩm thiếu quy trình kho";
                    case 10:
                        return "Sản phẩm chưa xác định nguyên liệu";
                    case 11:
                        return "Sản phẩm thiếu thông tin thiết kế";
                    case 12:
                        return "Sản phẩm thiếu trọng lượng";
                    case 13:
                        return "Sản phẩm thiếu tên - mã khách hàng";
                    default:
                        return "";
                }
            }

            public static string StockOrderNum = "StockOrderNum",
                                TransactionProductNum = "TransactionProductNum",
                                OrderNum = "OrderNum",
                                InvoiceNum = "InvoiceNum",
                                NoteNum = "NoteNum",
                                PurchaseOrderNum = "PurchaseOrderNum",
                                TransactionMaterialNum = "TransactionMaterialNum",
                                MaterialUseNum = "MaterialUseNum",
                                FuelNum = "FuelNum",
                                QuoteNum = "QuoteNum",
                                PlatingNum = "PlatingNum",
                                ToolNum = "ToolNum",
                                Production2 = "Production2",
                                DeductionCNCMachine = "DeductionCNCMachine",
                                DeductionCamesMachine = "DeductionCamesMachine",
                                ReplaceMaterialOnCamesShift1 = "ReplaceMaterialOnCamesShift1",
                                ReplaceMaterialOnCamesShift2 = "ReplaceMaterialOnCamesShift2",
                                FactoryDayTiming = "FactoryDayTiming",
                                FactoryFullDayTiming = "FactoryFullDayTiming",
                                FactoryShiftTiming = "FactoryShiftTiming",
                                FactoryFullShiftTiming = "FactoryFullShiftTiming",
                                DayTiming = "DayTiming",
                                BaseInventoryPriceRate = "BaseInventoryPriceRate",
                                BaseProductionPriceRate = "BaseProductionPriceRate",
                                ExchangeToVndRate = "ExchangeToVndRate",
                                ExchangeToVndRate2 = "ExchangeToVndRate2",
                                ExchangeToVndRate3 = "ExchangeToVndRate3",
                                WorkOrderTolerance = "WorkOrderTolerance",
                                MaterialWorkPieceDesign = "MaterialWorkPieceDesign",
                                RoundMaterialMaterialDesign = "RoundMaterialMaterialDesign",
                                ProductionLossRateDesign = "ProductionLossRateDesign",
                                PackingProductivity = "PackingProductivity";

            public static double GetParameterValue(string param) {
                var value = 0.0;
                try {
                    using (var vfi = new vfiContext()) {
                        var paramValue = vfi.Parameters.FirstOrDefault(p => p.ParamCode.Equals(param));
                        if (paramValue == null) {
                            paramValue = new Parameter() {
                                ParamCode = param,
                                Name = param,
                                Value = "0",
                                ModifiedDate = DateTime.Now
                            };
                            vfi.Parameters.Add(paramValue);
                            vfi.SaveChanges();
                        }
                        value = Convert.ToDouble(paramValue.Value);
                    }
                }
                catch (Exception) { }
                return value;
            }
        }
        #endregion

        #region machine
        public static class Machine {
            //public static string FactoryVF2 = "VF2";

            //public static class Diagram {
            //    public enum Type {
            //        Cnc = 2,
            //        Cames = 1,
            //    }
            //    public static string GetText(int status) {
            //        var rs = "";

            //        switch (status) {
            //            case 1:
            //                rs = "Cames";
            //                break;
            //            case 2:
            //                rs = "CNC";
            //                break;
            //            default:
            //                rs = "";
            //                break;
            //        }
            //        return rs;
            //    }
            //}

            public static List<MachineDiagram> DiagramTemplate {
                get {
                    return new List<MachineDiagram> { 
                        new MachineDiagram { 
                            DiagramType = 1, 
                            DiagramName = "Mẫu 1",
                            Information = "2 cột - khoảng cách - 2 cột"
                        },
                        new MachineDiagram { 
                            DiagramType = 2, 
                            DiagramName = "Mẫu 2",
                            Information = "1 cột - khoảng cách - 2 cột - khoảng cách - 1 cột"
                        },
                        new MachineDiagram { 
                            DiagramType = 3, 
                            DiagramName = "Mẫu 3",
                            Information = "3 cột to"
                        },
                    };
                }
            }
            public static string GetDiagramText(int type) {
                var rs = DiagramTemplate.FirstOrDefault(x => x.DiagramType == type);
                return rs != null ? rs.DiagramName : "NULL";
            }

            public static class State {

                public enum RepairStatus {
                    None = 1,
                    Finish = 2,
                    UnFinish = 3,
                    Wrong = 5,
                    Delete = 4,
                    Stop = 6,
                }
                public static string GetRepairStatusText(int status) {
                    var rs = "";
                    switch (status) {
                        case (byte)RepairStatus.None:
                            rs = "Chưa xong";
                            break;
                        case (byte)RepairStatus.Finish:
                            rs = "Đạt";
                            break;
                        case (byte)RepairStatus.UnFinish:
                            rs = "Không Đạt";
                            break;
                        case (byte)RepairStatus.Delete:
                            rs = "Hủy";
                            break;
                        case (byte)RepairStatus.Wrong:
                            rs = "Sai lỗi";
                            break;
                        case (byte)RepairStatus.Stop:
                            rs = "Ngưng";
                            break;
                        default:
                            rs = status + "";
                            break;
                    }
                    return rs;
                }
                public static int Normal = 1;
                public static int OutOfMaterial = 21;
                public static int BadState = 59;
                public static int Setup = 60;
                public static int OutOfTool = 61;
                public static int EmptyMachine = 61;
                public static int Done = 65;
                public static int Repairing = 70;
                public static int Error = 0;
                public enum StateId {
                    Normal = 1,
                    OutOfMaterial = 21,
                    BadState = 59,
                    Setup = 60,
                    OutOfTool = 61,
                    Done = 65,
                    Repairing = 70,
                }
                public static List<int> GetNoneProductonStateList() {
                    return new List<int>
                    {
                        OutOfMaterial,
                        BadState,
                        Setup,
                        OutOfTool,
                    };
                }
                public static List<int> GetStaticStateList() {
                    return new List<int>
                    {
                        Normal,
                        OutOfMaterial,
                        BadState,
                        Setup,
                        OutOfTool,
                        Repairing,
                        Done
                    };
                }
                public static List<int> GetStopStateList() {
                    return new List<int>
                    {
                        Normal,
                        OutOfMaterial,
                        BadState,
                        Setup,
                        OutOfTool,
                        Repairing,
                        Done
                    };
                }
            }

            public static TrackUpMachineModel LastTrackUpProduct(int machineId, string machineContain, int materialId, int productId, DateTime date) {
                using (var vfi = new vfiContext()) {
                    var lastTrackFound =
                                   (from t in vfi.TrackUpMachines
                                    where
                                        t.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                        (machineId == 0 || t.MachineId == machineId) &&
                                        t.Machine.MachineName.Contains(machineContain) &&
                                        (materialId == 0 || t.MaterialId == materialId) &&
                                        (productId == 0 || t.ProductId == productId) &&
                                        ((t.DeliveryDate != null
                                            ? t.DeliveryDate.Value <= date
                                            : t.StartDate <= date)
                                        || (t.StartDate <= date))
                                    //select t
                                    select new TrackUpMachineModel() {
                                        MachineId = t.MachineId,
                                        MachineName = t.Machine.MachineName,
                                        ProductId = t.ProductId,
                                        ProductCode = t.Product.ProductCode,
                                        ProductLength = t.Product.Length ?? 0,
                                        ProductionWeight = t.Product.ProductionWeight ?? 0,
                                        MaterialId = t.MaterialId,
                                        MaterialCode = t.Material.MaterialCode,

                                        StartDate = t.StartDate,
                                        DeliveryDate = t.DeliveryDate,
                                        ReportDate = t.DeliveryDate != null ? t.DeliveryDate.Value : t.StartDate,

                                        Quantity = t.Quantity,
                                        RoundPerMinute = t.RoundPerMinute,
                                        RealProductivity = t.RealProductivity,
                                        RealRate = t.RealRate,
                                        WorkPiece = t.WorkPiece,
                                        KnifeCut = t.KnifeCut,

                                        //t.TrackUpMaterials,
                                    })
                                   .OrderByDescending(t => t.ReportDate)
                                   .FirstOrDefault();
                    return lastTrackFound;
                }
            }

            public static TrackUpMachineModel LastTrackUpMachine(int machineId, int? materialId, int? productId, DateTime date) {
                var lastDateTime = MyUtilities.Function.ParseLastDateTime(date.ToString("dd/MM/yyyy HH:mm:ss"));
                using (var vfi = new vfiContext()) {
                    var lastTrackFound =
                                   (from t in vfi.TrackUpMachines
                                    where
                                        t.Status == (byte)MyUtilities.Transaction.Status.Approved && 
                                        t.MachineId == machineId &&
                                        (materialId == null || t.MaterialId == materialId) &&
                                        (productId == null || t.ProductId == productId) &&
                                        ((t.DeliveryDate != null
                                            ? t.DeliveryDate.Value <= date
                                            : t.StartDate <= date)
                                        || (t.StartDate <= date))
                                    select new TrackUpMachineModel() {
                                        TrackId = t.TrackId,
                                        MachineId = t.MachineId,
                                        MachineName = t.Machine.MachineName,
                                        ProductId = t.ProductId,
                                        ProductCode = t.Product.ProductCode,
                                        ProductLength = t.Product.Length ?? 0,
                                        ProductionWeight = t.Product.ProductionWeight ?? 0,
                                        MaterialId = t.MaterialId,
                                        MaterialCode = t.Material.MaterialCode,

                                        StartDate = t.StartDate,
                                        DeliveryDate = t.DeliveryDate,
                                        ReportDate = t.DeliveryDate != null ? t.DeliveryDate.Value : t.StartDate,

                                        Quantity = t.Quantity,
                                        RoundPerMinute = t.RoundPerMinute,
                                        RealProductivity = t.RealProductivity,
                                        RealRate = t.RealRate,
                                        WorkPiece = t.WorkPiece,
                                        KnifeCut = t.KnifeCut,

                                        TrackUpMaterials = t.TrackUpMaterials.ToList(),
                                    })
                                   .OrderByDescending(t => t.ReportDate)
                                   .FirstOrDefault();
                    return lastTrackFound;
                }
            }
        }
        #endregion

        #region warehouse
        public static class Warehouse {
            public enum Id {
                Production1 = 1,
                Cnc = 2,

                Production2 = 12,
                Production2B = 18,
                Production2C = 19,
                Production2D = 20,

                HeatTreatment = 3,
                SurfaceTreatment = 4,

                WaitingPlating = 5,
                Plating = 6,
                PlatingTest = 17,

                QcA = 7,// 
                QcB = 13,// hang xi ma ve
                QcC = 31,// hang dua di kiem

                Processing = 8,
                Processing2 = 16,
                ReProcessing = 21,
                Tranfer = 22,
                ReTurning = 23,
                ReDrilling = 24, //	NULL	GCL(Khoan)
                ReGrinding = 25, //	NULL	GCL(Mài nhám)
                ReProcessThuCong = 26, //	NULL	GCL(Thủ công)
                ReProcessMvt = 27, //	NULL	GCL(Mài vô tâm)
                ReSurface = 28, //	NULL	GCL(Rung bóng)
                ReTesting = 29, //	NULL	GCL(Kiểm tra lại)
                RePolir = 30, //	NULL	GCL(Polir)

                Defect = 9,

                Finish = 10,
                Packing = 32, // Thành phẩm/ Đóng gói

                Business = 11,
                Return = 14,
                Destroy = 15,
            }

            public static List<int> GetWarehouseId_SumTotalQuantity() {
                return new List<int>
                    {
                        Cnc,
                        Production2,
                        Production2B,
                        Production2C,
                        Production2D,
                        HeatTreatment,
                        SurfaceTreatment,
                        WaitingPlating,
                        Plating,
                        PlatingTest,
                        QcA,
                        QcB,
                        QcC,
                        Packing,
                        Processing,
                        ReProcessing,
                        //Processing2,
                        ReTurning,
                        ReGrinding,
                        ReDrilling,
                        ReProcessThuCong,
                        ReProcessMvt,
                        ReSurface,
                        ReTesting,
                        RePolir,
                        Finish
                    };
            }

            public static List<int> GetWarehouseId_SumTotalQuantity2() {
                return new List<int>
                    {
                        Cnc,
                        Production2,
                        Production2B,
                        Production2C,
                        Production2D,
                        HeatTreatment,
                        SurfaceTreatment,
                        WaitingPlating,
                        Plating,
                        PlatingTest,
                        QcA,
                        QcB,
                        QcC,
                        Packing,
                        Finish
                    };
            }

            public static List<int> GetWarehouseIdQc() {
                return new List<int>
                    {
                        QcA,
                        QcB,
                        QcC,
                    };
            }

            public static List<int> GetWarehouseIds_Plating() {
                return new List<int>
                    {
                        WaitingPlating,
                        Plating,
                        PlatingTest,
                    };
            }
            public static List<int> GetWarehouseIdProduction2_ALL() {
                return new List<int>
                    {
                        Production2,
                        Production2B,
                        Production2C,
                        Production2D,
                    };
            }

            public static List<int> GetWarehouseIdProduction2_PROCESS() {
                return new List<int>
                    {
                        Production2B,
                        Production2C,
                        Production2D,
                    };
            }
            public static List<int> GetWarehouseId_NotSumTotalQuantity() {
                return new List<int>
                    {
                        Production1,
                        Defect,
                        Business,
                        Return,
                        Destroy
                    };
            }

            public static List<int> GetWarehouseId_NotSumTotalQuantity2() {
                return new List<int>
                    {
                        Production1,
                        Defect,
                        Business,
                        Return,
                        Destroy,
                        Processing,
                        Processing2,
                        ReProcessing,
                        Tranfer
                    };
            }

            public static List<int> GetWarehouseId_NoSchedule() {
                return new List<int>
                    {
                        Production1,
                        Processing,
                        Processing2,
                    };
            }

            public static List<int> GetWarehouseId_Process() {
                return new List<int>
                    {
                        Production1,
                        Cnc,
                        Production2,
                        HeatTreatment,
                        SurfaceTreatment,
                        WaitingPlating,
                        Plating,
                        PlatingTest,
                        QcA,
                        QcB,
                        QcC,
                        Packing,
                        Finish
                    };
            }

            public static List<int> GetWarehouseId_ProcessBack() {
                return new List<int>
                {
                    Cnc,
                    Production2,
                    HeatTreatment,
                    SurfaceTreatment,
                    WaitingPlating,
                    QcA,
                    QcB,
                    Defect,
                    ReProcessing
                };
            }
            public static List<int> GetWarehouseId_ReProcessing() {
                return new List<int>
                    {
                        ReTurning,
                        ReGrinding,
                        ReDrilling,
                        ReProcessThuCong,
                        ReProcessMvt,
                        ReSurface,
                        ReTesting,
                        RePolir,
                        Processing,
                        ReProcessing,
                    };
            }

            public static List<int> GetWarehouseId_ReProcessingOnly() {
                return new List<int>
                    {
                        ReTurning,
                        ReGrinding,
                        ReDrilling,
                        ReProcessThuCong,
                        ReProcessMvt,
                        ReSurface,
                        ReTesting,
                        RePolir,
                        ReProcessing,
                    };
            }

            public static List<int> GetWarehouseId_ExceptionRotate() {
                return new List<int>
                    {
                        Destroy,
                        Tranfer
                    };
            }

            public static int Production1 = 1;
            public static int Cnc = 2;
            public static int Production2 = 12;
            public static int Production2B = 18;
            public static int Production2C = 19;
            public static int Production2D = 20;
            public static int HeatTreatment = 3;
            public static int SurfaceTreatment = 4;
            public static int WaitingPlating = 5;
            public static int Plating = 6;
            public static int PlatingTest = 17;
            public static int QcA = 7;
            public static int Processing = 8;
            public static int Processing2 = 16;
            public static int Defect = 9;
            public static int Finish = 10;
            public static int Packing = 32;
            public static int Business = 11;
            public static int QcB = 13;
            public static int QcC = 31;
            public static int Return = 14;
            public static int Destroy = 15;
            public static int ReProcessing = 21;
            public static int Tranfer = 22;
            public static int ReTurning = 23; //	NULL	GCL(Tiện)
            public static int ReDrilling = 24; //	NULL	GCL(Khoan)
            public static int ReGrinding = 25; //	NULL	GCL(Mài nhám)
            public static int ReProcessThuCong = 26; //	NULL	GCL(Thủ công)
            public static int ReProcessMvt = 27; //	NULL	GCL(Mài vô tâm)
            public static int ReSurface = 28; //	NULL	GCL(Rung bóng)
            public static int ReTesting = 29; //	NULL	GCL(Kiểm tra lại)
            public static int RePolir = 30; //	NULL	GCL(Polir)
        }
        #endregion

        #region auto number code
        public static class AutoIncrease {
            public enum IncreaseNum {
                Product = 1,
                Material = 2,
                Order = 3,
                PurchaseOrder = 4,
                OrderNote = 5,
                Invoice = 6,
                MaterialUse = 7,
                Fuel = 8,
                Plating = 9,
                Tool = 10,
                Quote = 11,
                Production2 = 12,
                Production1 = 13,
            }

            public static string GetParam(int type, int increatNum) {
                try {
                    string code = "";
                    int num;
                    string paramCode = "";
                    switch (type) {
                        case (int)IncreaseNum.Production1:
                        case (int)IncreaseNum.Product:
                            code = "VFTP-";
                            paramCode = "TransactionProductNum";
                            break;
                        case (int)IncreaseNum.Material:
                            code = "VFTM-";
                            paramCode = "TransactionMaterialNum";
                            break;
                        case (int)IncreaseNum.Order:
                            code = "VFO-";
                            paramCode = "OrderNum";
                            break;
                        case (int)IncreaseNum.PurchaseOrder:
                            code = "VFPO-";
                            paramCode = "PurchaseOrderNum";
                            break;
                        case (int)IncreaseNum.OrderNote:
                            code = "VFN-";
                            paramCode = "NoteNum";
                            break;
                        case (int)IncreaseNum.Invoice:
                            code = "VFI-";
                            paramCode = "InvoiceNum";
                            break;
                        case (int)IncreaseNum.MaterialUse:
                            code = "VFMU-";
                            paramCode = "MaterialUseNum";
                            break;
                        case (int)IncreaseNum.Fuel:
                            code = "VFMF-";
                            paramCode = "FuelNum";
                            break;
                        case (int)IncreaseNum.Tool:
                            code = "VFMT-";
                            paramCode = "ToolNum";
                            break;
                        case (int)IncreaseNum.Plating:
                            code = "VFMP-";
                            paramCode = "PlatingNum";
                            break;
                        case (int)IncreaseNum.Quote:
                            code = "VFQ-";
                            paramCode = "QuoteNum";
                            break;
                        case (int)IncreaseNum.Production2:
                            code = "VFP2-";
                            paramCode = "Production2";
                            break;
                    }
                    using (var vfi = new vfiContext()) {
                        var param = vfi.Parameters.FirstOrDefault(f => f.ParamCode.Equals(paramCode));
                        if (param == null) {
                            param = new Parameter {
                                Name = paramCode,
                                ParamCode = paramCode,
                                ModifiedDate = DateTime.Now,
                                Value = "1",
                            };
                            vfi.Parameters.Add(param);
                            vfi.SaveChanges();
                        }
                        num = Convert.ToInt32(param.Value) + increatNum;
                    }
                    var y = DateTime.Today.Year % 100;
                    code += y + "" + (num > 9999
                                          ? num + ""
                                          : String.Format("{0:0000}", num));
                    if (CheckParam(code, type)) {
                        IncreateParam(paramCode);
                        return GetParam(type, increatNum);
                    }
                    else {
                        IncreateParam(paramCode);
                    }
                    return code;
                }
                catch (Exception ex) {
                    throw new AggregateException("Lỗi param !\n" + ex.Message);
                }
            }

            private static bool CheckParam(string num, int type) {
                var check = false;
                try {

                    using (var vfi = new vfiContext()) {
                        switch (type) {
                            case (int)IncreaseNum.Production1:
                                check =
                                    vfi.ImportFormSX1.FirstOrDefault(
                                        f => f.TransactionCode.Equals(num.Trim(), StringComparison.OrdinalIgnoreCase)) !=
                                    null;
                                break;
                            case (int)IncreaseNum.Product:
                            case (int)IncreaseNum.Material:
                                check =
                                    vfi.Transactions.FirstOrDefault(
                                        f => f.TransactionCode.Equals(num.Trim(), StringComparison.OrdinalIgnoreCase)) !=
                                    null;
                                break;
                            case (int)IncreaseNum.Order:
                                check =
                                    vfi.Orders.FirstOrDefault(
                                        f => f.OrderNumber.Equals(num.Trim(), StringComparison.OrdinalIgnoreCase)) !=
                                    null;
                                break;
                            case (int)IncreaseNum.PurchaseOrder:
                                check =
                                    vfi.PurchaseOrders.FirstOrDefault(
                                        f => f.RevisionNumber.Equals(num.Trim(), StringComparison.OrdinalIgnoreCase)) !=
                                    null;
                                break;
                            case (int)IncreaseNum.OrderNote:
                                check =
                                    vfi.OrderNotes.FirstOrDefault(
                                        f => f.NoteNumber.Equals(num.Trim(), StringComparison.OrdinalIgnoreCase)) !=
                                    null;
                                break;
                            case (int)IncreaseNum.Invoice:
                                check =
                                    vfi.Invoices.FirstOrDefault(
                                        f => f.InvoiceNumber.Equals(num.Trim(), StringComparison.OrdinalIgnoreCase)) !=
                                    null;
                                break;
                            case (int)IncreaseNum.MaterialUse:
                                check =
                                    vfi.MaterialUseInShifts.FirstOrDefault(
                                        f => f.UsedCode.Equals(num.Trim(), StringComparison.OrdinalIgnoreCase)) != null;
                                break;
                            case (int)IncreaseNum.Fuel:
                                check =
                                    vfi.TransactionFpts.FirstOrDefault(
                                        f => f.TransactionCode.Equals(num.Trim(), StringComparison.OrdinalIgnoreCase)) !=
                                    null;
                                break;
                            case (int)IncreaseNum.Tool:
                                check =
                                    vfi.TransactionFpts.FirstOrDefault(
                                        f => f.TransactionCode.Equals(num.Trim(), StringComparison.OrdinalIgnoreCase)) !=
                                    null;
                                break;
                            case (int)IncreaseNum.Plating:
                                check =
                                    vfi.PlatingForms.FirstOrDefault(
                                        f => f.PlatingFormNumber.Equals(num.Trim(), StringComparison.OrdinalIgnoreCase)) !=
                                    null;
                                break;
                            case (int)IncreaseNum.Quote:
                                check =
                                    vfi.QuoteForms.FirstOrDefault(
                                        f => f.QuoteNumber.Contains(num.Trim())) != null;
                                break;
                            case (int)IncreaseNum.Production2:
                                check =
                                    vfi.Production2Transaction.FirstOrDefault(
                                        f => f.TransactionCode.Contains(num.Trim())) != null;
                                break;
                        }
                    }
                }
                catch (Exception) {
                    return check;
                }
                return check;
            }

            private static void IncreateParam(string parameterCode) {
                using (var vfi = new vfiContext()) {
                    var parameter = vfi.Parameters.FirstOrDefault(p => p.ParamCode.Equals(parameterCode));
                    if (parameter == null)
                        throw new AggregateException("Lỗi số tự tăng !");
                    parameter.Value = (Convert.ToInt32(parameter.Value) + 1) + "";
                    var a = vfi.SaveChanges();
                }
            }

        }
        #endregion

        #region transaction
        public static class Transaction {
            public enum EoIEnum {
                Import = '0',
                Export = '1',
                Rotate = '2',
                ExportTerm = '3',
                Combine = '4',
            }
            public enum PeriodType {
                Year = 1,
                Month = 2,
                Day = 3
            }
            public enum Status {
                Open = 1,
                Approved = 2,
                Cancel = 3,
                Processing = 4,
            }
            public static class MoP {
                public static bool Product = false;
                public static bool Material = true;
            }
            public class CastText {
                public static string GetTextEoI(string status) {
                    var rs = "";

                    switch (status) {
                        case "0":
                            rs = "Nhập";
                            break;
                        case "1":
                            rs = "Xuất";
                            break;
                        case "2":
                            rs = "Chuyển";
                            break;
                    }

                    return rs;
                }

                public static string GetValueEoI(string value) {
                    var rs = "";

                    if (value == EoIEnum.Import.ToString())
                        rs = "0";
                    else if (value == EoIEnum.Export.ToString())
                        rs = "1";
                    else if (value == EoIEnum.Rotate.ToString())
                        rs = "2";

                    return rs;
                }
                public static string GetTextStatus(int status) {
                    var rs = "";

                    switch (status) {
                        case (int)Status.Open:
                            rs = "Đợi duyệt";
                            break;
                        case (int)Status.Approved:
                            rs = "Đã duyệt";
                            break;
                        case (int)Status.Cancel:
                            rs = "Huỷ bỏ";
                            break;
                        case (int)Status.Processing:
                            rs = "Đang xử lý";
                            break;
                        default:
                            rs = "";
                            break;
                    }

                    return rs;
                }
                public static string GetDefectTransactionTextStatus(int status) {
                    var rs = "";

                    switch (status) {
                        case (int)Status.Open:
                            rs = "Đợi phân lỗi";
                            break;
                        case (int)Status.Approved:
                            rs = "Đã phân lỗi";
                            break;
                        case (int)Status.Cancel:
                            rs = "Huỷ bỏ";
                            break;
                        case (int)Status.Processing:
                            rs = "Đang phân lỗi";
                            break;
                        default:
                            rs = "";
                            break;
                    }

                    return rs;
                }
            }

            public enum ProductionLockType {
                Production1 = 1,
                CNC = 2,
                Production2 = 3,
            }
            public static bool IsLock(DateTime date, ProductionLockType type) {
                using (var vfi = new vfiContext()) {
                    var productionLock = vfi.ProductionLocks.FirstOrDefault(pl => pl.LockDate == date);
                    if (productionLock == null) return false;
                    switch (type) {
                        case ProductionLockType.Production1:
                            return productionLock.Production1Lock;
                        case ProductionLockType.CNC:
                            return productionLock.CNCLock;
                        case ProductionLockType.Production2:
                            return productionLock.Production2Lock;
                        default:
                            break;
                    }
                }
                return false;
            }

            public enum FormTypeEnum {
                ImportSX1 = 1,
                ImportCNC = 2,
                ExportGCN_NCU = 7,
                ImportNCU_QCB = 8,
                ExportTP = 10,
                ExportChange = 14,
            }

            public static string GetFromTypeText(int status) {
                var rs = "";

                switch (status) {
                    case 1:
                        rs = FormTypeEnum.ImportSX1.ToString();
                        break;
                    case 7:
                        rs = FormTypeEnum.ExportGCN_NCU.ToString();
                        break;
                    case 8:
                        rs = FormTypeEnum.ImportNCU_QCB.ToString();
                        break;
                    case 10:
                        rs = FormTypeEnum.ExportTP.ToString();
                        break;
                    case 14:
                        rs = FormTypeEnum.ExportChange.ToString();
                        break;
                }

                return rs;
            }

            // 08/05/2026
                public static bool CheckDesign(long transactionId) {
                    try {
                        using (var vfi = new vfiContext()) {
                            var transaction = vfi.Transactions.FirstOrDefault(p => p.TransactionId == transactionId);
                            if (transaction == null) return false;
                            if (transaction.FinishDesign) return true;

                            return true;
                        }
                    }
                    catch (Exception ex) {
                        throw ex;
                    }
                }

                public static void UpdateTransactionDesign(long transactionId) {
                    try {
                        using (var vfi = new vfiContext()) {
                            var transaction = vfi.Transactions.FirstOrDefault(p => p.TransactionId == transactionId);
                            if (transaction == null) return;
                            if (transaction.FinishDesign) return;
                            transaction.FinishDesign = true;
                            vfi.SaveChanges();
                        }
                    }
                    catch (Exception ex) {
                        throw ex;
                    }
                }
            }
        #endregion

        #region section
        public static class Section {
            public static double Time = 28800; //8h*60m*60s
            public static double Day = 25;
            public static double Employee = 13;
            public static double Total = Time * Day * Employee;
            public static int ReProcessSection = 22;
        }
        #endregion

        #region sales
        public static class Sales {
            //2016-06-30 23:59:58.000 hien thi duy nhat bao cao thang 7
            //public static DateTime StartTaxInvoiceDateCheat = new DateTime(2018, 1, 1, 0, 0, 0).AddSeconds(-2);
            //2016-06-30 23:59:59.000 hien thi xuyen suot
            public static DateTime StartTaxInvoiceDate = new DateTime(2019, 1, 1, 0, 0, 0).AddSeconds(-1);
            public static DateTime StartInvoiceDate = new DateTime(2014, 12, 31, 10, 0, 0);
            public static DateTime StartOrderDate = new DateTime(2014, 7, 1).AddSeconds(-1);
            public static DateTime StartOrderReportDate = new DateTime(2014, 4, 1).AddSeconds(-1);
            public static int CustomerKcx = 6;
            public static int CustomerForeign = 7;
            public enum EmployeeType {
                Sales = 1,
                Production2 = 2,
                Production2B = 3,
            }

            public enum CustomerState {
                NoActive = 0,
                Active = 1,
                NewCustomer = 2
            }

            public enum DeliveryStatus {
                Normal = 0,
                Late = 1,
                WillLate = 2
            }

            public enum ProductInvStatus {
                Enough = 0,
                NotFinish = 1,
                NotProduction = 2
            }
            public static string GetDeliveryStatus(int state) {
                string name = "";
                switch (state) {
                    case (int)DeliveryStatus.Normal:
                        name = "";
                        break;
                    case (int)DeliveryStatus.Late:
                        name = "Late";
                        break;
                    case (int)DeliveryStatus.WillLate:
                        name = "Will be late";
                        break;
                }
                return name;
            }
            public static string GetProductInvStatus(int state) {
                string name = "";
                switch (state) {
                    case (int)ProductInvStatus.Enough:
                        name = "Enough";
                        break;
                    case (int)ProductInvStatus.NotFinish:
                        name = "Not Finish";
                        break;
                    case (int)ProductInvStatus.NotProduction:
                        name = "Not Production";
                        break;
                }
                return name;
            }

            public static List<int> GetCustomerAccessList(string userName) {
                var ids = new List<int>();
                using (var vfi = new vfiContext()) {
                    var user = vfi.Users.FirstOrDefault(u => u.Username.Equals(userName));
                    if (user == null)
                        return ids;
                    var customerAccess =
                        vfi.CustomerAccessPermissions.Where(cap => cap.UserId == user.UserId && cap.Active).ToList();
                    ids = customerAccess.Select(ca => ca.CustomerId).ToList();
                }
                return ids;
            }

            public static string GetCustomerState(int state) {
                string name = "";
                switch (state) {
                    case (int)CustomerState.NoActive:
                        name = "Không hoạt động";
                        break;
                    case (int)CustomerState.Active:
                        name = "Hoạt động";
                        break;
                    case (int)CustomerState.NewCustomer:
                        name = "KH mới";
                        break;
                }
                return name;
            }
            public enum Status {
                Waiting = 1,
                Completed = 2,
                Cancel = 3,
                InProcess = 4,
            }
            public static string GetText(int status) {
                var rs = "";

                switch (status) {
                    case (byte)Status.Waiting:
                        rs = "Chưa giao";
                        break;
                    case (byte)Status.Completed:
                        rs = "Đã giao";
                        break;
                    case (byte)Status.Cancel:
                        rs = "Huỷ bỏ";
                        break;
                    case (byte)Status.InProcess:
                        rs = "Đang giao";
                        break;
                    default:
                        rs = "Chưa giao";
                        break;
                }

                return rs;
            }

        }
        #endregion

        #region Invoice
        // 05/05/2026
        public static class Invoice {
            public static bool CheckDesign(int invoiceId) {
                //var chk = false;
                try {
                    using (var vfi = new vfiContext()) {
                        var invoice = vfi.Invoices.FirstOrDefault(p => p.InvoiceId == invoiceId);
                        if (invoice == null) return false;
                        if (invoice.FinishDesign) return true;

                        return true;
                    }
                }
                catch (Exception ex) {
                    throw ex;
                }
            }
            
            public static void UpdateInvoiceDesign(int invoiceId) {
                try {
                    using (var vfi = new vfiContext()) {
                        var invoice = vfi.Invoices.FirstOrDefault(p => p.InvoiceId == invoiceId);
                        if (invoice == null) return;
                        if (invoice.FinishDesign) return;
                        invoice.FinishDesign = true;
                        vfi.SaveChanges();
                    }
                }
                catch (Exception ex) {
                    throw ex;
                }
            }
        }

        #endregion        
        


        #region purchasing
        public static class PurchaseOrder {
            public static DateTime StartTaxInvoiceDate = new DateTime(2016, 12, 1, 0, 0, 0).AddSeconds(-1);

            public enum FptLot {
                Fuel = 1,
                Plating = 2,
                Tool = 3,
            }

            public enum EoILot {
                Import = 1,
                Export = 2,
            }

            public enum TypeLot {
                Normal = 1,
                UnNormal = 2,
            }

            public enum InquiryEnum {
                Pending = 1,
                Approved = 3,
                MakePo = 5,
                Cancel = 9
            }

            public enum EvaluationEnum {
                Pending = 1,
                Approved = 3,
                Cancel = 5
            }

            public enum PurchaseEnum {
                Waiting = 1,
                Comfirm = 3,
                Approved = 5,
                FinalApproved = 7,
                Delivering = 9,
                Delivered = 11,
                Cancel = 13,
            }

            public static string GetEvaluationEnumStatusName(int status) {
                string name = "";
                switch (status) {
                    case (int)EvaluationEnum.Pending:
                        name = "Chờ xác nhận";
                        break;
                    case (int)EvaluationEnum.Approved:
                        name = "Đã xác nhận";
                        break;
                    case (int)EvaluationEnum.Cancel:
                        name = "Hủy";
                        break;
                }
                return name;
            }

            public static string GetPurchaseEnumStatusName(int status) {
                string name = "";
                switch (status) {
                    case (int)PurchaseEnum.Waiting:
                        name = "Chờ xác nhận";
                        break;
                    case (int)PurchaseEnum.Comfirm:
                        name = "Đã xác nhận";
                        break;
                    case (int)PurchaseEnum.Approved:
                        name = "Đã chuyển thành phiếu mua";
                        break;
                    case (int)PurchaseEnum.FinalApproved:
                        name = "Đã duyệt phiếu mua";
                        break;
                    case (int)PurchaseEnum.Delivering:
                        name = "Đang giao";
                        break;
                    case (int)PurchaseEnum.Delivered:
                        name = "Đã giao";
                        break;
                    case (int)PurchaseEnum.Cancel:
                        name = "Hủy";
                        break;
                }
                return name;
            }

            public static string GetInquiryTrackingStatusName(int status) {
                string name = "";
                switch (status) {
                    case (int)InquiryEnum.Pending:
                    case (int)InquiryEnum.Approved:
                        name = "Chưa đặt";
                        break;
                    case (int)InquiryEnum.MakePo:
                        name = "Đã đặt";
                        break;
                }
                return name;
            }

            public static string GetInquiryEnumStatusName(int status) {
                string name = "";
                switch (status) {
                    case (int)InquiryEnum.Pending:
                        name = "Chờ duyệt";
                        break;
                    case (int)InquiryEnum.Approved:
                        name = "Đã duyệt";
                        break;
                    case (int)InquiryEnum.MakePo:
                        name = "Đã có đơn mua";
                        break;
                    case (int)InquiryEnum.Cancel:
                        name = "Hủy";
                        break;

                }
                return name;
            }

            public static string GetFptName(int fpt) {
                string name = "";
                switch (fpt) {
                    case (int)FptLot.Fuel:
                        name = "NL & BB & HC";
                        break;
                    case (int)FptLot.Plating:
                        name = "Gia Công Ngoài";
                        break;
                    case (int)FptLot.Tool:
                        name = "Công Cụ";
                        break;

                }
                return name;
            }

            public static string GetEoIName(int eoi, int type) {
                string name = "";
                switch (eoi) {
                    case (int)EoILot.Import:
                        name = "Nhập";
                        if (type == 2)
                            name += " thêm";
                        break;
                    case (int)EoILot.Export:
                        name = "Xuất ";// + Tool.GetTypeText(type);
                        //name += " hủy";
                        break;
                }
                return name;
            }

            public static string GetFptLotParam(int fpt, int fptId, DateTime createDate, int count) {
                var param = "";
                try {
                    switch (fpt) {
                        case (int)FptLot.Fuel:
                            using (var vfi = new vfiContext()) {
                                var fuelInv =
                                    vfi.FuelInventories.Where(
                                        fi => fi.FuelId == fptId && fi.CreateDate.Year == createDate.Year);
                                var inYear = String.Format("{0:00}", fuelInv.Count() + 1);
                                var inMonth =
                                    Convert.ToChar(fuelInv.Count(fi => fi.CreateDate.Month == createDate.Month) + 65 +
                                                   count);
                                //var kytubatdau = Convert.ToChar(inMonth);
                                //param = inYear + inMonth + createDate.ToString("MM") + createDate.ToString("yy");
                                param = createDate.ToString("yyMM") + inMonth + inYear;
                            }
                            break;
                        case (int)FptLot.Tool:
                            using (var vfi = new vfiContext()) {
                                var toolInv =
                                    vfi.ToolInventories.Where(
                                        fi => fi.ToolId == fptId && fi.CreateDate.Year == createDate.Year);
                                var inYear = String.Format("{0:00}", toolInv.Count() + 1);
                                var inMonth =
                                    Convert.ToChar(toolInv.Count(fi => fi.CreateDate.Month == createDate.Month) + 65 +
                                                   count);
                                //var kytubatdau = Convert.ToChar(inMonth);
                                //param = inYear + inMonth + createDate.ToString("MM") + createDate.ToString("yy");
                                param = createDate.ToString("yyMM") + inMonth + inYear;
                            }
                            break;
                    }
                }
                catch (Exception ex) {
                    throw ex;
                }
                return param;
            }
            public static string GetPlatingStatusText(int status) {
                var rs = "";

                switch (status) {
                    case 1:
                        rs = "Chưa duyệt";
                        break;
                    case 2:
                        rs = "Hoàn thành";
                        break;
                    case 3:
                        rs = "Huỷ bỏ";
                        break;
                    case 4:
                        rs = "Đang tiến hành";
                        break;
                    default:
                        rs = "Chưa duyệt";
                        break;
                }

                return rs;
            }

        }

        #endregion

        #region product
        public static class Product {
            // 2019-07-09 65 -> 70 : cho bao cao thang 5
            //public static double BaseProductPrice = 0.70;
            //public static double BaseProductionPrice = 0.60;
            public static double PointValue = 25000;
            public static int ProductionDecimalPoint = 1;
            //public static double ExchangeRateDesign = 22500;
            public static double MinVndPrice = 50;
            public static int StartShift1_HOUR = 7;
            public static int StartShift2_HOUR = 19;
            public static int EndShift_HOUR = 21;
            public static int RingId = 1512;
            public static int DaiwaRing = 67;
            public static DateTime StartWorkpieceDate = new DateTime(2016, 11, 1, 0, 0, 0).AddSeconds(-1);
            //public static double Second7_5h = 27000; //7.5h
            //public static double Second8h = 28800; //8h
            //public static double Second10h = 36000; // 10h
            //public static double Second12h = 43200; // 12h
            //public static double Second20h = 72000; //20h
            //public static double Second24h = 86400; // 24h

            public static ShiftDate GetShiftRangeDate(int shift, DateTime date) {
                switch (shift) {
                    case 1:
                        return new ShiftDate {
                            FromDate = new DateTime(date.Year, date.Month, date.Day, StartShift1_HOUR, 0, 0),
                            ToDate = new DateTime(date.Year, date.Month, date.Day, StartShift2_HOUR, 0, 0).AddSeconds(-1)
                        };
                    case 2:
                        return new ShiftDate {
                            FromDate = new DateTime(date.Year, date.Month, date.Day, StartShift2_HOUR, 0, 0),
                            ToDate = new DateTime(date.Year, date.Month, date.Day, StartShift1_HOUR, 0, 0).AddDays(1).AddSeconds(-1)
                        };
                    default:
                        return new ShiftDate {
                            FromDate = DateTime.Today,
                            ToDate = DateTime.Now
                        };
                }
            }

            public static int GetProductRate(
                double materialLenght, double materialWorkpiece,
                double productLenght, double knifeCut) {
                return productLenght + knifeCut > 0
                           ? Function.RoundDown((materialLenght - materialWorkpiece) / (productLenght + knifeCut)) - 1
                           : 1;
            }

            public static int GetMaterialRateInFactoryDayTime(double productivity, double productionRate) {
                return productionRate > 0
                    ? GetMaterialRateInTime(Monitor.GetParameterValue(Monitor.FactoryDayTiming), productivity, productionRate)
                    : 0;
            }

            public static int GetMaterialRateInFactoryShiftTime(double productivity, double productionRate) {
                return productionRate > 0
                    ? GetMaterialRateInTime(Monitor.GetParameterValue(Monitor.FactoryShiftTiming), productivity, productionRate)
                    : 0;
            }

            public static int GetMaterialRateInFactoryFullShiftTime(double productivity, double productionRate) {
                return productionRate > 0
                    ? GetMaterialRateInTime(Monitor.GetParameterValue(Monitor.FactoryFullShiftTiming), productivity, productionRate)
                    : 0;
            }

            public static int GetMaterialRateInTime(double seconds, double productivity, double productionRate) {
                return productionRate > 0
                    ? Function.RoundUp(GetProductionRateInTime(seconds, productivity) / productionRate)
                    : 0;
            }

            public static int GetProductionRateInTime(double seconds, double productivity) {
                return productivity > 0
                    ? Function.RoundUp(seconds / productivity)
                    : 0;
            }

            //public static int GetDesignCncProductionRateInTime(double seconds, double millProductivity) {
            //    return millProductivity > 0
            //        ? Function.RoundUp(seconds / millProductivity)
            //        : 0;
            //}

            public static int GetCncProductionRateInFactoryDayTime(double productivity, double productionRate) {
                return productionRate > 0 && productivity > 0
                    ? GetCncProductionRateInTime(Monitor.GetParameterValue(Monitor.FactoryDayTiming), productivity , productionRate)
                    : 0;
            }

            public static int GetCncProductionRateInFactoryShiftTime(double productivity, double productionRate) {
                return productionRate > 0 && productivity > 0
                    ? GetCncProductionRateInTime(Monitor.GetParameterValue(Monitor.FactoryDayTiming), productivity, productionRate)
                    : 0;
            }

            public static int GetCncProductionRateInTime(double seconds, double productivity, double productionRate) {
                return productionRate > 0 && productivity > 0
                    ? Function.RoundUp(seconds / (productivity / productionRate))
                    : 0;
            }

            public static int GetProductionRateInFactoryDayTime(double productivity) {
                return productivity > 0
                    ? GetProductionRateInTime(Monitor.GetParameterValue(Monitor.FactoryDayTiming), productivity)
                    : 0;
            }

            public static int GetProductionRateInFactoryShiftTime(double productivity) {
                return productivity > 0
                    ? GetProductionRateInTime(Monitor.GetParameterValue(Monitor.FactoryShiftTiming), productivity)
                    : 0;
            }

            public static int GetProductionRateInFactoryFullShiftTime(double productivity) {
                return productivity > 0
                    ? GetProductionRateInTime(Monitor.GetParameterValue(Monitor.FactoryFullShiftTiming), productivity)
                    : 0;
            }

            public static int GetProductionRateInDayTime(double productivity) {
                return productivity > 0
                    ? GetProductionRateInTime(Monitor.GetParameterValue(Monitor.DayTiming), productivity)
                    : 0;
            }

            public static string DetectCurrency(double productPrice) {
                if (productPrice <= MinVndPrice)
                    return "USD";

                var temp = Convert.ToInt32(productPrice);
                if (productPrice - temp != 0) {
                    return "USD";
                }

                return "VND"; // default return
            }

            //public static int ParseVndPrice(double? productPrice) {
            //    var price = productPrice ?? 0;

            //    if (price <= MinVndPrice) {
            //        price = price * ExchangeRateDesign;
            //    }
            //    else {
            //        price = Math.Round(price, 4);
            //        var temp = Convert.ToInt32(price);
            //        if (price - temp != 0)
            //            price = price * ExchangeRateDesign;
            //    }
            //    return Function.RoundUp(price);
            //}
            public static int ProductVndPrice(double? productPrice) {
                var exchangeRate = 1.0;
                using (var vfi = new tammaContext()) {
                    exchangeRate = Monitor.GetParameterValue(Monitor.ExchangeToVndRate);
                }
                return ProductVndPrice(productPrice, 1, exchangeRate);
            }

            public static int ProductVndPrice(double? productPrice, double priceRate, double exchangeRate) {
                var price = productPrice ?? 0;

                if (price <= MinVndPrice) {
                    price = price * exchangeRate * priceRate;
                }
                else {
                    price = Math.Round(price, 4);
                    var temp = Convert.ToInt32(price);
                    if (price - temp != 0)
                        price = price * exchangeRate;
                    price = price * priceRate;
                }
                return Function.RoundUp(price);
            }

            //public static int ProductPrice(double? productPrice) {
            //    return ProductVndPrice(productPrice, BaseProductPrice, ExchangeRateDesign);
            //}
            //public static int ProductionPrice(double? productPrice) {
            //    return ProductVndPrice(productPrice, BaseProductionPrice, ExchangeRateDesign);
            //}

            public static bool CheckDesign(int productId) {
                //var chk = false;
                try {
                    using (var vfi = new vfiContext()) {
                        var product = vfi.Products.FirstOrDefault(p => p.ProductId == productId);
                        if (product == null) return false;
                        if (product.FinishDesign) return true;
                        //thong tin san pham
                        if (product.Diameter == null || product.Length == null || product.KnifeCut == null ||
                            product.Productivity == null || product.ProductionRate == null)
                            return false;
                        if (product.Diameter == 0 || product.Length == 0 || product.KnifeCut == 0 ||
                            product.Productivity == 0 || product.ProductionRate == 0)
                            return false;
                        if (product.MaterialId == null)
                            return false;
                        if (product.ProcessingDesign == null)
                            return false;
                        var toolsDesign = vfi.ProductionTools.Where(pt => pt.Active && pt.ProductId == productId);
                        if (!toolsDesign.Any())
                            return false;
                        var process = vfi.ProductionProcesses.Where(pp => pp.ProductId == productId && pp.IsNecessary);
                        if (!process.Any())
                            return false;

                        return true;
                    }
                }
                catch (Exception ex) {
                    throw ex;
                }
                return false;
            }

            public static void UpdateProductDesign(int productId) {

                //var chk = false;
                try {
                    using (var vfi = new vfiContext()) {
                        var product = vfi.Products.FirstOrDefault(p => p.ProductId == productId);
                        if (product == null) return;
                        if (product.FinishDesign) return;
                        //thong tin san pham
                        if (product.Diameter == null || product.Length == null || product.KnifeCut == null ||
                            product.Productivity == null || product.ProductionRate == null)
                            return;
                        if (product.Diameter == 0 || product.Length == 0 || product.KnifeCut == 0 ||
                            product.Productivity == 0 || product.ProductionRate == 0)
                            return;
                        if (product.MaterialId == null)
                            return;
                        if (product.ProcessingDesign == null)
                            return;
                        var toolsDesign = vfi.ProductionTools.Where(pt => pt.Active && pt.ProductId == productId);
                        if (!toolsDesign.Any())
                            return;
                        var processes = vfi.ProductionProcesses.Where(pp => pp.ProductId == productId && pp.IsNecessary);
                        if (!processes.Any())
                            return;
                        if (processes.Any(x => x.Warehouse.IsProduction2) && !product.ProductionSections.Any(x => x.Active))
                            return;
                        if (processes.Any(x => x.Warehouse.IsHeatTreatment) && !product.ProductionHeatTreatments.Any(x => x.Active))
                            return;
                        if (processes.Any(x => x.Warehouse.IsPolish) && !product.ProductionPolishes.Any(x => x.Active))
                            return;
                        if (processes.Any(x => x.Warehouse.IsPlating) && !product.ProductionPlatings.Any(x => x.Active))
                            return;

                        product.FinishDesign = true;
                        vfi.SaveChanges();
                    }
                }
                catch (Exception ex) {
                    throw ex;
                }
            }

            public static double GetProductWeight(string materialName, double outDiameterDesign, double inDiameterDesign,
                                                  double productLength, double knifeCut, string shapeDesign) {
                var w = outDiameterDesign * outDiameterDesign;
                switch (shapeDesign.Trim()) {
                    case "H":
                        w = w * 1.1;
                        break;
                    case "S":
                    case "R":
                        w = w * 1.3;
                        break;
                    case "T":
                        w -= (inDiameterDesign * inDiameterDesign);
                        break;
                }
                w = w * (productLength + knifeCut) * 1.1 * 0.0066;
                if (materialName.StartsWith("A"))
                    w = w / 3;
                return w;
            }

            public enum ProductStatusEnum {
                Calculating = 1,
                //Quoting = 2,
                //Sampling = 3,
                //Producing = 4,
                Calculated = 5,
            }

            public static string GetText(int status) {
                switch (status) {
                    case (int)ProductStatusEnum.Calculating:
                        return "1.Sản phẩm mới";
                    case (int)ProductStatusEnum.Calculated:
                        return "2.Đã TNS";
                    //case (int)ProductStatusEnum.Quoting:
                    //    return "3.Báo giá";
                    //case (int)ProductStatusEnum.Sampling:
                    //    return "4.Làm mẫu";
                    //case (int)ProductStatusEnum.Producing:
                    //    return "5.Sản xuất";
                    default:
                        return "";
                }
            }

            public static string GetAutoProductCode() {
                string a = "";
                using (var vfi = new vfiContext()) {
                    var count = vfi.Products.Count(p => p.ModifiedDate.Year == DateTime.Now.Year);
                    a = DateTime.Now.ToString("yyMMdd") + (count + 1);
                }
                return a;
            }

            public static string GetProductionLot(string lot, int productId, int materialInvId) {
                using (var vfi = new vfiContext()) {
                    //1718C011
                    //17: tuan 17 - 18: nam 2018 - C01: may - 1: so tang trong tuan
                    // san pham co lo san xuat trong tuan
                    var productInv =
                        vfi.ProductInventories.FirstOrDefault(
                            id => id.LotNumber.Contains(lot) && id.ProductId == productId);
                    lot = lot.Trim();
                    if (productInv == null) {
                        return lot += "01";
                    }
                    // san pham co lo san xuat trong tuan cung nguyen lieu
                    productInv =
                        vfi.ProductInventories.FirstOrDefault(
                            id => id.LotNumber.Contains(lot) &&
                                  id.ProductId == productId && id.MaterialInvId == materialInvId);
                    if (productInv != null) {
                        return productInv.LotNumber;
                    }
                    //san pham co lo san xuat trong tuan khac nguyen lieu
                    var productInvs =
                        vfi.ProductInventories.Where(id => id.LotNumber.Contains(lot) && id.ProductId == productId)
                            .Select(id => id.LotNumber)
                            .Distinct().Count();
                    return lot += String.Format("{0:00}", productInvs + 1);
                }
            }

            public static double GetProductInvWeight(int productId, int warehouseId) {
                if (warehouseId == 0) return 0;
                using (var vfi = new vfiContext()) {
                    var product = vfi.Products.FirstOrDefault(p => p.ProductId == productId);
                    var warehouse = vfi.Warehouses.FirstOrDefault(x => x.WarehouseId == warehouseId);
                    if (product == null || warehouse == null) return 0;
                    if (warehouse.IsProduction) return product.ProductionWeight ?? 0;
                    if (warehouse.IsCncMilling) return product.CncWeight ?? 0;
                    if (warehouse.IsProduction2 || warehouse.IsProduction2Process) return product.Production2Weight ?? 0;
                    if (warehouse.IsProduction2Process) return product.Production2Weight ?? 0;
                    if (warehouse.IsHeatTreatment) return product.HeatTreatmentWeight ?? 0;
                    if (warehouse.IsPolish) return product.SurfaceTreatmentWeight ?? 0;
                    if (warehouse.IsPlating) return product.WaitingPlatingWeight ?? 0;
                    // qc - packing - finish - re-process
                    return product.QcWeight ?? 0;
                }
            }

            public static List<CalculatedProductProcess> GetCalculatedProductionProcess(int productId) {
                var model = new List<CalculatedProductProcess>();
                try {
                    using (var vfi = new vfiContext()) {
                        var productionProcesses = from pp in vfi.ProductionProcesses
                                                  where productId == pp.ProductId &&
                                                        pp.IsNecessary && pp.IsAlert
                                                  orderby pp.ProcessIndex descending
                                                  select pp;
                        var platings = (from pp in vfi.ProductionPlatings
                                        where productId == pp.ProductId &&
                                        pp.Active
                                        select pp).ToList();
                        var sections = (from ps in vfi.ProductionSections
                                        where ps.ProductId == productId &&
                                        ps.Active
                                        select ps).ToList();

                        var afterWarehouseProcessIds = new List<int>();
                        foreach (var process in productionProcesses) {
                            var entity = new CalculatedProductProcess() {
                                WarehouseProcessId = process.WarehouseId,
                                WarehouseProcessName = process.Warehouse.WarehouseName,
                                WarehouseIds = new List<int> { process.WarehouseId },
                                AfterWarehouseProcessIds = afterWarehouseProcessIds,
                                ProcessDay = 0,
                                ProductivityInDay = 0
                            };
                            switch (process.WarehouseId) {
                                case (int)Warehouse.Id.QcA:
                                case (int)Warehouse.Id.QcB:
                                case (int)Warehouse.Id.QcC:
                                    entity.WarehouseIds.AddRange(Warehouse.GetWarehouseIdQc());
                                    entity.ProcessDay = 3;
                                    break;
                                case (int)Warehouse.Id.WaitingPlating:
                                    entity.WarehouseIds.AddRange(Warehouse.GetWarehouseIds_Plating());
                                    if (platings.Any())
                                        entity.ProcessDay = platings.Sum(pp => pp.PlatingDay);
                                    break;
                                case (int)Warehouse.Id.Production2:
                                    entity.WarehouseIds.AddRange(Warehouse.GetWarehouseIdProduction2_ALL());
                                    if (sections.Any()) {
                                        var smallestSection = sections.Min(ps => ps.Productivity);
                                        entity.ProductivityInDay = Product.GetProductionRateInDayTime(smallestSection);
                                        //entity.ProductivityInDay = Product.GetProductionRateInTime(Product.Second7_5h, smallestSection);
                                    }
                                    break;
                                case (int)Warehouse.Id.Cnc:
                                    entity.ProductivityInDay = Product.GetProductionRateInFactoryDayTime(process.Product.MillProductivity ?? 0);
                                    //entity.ProductivityInDay = Product.GetDesignCncProductionRateInTime(Product.Second20h, process.Product.MillProductivity ?? 0);
                                    break;
                                case (int)Warehouse.Id.Production1:
                                    entity.ProductivityInDay = Product.GetProductionRateInFactoryDayTime(process.Product.Productivity ?? 0);
                                    //entity.ProductivityInDay = Product.GetProductionRateInTime(Product.Second7_5h, process.Product.Productivity ?? 0);
                                    break;
                                default:
                                    entity.ProcessDay = 1;
                                    break;
                            }
                            entity.WarehouseIds = entity.WarehouseIds.Distinct().ToList();
                            afterWarehouseProcessIds.AddRange(entity.WarehouseIds);
                            afterWarehouseProcessIds = afterWarehouseProcessIds.Distinct().ToList();
                            model.Add(entity);
                        }
                    }
                }
                catch (Exception ex) {
                    throw ex;
                }
                return model;
            }

            public static int GetPlanDay(int productId, double quantity, int warehouseId) {
                var productionAfter = 0;
                using (var vfi = new vfiContext()) {
                    var product = vfi.Products.FirstOrDefault(p => p.ProductId == productId);
                    if (product == null)
                        throw new AggregateException("Lỗi! Không tìm thấy sản phẩm");
                    var warehouses = Warehouse.GetWarehouseId_SumTotalQuantity();
                    var productInvs =
                        vfi.ProductInventories.Where(
                            pi => warehouses.Contains(pi.WarehouseId) && pi.ProductId == productId && pi.TotalQty > 0).ToList();
                    var totalInv = productInvs.Sum(pi => pi.TotalQty);
                    if (totalInv < quantity)
                        return GetProductionDay(productId, quantity);
                    productionAfter = 1;
                    var productInvsById = productInvs.Where(pi => pi.WarehouseId == Warehouse.Finish);
                    totalInv = productInvsById.Sum(pi => pi.TotalQty);
                    quantity -= totalInv;
                    if (quantity <= 0)
                        return productionAfter;
                    var processes =
                        vfi.ProductionProcesses.Where(
                            pp =>
                            pp.ProductId == productId && pp.IsNecessary &&
                            pp.WarehouseId != Warehouse.Production1 &&
                            pp.WarehouseId != Warehouse.Finish)
                           .OrderByDescending(pp => pp.ProcessIndex);
                    //var dayTiming = Monitor.GetParameterValue(Monitor.DayTiming);
                    foreach (var process in processes) {
                        if (quantity <= 0)
                            break;
                        //so ngay sx2
                        if (process.WarehouseId == Warehouse.Production2) {
                            if (!process.IsAlert) continue;
                            productInvsById =
                                productInvs.Where(
                                    pi =>
                                        pi.WarehouseId == Warehouse.Production2 ||
                                        pi.WarehouseId == Warehouse.Production2B ||
                                        pi.WarehouseId == Warehouse.Production2C ||
                                        pi.WarehouseId == Warehouse.Production2D).ToList();
                            totalInv = productInvsById.Sum(pi => pi.TotalQty);
                            var sections =
                                vfi.ProductionSections.Where(ps => ps.ProductId == productId &&
                                                                   ps.Active &&
                                                                   ps.SectionId != Section.ReProcessSection && ps.Productivity > 0);
                            if (sections.Any()) {
                                var smallestProductivity = sections.Max(ps => ps.Productivity);
                                var productivityInDay = 1;
                                if (smallestProductivity > 0) {
                                    productivityInDay = GetProductionRateInDayTime(smallestProductivity);
                                    //productivityInDay = Function.RoundUp(dayTiming / smallestProductivity);
                                }
                                productionAfter += Function.RoundUp(quantity / productivityInDay);
                                productionAfter += sections.Count();
                            }
                            else
                                productionAfter += 1;
                            quantity -= totalInv;
                        }
                        else if (process.WarehouseId == Warehouse.WaitingPlating) {
                            productInvsById =
                                productInvs.Where(
                                    pi =>
                                        pi.WarehouseId == Warehouse.WaitingPlating ||
                                        pi.WarehouseId == Warehouse.PlatingTest ||
                                        pi.WarehouseId == Warehouse.Plating).ToList();
                            totalInv = productInvsById.Sum(pi => pi.TotalQty);
                            //so ngay xi ma
                            var platings =
                                vfi.ProductionPlatings.Where(ps => ps.ProductId == productId && ps.Active);
                            if (platings.Any())
                                productionAfter += platings.Sum(ps => ps.PlatingDay);
                            else
                                productionAfter += 1;
                            quantity -= totalInv;
                        }
                        else if (process.WarehouseId == Warehouse.QcA ||
                                 process.WarehouseId == Warehouse.QcB)
                        //qc can 3 ngay
                        {
                            productInvsById =
                                productInvs.Where(
                                    pi =>
                                        pi.WarehouseId == Warehouse.QcA ||
                                        pi.WarehouseId == Warehouse.QcB).ToList();
                            totalInv = productInvsById.Sum(pi => pi.TotalQty);
                            productionAfter += 2;
                            quantity -= totalInv;
                        }
                        else if (process.WarehouseId == Warehouse.Cnc) {
                            productInvsById = productInvs.Where(pi => pi.WarehouseId == process.WarehouseId).ToList();
                            if (product.MillProductivity > 0) {
                                var productivityInDay = GetProductionRateInFactoryDayTime(product.MillProductivity ?? 0);
                                //var productivityInDay = Function.RoundUp(dayTiming / product.MillProductivity.Value);
                                productionAfter += Function.RoundUp(quantity / productivityInDay);
                            }
                            totalInv = productInvsById.Sum(pi => pi.TotalQty);
                            quantity -= totalInv;
                        }
                        else {
                            productInvsById = productInvs.Where(pi => pi.WarehouseId == process.WarehouseId).ToList();
                            totalInv = productInvsById.Sum(pi => pi.TotalQty);
                            productionAfter += 1;
                            quantity -= totalInv;
                        }
                        // tinh den kho can tinh, tinh xong se thoat
                        if (warehouseId == process.WarehouseId) break;
                    }
                }
                return productionAfter;
            }


            public static int GetProductionDay(int productId, double quantity) {
                var productionAfter = 0;
                //var factoryDayTiming = Monitor.GetParameterValue(Monitor.FactoryDayTiming);
                using (var vfi = new vfiContext()) {
                    var product = vfi.Products.FirstOrDefault(p => p.ProductId == productId);
                    if (product == null)
                        throw new AggregateException("Lỗi! Không tìm thấy sản phẩm");
                    var processes =
                        vfi.ProductionProcesses.Where(
                            pp =>
                            pp.ProductId == productId && pp.IsNecessary &&
                            pp.WarehouseId != Warehouse.Production1 &&
                            pp.WarehouseId != Warehouse.Production2)
                           .OrderBy(pp => pp.ProcessIndex);

                    var productionProductivity = product.Productivity ?? 0;
                    var sections =
                        vfi.ProductionSections.Where(
                            ps => ps.ProductId == productId && ps.Productivity > 0 &&
                                  ps.Active &&
                                  ps.SectionId != Section.ReProcessSection);
                    var sectionProductivity = 0.0;
                    if (sections.Any())
                        sectionProductivity = sections.OrderByDescending(ps => ps.Productivity).FirstOrDefault().Productivity;
                    // tinh tong so ngay cho ca sx 1 va sx 2
                    if (productionProductivity > 0)
                        if (productionProductivity > sectionProductivity) {
                            productionAfter = Function.RoundUp(quantity / GetProductionRateInFactoryDayTime(productionProductivity));
                            productionAfter += sections.Count();
                        }
                        else {
                            productionAfter +=
                                Convert.ToInt32(Function.RoundUp(quantity / GetProductionRateInDayTime(sectionProductivity), 0));
                            productionAfter += 2; // + cho sx 1 chay truoc
                            productionAfter += sections.Count();
                        }
                    //so ngay sx1
                    //if (product.Productivity != 0)
                    //{
                    //    productionAfter = Function.RoundUp(quantity / (Second/20h / product.Productivity.Value));
                    //}
                    foreach (var process in processes) {
                        //so ngay sx2
                        //if (process.WarehouseId == Warehouse.Production2)
                        //{
                        //    //var sections =
                        //    //    vfi.ProductionSections.Where(ps => ps.ProductId == productId && ps.Active);
                        //    if (sectionProductivity != 0)
                        //    {
                        //        //var smallestSection = sections.OrderBy(ps => ps.Productivity).FirstOrDefault();
                        //        productionAfter +=
                        //            Convert.ToInt32(Function.RoundUp(quantity / (Second/8h / sectionProductivity), 0));
                        //    }
                        //    productionAfter += sections.Count();
                        //}
                        //else 
                        if (process.WarehouseId == Warehouse.WaitingPlating) {
                            //so ngay xi ma
                            var platings =
                                vfi.ProductionPlatings.Where(ps => ps.ProductId == productId && ps.Active);
                            if (platings.Any())
                                productionAfter += platings.Sum(ps => ps.PlatingDay);
                            else
                                productionAfter += 1;
                        }
                        else if (process.WarehouseId == Warehouse.QcA ||
                                 process.WarehouseId == Warehouse.QcB)
                            //qc can 3 ngay
                            productionAfter += 2;
                        else
                            //kho khac can 1 ngay
                            productionAfter += 1;
                    }
                }
                return productionAfter;
            }
        }
        #endregion

        #region material
        public static class Material {
            public enum WorkpieceExport {
                Sales = 1,
                Destroy = 2,
                Process = 3,
            }
            public enum WorkpieceType {
                Piece = 1,
                Scrap = 2,
                Defect = 3,
            }

            public static List<int> GetMaterialIdentityType() {
                return new List<int> { 
                    (int)WorkpieceType.Piece, 
                    (int)WorkpieceType.Scrap, 
                    (int)WorkpieceType.Defect 
                };
            }
            public static string GetMaterialIdentityTypeName(int type) {
                switch (type) {
                    case (int)WorkpieceType.Piece:
                        return "Khối";
                    case (int)WorkpieceType.Scrap:
                        return "Vụn";
                    case (int)WorkpieceType.Defect:
                        return "Phế phẩm";
                    default:
                        return "";
                }
            }
            public static string CaseTextMaterialShape(string shape) {
                switch (shape.Trim()) {
                    case "D":
                        return "Rod";
                    case "R":
                        return "Rectangel";
                    case "S":
                        return "Square";
                    case "T":
                        return "Tube";
                    case "H":
                        return "Hexagon";
                    default:
                        return "";
                }
            }
            public enum UseType {
                Using = 1,
                SendBack = 2
            }

            public static string GetMaterialLot(int vendorId, int materialId, double lenght, DateTime createDate,
                int count) {
                var param = "";
                try {
                    using (var vfi = new vfiContext()) {
                        var vendor = vfi.Vendors.FirstOrDefault(v => v.VendorId == vendorId);
                        var materialInvs =
                            vfi.MaterialInventories.Where(
                                mi =>
                                    mi.VendorId == vendorId && mi.MaterialId == materialId &&
                                    mi.ImportDate.Year == createDate.Year);
                        var inYear = Convert.ToChar(materialInvs.Count() + 65 + count);
                        param = vendor.VendorCode + createDate.ToString("yy") + inYear;
                        var materialByLot =
                            vfi.MaterialInventories.FirstOrDefault(
                                mi =>
                                    mi.LotNumber.Equals(param) &&
                                    mi.MaterialId == materialId &&
                                    mi.VendorId == vendorId &&
                                    mi.Length == lenght);
                        if (materialByLot != null)
                            param = GetMaterialLot(vendorId, materialId, lenght, createDate, count + 1);
                    }
                }
                catch (Exception ex) {
                    throw ex;
                }
                return param;
            }

            public static string GetMaterialDesignNo(double outDiameter, double inDiameter,
                string diameterType, string shape) {
                if (outDiameter == 0 && inDiameter == 0)
                    return "";
                var a = "";
                if (inDiameter > 0) {
                    a = "(" + (shape + "").Trim() + ")(" + (outDiameter) +
                        "x" + (inDiameter) + ")-" + (diameterType + "").Trim();
                }
                else {
                    a = "(" + (shape + "").Trim() + ")" + (outDiameter) + "-" + (diameterType + "").Trim();
                }
                return a;
            }

            public static string GetMaterialDesignNo(double outDiameter, double inDiameter,
                string diameterType, string shape, double length) {
                if (outDiameter == 0 && inDiameter == 0)
                    return "";
                var a = "";
                if (inDiameter > 0) {
                    a = "(" + (shape + "").Trim() + ")(" + (outDiameter) +
                        "x" + (inDiameter) + ")x" + (length / 1000) + " -" + (diameterType + "").Trim();
                }
                else {
                    a = "(" + (shape + "").Trim() + ")" + (outDiameter) + "x" + (length / 1000) + "-" +
                        (diameterType + "").Trim();
                }
                return a;
            }

            public static string GetMaterialDesignNo(Models.Material material) {
                return GetMaterialDesignNo(material.OutDiameter, material.InDiameter, material.DiameterType,
                    material.Shape);
            }

            public static string GetMaterialDesignNo(Models.Material material, double lenght) {
                return GetMaterialDesignNo(material.OutDiameter, material.InDiameter, material.DiameterType,
                    material.Shape, lenght);
            }

            public static string GetMaterialFullCode(string name, double outDiameter, double inDiameter,
                double length,
                string diameterType, string shape) {
                if (string.IsNullOrWhiteSpace(name))
                    return "";
                var a = name;
                if (inDiameter > 0) {
                    a += "(" + (shape + "").Trim() + ")(" + (outDiameter) +
                         "x" + (inDiameter) + ")x" + (length / 1000) + "-" +
                         (diameterType + "").Trim();
                }
                else {
                    a += "(" + (shape + "").Trim() + ")" + (outDiameter) + "x" + (length / 1000) + "-" +
                         (diameterType + "").Trim();
                }
                return a;
            }

            public static string GetMaterialFullCode(Models.Material material, double length) {
                return GetMaterialFullCode(material.MaterialName, material.OutDiameter, material.InDiameter, length,
                    material.DiameterType, material.Shape);
            }

            public static string GetMaterialInvDesignNo(string name,
                double outDiameter, double inDiameter, double length,
                string diameterType, string shape,
                string vendorCode, string lotNumber) {
                var a = GetMaterialFullCode(name, outDiameter, inDiameter, length, diameterType, shape);
                if (lotNumber.Contains(vendorCode))
                    return a + "-" + lotNumber;
                return vendorCode + a + "-" + lotNumber;
            }

            public static string GetMaterialInvDesignNo(MaterialInventory inv) {
                var a = GetMaterialFullCode(inv.Material, inv.Length);
                if (inv.LotNumber.Contains(inv.Vendor.VendorCode))
                    return a + "-" + inv.LotNumber;
                return inv.Vendor.VendorCode + a + "-" + inv.LotNumber;
            }
        }
        #endregion

        #region tool
        public static class Tool {
            public enum ExportType {
                Production = 1,
                Production2 = 2,
                Destroy = 3,
                Return = 4
            }
            public enum ImportType {
                Normal = 1,
                Purchasing = 2,
            }

            public static string GetTypeText(int department) {
                switch (department) {
                    case (int)ExportType.Production:
                        return "SX1";
                    case (int)ExportType.Production2:
                        return "SX2";
                    case (int)ExportType.Destroy:
                        return "huỷ";
                    case (int)ExportType.Return:
                        return "trả";
                    default:
                        return "";
                }
            }

            public static string GetFullToolDesign(string toolDesignNo, string toolMaterial, string toolProduction) {
                var str = toolDesignNo;
                if (!string.IsNullOrWhiteSpace(toolMaterial))
                    str += ("-" + toolMaterial);
                if (!string.IsNullOrWhiteSpace(toolProduction))
                    str += ("-" + toolProduction);
                return str;
            }
        }
        #endregion

        #region work order
        public static class WorkOrder {
            public static List<int> ActivatedStatus {
                get {
                    return new List<int> { 
                        (int)MyUtilities.WorkOrder.Status.Pending, 
                        (int)MyUtilities.WorkOrder.Status.Actived, 
                        (int)MyUtilities.WorkOrder.Status.InProcess };
                }
            }
            public static List<MySystem.MyStatusModel> ActivatedStatusModel {
                get {
                    return ActivatedStatus.Select(x => new MySystem.MyStatusModel { 
                        Value = x,
                        Text = MyUtilities.WorkOrder.GetText(x) 
                    }).ToList();
                }
            }
            public enum Status {
                Pending = 1,
                Actived = 2,
                InProcess = 3,
                Finish = 4,
                Cancel = 9,
            }
            public static string GetText(int status) {
                var rs = "";

                switch (status) {
                    case (byte)Status.Pending:
                        rs = "Đang chờ";
                        break;
                    case (byte)Status.Actived:
                        rs = "Đã kích hoạt";
                        break;
                    case (byte)Status.InProcess:
                    //case (byte)Status.SecondProcess:
                        rs = "Đang xử lý";
                        break;
                    case (byte)Status.Finish:
                        rs = "Hoàn thành";
                        break;
                    case (byte)Status.Cancel:
                        rs = "Hủy";
                        break;
                    default:
                        rs = "";
                        break;
                }

                return rs;
            }
        }
        #endregion

        #region accounting

        public static class Accounting {


            public enum Status {
                NotApprove = 1,
                Approved = 2,
                Cancel = 3,
                InProcess = 4,
            }

            public static string GetStatusText(int status) {
                var rs = "";

                switch (status) {
                    case (int)Status.NotApprove:
                        rs = "Chưa duyệt";
                        break;
                    case (int)Status.Approved:
                        rs = "Đã duyệt";
                        break;
                    case (int)Status.Cancel:
                        rs = "Huỷ bỏ";
                        break;
                    case (int)Status.InProcess:
                        rs = "Đang tiến hành";
                        break;
                    default:
                        rs = "Chưa duyệt";
                        break;
                }

                return rs;
            }

        }
        #endregion
    }
}