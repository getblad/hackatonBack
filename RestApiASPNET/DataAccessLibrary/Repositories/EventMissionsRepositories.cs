using DataAccessLibrary.CustomExceptions;
using DataAccessLibrary.Enums;
using DataAccessLibrary.Models;
using DataAccessLibrary.Services;

namespace DataAccessLibrary.Repositories;

public class EventMissionsRepositories:DbRepositories<EventMission>
{
    public EventMissionsRepositories(HpContext context) : base(context)
    {
    }

    public async Task AssignMission(EventMission eventMission)
    {
        try
        {
            eventMission.CreateTime = DateTime.Now; 
            eventMission.UpdateTime = DateTime.Now; 
            eventMission.RowStatusId = (int)StatusEnums.Active;
            if ((await GetOne(eventMission.MissionId))!.RowStatusId == (int)StatusEnums.Delete)
            {
                throw new NotFoundException($"Mission {eventMission.MissionId} not found");
            } 
            await Create(eventMission);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            switch (e)
            {
                case NotFoundException:
                    throw;
            }
            throw new AlreadyExistingException();
        }
        
    }
}