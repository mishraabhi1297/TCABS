using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Identity.Dapper.Connections;
using Identity.Dapper.Entities;
using Identity.Dapper.UnitOfWork.Contracts;
using Microsoft.AspNetCore.Identity;
using TCABS.Data.Models.Admin;
using TCABS.Data.Models.Entities;

namespace TCABS.Data.Repository
{
    public class StudyPeriodRepository
    {
        private readonly UserManager<DapperIdentityUser> _userManager;

        private readonly IUnitOfWork _transaction;
        private readonly IConnectionProvider _connectionProvider;

        public StudyPeriodRepository(UserManager<DapperIdentityUser> userManager, IConnectionProvider connection, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _transaction = unitOfWork;
            _connectionProvider = connection;
        }

        public async Task<int> CreateStudyPeriodAsync(StudyPeriod model)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.ExecuteScalarAsync<int>("DBIG5_ADMIN.ADD_STUDYPERIOD_VIASQLDEV",
                        new { pSTUDYPERIOD_NAME = model.StudyPeriod_Name },
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                _transaction.DiscardChanges();
                Console.WriteLine(ex);
                return 0;
            }
        }

        //Get all Study Periods
        public async Task<IEnumerable<StudyPeriod>> GetStudyPeriodsAsync()
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<StudyPeriod>(
                         "DBIG5_ADMIN.READ_STUDYPERIOD_LIST_VIASQLDEV",
                         commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        //Get single study period
        public async Task<StudyPeriod> GetStudyPeriodsAsync(int id)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QuerySingleAsync<StudyPeriod>("DBIG5_ADMIN.READ_STUDYPERIOD_VIASQLDEV",
                        new { pSTUDYPERIOD_ID = id }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<bool> EditStudyPeriodAsync(StudyPeriod model)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.ExecuteScalarAsync<bool>("DBIG5_ADMIN.UPDATE_STUDYPERIOD_VIASQLDEV",
                        new { pSTUDYPERIOD_ID = model.StudyPeriod_ID, pSTUDYPERIOD_NAME = model.StudyPeriod_Name },
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                //rollback trans
                _transaction.DiscardChanges();
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<int> DeleteStudyPeriodAsync(int? id)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.ExecuteScalarAsync<int>("dbig5_admin.DELETE_STUDYPERIOD_VIASQLDEV",
                        new { pSTUDYPERIOD_ID = id }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                _transaction.DiscardChanges();
                Console.WriteLine(ex);
            }
            return 0;
        }
    }
}
