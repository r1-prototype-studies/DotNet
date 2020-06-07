using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHostWebApi
{
    public class BookController : System.Web.Http.ApiController
    {
        book[] books = new book[]

        {
            new book { Id = 1, Name = "Maths", Author = "Aroan", Rating = 5 },
            new book { Id = 2, Name = "Science", Author = "Sharon", Rating = 4 },
            new book { Id = 3, Name = "Social", Author = "Aroan", Rating = 3 },
            new book { Id = 4, Name = "English", Author = "Sharon", Rating = 2 },
            new book { Id = 5, Name = "Tamil", Author = "Aroan", Rating = 1 },
            new book { Id = 6, Name = "Scripture", Author = "John", Rating = 6 }
        };

        public IEnumerable<book> GetBooks()
        {
            return books;
        }

        public book GetBook(int id)
        {
            var book = books.FirstOrDefault(x => x.Id.Equals(id));
            if (book == null)
            {
                throw new System.Web.Http.HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }
            return book;
        }

        public IEnumerable<book> GetBooksByAuthor(string author)
        {
            var book = books.Where(x => x.Author.Equals(author, StringComparison.OrdinalIgnoreCase));
            if (book == null)
            {
                throw new System.Web.Http.HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }
            return book;
        }
    }
}
