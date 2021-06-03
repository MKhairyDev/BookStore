using System.Collections.Generic;

namespace BookStore.Domain.Entities
{
   public class Book: BaseEntity
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public int PublishDate { get; set; }
        public List<Author>Authors { get; set; }
    }
}
