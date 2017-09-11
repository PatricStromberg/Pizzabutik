using System.ComponentModel.DataAnnotations;

namespace NostalgiPizza.Models
{
    public class LoginOrRegisterViewModel
    {
        public LogInViewModel LogInViewModel { get; set; }
        public RegisterViewModel RegisterViewModel { get; set; }
    }
}
