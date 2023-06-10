using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcSqlCrudOpt.Data;
using MvcSqlCrudOpt.Models;
using MvcSqlCrudOpt.Models.Domain;

namespace MvcSqlCrudOpt.Controllers
{


    public class EmployeeController : Controller
    {
        private readonly MvcDemoDbContext mvcDemoDbContext;

        public EmployeeController(MvcDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }

        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var employees = await mvcDemoDbContext.Employees.ToListAsync();
            return View(employees);


        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DateOfBirth = addEmployeeRequest.DateOfBirth,

            };

            await mvcDemoDbContext.Employees.AddAsync(employee);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Add");
        }
        [HttpGet]
        public async Task<IActionResult> view(Guid id)
        {
            var employee = await mvcDemoDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth,

                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");

        }

        [HttpPost] 
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await mvcDemoDbContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.Department = model.Department;
                employee.DateOfBirth = model.DateOfBirth;

                await mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");

            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await mvcDemoDbContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                mvcDemoDbContext.Employees.Remove(employee);
                await mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }


            return RedirectToAction("Index");
        } 
    }
}
