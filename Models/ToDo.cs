using SQLite;

namespace Tilt.Models;

[Table("todos")]
public class ToDo
{
    [PrimaryKey, AutoIncrement, Column("id")]
    public int Id { get; set; }

    [Column("description")]
    public string Description { get; set; } = string.Empty;

    [Column("is_done")]
    public bool IsDone { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
