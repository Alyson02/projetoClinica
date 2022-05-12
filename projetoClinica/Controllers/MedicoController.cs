using MySql.Data.MySqlClient;
using projetoClinica.Dados;
using projetoClinica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projetoClinica.Controllers
{
    public class MedicoController : Controller
    {
        AcoesMedico acoesMedico = new AcoesMedico();

        public void carregaEspecialidades()
        {
            List<SelectListItem> ag = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=bdClinica;User=root;pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbespecialidade;", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ag.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
                con.Open();
            }


            ViewBag.especialidade = new SelectList(ag, "Value", "Text");
        }


        // GET: Medico
        public ActionResult CreateMedico()
        {
            Session["tipo"] = "";
            carregaEspecialidades();
            return View();
        }

        [HttpPost]
        public ActionResult CreateMedico(ModelMedico medico)
        {
            Session["tipo"] = "";

            carregaEspecialidades();
            medico.IdEspecialidade = Request["especialidade"];

            acoesMedico.inserirMedico(medico);

            ViewBag.Msg = "Cadastro realizado com sucesso";
            return View();
        }
    }
}