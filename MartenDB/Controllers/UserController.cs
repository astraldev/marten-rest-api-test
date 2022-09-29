
using MartenDB.Models;
using Microsoft.AspNetCore.Mvc;
using Marten;


namespace MartenDB.Controllers.UserController {
  public class UserDocumentController : Controller {

    [Route("/api/events/list{id}")]
    [HttpGet]
    public IActionResult GetAllEvents(Guid id, IQuerySession _query) {
      return Ok(_query.Events.AggregateStream<UsersUpdated>(id));
    }

    [Route("/api/users/create")]
    [HttpPost] 
    public IActionResult CreateUser([FromBody] UserDetailsModel userDetails, IDocumentSession _session ) {
      if((userDetails.Name.Length > 0) && (userDetails.Age > 16)){
        _session.Store<UserDetailsModel>(userDetails);

        var eventId = _session.Events.StartStream(
          new UserRegistered(userDetails, DateTime.Now)
        );

        _session.SaveChangesAsync();
        return Ok(
          $"Here is your identifier: {userDetails.Id}\nHere is your events id: {eventId}"
        );
      }else{
        return BadRequest("Requires name to be greater than 0 and age to be greater than 16");
      }
    }

    [Route("/api/users/update{id,event}")]
    [HttpPut]

    public IActionResult UpdateUser(Guid id, Guid ev, [FromBody] string Name, [FromBody] int Age, IDocumentSession _session){
      var user = _session.Load<UserDetailsModel>(id);

      if(user == null) return BadRequest("User not found.");
      
      _session.Update(user);

      user.Age = Age;
      user.Name = Name;

      _session.Events.Append(ev, new UserDetailsUpdated(user, DateTime.Now));
      _session.SaveChanges();


      return Ok(
        $"User details updated sucessfully.\nID = {id}\nEvent: {ev}"
      );

    }

    [Route("/api/users/get{id}")]
    [HttpGet]
    public async Task<IActionResult> GetUser(string id, IQuerySession _query) {
      return Ok(await _query.LoadAsync<UserDetailsModel>(id));
    }

    [Route("/api/admin/users/list")]
    [HttpGet]
    public IActionResult ListUsers(IQuerySession _session){
      return Ok(_session.Query<UserDetailsModel>().ToList());
    }
   }
}