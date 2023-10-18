namespace Infrastructure.Abstractions;

public interface IRandomNumberService
{
    Task<int> GetRandomNumber();
}