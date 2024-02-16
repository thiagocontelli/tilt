using CommunityToolkit.Mvvm.ComponentModel;
using Tilt.Models;

namespace Tilt.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    IEnumerable<ToDo> toDos = [];

    public MainViewModel()
    {
        toDos =
        [
            new()
            {
                Id = 1,
                CreatedaAt = DateTime.Now,
                Description = "First ToDo",
                IsDone = false
            },
            new()
            {
                Id = 2,
                CreatedaAt = DateTime.Now,
                Description = "Second ToDo",
                IsDone = false
            },
            new()
            {
                Id = 3,
                CreatedaAt = DateTime.Now,
                Description = "Third ToDo",
                IsDone = false
            }
        ];
    }

    public void ClearAllToDos() => ToDos = [];

    public void AddTodo(string toDoDescription)
    {
        if (string.IsNullOrWhiteSpace(toDoDescription)) return;

        List<ToDo> newToDos = ToDos.ToList();

        ToDo newToDo = new()
        {
            Id = newToDos.Count + 1,
            CreatedaAt = DateTime.Now,
            Description = toDoDescription,
            IsDone = false
        };

        newToDos.Add(newToDo);

        ToDos = newToDos;
    }
}
