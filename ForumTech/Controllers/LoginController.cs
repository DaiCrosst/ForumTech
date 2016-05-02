using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ForumTech.Repositorio;
using  ForumTech.Models;
using  System.Web.Security;

namespace ForumTech.Controllers
{
    public class LoginController : Controller
    {

        private usuario autenticarUsuario;
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(String login, String senha)
        {
            autenticarUsuario = AutenticarUsuario.autenticarUsuario(login, senha);

            if (autenticarUsuario != null)
            {
                Session.Add("usuario", autenticarUsuario);
                return Redirect("/Topico/Index");
            }
            else
            {
                ViewBag.ErroAutenticacao = "Usuário ou senha invalidos";
                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult CadastrarUsuario(usuario usuario)
        {
            BD_FORUMEntities ctx = new BD_FORUMEntities();

            var senhaCriptografada = FormsAuthentication.HashPasswordForStoringInConfigFile(usuario.senha, "MD5");

            usuario.senha = senhaCriptografada;
            usuario.data_cadastro = DateTime.Now;
            usuario.adm = "N";
            ctx.usuario.Add(usuario);
            ctx.SaveChanges();

            ViewBag.StatusCadastro = "Cadastro realizado com sucesso.";

            return View("Index");
        }
    }
}