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
        public DateTime PublishDate { get; set; }
        public List<AuthorDto> Authors { get; set; } = new List<AuthorDto>();
    }
}
