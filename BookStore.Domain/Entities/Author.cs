using System.Collections.Generic;

namespace BookStore.Domain.Entities
{
    public class Author: BaseEntity
    {
        public string Name { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
    }
}