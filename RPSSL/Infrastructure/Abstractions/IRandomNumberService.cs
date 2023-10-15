using Domain.Shared;

namespace Infrastructure.Abstractions;

public interface IRandomNumberService
{
    Task<int> GetRandomNumber();
}