using System.Diagnostics.SymbolStore;
using Library.Api.Abstractions;
using Library.Api.Models;
using Library.Api.Services;

namespace Library.Api.Modules;

public class BooksModule : IEndpointModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("/books").WithName("Books").WithOpenApi();

        endpoints.MapGet("", async (IBooksService service, string? searchTerm) =>
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchResults = await service.SearchBooksAsync(searchTerm);
                return Results.Ok(searchResults);
            }
            var books = await service.GetAllBooksAsync();
            return Results.Ok(books);
        });

        endpoints.MapGet("{isbn}", async (string isbn, IBooksService service) =>
        {
            var book = await service.GetBookByIsbnAsync(isbn);
            return book is null ? Results.NotFound() : Results.Ok(book);
        });

        endpoints.MapPost("", async (IBooksService service, Book book) =>
        {
            var isCreated = await service.AddBookAsync(book);
            return isCreated ? Results.Created($"/books/{book.Isbn}", book.Isbn) : Results.BadRequest();
        });

        endpoints.MapDelete("{isbn}", async (IBooksService service, string isbn) =>
        {
            var isDeleted = await service.DeleteBookAsync(isbn);
            return isDeleted ? Results.NoContent() : Results.NotFound();
        });

        endpoints.MapPut("{isbn}", async (Book book, IBooksService service) =>
        {
            var isUpdated = await service.UpdateBookAsync(book);
            return isUpdated ? Results.NoContent() : Results.NotFound();
        });
    }
}