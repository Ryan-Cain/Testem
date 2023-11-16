#pragma warning disable CS8618
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Testem.Models;
public class Member
{
    [Key]
    public int MemberId { get; set; }

    public int? UserId { get; set; }

    public int GroupId { get; set; }

    public bool IsAdmin { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    // Nav Props
    public Group Group { get; set; }
    public User User { get; set; }

}