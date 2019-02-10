using DesafioPartnerGroup.Models;
using DesafioPartnerGroup.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DesafioPartnerGroup.Controllers
{
    public class MarcaController : ApiController
    {
        [Route("marcas")]
        [AcceptVerbs("GET")]
        public async Task<ResponseModel<List<MarcaModel>>> GetMarcas()
        {
            ResponseModel<List<MarcaModel>> response = new ResponseModel<List<MarcaModel>>();

            try
            {
                MarcaService marcaService = new MarcaService();
                List<MarcaModel> marcas = await marcaService.GetMarcas();

                response.Value = marcas;
            }
            catch (Exception ex)
            {
                response.Erro = true;
                response.MensagemErro = ex.Message;
            }

            return response;
        }

        [Route("marcas/{id}")]
        [AcceptVerbs("GET")]
        public async Task<ResponseModel<MarcaModel>> GetMarca(int? id)
        {
            ResponseModel<MarcaModel> response = new ResponseModel<MarcaModel>();

            try
            {
                MarcaService marcaService = new MarcaService();
                MarcaModel marca = await marcaService.GetMarca(id);

                response.Value = marca;
            }
            catch (Exception ex)
            {
                response.Erro = true;
                response.MensagemErro = ex.Message;
            }

            return response;
        }

        [Route("marcas/{id}/patrimonios")]
        [AcceptVerbs("GET")]
        public async Task<ResponseModel<List<PatrimonioModel>>> GetPatrimoniosMarca(string id)
        {
            ResponseModel<List<PatrimonioModel>> response = new ResponseModel<List<PatrimonioModel>>();

            try
            {
                MarcaService marcaService = new MarcaService();
                List<PatrimonioModel> marcas = await marcaService.GetPatrimoniosMarca(id);

                response.Value = marcas;
            }
            catch (Exception ex)
            {
                response.Erro = true;
                response.MensagemErro = ex.Message;
            }

            return response;
        }

        [HttpPost]
        [Route("marcas")]
        public async Task<ResponseModel<bool>> PostMarca(MarcaModel marca)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();

            try
            {
                MarcaService marcaService = new MarcaService();
                bool sucesso = await marcaService.PostMarca(marca);

                response.Value = sucesso;
            }
            catch (Exception ex)
            {
                response.Erro = true;
                response.MensagemErro = ex.Message;
            }

            return response;
        }

        [HttpPut]
        [Route("marca/{marca}")]
        public async Task<ResponseModel<bool>> PutMarca(MarcaModel marca)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();

            try
            {
                MarcaService marcaService = new MarcaService();
                bool sucesso = await marcaService.PutMarca(marca);

                response.Value = sucesso;
            }
            catch (Exception ex)
            {
                response.Erro = true;
                response.MensagemErro = ex.Message;
            }

            return response;
        }

        [HttpDelete]
        [Route("marca/{id}")]
        public async Task<ResponseModel<bool>> DeleteMarca(int? id)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();

            try
            {
                MarcaService marcaService = new MarcaService();
                bool sucesso = await marcaService.DeleteMarca(id);

                response.Value = sucesso;
            }
            catch (Exception ex)
            {
                response.Erro = true;
                response.MensagemErro = ex.Message;
            }

            return response;
        }
    }
}