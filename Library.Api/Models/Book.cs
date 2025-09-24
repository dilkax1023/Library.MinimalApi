namespace Library.Api.Models;

public class Book
{
    public string Isbn { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
    public string Author { get; set; } = null!;
    public DateTime ReleaseDate { get; set; }
    public int PageCount { get; set; }
}