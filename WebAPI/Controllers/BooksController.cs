using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Models;
using WebAPI.Data.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _service;
        public BooksController(IBookService service)
        {
            this._service = service;
        }

        // GET: api/<BooksController>
        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            var items = _service.GetAll();
            return Ok(items);
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public ActionResult<Book> Get(Guid id)
        {
            var item = _service.GetByID(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // POST api/<BooksController>
        [HttpPost]
        public ActionResult Post([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = _service.Add(book);
            return CreatedAtAction("Get", new { id = item.Id }, item);
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public ActionResult Remove(Guid id)
        {
            var existingItem = _service.GetByID(id);
            if (existingItem == null)
            {
                return NotFound();
            }
            _service.remove(id);
            return Ok();
        }
    }
}
