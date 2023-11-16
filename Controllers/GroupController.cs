using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Testem.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Serialization;

using System;
using System.Linq;


namespace Testem.Controllers;

// [SessionCheck]
public class GroupController : Controller
{
    private readonly ILogger<GroupController> _logger;
    private MyContext _context;
    public GroupController(ILogger<GroupController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }


    // // Main landing page
    // [HttpGet("/groups")]
    // public IActionResult Groups()
    // {
    //     List<Group> Groups = _context.Groups.OrderByDescending(c => c.GroupId).Include(c => c.Likes).Include(c => c.User).ToList();
    //     return View(Groups);
    // }

    // [HttpPost("/groups/search")]
    // public IActionResult GroupsSearch(string mediumUsed)
    // {

    //     List<Group> Groups = _context.Groups.Where(p => p.Medium == mediumUsed).OrderByDescending(c => c.GroupId).Include(c => c.Likes).Include(c => c.User).ToList();
    //     return View("Groups");
    // }

    // Form for making a new group
    [HttpGet("/groups/new")]
    public IActionResult NewGroup()
    {
        return View();
    }

    // Create Group Route
    [HttpPost("/groups/create")]
    public IActionResult CreateGroup(Group newGroup)
    {
        if (ModelState.IsValid)
        {
            // newGroup.UserId = (int)HttpContext.Session.GetInt32("UserId");
            newGroup.UniqueCode = RandoString.GenerateRandomString(6);
            _context.Add(newGroup);
            _context.SaveChanges();
            return RedirectToAction("Dashboard", "User");
        }
        else
        {
            return View("AddGroup");
        }
    }

    // [HttpGet("/groups/{groupId}/edit")]
    // public IActionResult EditGroup(int groupId)
    // {
    //     Group? group = _context.Groups.Include(p => p.Likes).FirstOrDefault(d => d.GroupId == groupId);
    //     if (group.UserId != HttpContext.Session.GetInt32("UserId"))
    //     {
    //         HttpContext.Session.Clear();
    //         return RedirectToAction("Index", "User");
    //     }
    //     return View("EditGroup", group);
    // }

    // [HttpPost("groups/{groupId}/update")]
    // public IActionResult UpdateGroup(Group newGroup, int groupId)
    // {
    //     Group? OldGroup = _context.Groups.FirstOrDefault(i => i.GroupId == groupId);
    //     if (OldGroup != null && ModelState.IsValid && OldGroup.UserId == HttpContext.Session.GetInt32("UserId"))
    //     {
    //         OldGroup.Title = newGroup.Title;
    //         OldGroup.Medium = newGroup.Medium;
    //         OldGroup.Image = newGroup.Image;
    //         OldGroup.ForSale = newGroup.ForSale;
    //         OldGroup.UpdatedAt = DateTime.Now;
    //         _context.SaveChanges();
    //         return RedirectToAction("Groups");
    //     }
    //     else
    //     {
    //         if (OldGroup != null && !ModelState.IsValid)
    //         {
    //             return View("EditGroup", OldGroup);
    //         }
    //         // It should be the old version so we can keep the ID
    //         return RedirectToAction("Groups");
    //     }
    // }

    // Read One Group Route
    [HttpGet("/groups/{groupId}")]
    public IActionResult ShowGroup(int groupId)
    {
        Group? Groups = _context.Groups.FirstOrDefault(g => g.GroupId == groupId);
        return View(Groups);
    }

    // //Likes a group
    // [HttpPost("/groups/{groupId}/like")]
    // public IActionResult LikeGroup(int groupId, string nextRoute)
    // {
    //     int userId = (int)HttpContext.Session.GetInt32("UserId");
    //     Like? OneLike = _context.Likes.FirstOrDefault(p => p.GroupId == groupId && p.UserId == userId);
    //     if (OneLike == null)
    //     {
    //         Like newLike = new Like();
    //         newLike.GroupId = groupId;
    //         newLike.UserId = (int)HttpContext.Session.GetInt32("UserId");
    //         _context.Add(newLike);
    //         _context.SaveChanges();
    //     }
    //     else
    //     {
    //         _context.Remove(OneLike);
    //         _context.SaveChanges();
    //     }
    //     if (nextRoute == "group")
    //     {
    //         return Redirect($"/groups/{groupId}");
    //     }
    //     else
    //     {
    //         return RedirectToAction("Groups");
    //     }
    // } 


    // // Delete Group
    // [HttpGet("/groups/{groupId}/destroy")]
    // public IActionResult DestroyGroup(int GroupId)
    // {
    //     Group? GroupToDestroy = _context.Groups.SingleOrDefault(i => i.GroupId == GroupId);
    //     if (GroupToDestroy.UserId == HttpContext.Session.GetInt32("UserId"))
    //     {
    //         _context.Groups.Remove(GroupToDestroy);
    //         _context.SaveChanges();
    //     }
    //     return RedirectToAction("Groups");
    // }


}



public class RandoString
{
    public static string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random random = new Random();

        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
