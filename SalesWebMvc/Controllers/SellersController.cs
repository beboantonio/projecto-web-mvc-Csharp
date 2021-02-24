using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;
        public SellersController(SellerService sellerService, DepartmentService departmentService) 
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }
        public IActionResult Index() // Chamando o controlador - C
        {
            var list = _sellerService.FindAll(); // O controlador chama o Service do (Models) - M
            return View(list); // E encaminha os dados para uma View - V
        }

        public IActionResult Create() 
        {
            var department = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = department };
            return View(viewModel);
        }

        [HttpPost] // Indicando que é uma ação de POST
        [ValidateAntiForgeryToken] // Para prevenir que a minha aplicação sofra ataque CSCRS: Prevenir que alguém aproveite minha autenticaçõ e envie dados maliciosos 
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            // return RedirectToAction("Index"); Pode ser escrito de outra forma, como mostra abaixo
            return RedirectToAction(nameof(Index)); // Redireciona para o Index
        }

        public IActionResult Delete(int? id) 
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided"});
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null) 
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }

        [HttpPost] // Indicando que é uma ação de POST
        [ValidateAntiForgeryToken] // Para prevenir que a minha aplicação sofra ataque CSCRS: Prevenir que alguém aproveite minha autenticaçõ e envie dados maliciosos 
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id) 
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }

        public IActionResult Edit(int? id) 
        {
            if (id == null) { return RedirectToAction(nameof(Error), new { message = "Id not provided" }); }

            var obj = _sellerService.FindById(id.Value);
            if(obj == null) { return RedirectToAction(nameof(Error), new { message = "Id not found" }); }
            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost] // Indicando que é uma ação de POST
        [ValidateAntiForgeryToken] // Para prevenir que a minha aplicação sofra ataque CSCRS: Prevenir que alguém aproveite minha autenticaçõ e envie dados maliciosos 
        public IActionResult Edit(int id, Seller seller) 
        {
            if (id != seller.Id) 
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e) 
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message) 
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}
