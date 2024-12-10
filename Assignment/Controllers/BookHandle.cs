using Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookHandle : ControllerBase
    {
        private readonly BookContext _bookContext;

        public BookHandle(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
        {
            var books = await _bookContext.Books
                .Select(book => new BookDTO
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    ISBN = book.ISBN,
                    PublicationDate = book.PublicationDate
                })
                .ToListAsync();

            return Ok(books);
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetBook(Guid id)
        {
            var book = await _bookContext.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                PublicationDate = book.PublicationDate
            });
        }

       
        [HttpPost]
        public async Task<ActionResult<BookDTO>> AddBook(BookDTO bookDTO)
        {
            var book = new Book
            {
                Title = bookDTO.Title,
                Author = bookDTO.Author,
                ISBN = bookDTO.ISBN,
                PublicationDate = bookDTO.PublicationDate
            };

            _bookContext.Books.Add(book);
            await _bookContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                PublicationDate = book.PublicationDate
            });
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, BookDTO bookDTO)
        {
            if (id != bookDTO.Id)
            {
                return BadRequest();
            }

            var book = await _bookContext.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            book.Title = bookDTO.Title;
            book.Author = bookDTO.Author;
            book.ISBN = bookDTO.ISBN;
            book.PublicationDate = bookDTO.PublicationDate;

            try
            {
                await _bookContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!_bookContext.Books.Any(b => b.Id == id))
            {
                return NotFound();
            }

            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var book = await _bookContext.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookContext.Books.Remove(book);
            await _bookContext.SaveChangesAsync();

            return NoContent();
        }
    }

}
