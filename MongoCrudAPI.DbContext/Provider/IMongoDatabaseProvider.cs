
namespace MongoCrudAPI.DbContext.Provider
{
    public interface IMongoDatabaseProvider
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
