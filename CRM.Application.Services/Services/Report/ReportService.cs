using CRM.Application.Abstraction;

namespace CRM.Application.Services
{
    public class ReportService : IReportService
    {
        public async Task<Dictionary<string, int>> GetNewClientsCount()
        {
            Dictionary<string, int> records = new()
            {
                { "Styczeń", 3 },
                { "Luty", 5 },
                { "Marzec", 11 },
                { "Kwiecień", 7 },
                { "Maj", 5 },
                { "Czerwiec", 6 },
                { "Lipiec", 9 },
                { "Sierpień", 10 },
                { "Wrzesień", 4 },
                { "Październik", 3 },
                { "Listopad", 7 },
                { "Grudzień", 6 }
            };

            return records;
        }

        public async Task<Dictionary<string, int>> GetNewTasksCount()
        {
            Dictionary<string, int> records = new()
            {
                { "Styczeń", 12 },
                { "Luty", 3 },
                { "Marzec", 7 },
                { "Kwiecień", 9 },
                { "Maj", 10 },
                { "Czerwiec", 8 },
                { "Lipiec", 6 },
                { "Sierpień", 5 },
                { "Wrzesień", 7 },
                { "Październik", 9 },
                { "Listopad", 10 },
                { "Grudzień", 8 }
            };

            return records;
        }
    }
}
