using projetoClinica.Dados;
using projetoClinica.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace projetoClinica.Controllers
{
    public class EspecialidadeController : Controller
    {
        AcoesEspecialidade acaoEspecialidade = new AcoesEspecialidade();

        // GET: Especialidade
        public ActionResult Index()
        { 
            Session["tipo"] = "";
            return View();
        }

        public ActionResult CadEspecialidade()
        {
            Session["tipo"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult CadEspecialidade(ModelEspecialidade esp)
        {
            acaoEspecialidade.inserirEspecialidade(esp);
            Response.Write($"<script>alert('Especialidade {esp.Especialidade} cadastrado com sucesso')</script>");
            return View();
        }

        public ActionResult getEspecialidade()
        {
            Session["tipo"] = "";

            GridView dgv = new GridView();
            dgv.DataSource = acaoEspecialidade.ConsultaEspecialidade();
            dgv.DataBind();
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            dgv.RenderControl(htw);
            ViewBag.GridViewString = sw.ToString();
            return View();
        }
    }
}