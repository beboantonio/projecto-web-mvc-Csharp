using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context;

        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        // Fazendo um método "assíncrono" que faz uma execução no banco de dados de forma "assincrona"
        public async Task<List<Department>> FindAllAsync() 
        {
            return await _context.Department.OrderBy(x => x.Nome).ToListAsync();
        }
    }
}
