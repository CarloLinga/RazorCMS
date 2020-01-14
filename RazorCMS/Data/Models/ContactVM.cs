using RazorCms.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorCMS.Data.Models
{
    public class ContactVM
    {
        public Contact Contact { get; set; }
        public string ContactCompany { get; set; }
    }
}
