using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using ForumTech.Models;


namespace ForumTech.Repositorio
{
   public class AutenticarUsuario
    {
        public static usuario autenticarUsuario(String login, String senha)
        {
            var senhaCriptografada = FormsAuthentication.HashPasswordForStoringInConfigFile(senha, "MD5");

            try
            {
                using (BD_FORUMEntities ctx = new BD_FORUMEntities())
                {
                    var consultaUsuario =
                        ctx.usuario.Where(x => x.login_email == login && x.senha == senhaCriptografada).FirstOrDefault();
                    return consultaUsuario;
                }
            }
            catch (Exception)
            {
                
                return null;
            }
        }

        public static Boolean verificarSessaoUsuario()
        {
            usuario usuarioLogado = (usuario) HttpContext.Current.Session["usuario"];

            if (usuarioLogado == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static Boolean verificarSessaoUsuarioAdm()
        {
            usuario usuarioLogado = (usuario) HttpContext.Current.Session["usuario"];

            if (usuarioLogado != null && usuarioLogado.adm=="S")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static usuario retornarUsuarioDaSessao()
        {
            usuario usuarioLogado = (usuario) HttpContext.Current.Session["usuario"];
            return usuarioLogado;
        }

       public static void FinalizarSessao()
       {
           HttpContext.Current.Session.Clear();
       }
    }
}
