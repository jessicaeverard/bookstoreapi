using BookStore.Entities;

namespace BookStore.Dto
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int? AuthorId { get; set; }

        public AuthorDto? Author { get; set; }
    }

    public class AuthorDto
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }

    }

}
