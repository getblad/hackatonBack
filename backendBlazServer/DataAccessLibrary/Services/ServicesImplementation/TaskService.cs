using DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;
using TaskTable = DataAccessLibrary.Models.Task;

namespace DataAccessLibrary.Services.ServicesImplementation;

public class TaskService:ITaskService
{
    private HpContext _context;

    public TaskService(HpContext context)
    {
        _context = context;
    }

    public Task<List<TaskTable>> GetTasks()
    {
        return Task.FromResult(_context.Tasks.ToList());
    }

    public void AddTask(TaskTable newTask)
    {
        try
        {
            _context.Tasks.Add(newTask);
            _context.SaveChanges();
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void UpdateTask(long id, TaskTable task)
    {
        try
        {
            var local = _context.Set<TaskTable>().Local.FirstOrDefault(entry => entry.TaskId.Equals(task.TaskId));
            if (local != null)
            {
                // detach
                _context.Entry(local).State = EntityState.Detached;
            }

            _context.Entry(task).State = EntityState.Modified;
            _context.SaveChanges();
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public TaskTable SingleTask(long id)
    {
        try
        { 
            return _context.Tasks.Find(id);

        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void DeleteTask(long id)
    {
        try
        {
            TaskTable? table = _context.Tasks.Find(id);
            _context.Tasks.Remove(table);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}