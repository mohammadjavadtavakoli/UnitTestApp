using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Models;

namespace WebAPI.Data.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();
        Book Add(Book book);
        Book GetByID(Guid id);
        void remove(Guid id);
    }
}
