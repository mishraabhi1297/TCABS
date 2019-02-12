using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Identity.Dapper.Connections;
using Identity.Dapper.Entities;
using Microsoft.AspNetCore.Identity;
using TCABS.Data.Models.AdminViewModels;
using TCABS.Data.Models.Entities;

namespace TCABS.Data.Repository
{
    public class RoleRepository
    {
        private readonly RoleManager<DapperIdentityRole> _roleManager;
        private readonly IConnectionProvider _connectionProvider;

        public RoleRepository(RoleManager<DapperIdentityRole> roleManager, IConnectionProvider connection)
        {
            _roleManager = roleManager;
            _connectionProvider = connection;
        }

        public async Task<IEnumerable<IdentityRole>> GetIdentityRolesAsync()
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<IdentityRole>("dbig5_admin.READ_IDENTITYROLE_LIST_VIASQLDEV", commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<int> CreateIdentityRoleAsync(RoleClaimViewModel model)
        {
            try
            {
                var encodedString = string.Join(',', model.SelectedClaims);

                using (var connection = _connectionProvider.Create())
                {
                    return await connection.ExecuteScalarAsync<int>("dbig5_admin.ADD_IDENTITYROLE_VIASQLDEV", new { pIdentityRoleName = model.RoleName, pEncodedString = encodedString }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            // Return "Fail to Insert"
            return 0;
        }

        public async Task<IEnumerable<IdentityClaim>> GetIdentityClaimListAsync()
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<IdentityClaim>("dbig5_admin.READ_IDENTITYCLAIM_LIST_VIASQLDEV",
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<int> CreateRoleClaimsAsync(RoleClaimViewModel model)
        {
            try
            {
                var encodedString = model.RoleID.ToString() + ',' + string.Join(',', model.SelectedClaims);

                using (var connection = _connectionProvider.Create())
                {
                    return await connection.ExecuteScalarAsync<int>("dbig5_admin.ADD_CLAIMS_TO_ROLE_VIASQLDEV", new { pEncodedString = encodedString }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            // Return "Fail to Insert"
            return 0;
        }

        public async Task<IEnumerable<ClaimCheckboxViewModel>> GetIdentityClaimsForRoleAsync(int roleID)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<ClaimCheckboxViewModel>("dbig5_admin.READ_CLAIMS_FOR_ROLE_VIASQLDEV", new { pRoleID = roleID }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<bool> DeleteRoleAsync(int? id)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    var result = await connection.ExecuteAsync("dbig5_admin.DELETE_ACCESS_ROLE_VIASQLDEV", new { pRoleID = id }, commandType: CommandType.StoredProcedure);

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
