using Domain.Entities;

namespace Infrastructure.Abstractions;

public interface IRandomNumberService
{
    Task<RandomNumber> GetRandomNumber();
}