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
    public class PacienteController : Controller
    {
        AcoesPaciente acoesPaciente = new AcoesPaciente();

        public ActionResult GetPacientes()
        {
            Session["tipo"] = "";

            GridView dgv = new GridView();
            dgv.DataSource = acoesPaciente.ConsultaPacientes();
            dgv.DataBind();
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            dgv.RenderControl(htw);
            ViewBag.GridViewString = sw.ToString();
            return View();
        }


        public ActionResult CadPaciente()
        {
            Session["tipo"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult CadPaciente(ModelPaciente paciente)
        {
            Session["tipo"] = "";
            acoesPaciente.inserirPaciente(paciente);
            ViewBag.Msg = "Paciente cadastrado com sucesso!";
            return View();
        }
    }
}