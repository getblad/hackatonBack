using DataAccessLibrary.CustomExceptions;
using DataAccessLibrary.Enums;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Logging;

namespace DataAccessLibrary.Repositories;

public class EventMissionsRepositories:DbRepositories<EventMission>
{
    private readonly ILogger _logger;

    public EventMissionsRepositories(HpContext context/*, ILogger logger*/) : base(context) /*logger)*/
    {
        
    }

    public async Task AssignMission(EventMission eventMission)
    {
        try
        {
            eventMission.CreateTime = DateTime.UtcNow; 
            eventMission.UpdateTime = DateTime.UtcNow; 
            eventMission.RowStatusId = (int)StatusEnums.Active;
            if ((await GetOne(eventMission.MissionId))!.RowStatusId == (int)StatusEnums.Delete)
            {
                throw new NotFoundException($"Mission {eventMission.MissionId} not found");
            } 
            await Create(eventMission);
            _logger.LogInformation("Mission is successfully created");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            switch (e)
            {
                case NotFoundException:
                    throw;
            }
            throw new AlreadyExistingException();
        }
        
    }
}