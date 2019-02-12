using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Identity.Dapper.Connections;
using TCABS.Data.Models.Entities;

namespace TCABS.ViewComponents
{
    [ViewComponent(Name = "UnitSelector")]
    public class UnitSelectorViewComponent : ViewComponent
    {
        private readonly IConnectionProvider _connectionProvider;

        public UnitSelectorViewComponent(IConnectionProvider connection)
        {
            _connectionProvider = connection;
        }

        public async Task<IViewComponentResult> InvokeAsync(string userID) => View("UnitSelector", await GetUnitsAsync(Convert.ToInt32(userID)));

        private async Task<IEnumerable<UnitAssociation>> GetUnitsAsync(int userID)
        {
            try
            {
                if (userID != 0)
                {
                    using (var connection = _connectionProvider.Create())
                    {
                        return await connection.QueryAsync<UnitAssociation>("dbig5_admin.READ_UNITS_FOR_USER_VIASQLDEV",
                            new { pUserID = userID }, commandType: CommandType.StoredProcedure);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return new List<UnitAssociation>();
        }
    }
}
