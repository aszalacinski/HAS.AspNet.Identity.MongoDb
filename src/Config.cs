using Microsoft.AspNetCore.Identity;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using System;
using System.Threading;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Microsoft.AspNetCore.Identity.MongoDb
{
    internal static class Config
    {
        private static bool _initialized = false;
        private static object _initializationLock = new object();
        private static object _initializationTarget;

        public static void EnsureConfigured()
        {
            EnsureConfiguredImpl();
        }

        private static void EnsureConfiguredImpl()
        {
            LazyInitializer.EnsureInitialized(ref _initializationTarget, ref _initialized, ref _initializationLock, () =>
            {
                Configure();
                return null;
            });
        }

        private static void Configure()
        {
            RegisterConventions();

            BsonClassMap.RegisterClassMap<IdentityUser>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(c => c.Id)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId))
                    .SetIdGenerator(StringObjectIdGenerator.Instance);

                cm.MapCreator(user => new IdentityUser(user.UserName, user.Email));
            });

            BsonClassMap.RegisterClassMap<UserClaim>(cm =>
            {
                cm.AutoMap();
                cm.MapCreator(c => new UserClaim(c.ClaimType, c.ClaimValue));
            });

            BsonClassMap.RegisterClassMap<UserEmail>(cm =>
            {
                cm.AutoMap();
                cm.MapCreator(cr => new UserEmail(cr.Value));
            });

            BsonClassMap.RegisterClassMap<UserContactRecord>(cm =>
            {
                cm.AutoMap();
            });

            BsonClassMap.RegisterClassMap<UserPhoneNumber>(cm =>
            {
                cm.AutoMap();
                cm.MapCreator(cr => new UserPhoneNumber(cr.Value));
            });

            BsonClassMap.RegisterClassMap<UserLogin>(cm =>
            {
                cm.AutoMap();
                cm.MapCreator(l => new UserLogin(new UserLoginInfo(l.LoginProvider, l.ProviderKey, l.ProviderDisplayName)));
            });

            BsonClassMap.RegisterClassMap<Occurrence>(cm =>
            {
                cm.AutoMap();
                cm.MapCreator(cr => new Occurrence(cr.Instant));
                cm.MapMember(x => x.Instant).SetSerializer(new DateTimeSerializer(DateTimeKind.Utc, BsonType.Document));
            });
        }

        private static void RegisterConventions()
        {
            var pack = new ConventionPack
            {
                new IgnoreIfNullConvention(false),
                new CamelCaseElementNameConvention(),
            };

            ConventionRegistry.Register("AspNetCore.Identity.MongoDB", pack, IsConventionApplicable);
        }

        private static bool IsConventionApplicable(Type type)
        {
            return type == typeof(IdentityUser)
                || type == typeof(UserClaim)
                || type == typeof(UserContactRecord)
                || type == typeof(UserEmail)
                || type == typeof(UserLogin)
                || type == typeof(UserPhoneNumber)
                || type == typeof(ConfirmationOccurrence)
                || type == typeof(FutureOccurrence)
                || type == typeof(Occurrence);
        }
    }
}
