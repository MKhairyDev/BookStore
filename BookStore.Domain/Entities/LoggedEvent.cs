using System;

namespace BookStore.Domain.Entities
{
    public class LoggedEvent
    {
        
        public int Id { get; set; }
        public string BookId { get; set; }
        public string Action { get; set; }
        public string Data { get; set; }
        public string Description { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
