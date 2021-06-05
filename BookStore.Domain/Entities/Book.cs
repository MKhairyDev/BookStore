using System;
using System.Collections.Generic;

namespace BookStore.Domain.Entities
{
   public class Book: BaseEntity
    {
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public string ShortDescription { get; set; }
        public List<Author> Authors { get; set; } = new List<Author>();
    }
}
