using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sports;

namespace DIYFE.Web
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
            
        }
        //EXAMPLE OF HOW TO USE WITH A ORM TOOL
        //public static List<CommunicationType> AllCommunicationTypes
        //{
        //    get
        //    {
        //        return HttpContext.Current.Application["communicationType"] as List<CommunicationType>;
        //    }
        //}
        #region Sports

        public static List<Team> MLBTeams
        {
            get {
                if (HttpContext.Current.Application["MLBTeams"] == null)
                {
                    ListAccess la = new ListAccess();
                    HttpContext.Current.Application["MLBTeams"] = la.AllTeams(1);
                }
                return (List<Team>)(HttpContext.Current.Application["MLBTeams"]);
            }
        }
        public static List<Team> NBATeams
        {
            get
            {
                if (HttpContext.Current.Application["NBATeams"] == null)
                {
                    ListAccess la = new ListAccess();
                    HttpContext.Current.Application["NBATeams"] = la.AllTeams(2);
                }
                return (List<Team>)(HttpContext.Current.Application["NBATeams"]);
            }
        }
        public static List<Team> NHLTeams
        {
            get
            {
                if (HttpContext.Current.Application["NHLTeams"] == null)
                {
                    ListAccess la = new ListAccess();
                    HttpContext.Current.Application["NHLTeams"] = la.AllTeams(3);
                }
                return (List<Team>)(HttpContext.Current.Application["NHLTeams"]);
            }
        }
        public static List<Team> NFLTeams
        {
            get
            {
                if (HttpContext.Current.Application["NFLTeams"] == null)
                {
                    ListAccess la = new ListAccess();
                    HttpContext.Current.Application["NFLTeams"] = la.AllTeams(4);
                }
                return (List<Team>)(HttpContext.Current.Application["NFLTeams"]);
            }
        }

        #endregion

        public static string ApplicationVar
        {
            get
            {
                return HttpContext.Current.Application["someVarName"] as string;
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

        public enum Leagues
        { 
          MLB= 1,
          NBA=2,
          NHL=3,
          NFL=4
        };

        public enum Seasons
        {
            _2002=1,
            _2003=2,
            _2004=3,
            _2005=4,
            _2006=5,
            _2007=6,
            _2008=7,
            _2009=8,
            _2010=9,
            _2011=10,
            _2012=11,
            _2013=12
        }

        public enum BettingSetup
        {
            MoneyLine = 1,
            Spread = 2,
            Spread3Point = 3
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