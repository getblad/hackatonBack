using System.Threading.Tasks;
using DataAccessLibrary.Models;

namespace DataAccessLibrary.Repositories;

public interface IMissionService
{
    List<Mission> GetMissions();
    void AddMission(Mission newMission);
    void UpdateMission(Mission mission);
    Mission SingleMission(int id);
    void DeleteMission(int id);
}