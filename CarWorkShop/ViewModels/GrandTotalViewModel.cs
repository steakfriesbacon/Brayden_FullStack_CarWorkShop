using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarWorkShop.ViewModels
{
    public class GrandTotalViewModel
    {

        [Key]
        public decimal GrandTotal { get; set; }
    }
}
