using Dapper;
using Library.Api.Models;
using Library.Api.Persistence;

namespace Library.Api.Services;

public class BooksService(IDbConnectionFactory dbConnectionFactory) : IBooksService
{
    public async Task<bool> AddBookAsync(Book book)
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync();
        var rowsAffected = await connection.ExecuteAsync(
            "INSERT INTO Books (Isbn, Title, ShortDescription, Author, ReleaseDate, PageCount) " +
            "VALUES (@Isbn, @Title, @ShortDescription, @Author, @ReleaseDate, @PageCount)", book);
        return rowsAffected > 0;
    }

    public async Task<Book?> GetBookByIsbnAsync(string isbn)
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync();
        var book = await connection.QuerySingleOrDefaultAsync<Book>(
            "SELECT * FROM Books WHERE Isbn = @Isbn", new { Isbn = isbn });
        return book;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync(); 
        var books = await connection.QueryAsync<Book>("SELECT * FROM Books");
        return books;
    }

    public async Task<bool> UpdateBookAsync(Book book)
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync();
        var rowsAffected = await connection.ExecuteAsync(
            "UPDATE Books SET Title = @Title, ShortDescription = @ShortDescription, " +
            "Author = @Author, ReleaseDate = @ReleaseDate, PageCount = @PageCount " +
            "WHERE Isbn = @Isbn", book);
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteBookAsync(string isbn)
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync();
        var rowsAffected = await connection.ExecuteAsync(
            "DELETE FROM Books WHERE Isbn = @Isbn", new { Isbn = isbn });
        return rowsAffected > 0;
    }

    public async Task<IEnumerable<Book>> SearchBooksAsync(string query)
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync();
        var books = await connection.QueryAsync<Book>(
            "SELECT * FROM Books WHERE Title LIKE @Query OR ShortDescription LIKE @Query OR Author LIKE @Query",
            new { Query = $"%{query}%" });
        return books;
    }
}