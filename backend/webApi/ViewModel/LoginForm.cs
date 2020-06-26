using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionApp.ViewModel
{
    public class LoginForm
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }


        public bool RememberMe = false;
    }
}
