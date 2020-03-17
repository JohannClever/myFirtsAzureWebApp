using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myFirtsAzureWebApp.Models
{
    public class Contact:EntityBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
