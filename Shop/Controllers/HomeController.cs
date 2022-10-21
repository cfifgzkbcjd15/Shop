using Microsoft.AspNetCore.Mvc;
using Shop.Code;
using Shop.Models;
using ShopBase.Data;
using System.Diagnostics;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private IDownload download;

        public HomeController(
            IDownload _download
            )
        {
            download = _download;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Download()
        {
            return View();
        }
        [HttpPost]
        [DisableRequestSizeLimit]
        public IActionResult Download(IFormFile uploadedFile)
        {
            try
            {
                if (uploadedFile != null)
                {
                    using (var stream = uploadedFile.OpenReadStream())
                    {
                        download.ImportSte(stream);
                    }
                }

                return RedirectToAction("Index");
            }
            catch{
                return Content("Ошибка");
            }
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}