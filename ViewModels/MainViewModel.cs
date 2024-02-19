using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Tilt.Models;
using Tilt.Repositories;

namespace Tilt.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    IEnumerable<ToDo> toDos = [];

    readonly ToDoRepository _toDoRepository;

    public MainViewModel(ToDoRepository toDoRepository)
    {
        _toDoRepository = toDoRepository;

        Task.Run(GetToDos);
    }

    async Task GetToDos() => ToDos = await _toDoRepository.GetToDos();

    public async void ClearAllToDos()
    {
        await _toDoRepository.DeleteToDos();
        await GetToDos();
    }

    public async void AddTodo(string toDoDescription)
    {
        if (string.IsNullOrWhiteSpace(toDoDescription)) return;

        ToDo newToDo = new()
        {
            Description = toDoDescription,
        };

        await _toDoRepository.UpsertToDo(newToDo);
        await GetToDos();
    }

    [RelayCommand]
    async Task ToggleIsDone(ToDo? toDo)
    {
        if (toDo == null) return;

        ToDo newToDo = new()
        {
            CreatedAt = toDo.CreatedAt,
            Description = toDo.Description,
            Id = toDo.Id,
            IsDone = toDo.IsDone,
        };

        await _toDoRepository.UpsertToDo(newToDo);
        await GetToDos();
    }
}
