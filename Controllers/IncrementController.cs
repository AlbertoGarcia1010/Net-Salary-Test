using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Salary.App.Utils;
using Salary.Models.Data;
using Salary.Models.Entities;
using Salary.Models.ViewModel;
using System.Text.Json;

namespace Salary.Controllers
{
    public class IncrementController: PrivateBaseController
    {
        private readonly IIncreaseData iIncreaseData;
        private readonly IDepartmentData iDepartmentData;

        public IncrementController(ILogger<IncrementController> logger, IIncreaseData iIncreaseData,
            IDepartmentData iDepartmentData) : base(logger)
        {
            this.iIncreaseData = iIncreaseData;
            this.iDepartmentData = iDepartmentData;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            List<SelectListItem> list = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "Todos" }
            };
            List<Department> departments = iDepartmentData.GetAll();
            foreach (var item in departments)
            {
                list.Add(new SelectListItem { Value = item.DepartmentID.ToString(), Text = item.Name.ToString() });
            }
            
            ViewBag.DepartmentID = new SelectList(list, "Value", "Text");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IncrementID,DepartmentID,IncrementPercent,StatusID,DateCreate")] IncrementVM increaseVM)
        {

            
            if (ModelState.IsValid)
            {
                iIncreaseData.Insert(increaseVM);

                return RedirectToAction("Index");
            }

            List<SelectListItem> list = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "Todos" }
            };
            List<Department> departments = iDepartmentData.GetAll();
            foreach (var item in departments)
            {
                list.Add(new SelectListItem { Value = item.DepartmentID.ToString(), Text = item.Name.ToString() });
            }
            ViewBag.DepartmentID = new SelectList(list, "Value", "Text", increaseVM.DepartmentID);

            return View(increaseVM);
        }

        public IActionResult LoadIncrease()
        {
            int start = Convert.ToInt32(Request.Query["start"]);
            int length = Convert.ToInt32(Request.Query["length"]);
            string searchValue = Request.Query["search[value]"];
            string sortColumnName = Request.Query["columns[" + Request.Query["order[0][column]"] + "][name]"];
            string sortDirection = Request.Query["order[0][dir]"];

            ListDataModel list = iIncreaseData.List();
            Console.WriteLine("list: " + JsonSerializer.Serialize(list));
            return Json(new { data = list.data, draw = Request.Query["draw"], recordsTotal = list.totalrows, recordsFiltered = list.rowsfiltered });
        }
    }
}
