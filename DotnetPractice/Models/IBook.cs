
// set of method/property signatures with no implementation and no state of its own.
interface IBook
{
    int Id { get; set; }
    string Name { get; set; }
    string Author { get; set; }
    int PageCount { get; set; }
    string GetDescription();
}