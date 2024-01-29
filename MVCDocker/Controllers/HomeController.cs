using Microsoft.AspNetCore.Mvc;

using MVCDocker.Models;
using NuGet.Protocol.Plugins;
using System.Diagnostics;
using System.IO;

namespace MVCDocker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PanditClinicContext _panditClinicContext;
        private readonly IConfiguration _iConfig;

        public HomeController(ILogger<HomeController> logger, PanditClinicContext panditClinicContext, IConfiguration iConfig)
        {
            _logger = logger;
            _panditClinicContext = panditClinicContext;
            _iConfig = iConfig;
        }

        //crud get and update operation 
        public IActionResult All()
        {
            List<UserMaster> userMaster = new();
            userMaster = _panditClinicContext.UserMaster.ToList();

            var s = userMaster.FirstOrDefault();
            ViewBag.Country = s;

            userMaster = userMaster.Select(user =>
            {
                user.UserName = "Dipak";
                return user;
            }).ToList();

            _panditClinicContext.SaveChanges();

            return View(userMaster);

            
        }


        public IActionResult ItemALL()
        {
            List<ItemMaster> ItemMaster = new();
            ItemMaster = _panditClinicContext.ItemMaster.ToList();
            return View(ItemMaster);
        }

        //file operations
        public IActionResult Index()
        {
            //access docker volume from local machine ,change yaml file local path
            string folderPath = "/app/data";
            string[] fileNames = Directory.GetFiles(folderPath);

            var s = "";
            foreach (string fileName in fileNames)
            {
                Console.WriteLine("File name: " + Path.GetFileName(fileName));
                s += "  File name: " + Path.GetFileName(fileName);
            }
            ViewBag.File = s;
            //added new file 
            string newFileName = "newfile.txt";
            string content = "This is the content of the new text file.";

            // Write the content to the new text file in the folder location
            System.IO.File.WriteAllText(Path.Combine(folderPath, newFileName), content);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}