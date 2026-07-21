using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetPractice.Models.Classes
{
    public class Book : IBook
    {
        public string? Id { get; set; }
        public string? Name { get; set; }

        public string DisplayName()
        {
            if (Id != null)
            {
                return Name.ToUpper();
            }
            else return "";
        }
    }
}