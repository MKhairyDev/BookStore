using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BookStore.Application.Commands.Books.CreateBookCommand;
using BookStore.Application.Commands.Books.UpdateBookCommand;
using BookStore.Application.Queries.GetBooksHistory.GetBooksById;
using BookStore.Application.Queries.GetBooksHistory.GetBooksHistoryQuery;
using BookStore.Application.Queries.GetBooksHistory.GetBooksQuery;
using BookStore.Application.Queries.GetBooksHistory.GetPagedBooksHistoryQuery;
using MediatR;

namespace BookStore.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator??throw  new ArgumentNullException(nameof(mediator));
        }
        /// <summary>
        /// GET api/controller, CRUD > Get by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _mediator.Send(new GetBooksByIdQuery {Id  = id }));
        }
        /// <summary>
        /// GET: api/controller, CRUD > Get by query parameters
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetBooks")]
        public async Task<IActionResult> Get([FromQuery] GetBooksQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        /// <summary>
        /// GET: api/controller, CRUD > Get by query parameters
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetBooksHistory")]
        public async Task<IActionResult> Get([FromQuery] GetBooksHistoryQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        /// <summary>
        /// GET: api/controller, Get book history with pagination support
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PagedHistory")]
        public async Task<IActionResult> PagedHistory(PagedQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        /// <summary>
        /// POST api/controller, CRUD > Create
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [Authorize]

        public async Task<IActionResult> Post(CreateBookCommand command)
        {
            var resp = await _mediator.Send(command);
            return CreatedAtAction(nameof(Post), resp);
        }
        /// <summary>
        /// PUT api/controller, CRUD > Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateBookCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(command));
        }

    }
}
