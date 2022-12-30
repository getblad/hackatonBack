using System.Threading.Tasks;
using DataAccessLibrary.Models;

namespace DataAccessLibrary.Repositories;

public interface IMissionService
{
    Task<List<Mission>> GetMissions();
    void AddMission(Mission newMission);
    void UpdateMission(long id, Mission mission);
    Mission SingleMission(long id);
    void DeleteMission(long id);
}