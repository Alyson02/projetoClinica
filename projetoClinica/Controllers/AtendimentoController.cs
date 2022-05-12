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
    public class AtendimentoController : Controller
    {
       

        public void carregaPacientes()
        {
            List<SelectListItem> ag = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=bdClinica;User=root;pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbPaciente order by nomePaciente;", con);
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


            ViewBag.paciente = new SelectList(ag, "Value", "Text");
        }

        public void carregaMedico()
        {
            List<SelectListItem> med = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=bdClinica;User=root;pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbMedico order by nomeMedico;", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    med.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
                con.Open();
            }


            ViewBag.medico = new SelectList(med, "Value", "Text");
        }

        AcoesAtendimento acoesAtendimento = new AcoesAtendimento();

        // GET: Atendimento
        public ActionResult Index()
        {
            Session["tipo"] = "";
            return View();
        }

        public ActionResult CadAtendimento()
        {
            Session["tipo"] = "";
            carregaMedico();
            carregaPacientes();
            return View();
        }

        [HttpPost]
        public ActionResult CadAtendimento(ModelAtendimento atendimento)
        {
            Session["tipo"] = "";
            carregaMedico();
            carregaPacientes();

            acoesAtendimento.TestarAgenda(atendimento);

            if (atendimento.confAgendamento == "0")
            {
                ViewBag.Msg = "Horário indisponivel";
            }
            else
            {
                atendimento.codPac = Request["paciente"]; //Pega o codigo do paciente que foi selecionado na lista
                atendimento.codMedico = Request["medico"]; //Pega o codigo do medico que foi selecionado na lista
                ViewBag.Msg = "Cadastro realizado com sucesso";
                acoesAtendimento.inserirAtendimento(atendimento);
            }
            return View();
        }
    }
}