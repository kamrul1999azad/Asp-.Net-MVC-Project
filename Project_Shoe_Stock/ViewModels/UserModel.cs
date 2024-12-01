using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_Shoe_Stock.ViewModels
{
    public class LoginModel
    {
        [Required, StringLength(20)]
        public string UserName { get; set; }
        [Required, StringLength(20), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}