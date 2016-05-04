using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ForumTech.Models;
using ForumTech.Repositorio;

namespace ForumTech.Controllers
{
    [VerificarUsuarioLogado]
    public class PostagemController : Controller
    {
        private BD_FORUMEntities db = new BD_FORUMEntities();
        // GET: Postagem
        public ActionResult Index(int? id_topico_forum)
        {
            var nomeTopico = db.topico_forum.Find(id_topico_forum);
            var postagem = db.postagem.Where(p => p.topico_forum.id_topico_forum == id_topico_forum);

            ViewBag.nomeTopico = nomeTopico.nome;
            return View(postagem.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.id_topico_forum = new SelectList(db.topico_forum, "id_topico_forum", "nome");
            ViewBag.id_usuario = new SelectList(db.usuario, "id_usuario", "nome");
            return View();
        }
        //id_postagem, id_topico_forum, id_usuario,id_resposta,mensagem,data_publicacao
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "id_topico_forum, id_usuario,mensagem,data_publicacao")] postagem
                postagem)
        {
            if (ModelState.IsValid)
            {
                usuario usuarioLogado = AutenticarUsuario.retornarUsuarioDaSessao();

                postagem.id_postagem = usuarioLogado.id_usuario;
                postagem.id_usuario = usuarioLogado.id_usuario;
                postagem.data_publicacao = DateTime.Now;

                db.postagem.Add(postagem);
                db.SaveChanges();

                return RedirectToAction("Index", new {id_topico_forum = postagem.id_topico_forum});
            }

            ViewBag.id_topico_forum = new SelectList(db.topico_forum, "id_topico_forum", "nome",
                postagem.id_topico_forum);
            ViewBag.id_usuario = new SelectList(db.usuario, "id_usuario", "nome", postagem.id_usuario);
            return View(postagem);
        }

        protected override void Dispose(bool disposing)
        { }
    }
}