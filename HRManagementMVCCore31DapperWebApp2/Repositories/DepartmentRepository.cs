using Dapper;
using HRManagementMVCCore31DapperWebApp2.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HRManagementMVCCore31DapperWebApp2.Repositories
{
    public class DepartmentRepository
    {
        private readonly IDbConnection _dbConnection;

        public DepartmentRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            const string query = "SELECT * FROM Department";
            return await _dbConnection.QueryAsync<Department>(query);
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            const string query = "SELECT * FROM Department WHERE DepartmentId = @DepartmentId";
            return await _dbConnection.QueryFirstOrDefaultAsync<Department>(query, new { DepartmentId = id });
        }

        public async Task<int> AddDepartmentAsync(Department department)
        {
            const string query = @"INSERT INTO Department (DepartmentName) 
                        VALUES (@DepartmentName)";
            return await _dbConnection.ExecuteAsync(query, department);
        }

        public async Task<int> UpdateDepartmentAsync(Department department)
        {
            const string query = @"UPDATE Department 
                        SET DepartmentName = @DepartmentName
                        WHERE DepartmentId = @DepartmentId";
            return await _dbConnection.ExecuteAsync(query, department);
        }

        public async Task<int> DeleteDepartmentAsync(int id)
        {
            const string query = "DELETE FROM Department WHERE DepartmentId = @DepartmentId";
            return await _dbConnection.ExecuteAsync(query, new { DepartmentId = id });
        }
    }
}
