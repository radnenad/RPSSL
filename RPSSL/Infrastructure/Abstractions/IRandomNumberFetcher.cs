namespace Infrastructure.Abstractions;

public interface IRandomNumberFetcher
{
    Task<int> FetchRandomNumberAsync();
}