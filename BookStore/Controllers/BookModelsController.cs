using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Entities;
using AutoMapper;
using BookStore.Services;
using BookStore.Dto;
using BookStore.Requests;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookModelsController : ControllerBase
    {
        //private readonly DataContext _context; //creates session with db, uses dataconext class that has been specified
        private readonly ILogger<BookModelsController> _logger;
        private readonly IBookService _bookService; //knows to check the interface for the following function
        private readonly IMapper _mapper;


        public BookModelsController(ILogger<BookModelsController> logger, IBookService bookService, IMapper mapper)
        {
            _logger = logger;
            _bookService = bookService;
            _mapper = mapper;
        }
        // GET: api/AuthorModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookModel>>> GetBookAsync()
        {
            var model = await _bookService.GetBookAsync(); //checks interface and sees this function and runs it 
            if (model == null)
            {
                return NotFound();
            }
            return model;
        }

        // GET: api/AuthorModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookModel>> GetBookByIdAsync(int id)
        {
            var bookModel = await _bookService.GetBookByIdAsync(id); // before it was _context.Pizza.FindAsync

            if (bookModel == null)
            {
                return NotFound();
            }
            return bookModel;
        }

        // PUT: api/AuthorModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookAsync(int id, [FromBody] UpdateBookRequest request)
        {
            try
            {
                await _bookService.UpdateBookAsync(id, request);
            }
            catch (DbUpdateConcurrencyException) //unexpected amount of rows are effected during save 
            { }
            return NoContent();
        }

        // POST: api/AuthorModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookModel>> InsertBookAsync(UpdateBookRequest request)
        {
            try
            {
                //_ // map request to model
                BookModel book = _mapper.Map<BookModel>(request);
                BookModel model = await _bookService.InsertBookAsync(book);

                //map pizzas and return dto
                BookDto bookDto = _mapper.Map<BookDto>(model);

                //return pizzaDto;
                return Ok(bookDto);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // DELETE: api/AuthorModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookByIdAsync(int id)
        {
            try
            {
                await _bookService.DeleteBookByIdAsync(id);
            }
            catch (Exception)
            {
                throw new Exception("did not delete ");
            }
            return NoContent(); //produces a 204 request - no content response if successfull
        }

    }
}

