using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ForumTech.Models;
using ForumTech.Repositorio;

namespace ForumTech.Controllers
{
    public class UsuariosController : Controller
    {
        private BD_FORUMEntities db = new BD_FORUMEntities();

        // GET: Usuarios
        public ActionResult Index()
        {
            if (AutenticarUsuario.verificarSessaoUsuario() && AutenticarUsuario.verificarSessaoUsuarioAdm())
            {
                return View(db.usuario.ToList());
            }
            return Redirect("/Login");
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (AutenticarUsuario.verificarSessaoUsuario() && AutenticarUsuario.verificarSessaoUsuarioAdm())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                usuario usuario = db.usuario.Find(id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }

                return View(usuario);
            }
            return Redirect("/Login");
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_usuario,nome,login_email,senha,adm,data_cadastro")] usuario usuario)
        {
            if (AutenticarUsuario.verificarSessaoUsuario() && AutenticarUsuario.verificarSessaoUsuarioAdm())
            {
                if (ModelState.IsValid)
                {
                    db.usuario.Add(usuario);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(usuario);
            }

            return Redirect("/Login");
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (AutenticarUsuario.verificarSessaoUsuario() && AutenticarUsuario.verificarSessaoUsuarioAdm())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                usuario usuario = db.usuario.Find(id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                return View(usuario);
            }
            return Redirect("/Login");
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_usuario,nome,login_email,senha,adm,data_cadastro")] usuario usuario)
        {
            if (AutenticarUsuario.verificarSessaoUsuario() && AutenticarUsuario.verificarSessaoUsuarioAdm())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(usuario).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(usuario);
            }

            return Redirect("/Login");
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (AutenticarUsuario.verificarSessaoUsuario()&& AutenticarUsuario.verificarSessaoUsuarioAdm())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                usuario usuario = db.usuario.Find(id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                return View(usuario);
            }
            return Redirect("/Login");
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (AutenticarUsuario.verificarSessaoUsuario() && AutenticarUsuario.verificarSessaoUsuarioAdm())
            {
                usuario usuario = db.usuario.Find(id);
                db.usuario.Remove(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return Redirect("/Login");
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
