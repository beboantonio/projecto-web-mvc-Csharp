using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService) 
        {
            _sellerService = sellerService;
        }
        public IActionResult Index() // Chamando o controlador - C
        {
            var list = _sellerService.FindAll(); // O controlador chama o Service do (Models) - M
            return View(list); // E encaminha os dados para uma View - V
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost] // Indicando que é uma ação de POST
        [ValidateAntiForgeryToken] // Para prevenir que a minha aplicação sofra ataque CSCRS: Prevenir que alguém aproveite minha autenticaçõ e envie dados maliciosos 
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            // return RedirectToAction("Index"); Pode ser escrito de outra forma, como mostra abaixo
            return RedirectToAction(nameof(Index)); // Redireciona para o Index
        }
    }
}
