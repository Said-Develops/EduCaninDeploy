using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EduCanin.Models.Entities;

public class ApplicationUser : IdentityUser
{

    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public DateOnly RegisterDate { get; set; }
    
    public ICollection<Dog> Dogs { get; set; } = new List<Dog>();

}
