using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorCms.Data;
using RazorCms.Data.Models;

namespace RazorCMS
{
    public class ContactEditModel : PageModel
    {
        private readonly RazorCmsContext _context;

        public ContactEditModel(RazorCmsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string PreviousPage { get; set; }

        [BindProperty]
        public Contact Contact { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            System.Diagnostics.Trace.WriteLine(this.PreviousPage);

            if (id == null)
            {
                return NotFound();
            }

            // Save the Previous Page Url
            string prevPage = Request.Headers["Referer"].ToString();
            HttpContext.Session.SetString("PreviousUrl", prevPage);

            Contact = await _context.Contacts
                .Include(c => c.Company).FirstOrDefaultAsync(m => m.ContactId == id);

            if (Contact == null)
            {
                return NotFound();
            }

            ViewData["CompanyId"] = new SelectList(
               _context.Companies.OrderBy(n => n.CompanyName), "CompanyId", "CompanyName");

            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Contact).State = EntityState.Modified;

            // System.Diagnostics.Trace.WriteLine(Request.Headers["Referer"].ToString());

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(Contact.ContactId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Get the Previous Page Url
            string prevPage = HttpContext.Session.GetString("PreviousUrl");

            if (!String.IsNullOrEmpty(prevPage))
            {
                System.Diagnostics.Trace.WriteLine(this.PreviousPage);
                return Redirect(prevPage);
            }
            else
            {
                return RedirectToPage("./Index");
            }
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.ContactId == id);
        }
    }
}
