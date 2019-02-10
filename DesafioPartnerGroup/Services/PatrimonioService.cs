using DesafioPartnerGroup.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DesafioPartnerGroup.Services
{
    public class PatrimonioService
    {
        private SqlConnection _conexao;

        public PatrimonioService()
        {
            try
            {
                ConexaoBD conexaoDB = new ConexaoBD();
                _conexao = conexaoDB.Conectar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<List<PatrimonioModel>> GetPatrimonios()
        {
            try
            {
                _conexao.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conexao;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * from Patrimonios";
                SqlDataReader dr = cmd.ExecuteReader();

                if (!dr.HasRows)
                {
                    dr.Close();
                    throw new Exception("Não existe Patrimônios cadastrados.");
                }
                else
                {
                    List<PatrimonioModel> patrimonios = new List<PatrimonioModel>();

                    while (dr.Read())
                    {
                        patrimonios.Add(new PatrimonioModel()
                        {
                            ID = Convert.ToInt32(dr["Id"]),
                            MarcaID = Convert.ToInt32(dr["MarcaID"]),
                            Nome = dr["Nome"].ToString(),
                            Descricao = dr["Descricao"].ToString(),
                            Tombo = dr["Tombo"] != DBNull.Value ? Convert.ToInt32(dr["Tombo"]) : (int?)null
                        });
                    }

                    dr.Close();

                    return Task.FromResult(patrimonios);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _conexao.Close();
            }
        }

        public Task<PatrimonioModel> GetPatrimonio(int? id)
        {
            try
            {
                _conexao.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conexao;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * from Patrimonios WHERE ID LIKE @id";
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();

                if (!dr.HasRows)
                {
                    dr.Close();
                    throw new Exception("Não existe Patrimônio com este ID.");
                }
                else
                {
                    PatrimonioModel patrimonio = new PatrimonioModel();

                    dr.Read();

                    patrimonio = new PatrimonioModel()
                    {
                        ID = Convert.ToInt32(dr["Id"]),
                        MarcaID = Convert.ToInt32(dr["MarcaID"]),
                        Nome = dr["Nome"].ToString(),
                        Descricao = dr["Descricao"].ToString(),
                        Tombo = dr["Tombo"] != DBNull.Value ? Convert.ToInt32(dr["Tombo"]) : (int?)null
                    };

                    return Task.FromResult(patrimonio);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _conexao.Close();
            }
        }

        public Task<bool> PostPatrimonio(PatrimonioModel patrimonio)
        {
            try
            {
                if (patrimonio == null)
                    throw new Exception("Não foi informado o objeto Patrimônio.");
                else if (patrimonio.MarcaID == null)
                    throw new Exception("O atributo MarcaID é obrigatório.");
                else if (string.IsNullOrEmpty(patrimonio.Nome))
                    throw new Exception("O atributo Nome é obrigatório.");

                //Gera Nº tombo
                Random random = new Random();
                int nTombo = random.Next(99999);

                _conexao.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conexao;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Patrimonios(MarcaID, Nome, Descricao, Tombo) VALUES (@marcaID, @nome, @descricao, @tombo)";
                cmd.Parameters.AddWithValue("@marcaID", patrimonio.MarcaID);
                cmd.Parameters.AddWithValue("@nome", patrimonio.Nome);
                cmd.Parameters.AddWithValue("@tombo", nTombo);

                if (!string.IsNullOrEmpty(patrimonio.Descricao))
                    cmd.Parameters.AddWithValue("@descricao", patrimonio.Descricao);
                else
                    cmd.Parameters.AddWithValue("@descricao", DBNull.Value);

                SqlDataReader dr = cmd.ExecuteReader();

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _conexao.Close();
            }
        }

        public Task<bool> PutPatrimonio(PatrimonioModel patrimonio)
        {
            try
            {
                if (patrimonio == null)
                    throw new Exception("Não foi informado o objeto Patrimônio.");
                else if (patrimonio.ID == null)
                    throw new Exception("O atributo ID é obrigatório.");

                List<string> camposEdicao = new List<string>();

                if (patrimonio.MarcaID != null)
                    camposEdicao.Add("MarcaID=@MarcaID");
                if (!string.IsNullOrEmpty(patrimonio.Nome))
                    camposEdicao.Add("Nome=@Nome");
                if (!string.IsNullOrEmpty(patrimonio.Descricao))
                    camposEdicao.Add("Descricao=@Descricao");

                if (camposEdicao.Count == 0)
                    throw new Exception("Informe os valores do patrimônio a serem editados.");

                _conexao.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conexao;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE Patrimonios SET "+ string.Join(",", camposEdicao.ToArray()) + " WHERE ID LIKE @ID";
                cmd.Parameters.AddWithValue("@ID", patrimonio.ID);

                if (patrimonio.MarcaID != null)
                    cmd.Parameters.AddWithValue("@MarcaID", patrimonio.MarcaID);
                if (!string.IsNullOrEmpty(patrimonio.Nome))
                    cmd.Parameters.AddWithValue("@Nome", patrimonio.Nome);
                if (!string.IsNullOrEmpty(patrimonio.Descricao))
                    cmd.Parameters.AddWithValue("@Descricao", patrimonio.Descricao);

                SqlDataReader dr = cmd.ExecuteReader();

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _conexao.Close();
            }
        }

        public Task<bool> DeletePatrimonio(int? id)
        {
            try
            {
                if (id == null)
                    throw new Exception("Não foi informado o ID do Patrimônio.");

                _conexao.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conexao;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE Patrimonios WHERE ID LIKE @ID";
                cmd.Parameters.AddWithValue("@ID", id);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.RecordsAffected == 0)
                    throw new Exception("Não existe Patrimônio com este ID.");

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _conexao.Close();
            }
        }
    }
}