using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Salary.App.Utils;
using Salary.Models.Data;
using Salary.Models.Entities;
using Salary.Models.ViewModel;
using System.Net;
using System.Text.Json;

namespace Salary.Controllers
{
    public class AssociateController: PrivateBaseController
    {
        private readonly IAssociateData iAssociateData;
        private readonly IDepartmentData iDepartmentData;

        public AssociateController(ILogger<AssociateController> logger,
            IAssociateData iAssociateData,
            IDepartmentData iDepartmentData) : base(logger)
        {
            this.iAssociateData = iAssociateData;
            this.iDepartmentData = iDepartmentData;
        }

        public IActionResult Index()
        {
            return View();
        }

        // [IsAuthorizedView("Associate", "Index")]
        public IActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(iDepartmentData.GetAll(), "DepartmentID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("AssociateID,DepartmentID,AssociateNumber,AssociateName,AssociateFirstLastName,AssociateSecondLastName,AssociateSalary,StatusID,DateCreate")] AssociateVM associateVM)
        {

            Associate associate = dataConvertion(associateVM);
            if (ModelState.IsValid)
            {
                iAssociateData.Insert(associate);

                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(iDepartmentData.GetAll(), "DepartmentID", "Name", associateVM.DepartmentID);
            return View(associateVM);
        }

        // [IsAuthorizedView("Associate", "Index")]
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            Associate associate = iAssociateData.Get(id);
            if (associate == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.NotFound);
            }
            AssociateVM associateVM = dataConvertionToVM(associate);
            return View(associateVM);
        }

        // [IsAuthorizedView("Associate", "Index")]
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            Associate associate = iAssociateData.Get(id);
            if (associate == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.NotFound);
            }
            ViewBag.DepartmentID = new SelectList(iDepartmentData.GetAll(), "DepartmentID", "Name", associate.DepartmentID);

            AssociateVM associateVM = dataConvertionToVM(associate);

            return View(associateVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("AssociateID,DepartmentID,AssociateNumber,AssociateName,AssociateFirstLastName,AssociateSecondLastName,AssociateSalary,StatusID,DateCreate,DateUpdate")] AssociateVM associateVM)
        {
            int isUpdate = 1;
            Associate associate = dataConvertion(associateVM, isUpdate);
            Console.WriteLine("Edit" + JsonSerializer.Serialize(associate));
            if (ModelState.IsValid)
            {
                iAssociateData.Edit(associate);

                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(iDepartmentData.GetAll(), "DepartmentID", "Name", associate.DepartmentID);

            return View(associateVM);
        }

        // [IsAuthorizedView("Associate", "Index")]
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            Associate associate = iAssociateData.Get(id);
            if (associate == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.NotFound);
            }

            return View(associate);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(string id)
        {
            Associate associate = iAssociateData.Get(id);

            Boolean success = iAssociateData.Delete(associate);
            if (success)
            {
                return Json(new { result = "Ok", url = "", msg = "Asociado Eliminado" });
            }
            else
            {
                return Json(new { result = "Error", url = "", msg = "Error al eliminar" });
            }
        }

        public IActionResult LoadAssociates()
        {
            Console.WriteLine("LoadAssociates");
            int start = Convert.ToInt32(Request.Query["start"]);
            int length = Convert.ToInt32(Request.Query["length"]);
            string searchValue = Request.Query["search[value]"];
            string sortColumnName = Request.Query["columns[" + Request.Query["order[0][column]"] + "][name]"];
            string sortDirection = Request.Query["order[0][dir]"];

            ListDataModel list = iAssociateData.List();

            Console.WriteLine("Associates: " + JsonSerializer.Serialize(list));

            return Json(new { data = list.data, draw = Request.Query["draw"], recordsTotal = list.totalrows, recordsFiltered = list.rowsfiltered });
        }

        private Associate dataConvertion(AssociateVM associateVM, int isUpdate = 0)
        {
            Associate associate = new Associate();
            DateTime date = DateTime.Now;
            string dateFormatCustom = date.ToString("yyyy/MM/dd HH:mm:ss");

            associate.DepartmentID = associateVM.DepartmentID;
            associate.AssociateNumber = associateVM.AssociateNumber;
            associate.AssociateName = associateVM.AssociateName;
            associate.AssociateFirstLastName = associateVM.AssociateFirstLastName;
            associate.AssociateSecondLastName = associateVM.AssociateSecondLastName;
            associate.AssociateSalary = associateVM.AssociateSalary;
            associate.StatusID = 1;

            if (isUpdate > 0)
            {
                associate.AssociateID = associateVM.AssociateID;
                associate.DateUpdate = DateTime.Parse(dateFormatCustom);
                associate.DateCreate = associateVM.DateCreate;
            }
            else
            {
                associate.DateCreate = DateTime.Parse(dateFormatCustom);
            }

            return associate;
        }

        private AssociateVM dataConvertionToVM(Associate associate)
        {
            AssociateVM associateVM = new AssociateVM();
            DateTime date = DateTime.Now;
            string dateFormatCustom = date.ToString("yyyy/MM/dd HH:mm:ss");

            associateVM.AssociateID = associate.AssociateID;
            associateVM.DepartmentID = associate.DepartmentID;
            associateVM.AssociateNumber = associate.AssociateNumber;
            associateVM.AssociateName = associate.AssociateName;
            associateVM.AssociateFirstLastName = associate.AssociateFirstLastName;
            associateVM.AssociateSecondLastName = associate.AssociateSecondLastName;
            associateVM.AssociateSalary = associate.AssociateSalary;
            associateVM.StatusID = 1;
            associateVM.DateCreate = associate.DateCreate;

            return associateVM;
        }
    }
}
