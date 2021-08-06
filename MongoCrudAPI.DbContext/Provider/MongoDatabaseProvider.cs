
namespace MongoCrudAPI.DbContext.Provider
{
    public class MongoDatabaseProvider : IMongoDatabaseProvider
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
