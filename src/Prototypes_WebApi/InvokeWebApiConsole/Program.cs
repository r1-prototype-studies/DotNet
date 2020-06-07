using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InvokeWebApiConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var httpClient = new System.Net.Http.HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:8080");

            ListAllBooks(httpClient);
            Console.ReadLine();
            ListBook(httpClient, 5);
            Console.ReadLine();
            ListBook(httpClient, "Aroan");

            Console.WriteLine("Hit [Enter] ");
            Console.ReadLine();
        }

        private static void ListBook(HttpClient httpClient, string author)
        {
            HttpResponseMessage response = httpClient.GetAsync($"/api/book?author={author}").Result;

            //This method throws an exception if the HTTP response status is an error code. 
            response.EnsureSuccessStatusCode();

            var books = response.Content.ReadAsAsync<IEnumerable<book>>().Result;

            books.Select(x => { Console.WriteLine($"{x.Id}, {x.Author}, {x.Name}, {x.Rating}"); return x; }).ToList();
        }

        private static void ListBook(HttpClient httpClient, int id)
        {
            HttpResponseMessage response = httpClient.GetAsync($"/api/book/{id}").Result;

            //This method throws an exception if the HTTP response status is an error code. 
            response.EnsureSuccessStatusCode();

            var book = response.Content.ReadAsAsync<book>().Result;

            Console.WriteLine($"{book.Id}, {book.Author}, {book.Name}, {book.Rating}");
        }

        private static void ListAllBooks(HttpClient httpClient)
        {
            HttpResponseMessage response = httpClient.GetAsync($"/api/book").Result;

            //This method throws an exception if the HTTP response status is an error code. 
            response.EnsureSuccessStatusCode();

            var books = response.Content.ReadAsAsync<IEnumerable<book>>().Result;

            books.Select(x => { Console.WriteLine($"{x.Id}, {x.Author}, {x.Name}, {x.Rating}"); return x; }).ToList();
        }

        public class book
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Author { get; set; }

            public decimal Rating { get; set; }
        }
    }
}
