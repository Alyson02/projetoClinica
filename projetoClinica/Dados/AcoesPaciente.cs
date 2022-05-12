using MySql.Data.MySqlClient;
using projetoClinica.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace projetoClinica.Dados
{
    public class AcoesPaciente
    {
        conexao con = new conexao();
        public void inserirPaciente(ModelPaciente paciente) //Método para inserir tipo do usuário
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbpaciente values (default, @nome, @telefone, @email)", con.MyConectarBD());
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = paciente.Nome;
            cmd.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = paciente.Telefone;
            cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = paciente.Email;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public DataTable ConsultaPacientes()//Método de consulta do tipo de usuário
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbPaciente", con.MyDesconectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable paciente = new DataTable();
            da.Fill(paciente);
            con.MyDesconectarBD();
            return paciente;
        }
    }
}