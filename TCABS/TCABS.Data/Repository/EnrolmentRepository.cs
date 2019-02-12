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
    public class EnrolmentRepository
    {
        private readonly UserManager<DapperIdentityUser> _userManager;

        private readonly IUnitOfWork _transaction;
        private readonly IConnectionProvider _connectionProvider;

        public EnrolmentRepository(UserManager<DapperIdentityUser> userManager, IConnectionProvider connection, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _transaction = unitOfWork;
            _connectionProvider = connection;
        }

        public async Task<int> CreateEnrolmentAsync(Enrolment model)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.ExecuteScalarAsync<int>("DBIG5_ADMIN.ADD_ENROLMENT_VIASQLDEV",
                        new
                        {
                            pId = model.Student_ID,
                            pUNITOFFERING_ID = model.UnitOffering_ID
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

        public async Task<IEnumerable<Enrolment>> FetchAllEnrolmentsAsync()
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<Enrolment>(
                        "DBIG5_ADMIN.READ_ENROLMENT_LIST_VIASQLDEV", commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<IEnumerable<Enrolment>> GetStudentListAsync()
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<Enrolment>(
                        "DBIG5_ADMIN.READ_STUDENT_FOR_DROPDOWN_LIST_VIASQLDEV", commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<IEnumerable<Enrolment>> GetUnitOfferingListAsync()
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<Enrolment>(
                        "DBIG5_ADMIN.READ_UNITOFFERING_FOR_DROPDOWN_LIST_VIASQLDEV", commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<int> EditEnrolmentAsync(Enrolment model)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.ExecuteScalarAsync<int>("DBIG5_ADMIN.UPDATE_ENROLMENT_VIASQLDEV",
                        new
                        {
                            pENROLMENT_ID = model.Enrolment_ID,
                            pId = model.Student_ID,
                            pUNITOFFERING_ID = model.UnitOffering_ID
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
    }
}
