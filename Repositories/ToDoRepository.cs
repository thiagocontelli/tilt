using SQLite;
using Tilt.Models;

namespace Tilt.Repositories;

public class ToDoRepository
{
    const string DB_NAME = "AppDatabase.db3";

    readonly SQLiteAsyncConnection _connection;

    public ToDoRepository()
    {
        _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
        _connection.CreateTableAsync<ToDo>();
    }

    public async Task UpsertToDo(ToDo toDo)
    {
        if (toDo.Id != 0)
            await _connection.UpdateAsync(toDo);
        else
            await _connection.InsertAsync(toDo);
    }

    public async Task<List<ToDo>> GetToDos()
    {
        return await _connection.Table<ToDo>().ToListAsync();
    }

    public async Task DeleteToDos()
    {
        await _connection.DeleteAllAsync<ToDo>();
    }
}
