using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorCms.Data;
using RazorCms.Data.Models;

namespace RazorCMS
{
    public class ContactIndexModel : PageModel
    {
        private readonly RazorCmsContext _context;

        public ContactIndexModel(RazorCmsContext context)
        {
            _context = context;
        }

        public IList<Contact> Contact { get;set; }

        public async Task OnGetAsync()
        {
            Contact = await _context.Contacts
                .Include(c => c.Company)
                .ToListAsync();
        }
    }
}
