using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using to_do_list2.Models;
using System.Text;
namespace to_do_list2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        /*public IActionResult Adder()
        {
            return View();
        }*/
        [HttpPost]
        public IActionResult Adder(string AddInput)
        {
            if (string.IsNullOrEmpty(AddInput))
            {
                return RedirectToAction("Index");
            }
            else
            {
                string text = @"Files/FileInfo.txt";
                int info = Convert.ToInt32(System.IO.File.ReadAllText(text));
                string newFile = @"Files/";
                info++;
                newFile += info;
                newFile += "0";
                newFile += ".txt";
                System.IO.File.WriteAllText(newFile, AddInput);
                System.IO.File.WriteAllText(text, Convert.ToString(info));
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult ReTasker(int id, int check,string RetInput)
        {
            if (string.IsNullOrEmpty(RetInput))
            {
                return RedirectToAction("Index");
            }
            else
            {
                string newFile = @"Files/"+id+check + ".txt";
                System.IO.File.WriteAllText(newFile, RetInput);
                return RedirectToAction("Index");
            }
            
        }
        public IActionResult ReChecker(int id, int check)
        {
            string file = @"Files/" + id + check + ".txt";
            string text = System.IO.File.ReadAllText(file);
            if (check == 0)
            {
                check = 1;
            }
            else
            {
                check = 0;
            }
            System.IO.File.Delete(file);
            file = @"Files/" + id + check + ".txt";
            System.IO.File.WriteAllText(file, text);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DelTask(int id)
        {
                string text = @"Files/FileInfo.txt";
                int info = Convert.ToInt32(System.IO.File.ReadAllText(text));
            for(int i = id+1; i <= info; i++)
            {
                string file = @"Files/" + i + 0 + ".txt";
                int ch = 0;
                if (System.IO.File.Exists(file) == false)
                {
                    file = @"Files/" + i + 1 + ".txt";
                    ch = 1;
                }
                string task1 = System.IO.File.ReadAllText(file);
                file = @"Files/" + (i-1) +ch+ ".txt";
                System.IO.File.WriteAllText(file, task1);
                if (i == info)
                {
                    System.IO.File.Delete(@"Files/" + info + ch + ".txt");
                }
            }
                info--;
                System.IO.File.WriteAllText(text, Convert.ToString(info));
            return RedirectToAction("Index");
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
