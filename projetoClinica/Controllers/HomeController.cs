using MySql.Data.MySqlClient;
using projetoClinica.Dados;
using projetoClinica.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace projetoClinica.Controllers
{
    public class HomeController : Controller
    {
        AcoesLogin acL = new AcoesLogin();
        AcoesTipoUsu acTipo = new AcoesTipoUsu();

        public void carregaTipoUsu() //Listar todos os tipos de usuário do banco
        {
            List<SelectListItem> tipoUsu = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=bdClinica;User=root;pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbTipoUsuario", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    tipoUsu.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();

            }
            ViewBag.tipoUsu = new SelectList(tipoUsu, "Value", "Text");
        }

        public ActionResult Index() //Carrega a página de login
        {
            Session["tipo"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult Index(ModelLogin cm) //Realiza o teste de login, direcionando para o About ou novamente para o login com a mensagem de erro
        {
            acL.TestarUsuario(cm);

            if(cm.usuario!=null)
            {
                FormsAuthentication.SetAuthCookie(cm.usuario, false);
                Session["usu"] = cm.usuario;
                Session["tipo"] = cm.codTipoUsuario;
                
                return RedirectToAction("About", "Home");
            }
            else
            {
                ViewBag.msgLogar = "Usuário e/ou senha incorreto(s)";
                return View();
            }    
        }

        public ActionResult SemAcesso() //Página sem acesso, quando o login não foi efetuado ou não tem privilégio para acessar
        {
            Response.Write("<script>alert('Sem Acesso - Faça o login no sistema')</script>");
            return View();
        }

        public ActionResult About() //Página principal, com controle de acesso, apenas logados podem entrar
        {
            if(Session["usu"]!=null)
            {
                ViewBag.Message = "Your application description page.";
                ViewBag.Usuario = Session["usu"];
                return View();
            }
            else
            {
                return RedirectToAction("SemAcesso", "Home");
            }   
        }

        public ActionResult Contact()//Página de contatos, só permite acesso de administradores
        {
            if (Session["usu"] != null && Session["tipo"].ToString()=="1")
            {
                ViewBag.Message = "Your contact page.";
                return View();
            }
            else
            {
                return RedirectToAction("SemAcesso", "Home");
            }
        }

        public ActionResult Logout()//Comando para encerrar o login, retornando ao Index
        {
            Session["usu"] = null;
            Session["tipo"] = null;
            return RedirectToAction("Index","Home");
        }

        public ActionResult CadTipo() //Carrega a página de cadastro do Tipo
        {
            if (Session["usu"] != null && Session["tipo"].ToString() == "1")
            {
                return View();
            }
            else
            {

                return RedirectToAction("SemAcesso", "Home");
            }
        }

        [HttpPost]
        public ActionResult CadTipo(ModelTipoUsu cmTipo) //Efetua o cadastro
        {
            acTipo.inserirTipoUsu(cmTipo);
            ViewBag.msgCad = "Cadastro Efetuado";
            return View();
        }

        public ActionResult ConsCadTipo() //Carrega a página de consulta do tipo
        {
            //verifica se o usuário está logado e se é admin
            if (Session["usu"] != null && Session["tipo"].ToString() == "1")
            {
                GridView dgv = new GridView();
                dgv.DataSource = acTipo.ConsultaTipo();
                dgv.DataBind();
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                dgv.RenderControl(htw);
                ViewBag.GridViewString = sw.ToString();
                return View();
            }
            else //caso não esteja logado ou não seja admin
            {
                return RedirectToAction("SemAcesso", "Home");
            }
        }

        public ActionResult CadLogin() //Carrega a página de cadastro do Login
        {
            if (Session["usu"] != null && Session["tipo"].ToString() == "1")
            {
                carregaTipoUsu();
                return View();
            }
            else
            {

                return RedirectToAction("SemAcesso", "Home");
            }
        }
        [HttpPost]
        public ActionResult CadLogin(ModelLogin cmLogin) //Efetua o cadastro do Login
        {
            carregaTipoUsu();
            if(cmLogin.senha == cmLogin.confirmaSenha)
            {
                cmLogin.codTipoUsuario = Request["tipoUsu"];
                acL.inserirLogin(cmLogin);
                Response.Write("<script>alert('Cadastro realizado com sucesso')</script>");
            }
            else
            {
                Response.Write("<script>alert('Senhas nao conferem')</script>");
            }
            return View();
            
        }

        }
}