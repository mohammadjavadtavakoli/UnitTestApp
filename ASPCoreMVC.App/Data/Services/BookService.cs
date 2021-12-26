using System;
using System.Collections.Generic;
using System.Linq;
using ASPCoreMVC.App.Data;
using ASPCoreMVC.App.Data.Models;
using ASPCoreMVC.App.Data.Services;
namespace ASPCoreMVC.App.Services
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _appDbContext;

        public BookService(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        public IEnumerable<Book> GetAll()
        {
            return _appDbContext.Books.ToList();
        }
        public Book Add(Book book)
        {
            book.Id = Guid.NewGuid();
            _appDbContext.Books.Add(book);
            _appDbContext.SaveChanges();
            return book;
        }

        public Book GetByID(Guid id)
        {
            return _appDbContext.Books.FirstOrDefault(x => x.Id == id);
        }

        public void remove(Guid id)
        {
            var existing = _appDbContext.Books.First(a => a.Id == id);
            _appDbContext.Books.Remove(existing);
            _appDbContext.SaveChanges();
        }
    }
}
