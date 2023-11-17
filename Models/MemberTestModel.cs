#pragma warning disable CS8618
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Testem.Models;
public class MemberTest
{
    [Key]
    public int MemberTestId { get; set; }
    public int MemberId {get; set;}
    public int TestId {get; set;}
    public bool Completed {get; set;}
    public bool Passed {get; set;}
    public int CorrectAnswers {get; set;}
    // public Question Question {get; set;}
    public List<Question> Questions {get; set;}
    // public List<Answer> Answer {get; set;}
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}