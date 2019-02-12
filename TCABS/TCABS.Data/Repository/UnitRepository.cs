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
    public class UnitRepository
    {
        private readonly UserManager<DapperIdentityUser> _userManager;

        private readonly IUnitOfWork _transaction;
        private readonly IConnectionProvider _connectionProvider;

        public UnitRepository(UserManager<DapperIdentityUser> userManager, IConnectionProvider connection, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _transaction = unitOfWork;
            _connectionProvider = connection;
        }

        public async Task<int> CreateUnitAsync(Unit model)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.ExecuteScalarAsync<int>("dbig5_admin.ADD_UNIT_VIASQLDEV",
                        new
                        {
                            pUNIT_CODE = model.Unit_Code,
                            pUNIT_NAME = model.Unit_Name
                        },
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return 0;
        }

        public async Task<IEnumerable<Unit>> FetchUnitListAsync()
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<Unit>("dbig5_admin.READ_UNIT_LIST_VIASQLDEV",
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<int> DeleteUnitAsync(int? id)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.ExecuteScalarAsync<int>("dbig5_admin.DELETE_UNIT_VIASQLDEV",
                        new { pUNIT_ID = id }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return 0;
        }

        //update unit
        public async Task<int> UpdateUnitAsync(Unit model)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.ExecuteScalarAsync<int>("dbig5_admin.UPDATE_UNIT_VIASQLDEV",
                        new
                        {
                            pUNIT_ID = model.Unit_Id,
                            pUNIT_CODE = model.Unit_Code,
                            pUNIT_NAME = model.Unit_Name
                        },
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
