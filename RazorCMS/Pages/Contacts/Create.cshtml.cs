using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorCms.Data;
using RazorCms.Data.Models;

namespace RazorCMS
{
    public class ContactCreateModel : PageModel
    {
        private readonly RazorCmsContext _context;

        public ContactCreateModel(RazorCmsContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies
                .OrderBy(b => b.CompanyName), "CompanyId", "CompanyName");

            // Save the Previous Page Url
            string prevPage = Request.Headers["Referer"].ToString();
            HttpContext.Session.SetString("PreviousUrl", prevPage);

            return Page();
        }

        [BindProperty]
        public Contact Contact { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Contacts.Add(Contact);
            await _context.SaveChangesAsync();

            // Get the Previous Page Url
            string prevPage = HttpContext.Session.GetString("PreviousUrl");

            if (!String.IsNullOrEmpty(prevPage))
            {
                return Redirect(prevPage);
            }
            else
            {
                return RedirectToPage("./Index");
            }
        }
    }
}
