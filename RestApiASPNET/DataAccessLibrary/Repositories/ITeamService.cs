using DataAccessLibrary.Models;

namespace DataAccessLibrary.Repositories;

public interface ITeamService
{
    List<TeamAdmin> GetTeams();
    void AddTeam(Team newTeam);
    void UpdateTeam(Team team);
    void DeleteTeam(int id);
}