using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorCms.Data;
using RazorCms.Data.Models;

namespace RazorCMS
{
    public class ContactDetailsModel : PageModel
    {
        private readonly RazorCms.Data.RazorCmsContext _context;

        public ContactDetailsModel(RazorCms.Data.RazorCmsContext context)
        {
            _context = context;
        }

        public Contact Contact { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Contact = await _context.Contacts
                .Include(c => c.Company).FirstOrDefaultAsync(m => m.ContactId == id);

            if (Contact == null)
            {
                return NotFound();
            }

            return Page();
        }

        public string ContactCardHtml
        {
            get
            {
                string card = "";
                Contact contact;
                Company company;

                contact = this.Contact;
                company = this.Contact.Company;

                if (!string.IsNullOrEmpty(contact.WorkTitle))
                {
                    card += contact.WorkTitle + "<br />" + Environment.NewLine;
                }

                if (!string.IsNullOrEmpty(contact.Company.CompanyName))
                {
                    card += contact.Company.CompanyName + "<br />" + Environment.NewLine;
                }

                if (!string.IsNullOrEmpty(contact.Email))
                {
                    card += "Email: " + contact.Email + "<br />" + Environment.NewLine;
                }

                if (!string.IsNullOrEmpty(contact.WorkPhone))
                {
                    card += "Office: " + contact.WorkPhone + "<br />" + Environment.NewLine;
                }
                else
                {
                    if (!string.IsNullOrEmpty(contact.Company.PhoneNo))
                    {
                        card += "Office: " + contact.Company.PhoneNo + "<br />" + Environment.NewLine;    
                    }
                }

                if (!string.IsNullOrEmpty(contact.MobilePhone))
                {
                    card += "Mobile: " + contact.MobilePhone + "<br />" + Environment.NewLine;
                }

                if (!string.IsNullOrEmpty(contact.HomePhone))
                {
                    card += "Home: " + contact.HomePhone + "<br />" + Environment.NewLine;
                }

                return card;
            }
}
    }
}
