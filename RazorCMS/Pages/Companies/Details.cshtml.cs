using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorCms.Data.Models;

namespace RazorCMS
{
    public class CompanyDetailsModel : PageModel
    {
        private readonly RazorCms.Data.RazorCmsContext _context;

        public CompanyDetailsModel(RazorCms.Data.RazorCmsContext context)
        {
            _context = context;
        }

        public Company Company { get; set; }
        
        public ICollection<Contact> Contacts { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Company = await _context.Companies
                .AsNoTracking()
                .Include(i => i.Contacts)
                .FirstOrDefaultAsync(c => c.CompanyId == id);

            if (Company == null)
            {
                return NotFound();
            }

            return Page();
        }

        public string CompanyCardHtml
        {
            get
            {
                string card;
                Company company;
                string[] city;
                string[] country;

                company = this.Company;
                city = new string[] { company.CityAddress, company.ProvinceState };
                country = new string[] { company.Country, company.ZipCode };

                card = "<address>" + Environment.NewLine;

                if (!String.IsNullOrEmpty(company.StreetAddress))
                {
                    card += company.StreetAddress + "<br />" + Environment.NewLine;
                }

                if (!String.IsNullOrEmpty(city.ToString()))
                {
                    card += String.Join(", ", city.Where(s => !String.IsNullOrEmpty(s)))
                        + "<br />" + Environment.NewLine;
                }

                if (!String.IsNullOrEmpty(country.ToString()))
                {
                    card += String.Join(", ", country.Where(s => !String.IsNullOrEmpty(s)))
                        + "<br />" + Environment.NewLine;
                }

                if (!String.IsNullOrEmpty(company.PhoneNo))
                {
                    card += "<i class=\"fas fa-phone\"></i>&nbsp;"
                        + company.PhoneNo + "<br />" + Environment.NewLine;
                }
                
                if (!String.IsNullOrEmpty(company.FaxNo))
                {
                    card += "<i class=\"fas fa-fax\"></i>&nbsp;"
                        + company.FaxNo + "<br />" + Environment.NewLine;
                }

                if (!String.IsNullOrEmpty(company.Website))
                {
                    card += "<a href=\"" + company.Website + "\" target= \"_blank\"" 
                        + ">" + company.Website + "</a><br />" + Environment.NewLine;
                }

                card += "</address>";

                return card;

            }
        }
    }
}
