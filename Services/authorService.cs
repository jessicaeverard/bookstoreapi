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
    public interface IAuthorService
    {
        Task<List<AuthorModel>> GetAuthorsAsync();

        Task<AuthorModel> GetAuthorByIdAsync(int id);

        Task<AuthorModel> InsertAuthorAsync(AuthorModel author);

        Task<AuthorModel> UpdateAuthorAsync(int id, UpdateAuthorRequest request);

        Task<AuthorModel> DeleteAuthorByIdAsync(int id);

    }
    /// <summary>
    /// this class needs to inherit from the interface above as it needs the tasks detailed above
    /// </summary>
    public class AuthorService : IAuthorService
    {
        public DataContext _dataContext;

        public AuthorService(DataContext dataContext)
        {
            _dataContext = dataContext;

        }

        //deletes item by id 
        public async Task<AuthorModel> DeleteAuthorByIdAsync(int id)
        {
            var author = await _dataContext.Authors.FirstOrDefaultAsync(p => p.AuthorId == id);
            var books = await _dataContext.Books.FirstOrDefaultAsync(p => p.AuthorId == id);

            
            if (author.Books == null)
            {
                var Entity = _dataContext.Authors.Remove(author);
                await _dataContext.SaveChangesAsync();
                return Entity.Entity;

            }

            return null;
        }

        // returns list of all pizzas in database
        public async Task<List<AuthorModel>> GetAuthorsAsync()
        {
            return await _dataContext.Authors.ToListAsync(); //recieves data from the controller, goes to the pizza model and retrieves all data
        }

        //method recieves a single pizza with the specified Id

        public async Task<AuthorModel> GetAuthorByIdAsync(int id)
        {
            return await _dataContext.Authors.FirstOrDefaultAsync(p => p.AuthorId == id);
        }

        //inserts new pizza into database
        public async Task<AuthorModel> InsertAuthorAsync(AuthorModel author)
        {
            var tEntity = _dataContext.Authors.Add(author);
            await _dataContext.SaveChangesAsync(); //wait until this is done
            return tEntity.Entity;
        }

        //updates an existing pizza in database

        public async Task<AuthorModel> UpdateAuthorAsync(int id, UpdateAuthorRequest request)
        {
            var author = await _dataContext.Authors.FirstOrDefaultAsync(p => p.AuthorId == id);

            author.Name = request.Name;

            var tEntity = _dataContext.Authors.Update(author); //updates object with data passed through above
            await _dataContext.SaveChangesAsync(); //saves the object

            return tEntity.Entity; //returns current entity to controller 
        }
    }
}
