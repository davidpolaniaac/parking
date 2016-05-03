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
    public class RegistroesController : Controller
    {
        private ParqueaderoEntities db = new ParqueaderoEntities();

        // GET: Registroes
        public ActionResult Index()
        {
            var registro = db.Registro.Include(r => r.Servicio).Include(r => r.User).Where(x=>x.Estado==false);
            return View(registro.ToList());
        }
        
        // GET: Registroes
        public ActionResult Registros()
        {
            var registro = db.Registro.Include(r => r.Servicio).Include(r => r.User).Where(x => x.Estado == true);
            return View(registro.ToList());
        }

        // GET: Registroes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registro registro = db.Registro.Find(id);
            if (registro == null)
            {
                return HttpNotFound();
            }
            return View(registro);
        }

        // GET: Registroes/Create
        public ActionResult Create()
        {
            ViewBag.IdServicio = new SelectList(db.Servicio, "IdServicio", "NombreServcio");
            ViewBag.IdUser = new SelectList(db.User, "Cedula", "Nombre");
            return View();
        }

        // POST: Registroes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdRegistro,Placa,FechaEntrada,IdServicio,IdUser,Estado,FechaSalida,CostoFinal")] Registro registro)
        {
            if (ModelState.IsValid)
            {
                registro.FechaEntrada = DateTime.Now;
                registro.Estado = false;
                db.Registro.Add(registro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdServicio = new SelectList(db.Servicio, "IdServicio", "NombreServcio", registro.IdServicio);
            ViewBag.IdUser = new SelectList(db.User, "Cedula", "Nombre", registro.IdUser);
            return View(registro);
        }
        // GET: Registroes/CreateSalida
        public ActionResult CreateSalida(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registro registro = db.Registro.Find(id);
            if (registro == null)
            {
                return HttpNotFound();
            }

            int horas = (DateTime.Now - Convert.ToDateTime(registro.FechaEntrada)).Hours;

            if (horas > 0)
            {
                ViewBag.Costo  = horas * (float)db.Servicio.FirstOrDefault(x => x.IdServicio == registro.IdServicio).Costo;
            }
            else
            {
                ViewBag.Costo  = (float)db.Servicio.FirstOrDefault(x => x.IdServicio == registro.IdServicio).Costo;
            }

            
            ViewBag.FechaSalida =DateTime.Now;
            ViewBag.IdServicio = new SelectList(db.Servicio, "IdServicio", "NombreServcio", registro.IdServicio);
            ViewBag.IdUser = new SelectList(db.User, "Cedula", "Nombre", registro.IdUser);
            return View(registro);
        }
        // POST: Registroes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSalida([Bind(Include = "IdRegistro,Placa,FechaEntrada,IdServicio,IdUser,Estado,FechaSalida,CostoFinal")] Registro registro)
        {
            if (ModelState.IsValid)
            {
                registro.FechaSalida = DateTime.Now;
                int horas = (registro.FechaSalida - Convert.ToDateTime(registro.FechaEntrada)).Hours;
                if (horas>0)
                {
                    registro.CostoFinal = horas*(float)db.Servicio.FirstOrDefault(x => x.IdServicio == registro.IdServicio).Costo;
                }
                else
                {
                    registro.CostoFinal= (float)db.Servicio.FirstOrDefault(x => x.IdServicio == registro.IdServicio).Costo;
                }
                registro.Estado = true;

                db.Entry(registro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdServicio = new SelectList(db.Servicio, "IdServicio", "NombreServcio", registro.IdServicio);
            ViewBag.IdUser = new SelectList(db.User, "Cedula", "Nombre", registro.IdUser);
            return View(registro);
        }
        // GET: Registroes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registro registro = db.Registro.Find(id);
            if (registro == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdServicio = new SelectList(db.Servicio, "IdServicio", "NombreServcio", registro.IdServicio);
            ViewBag.IdUser = new SelectList(db.User, "Cedula", "Nombre", registro.IdUser);
            return View(registro);
        }

        // POST: Registroes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdRegistro,Placa,FechaEntrada,IdServicio,IdUser,Estado,FechaSalida,CostoFinal")] Registro registro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdServicio = new SelectList(db.Servicio, "IdServicio", "NombreServcio", registro.IdServicio);
            ViewBag.IdUser = new SelectList(db.User, "Cedula", "Nombre", registro.IdUser);
            return View(registro);
        }

        // GET: Registroes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registro registro = db.Registro.Find(id);
            if (registro == null)
            {
                return HttpNotFound();
            }
            return View(registro);
        }

        // POST: Registroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Registro registro = db.Registro.Find(id);
            db.Registro.Remove(registro);
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
