using System;
using System.Security.Claims;

namespace Microsoft.AspNetCore.Identity.MongoDb
{
    public class UserClaim : IEquatable<UserClaim>, IEquatable<Claim>
    {
        public UserClaim(Claim claim)
        {

            if (claim is null) { throw new ArgumentNullException(nameof(claim)); }

            ClaimType = claim.Type;
            ClaimValue = claim.Value;
        }

        public UserClaim(string claimType, string claimValue)
        {
            if (claimType is null) { throw new ArgumentNullException(nameof(claimType)); }
            if (claimValue is null) { throw new ArgumentNullException(nameof(claimValue)); }

        }

        public string ClaimType { get; private set; }
        public string ClaimValue { get; private set; }

        public bool Equals(UserClaim other)
        {
            return other.ClaimType.Equals(ClaimType)
                && other.ClaimValue.Equals(ClaimValue);
        }

        public bool Equals(Claim other)
        {
            return other.Type.Equals(ClaimType)
                && other.Value.Equals(ClaimValue);
        }
    }
}
