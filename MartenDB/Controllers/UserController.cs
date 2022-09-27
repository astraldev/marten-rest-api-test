
using MartenDB.Models;
using Microsoft.AspNetCore.Mvc;
using Marten;


namespace MartenDB.Controllers.UserController {
  public class UserDocumentController : Controller {

    [Route("/api/users/create")]
    [HttpPost] 
    public IActionResult CreateUser([FromBody] UserDetailsModel userDetails, IDocumentSession _session ) {
      if((userDetails.Name.Length > 0) && (userDetails.Age > 16)){
        _session.Store<UserDetailsModel>(userDetails);
        _session.SaveChangesAsync();
        return Ok(
          $"Here is your identifier: {userDetails.Id}"
        );
      }else{
        return BadRequest("Requires name to be greater than 0 and age to be greater than 16");
      }
    }

    [Route("/api/users/get{id}")]
    [HttpGet]
    public async Task<IActionResult> GetUser(string id, IQuerySession _query) {
      return Ok(await _query.LoadAsync<UserDetailsModel>(id));
    }

    [Route("/admin/api/users/list")]
    [HttpGet]
    public IActionResult ListUsers(IQuerySession _session){
      return Ok(_session.Query<UserDetailsModel>().ToList());
    }
  }

  public class UserEventsController : Controller {}

}