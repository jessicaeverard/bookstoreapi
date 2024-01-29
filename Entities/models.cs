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

  

  //      [ForeignKey("AuthorId")]
       // public int AuthorId { get; set; }
       // public virtual AuthorModel? Author { get; set; }

        // If migrations dont work uncomment the above and remove these two lines

        public int AuthorId { get; set; }
        public AuthorModel Author { get; set; }
    }
   


}
