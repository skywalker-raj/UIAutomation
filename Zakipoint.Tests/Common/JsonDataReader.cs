using Microsoft.Extensions.Configuration;
namespace Zakipoint.Tests.Common
{
    public static class JsonDataReader
    {
        #region public Properties
        public static readonly IConfiguration Data = new ConfigurationBuilder().AddJsonFile(@"Data/Data.json").Build();
        #endregion
    }
}