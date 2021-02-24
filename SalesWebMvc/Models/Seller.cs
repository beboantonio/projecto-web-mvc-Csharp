using System;
using System.Collections.Generic;
using System.Linq;
namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public long BaseSlary { get; set; }

        public int DepartmentId { get; set; } // Obriga ao framework garantir que esse campo precisa de um valor que depende do "Department" e não deve ser NULL
        // Associações
        public Department Department { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller() { }

        public Seller(int id, string name, string email, DateTime birthDate, long baseSlary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSlary = baseSlary;
            Department = department;
        }

        public void RemoveSales(SalesRecord sr) 
        {
            Sales.Remove(sr);
        }

        public void AddSales(SalesRecord sr) 
        {
            Sales.Add(sr);
        }
        public double TotalSales(DateTime initial, DateTime final) 
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
