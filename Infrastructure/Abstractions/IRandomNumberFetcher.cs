using Domain.Entities;

namespace Infrastructure.Abstractions;

public interface IRandomNumberFetcher
{
    Task<RandomNumber> FetchRandomNumberAsync();
}