using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Microsoft.AspNetCore.Identity.MongoDb
{
    public class IdentityUser
    {
        private List<UserToken> _tokens;
        private List<UserClaim> _claims;
        private List<UserLogin> _logins;

        private IdentityUser() { }

        public IdentityUser(string userName, string email) : this(userName)
        {
            if (email != null)
            {
                Email = new UserEmail(email);
            }
        }

        public IdentityUser(string userName, UserEmail email) : this(userName)
        {
            if (email != null)
            {
                Email = email;
            }
        }

        public IdentityUser(string userName)
        {
            Id = ObjectId.GenerateNewId().ToString();
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            CreatedOn = new Occurrence();

            EnsureClaimsIsSet();
            EnsureLoginsIsSet();
            EnsureTokensIsSet();
        }

        public string Id { get; private set; }
        public string UserName { get; private set; }
        public string NormalizedUserName { get; private set; }
        public UserEmail Email { get; private set; }

        public UserPhoneNumber PhoneNumber { get; private set; }
        public string PasswordHash { get; private set; }
        public string SecurityStamp { get; private set; }
        public bool IsTwoFactorEnabled { get; private set; }

        public IEnumerable<UserClaim> Claims
        {
            get
            {
                EnsureClaimsIsSet();
                return _claims;
            }

            private set
            {
                EnsureClaimsIsSet();
                if (value != null)
                {
                    _claims.AddRange(value);
                }
            }
        }
        public IEnumerable<UserToken> Tokens
        {
            get
            {
                EnsureTokensIsSet();
                return _tokens;
            }

            private set
            {
                EnsureTokensIsSet();
                if (value != null)
                {
                    _tokens.AddRange(value);
                }
            }
        }
        public IEnumerable<UserLogin> Logins
        {
            get
            {
                EnsureLoginsIsSet();
                return _logins;
            }

            private set
            {
                EnsureLoginsIsSet();
                if (value != null)
                {
                    _logins.AddRange(value);
                }
            }
        }

        public int AccessFailedCount { get; private set; }
        public bool IsLockoutEnabled { get; private set; }
        public FutureOccurrence LockoutEndDate { get; private set; }

        public Occurrence CreatedOn { get; private set; }
        public Occurrence DeletedOn { get; private set; }

        public virtual void EnableTwoFactorAuthentication()
        {
            IsTwoFactorEnabled = true;
        }

        public virtual void DisableTwoFactorAuthentication()
        {
            IsTwoFactorEnabled = false;
        }

        public virtual void EnableLockout()
        {
            IsLockoutEnabled = true;
        }

        public virtual void DisableLockout()
        {
            IsLockoutEnabled = false;
        }

        public virtual void SetEmail(string email)
        {
            var mongoUserEmail = new UserEmail(email);
            SetEmail(mongoUserEmail);
        }

        public virtual void SetEmail(UserEmail mongoUserEmail)
        {
            Email = mongoUserEmail;
        }

        public virtual void SetNormalizedUserName(string normalizedUserName)
        {
            NormalizedUserName = normalizedUserName ?? throw new ArgumentNullException(nameof(normalizedUserName));
        }

        public virtual void SetPhoneNumber(string phoneNumber)
        {
            var mongoUserPhoneNumber = new UserPhoneNumber(phoneNumber);
            SetPhoneNumber(mongoUserPhoneNumber);
        }

        public virtual void SetPhoneNumber(UserPhoneNumber mongoUserPhoneNumber)
        {
            PhoneNumber = mongoUserPhoneNumber;
        }

        public virtual void SetPasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;
        }

        public virtual void SetSecurityStamp(string securityStamp)
        {
            SecurityStamp = securityStamp;
        }

        public virtual void SetAccessFailedCount(int accessFailedCount)
        {
            AccessFailedCount = accessFailedCount;
        }

        public virtual void ResetAccessFailedCount()
        {
            AccessFailedCount = 0;
        }

        public virtual void LockUntil(DateTime lockoutEndDate)
        {
            LockoutEndDate = new FutureOccurrence(lockoutEndDate);
        }
        public virtual void AddToken(UserToken mongoUserToken)
        {
            if (mongoUserToken == null)
            {
                throw new ArgumentNullException(nameof(mongoUserToken));
            }

            _tokens.Add(mongoUserToken);
        }

        public virtual void RemoveToken(UserToken mongoUserToken)
        {
            if (mongoUserToken == null)
            {
                throw new ArgumentNullException(nameof(mongoUserToken));
            }

            _tokens.Remove(mongoUserToken);
        }
        public virtual void AddClaim(Claim claim)
        {
            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }

            AddClaim(new UserClaim(claim));
        }

        public virtual void AddClaim(UserClaim mongoUserClaim)
        {
            if (mongoUserClaim == null)
            {
                throw new ArgumentNullException(nameof(mongoUserClaim));
            }

            _claims.Add(mongoUserClaim);
        }

        public virtual void RemoveClaim(UserClaim mongoUserClaim)
        {
            if (mongoUserClaim == null)
            {
                throw new ArgumentNullException(nameof(mongoUserClaim));
            }

            _claims.Remove(mongoUserClaim);
        }

        public virtual void AddLogin(UserLogin mongoUserLogin)
        {
            if (mongoUserLogin == null)
            {
                throw new ArgumentNullException(nameof(mongoUserLogin));
            }

            _logins.Add(mongoUserLogin);
        }

        public virtual void RemoveLogin(UserLogin mongoUserLogin)
        {
            if (mongoUserLogin == null)
            {
                throw new ArgumentNullException(nameof(mongoUserLogin));
            }

            _logins.Remove(mongoUserLogin);
        }

        public void Delete()
        {
            if (DeletedOn != null)
            {
                throw new InvalidOperationException($"User '{Id}' has already been deleted.");
            }

            DeletedOn = new Occurrence();
        }

        private void EnsureClaimsIsSet()
        {
            if (_claims == null)
            {
                _claims = new List<UserClaim>();
            }
        }
        private void EnsureTokensIsSet()
        {
            if (_tokens == null)
            {
                _tokens = new List<UserToken>();
            }
        }
        private void EnsureLoginsIsSet()
        {
            if (_logins == null)
            {
                _logins = new List<UserLogin>();
            }
        }
    }
}
