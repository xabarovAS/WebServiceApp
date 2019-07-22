using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebServiceApplication;
using WebServiceApplication.Models;

namespace WebServiceApplication.Controllers
{
    public class StatusTasksEmployeesController : Controller
    {
        private MatchingContext db = new MatchingContext();

        // GET: StatusTasksEmployees
        public async Task<ActionResult> Index()
        {
            return View(await db.StatusTasksEmployees.Include(s => s.TasksEmployee).Include(s => s.Employeе).ToListAsync());
        }

        // GET: StatusTasksEmployees/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusTasksEmployee statusTasksEmployee = await db.StatusTasksEmployees.Include(i => i.Employeе).Include(i => i.TasksEmployee).FirstOrDefaultAsync(i => i.ID == id); //FindAsync(id);
            if (statusTasksEmployee == null)
            {
                return HttpNotFound();
            }
            return View(statusTasksEmployee);
        }

        // GET: StatusTasksEmployees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StatusTasksEmployees/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,StatusTask")] StatusTasksEmployee statusTasksEmployee)
        {
            if (ModelState.IsValid)
            {
                db.StatusTasksEmployees.Add(statusTasksEmployee);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(statusTasksEmployee);
        }

        // GET: StatusTasksEmployees/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusTasksEmployee statusTasksEmployee = await db.StatusTasksEmployees.Include(i => i.Employeе).Include(i => i.TasksEmployee).FirstOrDefaultAsync(i => i.ID == id); //await db.StatusTasksEmployees.FindAsync(id);
            if (statusTasksEmployee == null)
            {
                return HttpNotFound();
            }
            return View(statusTasksEmployee);
        }

        // POST: StatusTasksEmployees/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,StatusTask")] StatusTasksEmployee statusTasksEmployee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statusTasksEmployee).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(statusTasksEmployee);
        }

        // GET: StatusTasksEmployees/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusTasksEmployee statusTasksEmployee = await db.StatusTasksEmployees.Include(i => i.Employeе).Include(i => i.TasksEmployee).FirstOrDefaultAsync(i => i.ID == id);
            if (statusTasksEmployee == null)
            {
                return HttpNotFound();
            }
            return View(statusTasksEmployee);
        }

        // POST: StatusTasksEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            StatusTasksEmployee statusTasksEmployee = await db.StatusTasksEmployees.FindAsync(id);
            db.StatusTasksEmployees.Remove(statusTasksEmployee);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
