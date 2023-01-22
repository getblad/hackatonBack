using DataAccessLibrary.Enums;
using DataAccessLibrary.Models;

namespace DataAccessLibrary.Repositories;

public class TwitterRepositories:DbRepositories<EventUser>
{
    private readonly HpContext _context;
    public TwitterRepositories(HpContext context) : base(context)
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
            eventTeam!.UpdateTime = DateTime.Now;
            eventTeam!.EventTeamPoint += twitterBonus;
            eventTeam.TeamTwitterPoint = true;
        }

        await _context.SaveChangesAsync();
        return eventTeams1;
    }
}