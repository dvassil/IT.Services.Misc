
namespace IT.Services.Misc
{
    public interface IQueryProcessor
    {
        string Process(string query);
        string BuildSPCall(string procedureName, string[] arguments);
    }
}
