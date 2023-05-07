namespace CRM.Application.Abstraction
{
    public interface IReportService
    {
        Task<Dictionary<string, int>> GetNewClientsCount();
        Task<Dictionary<string, int>> GetNewTasksCount();
    }
}
