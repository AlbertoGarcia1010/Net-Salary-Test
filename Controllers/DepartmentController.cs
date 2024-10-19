using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Salary.App.Utils;
using Salary.Models.Data;
using Salary.Models.DBContext;
using Salary.Models.Entities;
using Salary.Models.ViewModel;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace Salary.Controllers
{
    public class DepartmentController: PrivateBaseController
    {
        private readonly IDepartmentData iDepartmentData;

        public DepartmentController(ILogger<DepartmentController> logger, IDepartmentData iDepartmentData) : base(logger)
        {
            this.iDepartmentData = iDepartmentData;
        }

        public IActionResult Index()
        {
            return View();
        }

        // [IsAuthorizedView("Department", "Index")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("DepartmentID,Code,Name,StatusID,DateCreate")] DepartmentVM departmentVM)
        {

            Department department = dataConvertion(departmentVM);
            if (ModelState.IsValid)
            {
                iDepartmentData.Insert(department);

                return RedirectToAction("Index");
            }

            return View(departmentVM);
        }

        // [IsAuthorizedView("Department", "Index")]
        public IActionResult Details(string id)
        {
            Console.WriteLine("Detail: " + id);

            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            Department department = iDepartmentData.Get(id);
            if (department == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.NotFound);
            }

            return View(department);
        }

        // [IsAuthorizedView("Department", "Index")]
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            Department department = iDepartmentData.Get(id);
            if (department == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.NotFound);
            }

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("DepartmentID,Code,Name,StatusID,DateCreate,DateUpdate")] DepartmentVM departmentVM)
        {
            Console.WriteLine("Edit Department: " + JsonSerializer.Serialize(departmentVM));

            int isUpdate = 1;
            Department department = dataConvertion(departmentVM, isUpdate);

            if (ModelState.IsValid)
            {
                iDepartmentData.Edit(department);

                return RedirectToAction("Index");
            }
            return View(departmentVM);
        }

        // [IsAuthorizedView("Department", "Index")]
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            Department department = iDepartmentData.Get(id);
            if (department == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.NotFound);
            }

            return View(department);
        }

        public IActionResult LoadDepartments()
        {
            Console.WriteLine("LoadDepartments");

            int start = Convert.ToInt32(Request.Query["start"]);
            int length = Convert.ToInt32(Request.Query["length"]);
            string searchValue = Request.Query["search[value]"];
            string sortColumnName = Request.Query["columns[" + Request.Query["order[0][column]"] + "][name]"];
            string sortDirection = Request.Query["order[0][dir]"];

            ListDataModel list = iDepartmentData.List();

            Console.WriteLine("Departments: " + JsonSerializer.Serialize(list));

            return Json(new { data = list.data, draw = Request.Query["draw"], recordsTotal = list.totalrows, recordsFiltered = list.rowsfiltered });
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(string id)
        {
            Console.WriteLine("id : " + id);

            Department department = iDepartmentData.Get(id);
            Console.WriteLine("DeleteConfirmed : " + JsonSerializer.Serialize(department));

            Boolean success = iDepartmentData.Delete(department);
            if (success)
            {
                // TODO: check new format json response
                return Json(new { result = "Ok", url = "", msg = "Departamento Eliminado" });
            }
            else
            {
                // TODO: check new format json response
                return Json(new { result = "Error", url = "", msg = "Error al eliminar" });
            }

        }

        private Department dataConvertion(DepartmentVM departmentVM, int isUpdate = 0)
        {
            Department department = new Department();
            DateTime date = DateTime.Now;
            string dateFormatCustom = date.ToString("yyyy/MM/dd HH:mm:ss");

            department.Code = departmentVM.Code;
            department.Name = departmentVM.Name;
            department.StatusID = 1;
            if (isUpdate > 0)
            {
                department.DepartmentID = departmentVM.DepartmentID;
                department.DateUpdate = DateTime.Parse(dateFormatCustom);
                department.DateCreate = departmentVM.DateCreate;

            }
            else
            {
                department.DateCreate = DateTime.Parse(dateFormatCustom);
            }

            return department;
        }
    }
}
