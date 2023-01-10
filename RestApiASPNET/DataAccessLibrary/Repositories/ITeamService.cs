using DataAccessLibrary.Models;

namespace DataAccessLibrary.Repositories;

public interface ITeamService
{
    List<TeamDtoAdmin> GetTeams();
    void AddTeam(Team newTeam);
    void UpdateTeam(Team team);
    void DeleteTeam(int id);
}