
namespace MartenDB.Models;
public record UserRegistered(UserDetailsModel User, DateTime Date);
public record UserDetailsUpdated(UserDetailsModel User, DateTime Date);
