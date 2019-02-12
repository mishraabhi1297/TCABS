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
    public class UnitOfferingRepository
    {
        private readonly UserManager<DapperIdentityUser> _userManager;

        private readonly IUnitOfWork _transaction;
        private readonly IConnectionProvider _connectionProvider;

        public UnitOfferingRepository(UserManager<DapperIdentityUser> userManager, IConnectionProvider connection, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _transaction = unitOfWork;
            _connectionProvider = connection;
        }

        public async Task<int> CreateUnitOfferingAsync(UnitOffering model)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.ExecuteScalarAsync<int>("DBIG5_ADMIN.ADD_UNITOFFERING_VIASQLDEV",
                        new
                        {
                            pUNIT_ID = model.Unit_ID,
                            pSTUDYPERIOD_ID = model.StudyPeriod_ID,
                            pId = model.Id,
                            pUNITOFFERING_STARTDATE = model.UnitOffering_StartDate
                        },
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                _transaction.DiscardChanges();
                Console.WriteLine(ex);
            }
            //Return failed to insert
            return 0;
        }

        public async Task<UnitOffering> FetchSingleUnitOfferingAsync(int id)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QuerySingleAsync<UnitOffering>("dbig5_admin.READ_UNITOFFERING_VIASQLDEV",
                        new { pUNITOFFERING_ID = id }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<int> EditUnitOffering(UnitOffering model)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.ExecuteScalarAsync<int>("DBIG5_ADMIN.UPDATE_UNITOFFERING_VIASQLDEV",
                        new
                        {
                            pUNITOFFERING_ID = model.UnitOffering_ID,
                            pUNIT_ID = model.Unit_ID,
                            pSTUDYPERIOD_ID = model.StudyPeriod_ID,
                            pId = model.Id,
                            pSTARTDATE = model.UnitOffering_StartDate
                        },
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                _transaction.DiscardChanges();
                Console.WriteLine(ex);
            }
            return 0;
        }

        public async Task<IEnumerable<StudyPeriod>> GetStudyPeriodListAsync()
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<StudyPeriod>(
                        "DBIG5_ADMIN.READ_STUDYPERIOD_LIST_VIASQLDEV", commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<IEnumerable<UnitOffering>> FetchAllUnitOfferingsAsync()
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<UnitOffering>(
                        "DBIG5_ADMIN.READ_UNITOFFERING_LIST_VIASQLDEV", commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<IEnumerable<Unit>> GetUnitListAsync()
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<Unit>("DBIG5_ADMIN.GET_ALL_UNITS_VIASQLDEV",
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<int> DeleteUnitOffering(int? id)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    //sp will take care of the deletion as parameter value will 
                    //evaulate against SQL GETDATE() func. A unit offering will be deleted
                    return await connection.ExecuteScalarAsync<int>("DBIG5_ADMIN.DELETE_UNITOFFERING_VIASQLDEV",
                        new { pUNITOFFERING_ID = id },
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                _transaction.DiscardChanges();
                Console.WriteLine(ex);
            }
            return 0;
        }

        public async Task<IEnumerable<UnitOffering>> GetEmployeesAsync()
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<UnitOffering>("DBIG5_ADMIN.GET_CONVENOR_LIST_VIASQLDEV",
                        commandType: CommandType.StoredProcedure);
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
