using MySql.Data.MySqlClient;
using projetoClinica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projetoClinica.Dados
{
    public class AcoesMedico
    {
        conexao con = new conexao();
        public void inserirMedico(ModelMedico medico) //Método para inserir tipo do usuário
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbMedico values (default, @nome, @idEspecialidade)", con.MyConectarBD());
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = medico.Nome;
            cmd.Parameters.Add("@idEspecialidade", MySqlDbType.VarChar).Value = medico.IdEspecialidade;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }
    }
}