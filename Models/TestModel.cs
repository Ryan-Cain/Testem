#pragma warning disable CS8618
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Testem.Models;
public class Test
{
    [Key]
    public int TestId { get; set; }
    [Required(ErrorMessage ="Title is Required")]
    public int GroupId {get; set;}
    public string Name { get; set; }
    // public Question Question {get; set;}
    public List<Question> Questions {get; set;}
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}