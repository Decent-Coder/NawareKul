using System.Globalization;
using System.Net;

namespace NawareKulCore.Models
{
    public class LanguageMang
    {
        public static List<Languages> AvailableLanguages = new List<Languages> {
            new Languages {
                LanguageFullName = "English", LanguageCultureName = "en"
            },
            new Languages {
                LanguageFullName = "Marathi", LanguageCultureName = "Mr"
            },

        };
        public static bool IsLanguageAvailable(string lang)
        {
            return AvailableLanguages.Where(a => a.LanguageCultureName.Equals(lang)).FirstOrDefault() != null ? true : false;
        }
        public static string GetDefaultLanguage()
        {
            return !string.IsNullOrEmpty(setLanguage) ? setLanguage : AvailableLanguages[0].LanguageCultureName;
        }
        public void SetLanguage(string lang, HttpContext context)
        {
            try
            {
                setLanguage = lang;
                if (!IsLanguageAvailable(lang)) lang = GetDefaultLanguage();
                var cultureInfo = new CultureInfo(lang);
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
                var langCookie = new Cookie("culture", lang);
                langCookie.Expires = DateTime.Now.AddYears(1);
                context.Response.Cookies.Append("culture", lang, new CookieOptions { Expires = DateTime.Now.AddYears(1) });
            }
            catch (Exception) { }
        }

        public static string setLanguage { get; set; }
    }
    public class Languages
    {
        public string LanguageFullName
        {
            get;
            set;
        }
        public string LanguageCultureName
        {
            get;
            set;
        }
    }
}