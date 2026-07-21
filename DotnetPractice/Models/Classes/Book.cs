using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetPractice.Models.Classes
{
    // class definition with primary constructor
    public class Book(string name, string author, int pageCount)
    {
        public string Name { get; set; } = name;
        public string Author { get; set; } = author;
        public int PageCount { get; set;} = pageCount;

        // methods
        public string GetDescription() => $"Book name: {Name} - Author: {Author} - Page count: {PageCount}";
    }
}