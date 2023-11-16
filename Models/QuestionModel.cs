#pragma warning disable CS8618
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Testem.Models;
public class Question
{
    [Key]
    public int QuestionId { get; set; }
    public int TestId {get; set;}
    [DisplayName("New Question")]
    public string QuestionPhrase { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}