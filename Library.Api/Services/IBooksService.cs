using Library.Api.Models;

namespace Library.Api.Services;

public interface IBooksService
{
    Task<bool> AddBookAsync(Book book); 
    Task<Book?> GetBookByIsbnAsync(string isbn);
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<bool> UpdateBookAsync(Book book);
    Task<bool> DeleteBookAsync(string isbn);
    Task<IEnumerable<Book>> SearchBooksAsync(string query);
}