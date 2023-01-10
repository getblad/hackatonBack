using DataAccessLibrary.Models;

namespace DataAccessLibrary.Repositories;

public interface ITeamService
{
    List<Team> GetTeams();
    void AddTeam(Team newTeam);
    void UpdateTeam(Team team);
    void DeleteTeam(int id);
}