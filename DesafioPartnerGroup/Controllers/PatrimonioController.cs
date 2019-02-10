using DesafioPartnerGroup.Models;
using DesafioPartnerGroup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DesafioPartnerGroup.Controllers
{
    public class PatrimonioController : ApiController
    {
        [Route("patrimonios")]
        [AcceptVerbs("GET")]
        public async Task<ResponseModel<List<PatrimonioModel>>> GetPatrimonios()
        {
            ResponseModel<List<PatrimonioModel>> response = new ResponseModel<List<PatrimonioModel>>();

            try
            {
                PatrimonioService patrimonioService = new PatrimonioService();
                List<PatrimonioModel> patrimonios = await patrimonioService.GetPatrimonios();

                response.Value = patrimonios;
            }
            catch (Exception ex)
            {
                response.Erro = true;
                response.MensagemErro = ex.Message;
            }

            return response;
        }

        [Route("patrimonios/{id}")]
        [AcceptVerbs("GET")]
        public async Task<ResponseModel<PatrimonioModel>> GetPatrimonio(int? id)
        {
            ResponseModel<PatrimonioModel> response = new ResponseModel<PatrimonioModel>();

            try
            {
                PatrimonioService patrimonioService = new PatrimonioService();
                PatrimonioModel patrimonio = await patrimonioService.GetPatrimonio(id);

                response.Value = patrimonio;
            }
            catch (Exception ex)
            {
                response.Erro = true;
                response.MensagemErro = ex.Message;
            }

            return response;
        }

        [HttpPost]
        [Route("patrimonios")]
        public async Task<ResponseModel<bool>> PostPatrimonio(PatrimonioModel patrimonio)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();

            try
            {
                PatrimonioService patrimonioService = new PatrimonioService();
                bool sucesso = await patrimonioService.PostPatrimonio(patrimonio);

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
        [Route("patrimonio/{patrimonio}")]
        public async Task<ResponseModel<bool>> PutPatrimonio(PatrimonioModel patrimonio)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();

            try
            {
                PatrimonioService patrimonioService = new PatrimonioService();
                bool sucesso = await patrimonioService.PutPatrimonio(patrimonio);

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
        [Route("patrimonio/{id}")]
        public async Task<ResponseModel<bool>> DeletePatrimonio(int? id)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();

            try
            {
                PatrimonioService patrimonioService = new PatrimonioService();
                bool sucesso = await patrimonioService.DeletePatrimonio(id);

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