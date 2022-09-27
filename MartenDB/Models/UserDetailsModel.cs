using System;
namespace MartenDB.Models;

public record UserDetailsModel(string Name, int Age)
{
    public Guid Id = new Guid();
};
