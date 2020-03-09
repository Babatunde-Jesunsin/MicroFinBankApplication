using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroFinBank.Models.ViewModel
{
    public class transferDetails
    {
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public string Password { get; set; }
        public double Amount { get; set; }
    }
}