namespace Tilt.Models;

public class ToDo
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsDone { get; set; }
    public DateTime CreatedaAt { get; set; }
}
