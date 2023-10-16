namespace Domain.Entities;

public class Choice
{
    public int Id { get; }
    public string Name { get; }
    public List<Choice> Beats { get; }
    
    public Choice(int id, string name)
    {
        Id = id;
        Name = name;
        Beats = new List<Choice>();
    }
}
