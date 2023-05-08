using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModirOnline.Common
{
    public class Alert
    {
        public static string Entity_MaterialCategory = "طبقه بندی مواد اولیه";
        public static string Entity_Material = "مواد اولیه";
        public static string Entity_Inventory = "انبار";
        public static string Entity_InventoryAmount = "مواد اولیه در انبار";
        public static string Entity_Category = "طبقه بندی محصول";
        public static string GetInsertAlert(string EntityName)
        {
            return EntityName + " با موفقیت ثبت شد.";
        }
        public static string GetDeleteAlert(string EntityName)
        {
            return EntityName + " با موفقیت حذف شد.";
        }
        public static string GetUpdateAlert(string EntityName)
        {
            return EntityName + " با موفقیت بروز رسانی شد.";
        }

        public static string ServerException = "خطایی از سمت سرور رخ داده است.";
        public static string RemoveException = "امکان حذف این آیتم وجود ندارد.";
        public static string Success = "موفق";
        public static string UnSuccess = "نا موفق";
        public static string NotFound = "موردی یافت نشد";


        public static string GetVisibleStatus(int Visible)
        {
            if (Visible == 1)
            {
                return "قابل نمایش";
            }
            else
            {
                return "غیر قابل نمایش";
            }
        }
    }
}
