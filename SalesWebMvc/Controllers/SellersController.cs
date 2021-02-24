using Microsoft.AspNetCore.Mvc;
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
    }
}
