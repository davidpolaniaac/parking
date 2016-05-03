using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Parqueadero.Models;

namespace Parqueadero.Controllers
{
    [Authorize]
    public class TipoVehiculoesController : Controller
    {
        private ParqueaderoEntities db = new ParqueaderoEntities();

        // GET: TipoVehiculoes
        public ActionResult Index()
        {
            return View(db.TipoVehiculo.ToList());
        }

        // GET: TipoVehiculoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoVehiculo tipoVehiculo = db.TipoVehiculo.Find(id);
            if (tipoVehiculo == null)
            {
                return HttpNotFound();
            }
            return View(tipoVehiculo);
        }

        // GET: TipoVehiculoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoVehiculoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdVehiculo,NombreVehiculo")] TipoVehiculo tipoVehiculo)
        {
            if (ModelState.IsValid)
            {
                db.TipoVehiculo.Add(tipoVehiculo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoVehiculo);
        }

        // GET: TipoVehiculoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoVehiculo tipoVehiculo = db.TipoVehiculo.Find(id);
            if (tipoVehiculo == null)
            {
                return HttpNotFound();
            }
            return View(tipoVehiculo);
        }

        // POST: TipoVehiculoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdVehiculo,NombreVehiculo")] TipoVehiculo tipoVehiculo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoVehiculo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoVehiculo);
        }

        // GET: TipoVehiculoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoVehiculo tipoVehiculo = db.TipoVehiculo.Find(id);
            if (tipoVehiculo == null)
            {
                return HttpNotFound();
            }
            return View(tipoVehiculo);
        }

        // POST: TipoVehiculoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoVehiculo tipoVehiculo = db.TipoVehiculo.Find(id);
            db.TipoVehiculo.Remove(tipoVehiculo);
            db.SaveChanges();
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
