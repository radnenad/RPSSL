namespace Infrastructure.Abstractions;

public interface IRandomNumberFetcher
{
    Task<string> FetchRandomNumberAsync();
}