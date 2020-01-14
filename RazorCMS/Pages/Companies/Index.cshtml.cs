using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorCms.Data;
using RazorCms.Data.Models;

namespace RazorCMS
{
    public class CompanyIndexModel : PageModel
    {
        private readonly RazorCms.Data.RazorCmsContext _context;

        public CompanyIndexModel(RazorCms.Data.RazorCmsContext context)
        {
            _context = context;
        }

        public IList<Company> Company { get;set; }

        public async Task OnGetAsync()
        {
            Company = await _context.Companies.ToListAsync();
        }
    }
}
