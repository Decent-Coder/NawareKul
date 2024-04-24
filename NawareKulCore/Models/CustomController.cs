using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NawareKulCore.Models;

public class CustomController : Controller
{
    public CustomController()
    {
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var lang = string.Empty;
        var langCookie = Request.Cookies["culture"];
        if (langCookie != null)
        {
            lang = langCookie;
        }
        else
        {
            var acceptLanguageHeader = Request.Headers["Accept-Language"];

            // Check if the Accept-Language header is present and not empty
            if (!string.IsNullOrWhiteSpace(acceptLanguageHeader))
            {
                // Split the header value into individual language tags
                var languageTagsWithQuality = acceptLanguageHeader.ToString().Split(',');

                // Extract the first language tag
                var firstLanguageTag = languageTagsWithQuality.FirstOrDefault()?.Split(';')[0].Trim();

                // Use the first language tag as the culture
                if (!string.IsNullOrWhiteSpace(firstLanguageTag))
                {
                    lang = firstLanguageTag;
                }
            }

            // If no language tag was found, or if the header is not present, fall back to default language
            if (string.IsNullOrWhiteSpace(lang))
            {
                lang = LanguageMang.GetDefaultLanguage(); // Assuming LanguageMang.GetDefaultLanguage() returns a default language code
            }
        }

        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(lang)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
        );
        new LanguageMang().SetLanguage(lang, HttpContext);
        base.OnActionExecuting(context);
    }
}
