﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace RazorCms.Data.Models
{
    [Table("Company")]
    public partial class Company
    {
        [Column("CompanyID")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Please enter the Company Name")]
        [StringLength(50)]
        [Display(Name ="Company Name")]
        public string CompanyName { get; set; }

        [StringLength(255)]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [StringLength(50)]
        [Display(Name = "Town/City")]
        public string CityAddress { get; set; }

        [StringLength(50)]
        [Display(Name = "Province/State")]
        public string ProvinceState { get; set; }

        [StringLength(20)]
        [Display(Name = "Zip/Postal Code")]
        public string ZipCode { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(50)]
        [Display(Name = "Phone No.")]
        public string PhoneNo { get; set; }

        [StringLength(50)]
        [Display(Name = "Fax No.")]
        public string FaxNo { get; set; }

        [StringLength(50)]
        public string Website { get; set; }

        public ICollection<Contact> Contacts { get; set; }

    }
}