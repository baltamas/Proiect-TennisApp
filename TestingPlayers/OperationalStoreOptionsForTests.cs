using IdentityServer4.EntityFramework.Options;
using Microsoft.Extensions.Options;

namespace TestingPlayers
{
   
    public class OperationalStoreOptionsForTests : IOptions<OperationalStoreOptions>
    {
        public OperationalStoreOptions Value => new OperationalStoreOptions();
    }

}