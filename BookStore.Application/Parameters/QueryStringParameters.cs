namespace BookStore.Application.Parameters
{
   public class QueryStringParameters
    {
        //Todo:Read these values from configuration file
        const int MaxPageSize = 20;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public string OrderBy { get; set; }
    }
}
