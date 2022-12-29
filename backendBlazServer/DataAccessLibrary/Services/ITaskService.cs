using DataAccessLibrary.Models;
using TaskTable = DataAccessLibrary.Models.Task;

namespace DataAccessLibrary.Services;

public interface ITaskService
{
    Task<List<TaskTable>> GetTasks();
    void AddTask(TaskTable newTask);
    void UpdateTask(long id, TaskTable task);
    TaskTable SingleTask(long id);
    void DeleteTask(long id);
}