#pragma warning disable CS8618

namespace Testem.Models;

public class MyViewModel
{
    public User User {get;set;}
    public List<User> AllUsers {get;set;}

    public Group Group {get;set;}
    public List<Group> AllGroups {get;set;}

}