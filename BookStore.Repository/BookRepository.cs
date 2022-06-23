using BookStore.Models.DataModels;
using BookStore.Models.Models;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Repository
{
    public class BookRepository : BaseRepository
    {
        public ListResponse<Book> GetBooks(int pageIndex, int pagesize, string keyword)
        {

            keyword = keyword?.ToLower()?.Trim();
            var query = _context.Books.Where(c => keyword == null || c.Name.ToLower().Contains(keyword)).AsQueryable();
            int totalRcords = query.Count();
            List<Book> categories = query.Skip((pageIndex - 1) * pagesize).Take(pagesize).ToList();

            return new ListResponse<Book>()
            {
                Results = categories,
                TotalRecords = totalRcords,
            };
        }
        public Book GetBook(int id)
        {
            return _context.Books.SingleOrDefault(c => c.Id == id);
        }
        public Book AddBook(Book book)
        {

            var entry = _context.Books.Add(book);
            _context.SaveChanges();
            return entry.Entity;
        }
        public Book UpdateBook(Book book)
        {
            var entry = _context.Books.Update(book);
            _context.SaveChanges();

            return entry.Entity;
        }
        public bool DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(c => c.Id == id);
            if (book == null)
            {
                return false;
            }
            _context.Books.Remove(book);
            _context.SaveChanges();

            return true;
        }
    }
}
