using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SUPMS.Infrastructure.Utilities
{
    public static class ExtensionsMethod 
    {
        /// <summary>
        /// Covnert the Double to String 
        /// i.e if IsCurrency is true then Appends the Currency Symbol and Formates as per the Session VFormatter
        /// </summary>
        /// <param name="x">Double</param>
        /// <param name="IsCurrency">Boolean</param>
        /// <returns>String in Formate in Session VFormatter</returns>
        public static string ToStringExt(this double x , bool IsCurrency = false)
        {
            SessionManager sessionMgr = (SessionManager)HttpContext.Current.Session["REMSSession"];
            if (IsCurrency)
            {
                if (sessionMgr.vFormatter.SpaceAfterCurrency)
                {
                    if (sessionMgr.vFormatter.ShowInFront)
                    {
                        return sessionMgr.vFormatter.CurrenySymbol + " " + x.ToString(sessionMgr.vFormatter.decimalFormate);
                    }
                    else
                    {
                        return x.ToString(sessionMgr.vFormatter.decimalFormate) + " " + sessionMgr.vFormatter.CurrenySymbol;
                    }
                }
                else
                {
                    if (!sessionMgr.vFormatter.ShowInFront)
                    {
                        return x.ToString(sessionMgr.vFormatter.decimalFormate) + sessionMgr.vFormatter.CurrenySymbol;
                    }
                    else
                    {
                        return sessionMgr.vFormatter.CurrenySymbol + x.ToString(sessionMgr.vFormatter.decimalFormate);
                    }
                }
            }
            else
            {
                return x.ToString(sessionMgr.vFormatter.decimalFormate);
            }
        }
        /// <summary>
        /// Covnert the Decimal to String 
        /// i.e if IsCurrency is true then Appends the Currency Symbol and Formates as per the Session VFormatter
        /// </summary>
        /// <param name="x">Decimal</param>
        /// <param name="IsCurrency">Boolean</param>
        /// <returns>String in Formate in Session VFormatter</returns>
        public static string ToStringExt(this decimal x, bool IsCurrency = false)
        {
            SessionManager sessionMgr = (SessionManager)HttpContext.Current.Session["REMSSession"];
            if (IsCurrency)
            {
                if (sessionMgr.vFormatter.SpaceAfterCurrency)
                {
                    if (sessionMgr.vFormatter.ShowInFront)
                    {
                        return sessionMgr.vFormatter.CurrenySymbol + " " + x.ToString(sessionMgr.vFormatter.decimalFormate);
                    }
                    else
                    {
                        return x.ToString(sessionMgr.vFormatter.decimalFormate) + " " + sessionMgr.vFormatter.CurrenySymbol;
                    }
                }
                else
                {
                    if (!sessionMgr.vFormatter.ShowInFront)
                    {
                        return x.ToString(sessionMgr.vFormatter.decimalFormate) + sessionMgr.vFormatter.CurrenySymbol;
                    }
                    else
                    {
                        return sessionMgr.vFormatter.CurrenySymbol + x.ToString(sessionMgr.vFormatter.decimalFormate);
                    }
                }
            }
            else
            {
                return x.ToString(sessionMgr.vFormatter.decimalFormate);
            }
        }
        /// <summary>
        /// Convert the Int to String
        /// i.e if Currency is true then appends the currency symbol and formates as per the session VFormatter
        /// </summary>
        /// <param name="x">Interger Value</param>
        /// <param name="IsCurrency">Boolean</param>
        /// <returns>String in Formate as for session Vformatter</returns>
        public static string ToStringExt(this int x, bool IsCurrency = false)
        {
            SessionManager sessionMgr = (SessionManager)HttpContext.Current.Session["REMSSession"];
            if (IsCurrency)
            {
                if (sessionMgr.vFormatter.SpaceAfterCurrency)
                {
                    if (sessionMgr.vFormatter.ShowInFront)
                    {
                        return sessionMgr.vFormatter.CurrenySymbol + " " + x.ToString(sessionMgr.vFormatter.decimalFormate);
                    }
                    else
                    {
                        return x.ToString(sessionMgr.vFormatter.decimalFormate) + " " + sessionMgr.vFormatter.CurrenySymbol;
                    }
                }
                else
                {
                    if (!sessionMgr.vFormatter.ShowInFront)
                    {
                        return x.ToString(sessionMgr.vFormatter.decimalFormate) + sessionMgr.vFormatter.CurrenySymbol;
                    }
                    else
                    {
                        return sessionMgr.vFormatter.CurrenySymbol + x.ToString(sessionMgr.vFormatter.decimalFormate);
                    }
                }
            }
            else
            {
                return x.ToString();
            }
        }
        /// <summary>
        /// Convert the Long to String 
        ///  i.e if Currency is true then appends the currency symbol and formates as per the session VFormatter
        /// </summary>
        /// <param name="x">Long</param>
        /// <param name="IsCurrency">Boolean</param>
        /// <returns>String in Formate as for session VFormatter</returns>
        public static string ToStringExt(this long x, bool IsCurrency = false)
        {
            SessionManager sessionMgr = (SessionManager)HttpContext.Current.Session["REMSSession"];
            if (IsCurrency)
            {
                if (sessionMgr.vFormatter.SpaceAfterCurrency)
                {
                    if (sessionMgr.vFormatter.ShowInFront)
                    {
                        return sessionMgr.vFormatter.CurrenySymbol + " " + x.ToString(sessionMgr.vFormatter.decimalFormate);
                    }
                    else
                    {
                        return x.ToString(sessionMgr.vFormatter.decimalFormate) + " " + sessionMgr.vFormatter.CurrenySymbol;
                    }
                }
                else
                {
                    if (!sessionMgr.vFormatter.ShowInFront)
                    {
                        return x.ToString(sessionMgr.vFormatter.decimalFormate) + sessionMgr.vFormatter.CurrenySymbol;
                    }
                    else
                    {
                        return sessionMgr.vFormatter.CurrenySymbol + x.ToString(sessionMgr.vFormatter.decimalFormate);
                    }
                }
            }
            else
            {
                return x.ToString();
            }

        }
        /// <summary>
        /// Convert the Given Formate of the DateTime as Per String(DateTime) Formate in the Session VFormatter 
        /// </summary>
        /// <param name="x">DateTime</param>
        /// <returns>String in Formate as for session VFormatter ex:25-05-2016</returns>
        public static string ToStringExt(this DateTime  x)
        {
            SessionManager sessionMgr = (SessionManager)HttpContext.Current.Session["REMSSession"];
            return x.ToString(sessionMgr.vFormatter.DateFormate);
        }
        /// <summary>
        /// Convert string  to Decimal with Currency Extension as Per Currency Formate in the Session VFormatter 
        /// </summary>
        /// <param name="x">string</param>
        /// <returns>String With Currency Symbol Extension E.g:545.00AFN</returns>
        public static string ToSafeCurrencyExt(this string x)
        {
           return x.ToSafeDeciamlExt().ToStringExt(true);
        }
        /// <summary>
        /// Convert the Given String to String as Per String(DateTime) Formate in the Session VFormatter 
        /// </summary>
        /// <param name="x">String</param>
        /// <returns>String in Formate in Session VFormatter</returns>
        public static string ToSafeDateStringExt(this string x)
        {
           return x.ToSafeDateTimeExt().ToStringExt();
        }
        /// <summary>
        /// Convert the Given Formate of the String as Per Date Time Formate in the Session VFormatter 
        /// </summary>
        /// <param name="x">String </param>
        /// <param name="formate">String i.e Formate in which the String Date Time is.</param>
        /// <returns>String in Formate in Session VFormatter</returns>
        public static string ToSafeDateStringExt(this string x, string formate)
        {
            return x.ToSafeDateTimeExt(formate).ToStringExt();
        }
        /// <summary>
        /// Convert the String to Decimal 
        /// </summary>
        /// <param name="x">String</param>
        /// <returns>Decimal</returns>
        public static decimal ToSafeDeciamlExt(this string x)
        {
            if (string.IsNullOrEmpty(x))
            {
                return 0;
            }
            else
            {
                decimal iresult = 0;
                Decimal.TryParse(x, out iresult);
                return iresult;
            }
            
        }
        /// <summary>
        /// Convert the String to Int32 
        /// </summary>
        /// <param name="x">String</param>
        /// <returns>Int</returns>
        public static int ToSafeIntegerExt(this string x)
        {
            if (string.IsNullOrEmpty(x))
            {
                return 0;
            }
            else
            {
                int iresult = 0;
                 Int32.TryParse(x ,out iresult );
                 return iresult;
            }

        }
        /// <summary>
        /// Convert The Given String To DateTime
        /// </summary>
        /// <param name="x">String i.e DateTime IN String format</param>
        /// <returns>DateTime</returns>
        public static DateTime ToSafeDateTimeExt(this string x)
        {
            if (string.IsNullOrEmpty(x))
            {
                return new DateTime();
            }
            else
            {
                DateTime iresult = new DateTime();
                DateTime.TryParse(x, out iresult);
                return iresult;
            }

        }
        /// <summary>
        /// Convert the Given Formate of the String in Date Time 
        /// </summary>
        /// <param name="x">String</param>
        /// <param name="formate">String i.e Formate in which the String Date Time is.</param>
        /// <returns>Date Time</returns>
        public static DateTime ToSafeDateTimeExt(this string x, string formate)
        {
            if (string.IsNullOrEmpty(x))
            {
                return new DateTime();
            }
            else
            {
                var strresult = x;
                DateTime iresult = new DateTime();
                iresult = DateTime.ParseExact(x, formate, CultureInfo.InvariantCulture);
                return iresult;
            }
        }


        /// <summary>
        /// Convert The Given String To DateTime ,For Model Binding
        /// </summary>
        /// <param name="x">String i.e DateTime IN String format</param>
        /// <returns>DateTime</returns>
        public static DateTime ToSafeDateTimeWithModel(this string x)
        {
                SessionManager sessionMgr = (SessionManager)HttpContext.Current.Session["REMSSession"];
                string DateFormate = sessionMgr.vFormatter.DateFormate;
                return x.ToSafeDateTimeExt(DateFormate);
        }
        /// <summary>
        /// Convert the Given Formate of the String(decimal) to string Eg:3 Weeks 2 Days 2 Hours
        /// </summary>
        /// <param name="x">string !.e No.of milliseconds in string formate</param>
        /// <returns>string</returns>
        public static string ToDurationExt(this string x)
        {
            decimal total = x.ToSafeDeciamlExt();
            return total.ToDurationExt();
        }
        /// <summary>
        /// Converts Decimal to String Eg:3 Weeks 2 Days 2 Hours
        /// </summary>
        /// <param name="x">Decimal</param>
        /// <returns>String</returns>
        public static string ToDurationExt(this decimal x)
        {
            decimal weeks = 0;
            decimal days = 0;
            decimal hours = 0;
            decimal minutes = 0;
            decimal seconds = 0;
            seconds = x % 60;
            x = Math.Floor(x / 60);
            minutes = x % 60;
            x = Math.Floor(x / 60);
            hours = x % 24;
            x = Math.Floor(x / 24);
            days = x % 7;
            weeks = Math.Floor(x / 7);
            StringBuilder strText = new StringBuilder();
            if (weeks == 1)
                strText.Append(weeks).Append("week");
            else
                strText.Append(weeks).Append("weeks");
            if (days == 1)
                strText.Append(weeks).Append("day");
            else
                strText.Append(weeks).Append("days");
            if (hours == 1)
                strText.Append(weeks).Append("hour");
            else
                strText.Append(weeks).Append("hours");
            if (minutes == 1)
                strText.Append(weeks).Append("minute");
            else
                strText.Append(weeks).Append("minutes");

            return strText.ToString();
        }
        /// <summary>
        /// Covnert the Decimal to String 
        /// i.e if IsCurrency is true then Appends the Currency Symbol and Formates as per the Session VFormatter
        /// </summary>
        /// <param name="x">Decimal(Nullable)</param>
        /// <param name="IsCurrency">Boolean</param>
        /// <returns>String  as in Formate in Session VFormatter</returns>
        public static string ToSafeStringExt(this decimal? x, bool IsCurrency = false)
        {
            if (!x.HasValue)
            {
                return (0.00).ToStringExt(IsCurrency);
            }
            else
            {
                return x.Value.ToStringExt(IsCurrency);
            }
        }

        public static void SaveAsCloud(this HttpPostedFileBase value,string fileName)
        {

        }
        /// <summary>Gets the subdomain portion of a url, given a known "root" domain</summary>
        public static string GetSubdomain(this string url, string domain = null)
        {
            var subdomain = url;
            if (subdomain != null)
            {
                if (domain == null)
                {
                    // Since we were not provided with a known domain, assume that second-to-last period divides the subdomain from the domain.
                    var nodes = url.Split('.');
                    var lastNodeIndex = nodes.Length - 1;
                    if (lastNodeIndex > 0)
                        domain = nodes[lastNodeIndex - 1] + "." + nodes[lastNodeIndex];
                }

                // Verify that what we think is the domain is truly the ending of the hostname... otherwise we're hooped.
                if (!subdomain.EndsWith(domain))
                    throw new ArgumentException("Site was not loaded from the expected domain");

                // Quash the domain portion, which should leave us with the subdomain and a trailing dot IF there is a subdomain.
                subdomain = subdomain.Replace(domain, "");
                // Check if we have anything left.  If we don't, there was no subdomain, the request was directly to the root domain:
                if (string.IsNullOrWhiteSpace(subdomain))
                    return null;

                // Quash any trailing periods
                subdomain = subdomain.TrimEnd(new[] { '.' });
            }

            return subdomain;
        }

        /// <summary>
        /// change date to specific format
        /// </summary>
        /// <param name="x"></param>
        /// <param name="Formate"></param>
        /// <param name="ExpectedFormate"></param>
        /// <returns></returns>
        public static string ChangeToSpecificFormate(this string x,string Formate,string ExpectedFormate)
        {
            if (string.IsNullOrEmpty(x))
            {               
                return DateTime.Now.ToString(ExpectedFormate);
            }
            else
            {
                DateTime iresult = new DateTime();
                iresult = DateTime.ParseExact(x.Split(' ')[0], Formate.Split(' ')[0], CultureInfo.InvariantCulture);
                //string result1 = iresult.ToUniversalTime().ToString("u");
                //iresult = DateTime.ParseExact(x, "yyyy'-'MM'-'dd HH':'mm':'ss'Z'", CultureInfo.InvariantCulture);
                //DateTime result2;                
                //iresult = DateTime.ParseExact(x, Formate, CultureInfo.InvariantCulture);
                DateTime result1;
                if (DateTime.TryParseExact(iresult.ToString(ExpectedFormate), ExpectedFormate, CultureInfo.InvariantCulture, DateTimeStyles.None, out result1))
                {                    
                    return iresult.ToString(ExpectedFormate);
                    //for April 6th, 2012 like dates
                    //SimpleDateFormat format = new SimpleDateFormat("d");
                    //string date = iresult.ToString(ExpectedFormate);
                    //if (date.EndsWith("1") && !date.EndsWith("11"))
                    //    format = new SimpleDateFormat("EE MMM d'st', yyyy");
                    //else if (date.endsWith("2") && !date.endsWith("12"))
                    //    format = new SimpleDateFormat("EE MMM d'nd', yyyy");
                    //else if (date.endsWith("3") && !date.endsWith("13"))
                    //    format = new SimpleDateFormat("EE MMM d'rd', yyyy");
                    //else
                    //    format = new SimpleDateFormat("EE MMM d'th', yyyy");
                }
                else
                {                    
                    return iresult.ToShortDateString();
                }
            }
        }

        /// <summary>
        /// decimal value to string of stoprage format like kbs ,gbs etc...
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetInDigitalStorageFormat(this decimal length)
        {
            if (length >= 1024)
            {
                length = (length / 1024);
                string FormatType = "";
                int count = 0;
                while (length >= 1000)
                {
                    length = (length / 1000);
                    count++;
                }
                switch (count)
                {
                    case 0:
                        FormatType = "KB";
                        break;
                    case 1:
                        FormatType = "MB";
                        break;
                    case 2:
                        FormatType = "GB";
                        break;
                    case 3:
                        FormatType = "TB";
                        break;
                    case 4:
                        FormatType = "PB";
                        break;
                    default:
                        FormatType = "Bytes";
                        break;
                }
                return "(" + string.Format("{0:0.00}", length) + " " + FormatType + ")";
            }
            else
            {
                return "(" + string.Format("{0:0.00}", length) + " Bytes)";
            }
        }

    }


  
}
