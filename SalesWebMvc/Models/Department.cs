using System;
using System.Collections.Generic;
using System.Linq;
namespace SalesWebMvc.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string  Nome { get; set; }

        // Associação de objectos: muito para 1
        public ICollection<Seller> Seller { get; set; } = new List<Seller>();

        public Department() { }

        public Department(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
        public void AddSeller(Seller seller) 
        {
            Seller.Add(seller);
        }

        public double TotalSales(DateTime initial, DateTime final) 
        {
            return Seller.Sum(seller => seller.TotalSales(initial, final));
        }
    }
}
