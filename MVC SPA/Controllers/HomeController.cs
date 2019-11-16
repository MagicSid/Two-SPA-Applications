using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Angular_SPA.Models;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;

namespace Angular_SPA.Controllers
{
    public class HomeController : Controller
    {
        public class MethodData
        {
            public string id { get; set; }
            public string increment { get; set; }
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult TableView()
        {
            TestPeopleCollection testPeople = new TestPeopleCollection();
            return View(testPeople);
        }

        [HttpPost]
        public void CallIncrement(MethodData stuff)
        {
            TestPeopleModel testPersonToIncrement = new TestPeopleModel(int.Parse(stuff.id), "temp", int.Parse(stuff.increment));
            testPersonToIncrement.UpdateIncrement();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }

  
}
