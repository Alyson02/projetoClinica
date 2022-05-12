using MySql.Data.MySqlClient;
using projetoClinica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projetoClinica.Dados
{
    public class AcoesAtendimento
    {
        conexao con = new conexao();

        public void TestarAgenda(ModelAtendimento agenda) //verificar se a agenda está reservada
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbAtendimento where dataAtend = @data and horaAtend = @hora", con.MyConectarBD());

            cmd.Parameters.Add("@data", MySqlDbType.VarChar).Value = agenda.dataAtend;
            cmd.Parameters.Add("@hora", MySqlDbType.VarChar).Value = agenda.horaAtend;

            MySqlDataReader leitor;

            leitor = cmd.ExecuteReader();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    agenda.confAgendamento = "0";
                }

            }

            else
            {
                agenda.confAgendamento = "1";
            }

            con.MyDesconectarBD();
        }


        public void inserirAtendimento(ModelAtendimento cm)// Cadastrar o atendimento no BD
        {

            MySqlCommand cmd = new MySqlCommand("insert into tbAtendimento(codAtendimento, dataAtend, horaAtend, codMedico, codPac) values (default, @data, @hora, @codMed, @codPac)", con.MyConectarBD());
            cmd.Parameters.Add("@data", MySqlDbType.VarChar).Value = cm.dataAtend;
            cmd.Parameters.Add("@hora", MySqlDbType.VarChar).Value = cm.horaAtend;
            cmd.Parameters.Add("@codMed", MySqlDbType.VarChar).Value = cm.codMedico;
            cmd.Parameters.Add("@codPac", MySqlDbType.VarChar).Value = cm.codPac;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

    }
}