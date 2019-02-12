using System;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Identity.Dapper.Connections;
using TCABS.Data.Models.Entities;

namespace TCABS.ViewComponents
{
    [ViewComponent(Name = "UnitSelected")]
    public class UnitSelectedViewComponent : ViewComponent
    {
        private readonly IConnectionProvider _connectionProvider;

        public UnitSelectedViewComponent(IConnectionProvider connection)
        {
            _connectionProvider = connection;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? unitID)
        {
            return View("UnitSelected", await GetUnitAsync(unitID));
        }

        private async Task<UnitAssociation> GetUnitAsync(int? unitID)
        {
            try
            {
                if (unitID != null)
                {
                    using (var connection = _connectionProvider.Create())
                    {
                        return await connection.QuerySingleAsync<UnitAssociation>("dbig5_admin.GET_UNIT_ASSOCIATION_VIASQLDEV",
                            new { pUnitID = unitID }, commandType: CommandType.StoredProcedure);
                    }
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
