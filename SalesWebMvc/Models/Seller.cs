using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")] // {0} -> Nome; {1} -> 60; {2} -> 3
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Birth Date")] // Customizar a forma como a propriedade "BirthDate" irá aparecer no "View"
        [DataType(DataType.Date)] // Customizar o formato de data da propriedade "BirthDate" irá aparecer no "View"
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        [Display(Name = "Base Salary")] // Customizar a forma como a propriedade "BaseSlary" irá aparecer no "View"
        [DisplayFormat(DataFormatString = "{0:F2}")] // Customizar o formato dos números para a propriedade "BaseSlary" irá aparecer no "View"
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
