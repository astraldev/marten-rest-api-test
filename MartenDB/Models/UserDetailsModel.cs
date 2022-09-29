using System;
namespace MartenDB.Models;

public class UserDetailsModel
{
    public string Name {get; set;}
    public int Age {get; set;}

    public UserDetailsModel(string _name, int _age){
        Name = _name;
        Age = _age;
    }
    public Guid Id = new Guid();
};
