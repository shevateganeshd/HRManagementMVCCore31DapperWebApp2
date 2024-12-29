using Dapper;
using HRManagementMVCCore31DapperWebApp2.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HRManagementMVCCore31DapperWebApp2.Repositories
{
    public class EmployeeRepository
    {
        private readonly IDbConnection _dbConnection;

        public EmployeeRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            const string query = "SELECT emp.EmployeeId, emp.FirstName, emp.MiddleName, emp.LastName, emp.Address, emp.Email, emp.Phone, dept.DepartmentName FROM Employee emp INNER JOIN Department dept ON dept.DepartmentId=emp.DepartmentId";
            return await _dbConnection.QueryAsync<Employee>(query);
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            const string query = "SELECT emp.EmployeeId, emp.FirstName, emp.MiddleName, emp.LastName, emp.Address, emp.Email, emp.Phone, dept.DepartmentName FROM Employee emp INNER JOIN Department dept ON dept.DepartmentId=emp.DepartmentId WHERE EmployeeId = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Employee>(query, new { Id = id });
        }

        public async Task<int> AddEmployeeAsync(Employee employee)
        {
            const string query = @"INSERT INTO Employee (FirstName, MiddleName, LastName, Address, Email, Phone, DepartmentId) 
                        VALUES (@FirstName, @MiddleName, @LastName, @Address, @Email, @Phone, @DepartmentId)";
            return await _dbConnection.ExecuteAsync(query, employee);
        }

        public async Task<int> UpdateEmployeeAsync(Employee employee)
        {
            const string query = @"UPDATE Employee 
                        SET FirstName = @FirstName, MiddleName=@MiddleName, LastName = @LastName, Address=@Address, Email=@Email, Phone=@Phone, DepartmentId = @DepartmentId
                        WHERE EmployeeId = @EmployeeId";
            return await _dbConnection.ExecuteAsync(query, employee);
        }

        public async Task<int> DeleteEmployeeAsync(int id)
        {
            const string query = "DELETE FROM Employee WHERE EmployeeId = @EmployeeId";
            return await _dbConnection.ExecuteAsync(query, new { EmployeeId = id });
        }
    }
}
