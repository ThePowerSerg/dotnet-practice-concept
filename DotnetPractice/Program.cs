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
var filteredBook = books.FirstOrDefault(book => book.Id == 1);
//Console.WriteLine(filteredBook?.Name ?? "No books found with supplied ID");

var bookNameCheck = filteredBook?.Name.Contains("data");

var getId = books.Find(b => b.Id == 3);


//Console.WriteLine(bookNameCheck);

// create the Person object
var p1 = new Person("Alex", 30, "Boston");

Console.WriteLine(p1);

// Immutable - cannot be changed
// p1.Name = "serg";

var movedP1 = p1 with {City = "Norwell"};

Console.WriteLine(movedP1);

var employee = (Id: 101, Role: "Manager", Dept: "HR");

// Deconstructing into separate variables
var (id, role, department) = employee;

Console.WriteLine(id);

// -------------------------------------------------------
List<int> numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

foreach (var number in numbers)
{
     Console.WriteLine(number);
}

IEnumerable<int> evens = numbers.Where(x => x > 5);

foreach (int x in evens)
{
    Console.WriteLine(x);
}