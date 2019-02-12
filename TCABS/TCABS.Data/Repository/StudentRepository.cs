﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Identity.Dapper.Connections;
using Identity.Dapper.Entities;
using Identity.Dapper.UnitOfWork.Contracts;
using Microsoft.AspNetCore.Identity;
using TCABS.Data.Models.Admin;

namespace TCABS.Data.Repository
{
    public class StudentRepository
    {
        private readonly UserManager<DapperIdentityUser> _userManager;

        private readonly IUnitOfWork _transaction;
        private readonly IConnectionProvider _connectionProvider;

        public StudentRepository(UserManager<DapperIdentityUser> userManager, IConnectionProvider connection, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _transaction = unitOfWork;
            _connectionProvider = connection;
        }

        public async Task<IEnumerable<UserViewModel>> GetStudentsAsync()
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<UserViewModel>("dbig5_admin.READ_STUDENT_LIST_VIASQLDEV", commandType: CommandType.StoredProcedure);
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

        public async Task<int> CreateStudentAsync(UserViewModel model)
        {
            try
            {
                // Generate Default Password
                var dob = model.DateOfBirth.ToString("ddMMyy");
                // Create temporary User to generate the PasswordHash
                var user = new DapperIdentityUser { UserName = model.Email, Email = model.Email };
                var passwordHash = _userManager.PasswordHasher.HashPassword(user, dob);
                var roleString = model.SelectedRoles.First();
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

        public async Task<int> EditStudentAsync(UserViewModel model)
        {
            try
            {
                // Create objects for CreateEmployee Transaction
                var securityStamp = Guid.NewGuid().ToString("D");
                var roleString = model.SelectedRoles.FirstOrDefault();

                if (roleString == 0)
                {
                    // Didn't select a role.
                    return 0;
                }

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

        public async Task<bool> DeleteStudentAsync(int? id)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    var result = await connection.ExecuteAsync("dbig5_admin.DELETE_STUDENT_VIASQLDEV", new { pStudentID = id }, commandType: CommandType.StoredProcedure);

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