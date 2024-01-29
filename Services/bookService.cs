//Interacting with the database, injection of data context as well as methods for getting, inserting and updating database

using BookStore.Data;
using Microsoft.EntityFrameworkCore;
using BookStore.Entities;
using BookStore.Requests;
using System;

namespace BookStore.Services
{
    /// <summary>
    /// Interface is like the menu for the CRUD methods below 
    /// This directly talks to the database , what ever you return can be used for the controller
    /// </summary>
    public interface IBookService
    {
        Task<List<BookModel>> GetBookAsync();

        Task<BookModel> GetBookByIdAsync(int id);

        Task<BookModel> InsertBookAsync(BookModel book);

        Task<BookModel> UpdateBookAsync(int id, UpdateBookRequest request);

        Task<BookModel> DeleteBookByIdAsync(int id);

    }
    /// <summary>
    /// this class needs to inherit from the interface above as it needs the tasks detailed above
    /// </summary>
    public class BookService : IBookService
    {
        public DataContext _dataContext;

        public BookService(DataContext dataContext)
        {
            _dataContext = dataContext;

        }

        //deletes item by id 
        public async Task<BookModel> DeleteBookByIdAsync(int id)
        {
            var book = await _dataContext.Books.FirstOrDefaultAsync(p => p.Id == id);
            var tEntity = _dataContext.Books.Remove(book);
            await _dataContext.SaveChangesAsync();

            return tEntity.Entity;

        }

        // returns list of all pizzas in database
        public async Task<List<BookModel>> GetBookAsync()
        {
            return await _dataContext.Books.Include(b => b.Author).ToListAsync(); //recieves data from the controller, goes to the pizza model and retrieves all data
        }

        //method recieves a single pizza with the specified Id

        public async Task<BookModel> GetBookByIdAsync(int id)
        {
            return await _dataContext.Books.FirstOrDefaultAsync(p => p.Id == id);
        }

        //inserts new pizza into database
        public async Task<BookModel> InsertBookAsync(BookModel book)
        {
            var tEntity = _dataContext.Books.Add(book);
            await _dataContext.SaveChangesAsync(); //wait until this is done

            var newBook = tEntity.Entity;

            // referencign an author - for the author model you would have a collection (array) of books

            _dataContext.Entry(newBook)
                .Reference(b => b.Author)
                .Load();
           
            return newBook;
        }

        //updates an existing pizza in database

        public async Task<BookModel> UpdateBookAsync(int id, UpdateBookRequest request)
        {
            var book = await _dataContext.Books.FirstOrDefaultAsync(p => p.Id == id);

            book.Title = request.Title;

            var tEntity = _dataContext.Books.Update(book); //updates object with data passed through above
            await _dataContext.SaveChangesAsync(); //saves the object

            return tEntity.Entity; //returns current entity to controller 
        }
    }
}
