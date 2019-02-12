using System;
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
    public class FormRepository
    {
        private readonly UserManager<DapperIdentityUser> _userManager;

        private readonly IUnitOfWork _transaction;
        private readonly IConnectionProvider _connectionProvider;

        public FormRepository(UserManager<DapperIdentityUser> userManager, IConnectionProvider connection, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _transaction = unitOfWork;
            _connectionProvider = connection;
        }
        /*


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
        */
        public async Task<int> CreateFormCategoryAsync(FormCategoryViewModel model)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QuerySingleAsync<int>("dbig5_admin.ADD_CATEGORY_VIASQLDEV", new
                    {
                        pCATEGORY_TYPE = model.Category_Type
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

        public async Task<int> CreateFOAsync(FormViewModel model, int? id)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.ExecuteScalarAsync<int>("dbig5_admin.ADD_FO_VIASQLDEV", new
                    {
                        pFORM_NAME = model.Form_Name,
                        pUNITOFFERING_ID = id,
                        pDueDate = model.DueDate
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


        public async Task<int> CreateFormAsync(FormViewModel model)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QuerySingleAsync<int>("dbig5_admin.ADD_FORM_VIASQLDEV", new
                    {
                        pFORM_NAME = model.Form_Name
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

        //param @Context.Session.GetInt32("SELECTED_UNIT")
        public async Task<IEnumerable<FormOfferingViewModel>> GetFormOfferingsAsync(int? id)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<FormOfferingViewModel>("dbig5_admin.READ_FORMOFFERING_LIST_VIASQLDEV", 
                        new { pUNITOFFERING_ID = id},
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<IEnumerable<FormCategoryViewModel>> GetFormCategoriesAsync()
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<FormCategoryViewModel>("dbig5_admin.READ_CATEGORY_LIST_VIASQLDEV", commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<IEnumerable<FormQuestionViewModel>> GetFormItemsAsync(int id)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<FormQuestionViewModel>("dbig5_admin.READ_FORM_ITEMS_VIASQLDEV",
                        new { pFORM_ID = id },
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<IEnumerable<FormQuestionViewModel>> GetFormQuestionsAsync(int id)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<FormQuestionViewModel>("dbig5_admin.READ_QC_LIST_VIASQLDEV",
                        new { pCATEGORY_ID = id },
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
        //create qcasync
        public async Task<int> CreateFormQuestionAsync(FormCategoryViewModel model)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.ExecuteScalarAsync<int>("dbig5_admin.ADD_QC_VIASQLDEV",
                        new { pQuestion_Text = model.Question_Text, pCATEGORY_ID = model.Category_ID},
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


        public async Task<int> EditStudentAsync(UserViewModel model)
        {
            try
            {
                // Create objects for CreateEmployee Transaction
                var securityStamp = Guid.NewGuid().ToString("D");
                var roleString = model.SelectedRoles.First();

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
