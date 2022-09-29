using MartenDB.Models;

namespace MartenDB.Controllers.UserController {
  public class UsersUpdated {
    public Guid ID {get;set;}
    public List<string> Users {get;set;} = new();
    
    public void Apply(UserDetailsModel user){
      Users.Add(user.Name);
    }
  }
}