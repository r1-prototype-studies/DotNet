using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHostWebApi
{
    public class book
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public decimal Rating { get; set; }
    }
}
