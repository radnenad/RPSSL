using Domain.Entities;

namespace Application.Abstractions;

public interface IRandomChoiceService
{
    Task<Choice> GetRandomChoice();
}