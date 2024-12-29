using HRManagementMVCCore31DapperWebApp2.Models;
using HRManagementMVCCore31DapperWebApp2.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRManagementMVCCore31DapperWebApp2.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly DepartmentRepository _departmentRepository;

        public EmployeeController(EmployeeRepository repository, DepartmentRepository departmentRepository)
        {
            _employeeRepository = repository;
            _departmentRepository = departmentRepository;
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            return View(employees);
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            ViewBag.DepartmentList = new List<Department>();
            ViewBag.DepartmentList = _departmentRepository.GetAllDepartmentsAsync().Result.AsEnumerable<Department>().ToList();
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _employeeRepository.AddEmployeeAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewBag.DepartmentList = new List<Department>();
            ViewBag.DepartmentList = _departmentRepository.GetAllDepartmentsAsync().Result.AsEnumerable<Department>().ToList();
            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _employeeRepository.UpdateEmployeeAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _employeeRepository.DeleteEmployeeAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
