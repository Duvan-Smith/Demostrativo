using System.ComponentModel.DataAnnotations;

namespace IdentityServerHost.Quickstart.UI;

public class RegisterViewModel
{
    [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
    [Display(Name = "Nombre de usuario")]
    public string Username { get; set; } = default!;

    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress]
    [Display(Name = "Correo")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "La contraseña es obligatorio")]
    [StringLength(50, ErrorMessage = "El {0} debe terner logintud de {2}", MinimumLength = 5)]
    [DataType(DataType.Password)]
    [Display(Name = "Contraseña")]
    public string Password { get; set; } = default!;

    [Required(ErrorMessage = "La confirmacion de contraseña es obligatorio")]
    [Compare("Password", ErrorMessage = "Contraseñas diferentes")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirmar contraseña")]
    public string ConfirmPassword { get; set; } = default!;
}
