using System.Linq.Expressions;
using DataAccessLibrary.CustomExceptions;
using DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.Repositories;

public class EventRepositories:DbRepositories<Event>
{
    private readonly HpContext _context;

    public EventRepositories(HpContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task AssignMission(EventMission eventMission)
    {
        try
        {
            
            await _context.EventMissions.AddAsync(eventMission);
            await _context.SaveChangesAsync();
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new AlreadyExistingException();
        }
        
    }

    public async Task<Event> GetEvent(int eventId)
    {
        try
        {
            var @event = await _context.Events.Include(a => a.EventMissions)
                .ThenInclude(b => b.Mission).Where(events => events.EventId == eventId).FirstOrDefaultAsync();
            return (@event ?? throw new NotFoundException());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<Event> Create(Event model)
    {
        throw new NotImplementedException();
    }

    public Task<Event> Update(int id, Event model)
    {
        throw new NotImplementedException();
    }

    public Task<List<Event>> GetAll()
    {
        throw new NotImplementedException();
    }

    public DbRepositories<Event> Where(Expression<Func<Event, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public DbRepositories<Event> Where(List<Expression<Func<Event, bool>>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<Event> GetOne(int id, IEnumerable<string> includes)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public DbRepositories<Event> Get(List<string> includes)
    {
        throw new NotImplementedException();
    }
}
