using Microsoft.AspNetCore.Mvc;
using HandsOnMVCEFCoreCodeFirst_Demo2.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HandsOnMVCEFCoreCodeFirst_Demo2.Controllers
{
    public class EmployeeController : Controller
    {
        private ZuciDB324Context _context;
        public EmployeeController()
        {
            _context = new ZuciDB324Context();
        }
        public IActionResult Index() //Get All Employees
        {
            List<Employee> employees = _context.Employees.ToList();
            return View(employees);
        }
        public IActionResult Create() //Create New Employee
        {
            List<Project> projects= _context.Projects.ToList(); //retun list of projects from the table
            List<SelectListItem> items = new List<SelectListItem>();
            //add project details to SelectListItem
            foreach(var project in projects)
            {
                items.Add(new SelectListItem() { Text = project.ProjectName, Value = project.ProjectCode });
            }
            ViewBag.Projects = items;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            _context.Employees.Add(employee); //add new employee details to table
            _context.SaveChanges(); //add new record to a table permanently
            return RedirectToAction("Index");
        }
        public IActionResult Details(int EmployeeId)
        {
           // Employee employee = _context.Employees.Find(EmployeeId); //Find() used when searching using primary key column
           Employee employee=_context.Employees.SingleOrDefault(e=>e.EmployeeId == EmployeeId); //SingleOrDefault() used when searching using non primary key column
            return View(employee);

        }
        public IActionResult Delete(int EmployeeId)
        {
            Employee employee = _context.Employees.Find(EmployeeId);
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int EmployeeId)
        {
            List<Project> projects = _context.Projects.ToList(); //retun list of projects from the table
            List<SelectListItem> items = new List<SelectListItem>();
            //add project details to SelectListItem
            foreach (var project in projects)
            {
                items.Add(new SelectListItem() { Text = project.ProjectName, Value = project.ProjectCode });
            }
            ViewBag.Projects = items;
            Employee employee = _context.Employees.Find(EmployeeId);
            return View(employee);
        }
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
