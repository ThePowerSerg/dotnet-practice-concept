using System.Security.Cryptography.X509Certificates;
using DotnetPractice.Models;

// **************** Book **************************
var b = new Book("Harry Potter", "JK Rowling", 555);

DisplayDetails bookDetails = b.GetName;

Console.WriteLine("Book name is:" + bookDetails());

bookDetails = b.GetAuthor;

Console.WriteLine("Author is: " + bookDetails());

bookDetails = b.GetDescription;

Console.WriteLine("Description is: " + bookDetails());


// **************** Math Operation *******************
int AddNumbers(int x, int y) => x + y;

int SubtractNumbers(int x, int y) => x - y;
// create delegate variable
MathOperation operation;

// Assign the delegate variable to a method
operation = AddNumbers;

// should display 15
Console.WriteLine(operation(10, 5));

operation = SubtractNumbers;

// should display 5
Console.WriteLine(operation(10, 5));

// Lambda expression example - returns 50
MathOperation multiplyNumbers = (x, y) => x * y;

Console.WriteLine(multiplyNumbers(10, 5));