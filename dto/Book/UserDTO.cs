using System.ComponentModel.DataAnnotations;

namespace mvccore.dto.Book
{
    public class UserDTO
    {
        public long UserId { get; set; }

        public string? Name { get; set; }

        public string? MiddleName { get; set; }

        public string? Surname { get; set; }

        public DateTime? CreatedDate { get; set; }
    }


    public class UserUpdateDTO
    {
        public long UserId { get; set; }

        public string? Name { get; set; }

        public string? MiddleName { get; set; }

        public string? Surname { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }
    }

    public class bookDTO
    {
        public long BookId { get; set; }

        public string? Description { get; set; }

        public string? Name { get; set; }

        public int Icount { get; set; }

        public DateTime? CreatedDate { get; set; }

    }

    public class BookUpdateDTO
    {
        public long BookId { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public int Icount { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
