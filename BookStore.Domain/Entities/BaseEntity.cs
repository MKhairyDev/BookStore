using System;

namespace BookStore.Domain.Entities
{
    public class BaseEntity 
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
