using System.Threading.Tasks;

namespace OkVip.Gamble.Data;

public interface IGambleDbSchemaMigrator
{
	Task MigrateAsync();
}