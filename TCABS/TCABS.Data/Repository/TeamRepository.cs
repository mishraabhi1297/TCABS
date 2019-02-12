using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Identity.Dapper.Connections;
using Identity.Dapper.UnitOfWork.Contracts;
using TCABS.Data.Models.Entities;
using TCABS.Data.Models.Project;
using TCABS.Data.Models.Team;

namespace TCABS.Data.Repository
{
    public class TeamRepository
    {
        private readonly IUnitOfWork _transaction;
        private readonly IConnectionProvider _connectionProvider;

        public TeamRepository(IConnectionProvider connection, IUnitOfWork unitOfWork)
        {
            _transaction = unitOfWork;
            _connectionProvider = connection;
        }

        public async Task<IEnumerable<Team>> GetTeamsAsync()
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<Team>(
                        "dbig5_admin.READ_TEAM_LIST_VIASQLDEV", commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<string> GetUnitNameAsync(int? id)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QuerySingleAsync<string>(
                        "dbig5_admin.GET_UNIT_NAME", new { pUnitID = id }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<Team> GetTeamData(int? teamID, int? unitID)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QuerySingleAsync<Team>(
                        "dbig5_admin.GET_TEAM_DATA", new { pTeamID = teamID, pUnitID = unitID }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<IEnumerable<ProjectAlloc>> GetProjectsAsync(int? id)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<ProjectAlloc>(
                        "dbig5_admin.GET_PROJECTS_FOR_UNIT", new { pUnitID = id }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<IEnumerable<Team>> GetEmployeesAsync()
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<Team>(
                        "dbig5_admin.GET_EMPLOYEES", commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<int> EditTeamAsync(Team model)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.ExecuteScalarAsync<int>(
                        "dbig5_admin.EDIT_TEAM_VIASQLDEV",
                        new { pTEAM_ID = model.TeamID, pId = model.SupervisorUserID,
                            pTEAM_NAME = model.TeamName, pPROJECTALLOC_ID = model.ProjectAllocID},
                            commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return 0;
        }
    }
}
