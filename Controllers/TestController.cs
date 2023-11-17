using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Testem.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Testem.Controllers;

// [SessionCheck]
public class TestController : Controller
{
    private readonly ILogger<TestController> _logger;
    private MyContext _context;
    public TestController(ILogger<TestController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }


    // // Main landing page
    // [HttpGet("/tests")]
    // public IActionResult Tests()
    // {
    //     List<Test> Tests = _context.Tests.OrderByDescending(c => c.TestId).Include(c => c.Likes).Include(c => c.User).ToList();
    //     return View(Tests);
    // }

    // [HttpPost("/tests/search")]
    // public IActionResult TestsSearch(string mediumUsed)
    // {

    //     List<Test> Tests = _context.Tests.Where(p => p.Medium == mediumUsed).OrderByDescending(c => c.TestId).Include(c => c.Likes).Include(c => c.User).ToList();
    //     return View("Tests");
    // }

    // Form for making a new test
    [HttpGet("/tests/{groupId}/new/")]
    public IActionResult NewTest(int groupId)
    {
        ViewBag.groupId = groupId;
        return View();
    }

    // Form for making a new test
    [HttpGet("/tests/{testId}/edit/")]
    public IActionResult EditTest(int testId)
    {
        // List<Test> Tests = _context.Tests.Include(t => t.Questions).Where(t => t.GroupId == groupId).ToList();
        Test editTest = _context.Tests.Include(t => t.Questions).FirstOrDefault(t => t.TestId == testId);
        Group? Group = _context.Groups.Include(g => g.AllMembers).ThenInclude(m => m.User).FirstOrDefault(g => g.GroupId == editTest.GroupId);
        List<MemberTest> MemberTests = _context.MemberTests.ToList();
        MyViewModel MyModels = new MyViewModel
        {
            Group = Group,
            MemberTests = MemberTests,
            // Tests = Tests,
            Question = new Question(),
            Test = editTest
        }; 
        return View(MyModels);
    }

    // Add a question
    [HttpPost("/tests/{testId}/addquestion")]
    public IActionResult AddQuestion(int testId, Question newQuestion)
    {
        // Test editTest = _context.Tests.FirstOrDefault(t => t.TestId == testId);
        newQuestion.TestId = testId;
        _context.Add(newQuestion);
        _context.SaveChanges();
        return Redirect($"/tests/{testId}/edit");
    }

  
    // Edit Question
    [HttpGet("/tests/{testId}/editquestion/{questionId}")]
    public IActionResult EditQuestion(int testId, int questionId, Question editQuestion)
    {
        Test editTest = _context.Tests.Include(t => t.Questions).FirstOrDefault(t => t.TestId == testId);
        ViewBag.currentTest = editTest;
        ViewBag.questionId = questionId;
        return View();
    }
    // Update Question
    [HttpPost("/tests/{testId}/updatequestion/{questionId}")]
    public IActionResult UpdateQuestion(int testId, int questionId, Question editQuestion)
    {
        Question? OldQuestion = _context.Questions.FirstOrDefault(i => i.QuestionId == questionId);
        // need validation here to check if able to edit question
        if (OldQuestion != null && ModelState.IsValid)
        {
            OldQuestion.QuestionPhrase = editQuestion.QuestionPhrase;
            OldQuestion.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
        }
        return Redirect($"/tests/{testId}/edit");   
    }

    // Create Test Route
    [HttpPost("/tests/{groupId}/create")]
    public IActionResult CreateTest(Test newTest, int groupId)
    {
        if (newTest.Name != null)
        {
            newTest.GroupId = groupId;
            _context.Add(newTest);
            _context.SaveChanges();
            return Redirect($"/tests/{newTest.TestId}/edit");
        }
        else
        {
            return View("NewTest");
        }
    }

    [HttpGet("/tests/{testId}/assign/{memberId}")]
    public IActionResult AssignTest(int testId, int memberId)
    {
        // Need validation to check if memeber already assigned test
        MemberTest newMemberTest = new MemberTest();
        newMemberTest.MemberId = memberId;
        newMemberTest.TestId = testId;
        newMemberTest.Passed = false;
        newMemberTest.CorrectAnswers = 0;
        _context.Add(newMemberTest);
        _context.SaveChanges();
        return Redirect($"/tests/{testId}/edit");
    }
    // Unassign Test (Deletes it)
    [HttpGet("/tests/{testId}/unassign/{memberId}")]
    public IActionResult UnassignTest(int testId, int memberId)
    {
        // Need validation to check if memeber already assigned test and have right to delete

        MemberTest oldMemberTest = _context.MemberTests.FirstOrDefault(t => t.TestId == testId && t.MemberId == memberId);
        if (oldMemberTest != null)
        {
            _context.Remove(oldMemberTest);
            _context.SaveChanges();
        }
        return Redirect($"/tests/{testId}/edit");
    }

    // [HttpGet("/tests/{testId}/edit")]
    // public IActionResult EditTest(int testId)
    // {
    //     Test? test = _context.Tests.Include(p => p.Likes).FirstOrDefault(d => d.TestId == testId);
    //     if (test.UserId != HttpContext.Session.GetInt32("UserId"))
    //     {
    //         HttpContext.Session.Clear();
    //         return RedirectToAction("Index", "User");
    //     }
    //     return View("EditTest", test);
    // }

    

    // Read One Test Route
    [HttpGet("/tests/{testId}")]
    public IActionResult ShowTest(int testId)
    {
        Test? Tests = _context.Tests.FirstOrDefault(g => g.TestId == testId);
        return View(Tests);
    }

    // //Likes a test
    // [HttpPost("/tests/{testId}/like")]
    // public IActionResult LikeTest(int testId, string nextRoute)
    // {
    //     int userId = (int)HttpContext.Session.GetInt32("UserId");
    //     Like? OneLike = _context.Likes.FirstOrDefault(p => p.TestId == testId && p.UserId == userId);
    //     if (OneLike == null)
    //     {
    //         Like newLike = new Like();
    //         newLike.TestId = testId;
    //         newLike.UserId = (int)HttpContext.Session.GetInt32("UserId");
    //         _context.Add(newLike);
    //         _context.SaveChanges();
    //     }
    //     else
    //     {
    //         _context.Remove(OneLike);
    //         _context.SaveChanges();
    //     }
    //     if (nextRoute == "test")
    //     {
    //         return Redirect($"/tests/{testId}");
    //     }
    //     else
    //     {
    //         return RedirectToAction("Tests");
    //     }
    // } 


    // // Delete Test
    // [HttpGet("/tests/{testId}/destroy")]
    // public IActionResult DestroyTest(int TestId)
    // {
    //     Test? TestToDestroy = _context.Tests.SingleOrDefault(i => i.TestId == TestId);
    //     if (TestToDestroy.UserId == HttpContext.Session.GetInt32("UserId"))
    //     {
    //         _context.Tests.Remove(TestToDestroy);
    //         _context.SaveChanges();
    //     }
    //     return RedirectToAction("Tests");
    // }


}


