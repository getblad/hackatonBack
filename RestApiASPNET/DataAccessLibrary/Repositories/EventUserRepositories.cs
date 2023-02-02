using DataAccessLibrary.CustomExceptions;
using DataAccessLibrary.Enums;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories.Interfaces;

namespace DataAccessLibrary.Repositories;

public class EventUserRepositories:DbRepositories<EventUser>, IDbRepositories<EventUser>
{
    private readonly HpContext _context;
    public EventUserRepositories(HpContext context/*, ILogger logger*/) : base(context /*logger*/)
    {
        _context = context;
    }
    
    
    public async Task<List<EventTeam?>> GetTeamsByTwitter(int eventId, int twitterBonus, string[] users = null)
    {
            var eventTeams1 = await Where(@event =>
                    @event.EventId == eventId && users!.Contains(@event.User.UserTwitter!) &&
                    @event.RowStatusId == (int)StatusEnums.Active).Selector(a => a.EventTeam)
                .Where(team => team!.RowStatusId == (int)StatusEnums.Active && team.TeamTwitterPoint != true).GetAll();
            foreach (var eventTeam in eventTeams1)
            {
                eventTeam!.UpdateTime = DateTime.UtcNow;
                eventTeam!.EventTeamPoint += twitterBonus;
                eventTeam.TeamTwitterPoint = true;
            }
            await _context.SaveChangesAsync();
            return eventTeams1;
       
    }


    public async Task Delete(int eventId, int userIdDelete, int userId)
    {
     
             try
             {
                 var entry = await Where(a => a.EventId == eventId, b => b.UserId == userIdDelete).GetOne();
                 if (entry != null)
                 {
                     entry.RowStatusId = (int)StatusEnums.Delete;
                     entry.UpdateUserId = userId;
                     entry.UpdateTime = DateTime.UtcNow;
                 }
                 await _context.SaveChangesAsync();
             }
             catch (Exception e)
             {
                 throw new NotFoundException();
             }
     
     
            
    }


    public async Task<EventUser> Create(EventUser model)
    {


        var existingModel =
            _context.EventUsers.FirstOrDefault(a => a.EventId == model.EventId && a.UserId == model.UserId);
        if (existingModel != null)
        {
            
            existingModel.RowStatusId = (int)StatusEnums.Active;
            await _context.SaveChangesAsync();
            return existingModel;
        };
        return await base.Create(model);
    }
}