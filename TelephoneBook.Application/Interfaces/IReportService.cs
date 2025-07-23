using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.Domain.Common;
using TelephoneBook.Domain.Entities;

namespace TelephoneBook.Application.Interfaces
{
    public interface IReportService
    {
        Task<Result<Report>> CreateReportAsync(string location);
        Task<Result<List<Report>>> GetAllReportsAsync();
        Task<Result<Report?>> GetReportByIdAsync(string id);
        Task<Result<Report>> ReportCompletedAsync(string id);
    }
}
