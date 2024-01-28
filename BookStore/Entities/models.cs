using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Entities
{
    public class BookModel
    {
        [Key] //primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }


        public int AuthorId { get; set; }
        public AuthorModel Author { get; set; }

        //if one field is nullable it all has to be nullable

    }
   


}
