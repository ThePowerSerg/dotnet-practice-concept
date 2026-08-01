namespace DotnetPractice.Models
{
    // class definition with primary constructor 
    public class Book(int Id, string name, string author, int pageCount) : IBook
    {
        // properties allow for data manipulation
         public int Id { get; set; } = Id;
        public string Name { get; set; } = name;
        public string Author { get; set; } = author;
        public int PageCount { get; set; } = pageCount;

        // method returns a book description using string interpolation
        public string GetDescription() => $"Book name: {Name} - Author: {Author} - Page count: {PageCount}";
        public string GetName() => $"Book Name: {Name}";
        public string GetAuthor() => $"Book Author: {Author}";
    }
}