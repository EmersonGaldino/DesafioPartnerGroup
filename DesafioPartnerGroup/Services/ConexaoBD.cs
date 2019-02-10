using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DesafioPartnerGroup.Services
{
    public class ConexaoBD
    {
        private SqlConnection _conexao;

        public SqlConnection Conectar()
        {
            try
            {
                string strConexao = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ToString();
                _conexao = new SqlConnection(strConexao);

                return _conexao;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}