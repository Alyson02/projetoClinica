using MySql.Data.MySqlClient;
using projetoClinica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projetoClinica.Dados
{
    public class AcoesLogin
    {
        conexao con = new conexao();

        public void TestarUsuario(ModelLogin user) //Método para verificar se usuário e senha estão corretos
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbLogin where usuario = @usuario and senha = @senha", con.MyConectarBD());

            cmd.Parameters.Add("@usuario", MySqlDbType.VarChar).Value = user.usuario;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = user.senha;

            MySqlDataReader leitor;

            leitor = cmd.ExecuteReader();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    user.usuario = Convert.ToString(leitor["usuario"]);
                    user.senha = Convert.ToString(leitor["senha"]);
                    user.codTipoUsuario = Convert.ToString(leitor["codTipoUsuario"]);
                }
            }
            else
            {
                user.usuario = null;
                user.senha = null;
                user.codTipoUsuario = null;
            }

            con.MyDesconectarBD();
        }

        public void inserirLogin(ModelLogin cmLogin)//Método para cadastrar o Login
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbLogin values (@usuario, @senha, @tipoUsu)", con.MyConectarBD());
            cmd.Parameters.Add("@usuario", MySqlDbType.VarChar).Value = cmLogin.usuario;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = cmLogin.senha;
            cmd.Parameters.Add("@tipoUsu", MySqlDbType.VarChar).Value = cmLogin.codTipoUsuario;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }
    }
}