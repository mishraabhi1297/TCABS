using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Identity.Dapper.Connections;
using Identity.Dapper.UnitOfWork.Contracts;
using TCABS.Data.Models;

namespace TCABS.Data.Repository
{
    public class ReportRepository
    {
        private readonly IUnitOfWork _transaction;
        private readonly IConnectionProvider _connectionProvider;

        public ReportRepository(IConnectionProvider connection, IUnitOfWork unitOfWork)
        {
            _transaction = unitOfWork;
            _connectionProvider = connection;
        }


        public async Task<IEnumerable<Report_Project>> GetProjectsAsync(int? id)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<Report_Project>(
                        "dbig5_admin.REPORT_PROJECT_LIST_VIASQLDEV", new { pUnitID = id }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
    

    public async Task<IEnumerable<Report_Supervisor>> GetSupervisorAsync(int? id)
    {
        try
        {
            using (var connection = _connectionProvider.Create())
            {
                return await connection.QueryAsync<Report_Supervisor>(
                    "dbig5_admin.REPORT_LIST_SUPERVISORS_VIASQLDEV", new { pUnitID = id }, commandType: CommandType.StoredProcedure);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return null;
    }

        public async Task<IEnumerable<Report_Convenor>> GetConvenorAsync(int? id)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<Report_Convenor>(
                        "dbig5_admin.REPORT_LIST_REGISTERED_CONVENORS_VIASQLDEV", new { pUnitID = id }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<IEnumerable<Report_Student>> GetStudentAsync(int? id)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<Report_Student>(
                        "dbig5_admin.REPORT_LIST_ENROLLED_STUDENTS_VIASQLDEV", new { pUnitID = id}, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<IEnumerable<Report_Registered_Team>> GetRegisteredTeamsAsync(int? id)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<Report_Registered_Team>(
                        "dbig5_admin.REPORT_REGISTERED_TEAMS_VIASQLDEV", new { pUnitID = id }, commandType: CommandType.StoredProcedure); 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<IEnumerable<Report_Student_Summary>> GetStudentSummaryAsync(int? id)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<Report_Student_Summary>(
                        "dbig5_admin.REPORT_STUDENT_TEAMS_VIASQLDEV", new { pUnitID = id }, commandType: CommandType.StoredProcedure);  
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<IEnumerable<Report_Unit_Overview>> GetUnitOverviewAsync(int? id)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<Report_Unit_Overview>(
                        "dbig5_admin.REPORT_UNIT_OVERVIEW_VIASQLDEV", new { pUnitID = id }, commandType: CommandType.StoredProcedure);  
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<IEnumerable<Report_Project_Budget>> GetProjectBudgetAsync(int? id)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<Report_Project_Budget>(
                        "dbig5_admin.REPORT_PROJECT_BUDGET_VIASQLDEV", new { pUnitID = id }, commandType: CommandType.StoredProcedure);  
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<IEnumerable<Report_Team_Weekly>> GetTeamWeeklyAsync(int? id)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<Report_Team_Weekly>(
                        "dbig5_admin.REPORT_TEAM_WEEKLY_VIASQLDEV", new { pUnitID = id }, commandType: CommandType.StoredProcedure);  
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<IEnumerable<Report_Budget_Weekly>> GetBudgetWeeklyAsync(int? id)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<Report_Budget_Weekly>(
                        "dbig5_admin.REPORT_TEAM_BUDGET_VIASQLDEV", new { pUnitID = id },commandType: CommandType.StoredProcedure);  
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
        
        
    }
}
