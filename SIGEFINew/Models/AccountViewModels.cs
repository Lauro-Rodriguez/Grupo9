using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SIGEFINew.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Código")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "¿Recordar este explorador?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Usuario")]
        //[EmailAddress]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Empresa")]
        //[EmailAddress]
        public int Empresa { get; set; }

        [Required]
        [Display(Name = "Periodo")]
        //[EmailAddress]
        public int Periodo { get; set; }

        public int ID_EMPLEADO { get; set; }

        [Display(Name = "¿Recordar cuenta?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        public string id { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 10)]
        [Display(Name = "Identficación")]
        public string NumIdentif { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 10)]
        [Display(Name = "Nombre Completo")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Usuario")]
        public string Login { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(2, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 2)]
        [Display(Name = "Tipo")]
        public string TypeUser { get; set; }

        [Required]
        [Display(Name = "Activo")]
        public bool Active { get; set; }

        [Display(Name = "Empleado")]
        public int ID_EMPLEADO { get; set; }
    }

    public class EditViewModel
    {
        public string id { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 10)]
        [Display(Name = "Identficación")]
        public string NumIdentif { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 10)]
        [Display(Name = "Nombre Completo")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Required]
        [StringLength(2, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 2)]
        [Display(Name = "Tipo")]
        public string TypeUser { get; set; }

        [Required]
        [Display(Name = "Activo")]
        public bool Active { get; set; }

        [Display(Name = "Empleado")]
        public int ID_EMPLEADO { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }
    }
}
