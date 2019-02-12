using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace TCABS.Data.Authorization
{
    public class HasScopeHandler : AuthorizationHandler<PolicyRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PolicyRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Value == requirement.Scope))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
