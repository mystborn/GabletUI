using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabletUI.Services.Validation
{
    public interface IValidationService
    {
        public bool IsValidUsername(string username, out List<string> errors);
        public bool IsValidEmail(string email, out List<string> errors);
        public bool IsValidPassword(string password, out List<string> errors);
    }
}
