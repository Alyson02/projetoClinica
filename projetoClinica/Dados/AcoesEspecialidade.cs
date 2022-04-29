using MySql.Data.MySqlClient;
using projetoClinica.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace projetoClinica.Dados
{
    public class AcoesEspecialidade
    {
        conexao con = new conexao();
        public void inserirEspecialidade(ModelEspecialidade especialidade) //Método para inserir tipo do usuário
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbEspecialidade values (default, @especialidade)", con.MyConectarBD());
            cmd.Parameters.Add("@especialidade", MySqlDbType.VarChar).Value = especialidade.Especialidade;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public DataTable ConsultaEspecialidade()//Método de consulta do tipo de usuário
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbEspecialidade", con.MyDesconectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable especialidade = new DataTable();
            da.Fill(especialidade);
            con.MyDesconectarBD();
            return especialidade;
        }
    }
}