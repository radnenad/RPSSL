namespace Domain.Entities;

public class GameChoice
{
    public int Id { get; }
    public string Name { get; }
    public List<GameChoice> Beats { get; }
    
    public GameChoice(int id, string name)
    {
        Id = id;
        Name = name;
        Beats = new List<GameChoice>();
    }
}
