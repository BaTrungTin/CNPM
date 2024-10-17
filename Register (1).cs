using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOI.Core.Models
{
    public class Register
    {
        public string Name { get; set; }
         public string Password  { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string Phonenumber  { get; set; }
            

    }
}
