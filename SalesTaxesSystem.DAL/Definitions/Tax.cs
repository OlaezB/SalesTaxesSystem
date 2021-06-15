using System;
using System.Collections.Generic;
using System.Text;

namespace SalesTaxesSystem.DAL.Definitions
{
    public class Tax
    {
        public float Value { get; set; }
        public ICollection<string> Classes { get; set; }
    }
}
