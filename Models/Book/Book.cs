using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mvccore.Models.Book;

public partial class Book
{
    public long BookId { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public int Icount { get; set; }

    [Required]
    public DateTime? CreatedDate { get; set; }

    public bool? Active { get; set; }
}
