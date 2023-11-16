#pragma warning disable CS8618
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Testem.Models;
public class UserLogin
{
    [Required(ErrorMessage ="Must enter an email")]
    [EmailAddress]
    [DisplayName("Email")]
    public string LoginEmail { get; set; }

    [Required(ErrorMessage ="Must enter a password")]
    [DisplayName("Password")]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    public string LoginPassword { get; set; }
}