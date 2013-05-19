using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DIYFELib;


namespace DIYFEWeb
{
    public class AppStatic
    {
        public static void LoadStaticCache()
        {
            //EXAMPLE OF HOW TO USE WITH A ORM TOOL
            //using (var context = new MDMContext())
            //{
            //    HttpContext.Current.Application["communicationType"] = context.CommunicationType.ToList();
            //}
            HttpContext.Current.Application["someVarName"] = "This is a test.  Normally a  list of objects but since no DB connection is made it's only a string...le sigh, poor string.";

            DIYFELib.ListAccess la = new DIYFELib.ListAccess();
            HttpContext.Current.Application["Categories"] = la.AllCategory();

        }
        //EXAMPLE OF HOW TO USE WITH A ORM TOOL
        //public static List<CommunicationType> AllCommunicationTypes
        //{
        //    get
        //    {
        //        return HttpContext.Current.Application["communicationType"] as List<CommunicationType>;
        //    }
        //}


        public static string ApplicationVar
        {
            get
            {
                return HttpContext.Current.Application["someVarName"] as string;
            }
        }

        public static List<Category> Categories
        {
            get
            {
                return (List<Category>)(HttpContext.Current.Application["Categories"]);
            }
        }

        public static string LunceneIndexLocation
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["LuceneIndexLocation"] as string;
            }
        }
        
        public static string BaseSiteUrl
        {
            get
            {
                HttpContext context = HttpContext.Current;
                string baseUrl = context.Request.Url.Scheme + "://" + context.Request.Url.Authority + context.Request.ApplicationPath.TrimEnd('/') + '/';
                return baseUrl;
            }
        }

        #region Static Mail Settings

        public static string EmailSenderAddress
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["SenderAddress"].ToString();
            }
        }

        #endregion

    }
}