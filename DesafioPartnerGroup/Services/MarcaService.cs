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
    public class MarcaService
    {
        private SqlConnection _conexao;

        public MarcaService()
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

        public Task<List<MarcaModel>> GetMarcas()
        {
            try
            {
                _conexao.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conexao;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * from Marcas";
                SqlDataReader dr = cmd.ExecuteReader();

                if (!dr.HasRows)
                {
                    dr.Close();
                    throw new Exception("Não existe Marcas cadastradas.");
                }
                else
                {
                    List<MarcaModel> marcas = new List<MarcaModel>();

                    while (dr.Read())
                    {
                        marcas.Add(new MarcaModel()
                        {
                            ID = Convert.ToInt32(dr["Id"]),
                            Nome = dr["Nome"].ToString()
                        });
                    }

                    dr.Close();

                    return Task.FromResult(marcas);
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

        public Task<MarcaModel> GetMarca(int? id)
        {
            try
            {
                _conexao.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conexao;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * from Marcas WHERE ID LIKE @id";
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();

                if (!dr.HasRows)
                {
                    dr.Close();
                    throw new Exception("Não existe Marca com este ID.");
                }
                else
                {
                    MarcaModel marca = new MarcaModel();

                    dr.Read();

                    marca = new MarcaModel()
                    {
                        ID = Convert.ToInt32(dr["Id"]),
                        Nome = dr["Nome"].ToString()
                    };

                    return Task.FromResult(marca);
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

        public Task<List<PatrimonioModel>> GetPatrimoniosMarca(string id)
        {
            try
            {
                _conexao.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conexao;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * from Patrimonios WHERE MarcaID LIKE @id";
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();

                if (!dr.HasRows)
                {
                    dr.Close();
                    throw new Exception("Não existe Patrimônios cadastrados com esta Marca.");
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

        public Task<bool> PostMarca(MarcaModel marca)
        {
            try
            {
                if (marca == null)
                    throw new Exception("Não foi informado o objeto Marca.");
                else if (string.IsNullOrEmpty(marca.Nome))
                    throw new Exception("O atributo Nome é obrigatório.");

                _conexao.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conexao;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Marcas(Nome) VALUES(@nome)";
                cmd.Parameters.AddWithValue("@nome", marca.Nome);
                SqlDataReader dr = cmd.ExecuteReader();

                return Task.FromResult(true);
            }
            catch (SqlException ex)
            {
                if (ex.Errors[0].Number == 2627)
                    throw new Exception("Já existe uma Marca com este nome.");
                else
                    throw new Exception(ex.Message);
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

        public Task<bool> PutMarca(MarcaModel marca)
        {
            try
            {
                if (marca == null)
                    throw new Exception("Não foi informado o objeto Marca.");
                else if (marca.ID == null)
                    throw new Exception("O atributo ID é obrigatório.");
                else if (string.IsNullOrEmpty(marca.Nome))
                    throw new Exception("O atributo Nome é obrigatório.");

                _conexao.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conexao;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE Marcas SET Nome=@Nome WHERE ID LIKE @ID";
                cmd.Parameters.AddWithValue("@ID", marca.ID);
                cmd.Parameters.AddWithValue("@Nome", marca.Nome);
                SqlDataReader dr = cmd.ExecuteReader();

                return Task.FromResult(true);
            }
            catch (SqlException ex)
            {
                if (ex.Errors[0].Number == 2627)
                    throw new Exception("Já existe uma Marca com este nome.");
                else
                    throw new Exception(ex.Message);
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

        public Task<bool> DeleteMarca(int? id)
        {
            try
            {
                if (id == null)
                    throw new Exception("Não foi informado o ID da Marca.");

                _conexao.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conexao;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE Marcas WHERE ID LIKE @ID";
                cmd.Parameters.AddWithValue("@ID", id);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.RecordsAffected == 0)
                    throw new Exception("Não existe Marca com este ID.");

                return Task.FromResult(true);
            }
            catch (SqlException ex)
            {
                if (ex.Errors[0].Number == 547)
                    throw new Exception("Existe um patrimônio associado à esta Marca.");
                else
                    throw new Exception(ex.Message);
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