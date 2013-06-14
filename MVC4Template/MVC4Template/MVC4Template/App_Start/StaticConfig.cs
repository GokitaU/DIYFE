using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC4Template
{
    public class StaticConfig
    {
        public static void LoadStaticCache()
        {
            //EXAMPLE OF HOW TO USE WITH A ORM TOOL
            //using (var context = new MDMContext())
            //{
            //    HttpContext.Current.Application["communicationType"] = context.CommunicationType.ToList();
            //}
            HttpContext.Current.Application["someVarName"] = "This is a test.  Normally a  list of objects but since no DB connection is made it's only a string...le sigh, poor string.";

        }
        //EXAMPLE OF HOW TO USE WITH A ORM TOOL
        //public static List<CommunicationType> AllCommunicationTypes
        //{
        //    get
        //    {
        //        return HttpContext.Current.Application["communicationType"] as List<CommunicationType>;
        //    }
        //}


        public static string SiteName
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["SiteName"] as string;
            }
        }

        public static string LunceneIndexLocation
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["LuceneIndexLocation"] as string;
            }
        }
        
        public static string WebConfigSettingTest
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["TestAppSetting"].ToString();
            }
        }



        #region Static Mail Settings

        public static string MailSenderAddress
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["SenderAddress"].ToString();
            }
        }

        public static string MailErrorAddress
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ErrorAddress"].ToString();
            }
        }

        #endregion

    }
}