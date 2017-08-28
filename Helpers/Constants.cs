using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RfidSPA.Helpers
{
    public class Constants
    {
        public static class Strings
        {
            public static class JwtClaimIdentifiers
            {
                public const string Rol = "rol", Id = "id";
            }

            public static class JwtClaims
            {
                public const string ApiAccess = "api_access";
            }
        }

        public static class UserRolesConst
        {
            public  const string  Administrator = "Administrator";
            public const string StoreAdministrator = "StoreAdministrator";
            public const string StoreOperator = "StoreOperator";
            public const string Default = "Default";
            public const string God = "God";
        } 
    }
}
