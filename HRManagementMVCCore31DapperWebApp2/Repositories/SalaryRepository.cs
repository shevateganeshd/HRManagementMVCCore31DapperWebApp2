using Dapper;
using HRManagementMVCCore31DapperWebApp2.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HRManagementMVCCore31DapperWebApp2.Repositories
{
    public class SalaryRepository
    {
        private readonly IDbConnection _dbConnection;

        public SalaryRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Salary>> GetAllSalariesAsync()
        {
            const string query = "SELECT * FROM Salary";
            return await _dbConnection.QueryAsync<Salary>(query);
        }

        public async Task<Salary> GetSalaryByIdAsync(int id)
        {
            const string query = "SELECT * FROM Salary WHERE SalaryId = @SalaryId";
            return await _dbConnection.QueryFirstOrDefaultAsync<Salary>(query, new { SalaryId = id });
        }

        public async Task<int> AddSalaryAsync(Salary salary)
        {
            const string query = @"INSERT INTO Salary (EmployeeId, Basic, HRA, TA, Total) 
                        VALUES (@EmployeeId, @Basic, @HRA, @TA, @Total)";
            return await _dbConnection.ExecuteAsync(query, salary);
        }

        public async Task<int> UpdateSalaryAsync(Salary salary)
        {
            const string query = @"UPDATE Salary 
                        SET EmployeeId=@EmployeeId, Basic=@Basic, HRA=@HRA, TA=@TA, Total=@Total
                        WHERE SalaryId = @SalaryId";
            return await _dbConnection.ExecuteAsync(query, salary);
        }

        public async Task<int> DeleteSalaryAsync(int id)
        {
            const string query = "DELETE FROM Salary WHERE SalaryId = @SalaryId";
            return await _dbConnection.ExecuteAsync(query, new { SalaryId = id });
        }
    }
}
