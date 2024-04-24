using Microsoft.AspNetCore.Mvc;
using NawareKulCore.DataBL;
using NawareKulCore.Models;

namespace NawareKulCore.Controllers
{
    public class HomeController : CustomController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View(new AboutKulvruttant());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult MainDashboard(string Language)
        {
            if (string.IsNullOrEmpty(LanguageMang.setLanguage) || Language != null)
                new LanguageMang().SetLanguage(Language, HttpContext);
            return View();
        }

        public ActionResult Prastavna()
        {
            return View();
        }

        public ActionResult Dhopeshwar()
        {
            return View();
        }

        public ActionResult FamousPeoples()
        {
            return View(new FamousPeopleViewModel());
        }

        public ActionResult BharadwajHrushi()
        {
            return View();
        }

        public ActionResult FamilyTree()
        {
            return View();
        }

        public ActionResult OnlineForm()
        {
            return View();
        }

        [HttpPost("OnlineForm")]
        public ActionResult SubmitOnlineForm(PeopleDTO people)
        {
            return View();
        }

        public ActionResult NawarePeoples()
        {
            var data = Service.Get_ListOfNawarePeople();
            return View(data.Data);
        }


        public ActionResult Edit(int id)
        {
            var data = Service.Get_UserById(id);

            return View(data.Data);
        }
    }
}