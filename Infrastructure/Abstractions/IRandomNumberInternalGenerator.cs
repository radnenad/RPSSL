using Domain.Entities;

namespace Infrastructure.Abstractions;

public interface IRandomNumberInternalGenerator
{
    RandomNumber Generate();
}