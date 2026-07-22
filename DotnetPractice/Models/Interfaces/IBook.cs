interface IBook
{
    string Id { get; set;}
    string Name { get; set;}
    string DisplayName();
}