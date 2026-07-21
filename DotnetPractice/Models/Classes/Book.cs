using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetPractice.Models.Classes
{
    // class definition with default constructor
    public class Book(string name, string author, int pageCount)
    {
        private readonly string _name = name;
        private readonly string _author = author;
        private readonly int _pageCount = pageCount;

        // methods
        public string GetDescription()
        {
            return $"Book name: {_name} - Author: {_author} - Page count: {_pageCount}";
        }
    }
}