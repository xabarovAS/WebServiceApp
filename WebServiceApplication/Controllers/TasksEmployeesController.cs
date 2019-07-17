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
    public class TasksEmployeesController : Controller
    {
        private MatchingContext db = new MatchingContext();

        // GET: TasksEmployees
        public async Task<ActionResult> Index()
        {
            return View(await db.TasksEmployees.ToListAsync());
        }

        // GET: TasksEmployees/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TasksEmployee tasksEmployee = await db.TasksEmployees.FindAsync(id);
            if (tasksEmployee == null)
            {
                return HttpNotFound();
            }
            return View(tasksEmployee);
        }

        // GET: TasksEmployees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TasksEmployees/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Description,FullName")] TasksEmployee tasksEmployee)
        {
            if (ModelState.IsValid)
            {
                db.TasksEmployees.Add(tasksEmployee);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tasksEmployee);
        }

        // GET: TasksEmployees/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TasksEmployee tasksEmployee = await db.TasksEmployees.FindAsync(id);
            if (tasksEmployee == null)
            {
                return HttpNotFound();
            }
            return View(tasksEmployee);
        }

        // POST: TasksEmployees/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Description,FullName")] TasksEmployee tasksEmployee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tasksEmployee).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tasksEmployee);
        }

        // GET: TasksEmployees/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TasksEmployee tasksEmployee = await db.TasksEmployees.FindAsync(id);
            if (tasksEmployee == null)
            {
                return HttpNotFound();
            }
            return View(tasksEmployee);
        }

        // POST: TasksEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TasksEmployee tasksEmployee = await db.TasksEmployees.FindAsync(id);
            db.TasksEmployees.Remove(tasksEmployee);
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
