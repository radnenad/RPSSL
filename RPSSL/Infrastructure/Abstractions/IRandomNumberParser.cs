namespace Infrastructure.Abstractions;

public interface IRandomNumberParser
{
    int Parse(string content);
}