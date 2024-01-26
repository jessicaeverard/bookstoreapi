using System.ComponentModel.DataAnnotations;

namespace BookStore.Requests
{
    public class UpdateBookRequest
    {
        public string Title { get; set; }
        public string? Description { get; set; }

        public int? AuthorId { get; set; }

    }

    public class UpdateAuthorRequest
    {
        public string Name { get; set; }

    }
}
