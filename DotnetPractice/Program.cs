using System.Data.Common;
using System.Security.Cryptography.X509Certificates;
using DotnetPractice.Models;

// **************** Book **************************

var b = new Book(1, "Harry Potter", "JK Rowling", 555);

DisplayDetails bookDetails = b.GetName;

// Console.WriteLine("Book name is:" + bookDetails());

bookDetails = b.GetAuthor;

// Console.WriteLine("Author is: " + bookDetails());

bookDetails = b.GetDescription;

// Console.WriteLine("Description is: " + bookDetails());


// **************** Math Operation *******************
int AddNumbers(int x, int y) => x + y;

int SubtractNumbers(int x, int y) => x - y;
// create delegate variable
MathOperation operation;

// Assign the delegate variable to a method
operation = AddNumbers;

// should display 15
// Console.WriteLine(operation(10, 5));

operation = SubtractNumbers;

// should display 5
// Console.WriteLine(operation(10, 5));

// Lambda expression example - returns 50
MathOperation multiplyNumbers = (x, y) => x * y;

// Console.WriteLine(multiplyNumbers(10, 5));

List<Book> books = [
     new Book(1, "Harry Potter", "JK Rowling", 555),
     new Book(2, "The Hobbit", "J.R.R. Tolkien", 310),
     new Book(3, "Dune", "Frank Herbert", 412)
];

// Retrieve a specific book by Id using LINQ
var filteredBook = books.FirstOrDefault(b => b.Id == 0);
Console.WriteLine(filteredBook?.Name ?? "defaul");