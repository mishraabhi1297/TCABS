using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Identity.Dapper.Connections;
using Identity.Dapper.UnitOfWork.Contracts;
using TCABS.Data.Models.Entities;

namespace TCABS.Data.Repository
{
    public class ComponentRepository
    {
        private readonly IUnitOfWork _transaction;
        private readonly IConnectionProvider _connectionProvider;

        public ComponentRepository(IConnectionProvider connection, IUnitOfWork unitOfWork)
        {
            _transaction = unitOfWork;
            _connectionProvider = connection;
        }

        public async Task<IEnumerable<UnitAssociation>> GetUnitsForUserAsync(int userID)
        {
            try
            {
                using (var connection = _connectionProvider.Create())
                {
                    return await connection.QueryAsync<UnitAssociation>("dbig5_admin.READ_UNITS_FOR_USER_VIASQLDEV",
                        new { pUserID = userID }, commandType: CommandType.StoredProcedure);
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
