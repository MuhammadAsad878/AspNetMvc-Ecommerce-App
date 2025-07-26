using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Models.ViewModels
{
    public class ProductVM
    {

        
        public Product Product { get; set; } = new Product();

        [ValidateNever]
        public IEnumerable<SelectListItem> catListItems { get; set; }

    }
}
