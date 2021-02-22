using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Models;

namespace SalesWebMvc.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            List<Department> list = new List< Department > ();
            list.Add(new Department { Id = 1, Nome = "Eletronics" });
            list.Add(new Department { Id = 2, Nome = "Fashion" });

            return View(list);
        }
    }
}
