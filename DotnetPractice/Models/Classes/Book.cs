using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetPractice.Models.Classes
{
    // class definition with primary constructor 
    public class Book(string name, string author, int pageCount) : IBook
    {
        // properties allow for data manipulation
        public string Name { get; set; } = name;
        public string Author { get; set; } = author;
        public int PageCount { get; set;} = pageCount;

        // method returns a book description using string interpolation
        public string GetDescription() => $"Book name: {Name} - Author: {Author} - Page count: {PageCount}";
    }
}