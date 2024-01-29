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
    public class AuthorModelsController : ControllerBase
    {
        //private readonly DataContext _context; //creates session with db, uses dataconext class that has been specified
        private readonly ILogger<AuthorModelsController> _logger;
        private readonly IAuthorService _authorService; //knows to check the interface for the following function
        private readonly IMapper _mapper;


        public AuthorModelsController(ILogger<AuthorModelsController> logger, IAuthorService authorService, IMapper mapper)
        {
            _logger = logger;
            _authorService = authorService;
            _mapper = mapper;
        }
        // GET: api/AuthorModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorModel>>> GetAuthorsAsync()
        {
            var model = await _authorService.GetAuthorsAsync(); //checks interface and sees this function and runs it 
            if (model == null)
            {
                return NotFound();
            }
            return model;
        }

        // GET: api/AuthorModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorModel>> GetAuthorByIdAsync(int id)
        {
            var authorModel = await _authorService.GetAuthorByIdAsync(id); // before it was _context.Pizza.FindAsync

            if (authorModel == null)
            {
                return NotFound();
            }
            return authorModel;
        }

        // PUT: api/AuthorModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthorAsync(int id, [FromBody] UpdateAuthorRequest request)
        {
            try
            {
                await _authorService.UpdateAuthorAsync(id, request);
            }
            catch (DbUpdateConcurrencyException) //unexpected amount of rows are effected during save 
            { }
            return NoContent();
        }

        // POST: api/AuthorModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AuthorModel>> InsertAuthorAsync(UpdateAuthorRequest request)
        {
            try
            {
                //_ // map request to model
                AuthorModel author = _mapper.Map<AuthorModel>(request);
                AuthorModel model = await _authorService.InsertAuthorAsync(author);

                //map pizzas and return dto
                AuthorDto authorDto = _mapper.Map<AuthorDto>(model);

                //return pizzaDto;
                return NoContent();
            }
            catch (Exception)
            {
                throw new Exception("didnt fill in the body correctly");
            }
        }

        // DELETE: api/AuthorModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthorByIdAsync(int id)
        {
            try
            {
                Console.WriteLine(id);
                await _authorService.DeleteAuthorByIdAsync(id);

            }
            catch (Exception)
            {
                throw new Exception("did not delete ");
            }
            return NoContent(); //produces a 204 request - no content response if successfull
        }

    }
}

