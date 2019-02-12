using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Identity.Dapper.Connections;
using Identity.Dapper.UnitOfWork.Contracts;
using TCABS.Data.Models.Entities;
using TCABS.Data.Models.Project;

namespace TCABS.Data.Repository
{
    public class ProjectRepository
    {
        private readonly IUnitOfWork _transaction;
        private readonly IConnectionProvider _connectionProvider;

        public ProjectRepository(IConnectionProvider connection, IUnitOfWork unitOfWork)
        {
            _transaction = unitOfWork;
            _connectionProvider = connection;
        }

        public async Task<IEnumerable<ProjectRoleGroup>> GetProjectRoleGroupsAsync()
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<ProjectRoleGroup>(
                        "dbig5_admin.READ_PROJECT_ROLE_GROUP_LIST_VIASQLDEV", commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<IEnumerable<Project>> GetProjectsAsync(int? unitID)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<Project>(
                        "dbig5_admin.READ_PROJECT_LIST_VIASQLDEV", new { pUnitID = unitID } ,commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<int> CreateProjectAsync(Project model, int? unitID)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.ExecuteScalarAsync<int>("dbig5_admin.ADD_PROJECT_VIASQLDEV",
                        new { pProjectRoleGroupID = model.ProjectRoleGroup_ID, pProjectName = model.Project_NAME, pProjectBudget = model.Project_BUDGET, pUnitID = unitID },
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            // Return failed to insert
            return 0;
        }

        public async Task<Project> FetchProjectAsync(int id)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QuerySingleAsync<Project>("dbig5_admin.READ_PROJECT_VIASQLDEV",
                        new { pPROJECT_ID = id }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }

        public async Task<bool> EditProjectAsync(Project model, int? unitID)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.ExecuteScalarAsync<bool>("dbig5_admin.UPDATE_PROJECT_VIASQLDEV",
                        new { pProjectID = model.Project_ID, pProjectRoleGroupID = model.ProjectRoleGroup_ID, pProjectName = model.Project_NAME, pProjectBudget = model.Project_BUDGET, pUnitID = unitID },
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return false;
        }

        public async Task<int> DeleteProjectAsync(int? projectID, int? unitID)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QuerySingleAsync<int>("dbig5_admin.DELETE_PROJECT_VIASQLDEV",
                        new { pProjectID = projectID, pUnitID = unitID }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return 0;
        }

        public async Task<int> CreateProjectRoleGroupAsync(ProjectRoleGroup model)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.ExecuteScalarAsync<int>("dbig5_admin.ADD_PROJECT_ROLE_GROUP_VIASQLDEV",
                        new { pPROJECTROLEGROUP_TYPE = model.ProjectRoleGroup_TYPE },
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            // Return failed to insert
            return 0;
        }

        public async Task<int> EditProjectRoleGroupAsync(ProjectRoleGroup model)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QuerySingleAsync<int>("dbig5_admin.UPDATE_PROJECT_ROLE_GROUP_VIASQLDEV",
                        new { pProjectRoleGroup_ID = model.ProjectRoleGroup_ID, pPROJECTROLEGROUP_TYPE = model.ProjectRoleGroup_TYPE },
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return 0;
        }

        public async Task<int> DeleteProjectRoleGroupAsync(int? id)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QuerySingleAsync<int>("dbig5_admin.DELETE_PROJECT_ROLE_GROUP_VIASQLDEV",
                        new { pProjectRoleGroup_ID = id }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return 0;
        }

        public async Task<IEnumerable<ProjectRole>> GetProjectRoleListAsync()
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<ProjectRole>("dbig5_admin.READ_PROJECT_ROLE_LIST_VIASQLDEV",
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }

        public async Task<IEnumerable<int>> GetSelectedProjectRolesAsync(int? roleGroupID)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<int>("dbig5_admin.READ_SELECTED_ROLES_FOR_ROLE_GROUP_VIASQLDEV",
                        new { pProjectRoleGroupID = roleGroupID }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }

        public async Task<int> UpdateRoleInGroup(int roleID, int roleGroupID)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QuerySingleAsync<int>("dbig5_admin.UPDATE_ROLE_FOR_ROLE_GROUP_VIASQLDEV",
                        new { pProjectRoleID = roleID, pProjectRoleGroupID = roleGroupID }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return 0;
        }

        public async Task<int> CreateProjectRoleAsync(ProjectRole model)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QuerySingleAsync<int>("dbig5_admin.ADD_PROJECT_ROLE_VIASQLDEV",
                        new { pProjectRoleName = model.ProjectRoleName, pProjectRoleWage = model.ProjectRoleRate }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return 0;
        }

        public async Task<int> EditProjectRoleAsync(ProjectRole model)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QuerySingleAsync<int>("dbig5_admin.UPDATE_PROJECT_ROLE_VIASQLDEV",
                        new { pProjectRoleID = model.ProjectRoleID, pProjectRoleName = model.ProjectRoleName, pProjectRoleWage = model.ProjectRoleRate }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return 0;
        }

        public async Task<int> DeleteProjectRoleAsync(int? id)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QuerySingleAsync<int>("dbig5_admin.DELETE_PROJECT_ROLE_VIASQLDEV",
                        new { pProjectRoleID = id }, commandType: CommandType.StoredProcedure);
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
