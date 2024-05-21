using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mvccore.Models.Book;

public partial class User
{
    public long UserId { get; set; }

    [Required(ErrorMessage = "Username is required")]
    public string? UserName { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    public string? PassWord { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Middle Name is required")]
    public string? MiddleName { get; set; }

    [Required(ErrorMessage = "Surname is required")]
    public string? Surname { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? Active { get; set; }
}
