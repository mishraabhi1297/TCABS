using System;
using Microsoft.AspNetCore.Authorization;

namespace TCABS.Data.Authorization
{
    public class PolicyRequirement : IAuthorizationRequirement
    {
        public string Scope { get; }

        public PolicyRequirement(string scope)
        {
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
        }
    }
}
