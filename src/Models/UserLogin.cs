using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Identity.MongoDb
{
    public class UserLogin : IEquatable<UserLogin>, IEquatable<UserLoginInfo>
    {
        public UserLogin(UserLoginInfo loginInfo)
        {
            if (loginInfo is null) { throw new ArgumentNullException(nameof(loginInfo)); }

            LoginProvider = loginInfo.LoginProvider;
            ProviderKey = loginInfo.ProviderKey;
            ProviderDisplayName = loginInfo.ProviderDisplayName;
        }

        public string LoginProvider { get; private set; }
        public string ProviderKey { get; private set; }
        public string ProviderDisplayName { get; private set; }

        public bool Equals(UserLogin other)
        {
            return other.LoginProvider.Equals(LoginProvider)
                && other.ProviderKey.Equals(ProviderKey);
        }

        public bool Equals(UserLoginInfo other)
        {
            return other.LoginProvider.Equals(LoginProvider)
                && other.ProviderKey.Equals(ProviderKey);
        }
    }
}
