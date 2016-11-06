using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FKHY.Models.CustomerModels
{
    public class UserRegisterViewModel
    {
        public string Phone { get; set; }

        public string Password { get; set; }

        public string Code { get; set; }

        public bool Rem { get; set; }
    }
}
