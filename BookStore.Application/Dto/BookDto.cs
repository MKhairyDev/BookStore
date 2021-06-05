using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.Dto
{
   public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public int PublishDate { get; set; }
        public List<int> Authors { get; set; }
    }
}
