#pragma warning disable CS8618
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Testem.Models;
public class Group
{
    [Key]
    public int GroupId { get; set; }

    [Required(ErrorMessage ="Title is Required")]
    public string Name { get; set; } 
    // [Required(ErrorMessage ="Medium is Required")]
    public string UniqueCode { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public List<Member> AllMembers {get;set;} = new();
}