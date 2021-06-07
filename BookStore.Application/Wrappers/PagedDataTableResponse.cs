using BookStore.Application.Parameters;

namespace BookStore.Application.Wrappers
{
    public class PagedDataTableResponse<T> : Response<T>
    {
        public PagedDataTableResponse(T data, int pageNumber, RecordsCount recordsCount)
        {
            Draw = pageNumber;
            RecordsFiltered = recordsCount.RecordsFiltered;
            RecordsTotal = recordsCount.RecordsTotal;
            Data = data;
            Message = null;
            Succeeded = true;
            Errors = null;
        }

        public int Draw { get; set; }
        public int RecordsFiltered { get; set; }
        public int RecordsTotal { get; set; }
    }
}