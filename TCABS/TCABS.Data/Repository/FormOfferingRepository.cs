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

namespace TCABS.Data.Repository
{
    public class FormOfferingRepository
    {
        private readonly UserManager<DapperIdentityUser> _userManager;

        private readonly IUnitOfWork _transaction;
        private readonly IConnectionProvider _connectionProvider;

        public FormOfferingRepository(UserManager<DapperIdentityUser> userManager, IConnectionProvider connection, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _transaction = unitOfWork;
            _connectionProvider = connection;
        }
        //param @Context.Session.GetInt32("SELECTED_UNIT")
        public async Task<IEnumerable<FormOfferingViewModel>> GetFormOfferingsAsync(int? id)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<FormOfferingViewModel>("dbig5_admin.READ_FORMOFFERING_LIST_VIASQLDEV", 
                        new { pUNITOFFERING_ID = id },
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<IEnumerable<int>> GetRolesForUserAsync(int userID)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<int>("dbig5_admin.READ_ROLES_FOR_USER_VIASQLDEV", new { pUserID = userID }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<int> CreateEmployeeAsync(UserViewModel model)
        {
            try
            {
                // Create objects for CreateEmployee Transaction

                // Generate Default Password
                var dob = model.DateOfBirth.ToString("ddMMyy");
                // Create temporary User to generate the PasswordHash
                var user = new DapperIdentityUser { UserName = model.Email, Email = model.Email };
                var passwordHash = _userManager.PasswordHasher.HashPassword(user, dob);
                var roleString = string.Join(',', model.SelectedRoles);
                var securityStamp = Guid.NewGuid().ToString("D");

                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QuerySingleAsync<int>("dbig5_admin.ADD_USER_VIASQLDEV", new
                    {
                        pEmail = model.Email,
                        pPasswordHash = passwordHash,
                        pSecurityStamp = securityStamp,
                        pFirstname = model.FirstName,
                        pLastname = model.LastName,
                        pDateOfBirth = model.DateOfBirth.ToString("ddMMyy"),
                        pAddress = model.Address,
                        pUniqueID = model.UniqueID,
                        pRoleString = roleString
                    }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            // Return "Fail to Insert"
            return 0;
        }

        public async Task<int> EditEmployeeAsync(UserViewModel model)
        {
            try
            {
                // Create objects for CreateEmployee Transaction
                var securityStamp = Guid.NewGuid().ToString("D");
                var roleString = string.Join(',', model.SelectedRoles);

                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QuerySingleAsync<int>("dbig5_admin.UPDATE_USER_VIASQLDEV", new
                    {
                        pUserID = model.ID,
                        pEmail = model.Email,
                        pSecurityStamp = securityStamp,
                        pFirstname = model.FirstName,
                        pLastname = model.LastName,
                        pDateOfBirth = model.DateOfBirth.ToString("ddMMyy"),
                        pAddress = model.Address,
                        pUniqueID = model.UniqueID,
                        pRoleString = roleString
                    }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            // Return "Fail to Insert"
            return 0;
        }

        public async Task<bool> DeleteEmployeeAsync(int? id)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    var result = await connection.ExecuteAsync("dbig5_admin.DELETE_EMPLOYEE_VIASQLDEV", new { pEmpID = id }, commandType: CommandType.StoredProcedure);

                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }
    }
}
