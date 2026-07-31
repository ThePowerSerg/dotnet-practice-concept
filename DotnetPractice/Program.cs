using DotnetPractice.Models;

var b = new Book("Harry Potter", "JK Rowling", 555);

DisplayDetails details = b.GetName;

Console.WriteLine("Book name is:" + details());

details = b.GetAuthor;

Console.WriteLine("Author is: " + details());

details = b.GetDescription;

Console.WriteLine("Description is: " + details());
