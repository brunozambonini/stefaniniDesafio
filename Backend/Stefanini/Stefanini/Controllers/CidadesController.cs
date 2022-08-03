using Application.Models;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Stefanini.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadesController : ControllerBase
    {
        private readonly ICidadesService _cidadeService;

        public CidadesController(ICidadesService cidadeService)
        {
            _cidadeService = cidadeService;
        }

        // GET: api/Cidades
        /// <summary>
        /// Retorna todos lista de todas cidades, sem filtro
        /// </summary>
        /// <returns>Retorna lista de cidades</returns>
        /// <response code="200">Retorna lista de cidades</response>
        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var cidades = await _cidadeService.GetAll("");
                if (cidades != null)
                {
                    return Ok(cidades);
                }
                else
                {
                    return BadRequest("Página não encontrada.");
                }

            }
            catch (Exception ex)
            {

                return BadRequest($"Erro: {ex}");
            }
        }

        /// <summary>
        /// Retornar lista de cidade filtrando pelo nome
        /// </summary>
        /// <returns>Retorna lista de cidades</returns>
        /// <response code="200">Retorna lista de cidades</response>
        /// <param filter="termo usado para filtrar"></param>
        [HttpGet("{filter}")]
        public async Task<IActionResult> GetAllFiltering(string filter)
        {
            try
            {
                var cidades = await _cidadeService.GetAll(filter);
                if (cidades != null)
                {
                    return Ok(cidades);
                }
                else
                {
                    return BadRequest("Página não encontrada.");
                }

            }
            catch (Exception ex)
            {

                return BadRequest($"Erro: {ex}");
            }
        }

        //POST api/Cidade
        /// <summary>
        /// Cria uma nova cidade
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <param model="Objeto cidade (Id, Nome, UF)"></param>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Cidades model)
        {
            try
            {
                string erros = _cidadeService.CidadeValidate(model);
                if (!string.IsNullOrWhiteSpace(erros))
                {
                    // Retorna erro caso não seja válido
                    return BadRequest(erros);
                }

                // Salva no BD
                _cidadeService.Add(model);
                if (await _cidadeService.SaveChangeAsync())
                {
                    return Ok();
                }
                return BadRequest("Não foi possível salvar");
            }
            catch (Exception ex)
            {
                // Retorna erro com detalhes caso dê algum erro
                return BadRequest($"Erro: {ex}");
            }
        }


        //// PUT api
        /// <summary>
        /// Edita os dados da cidade ao passar o objeto do Cidade pelo Body
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <param model="Objeto cidade (Id, Nome, UF)"></param>
        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] Cidades model)
        {
            try
            {
                var cidade = await _cidadeService.GetCidadeById(model.Id);
                if (cidade != null)
                {
                    string errors = _cidadeService.CidadeValidate(model);
                    if (!string.IsNullOrWhiteSpace(errors))
                    {
                        // Retorna erro caso não seja válido
                        return BadRequest(errors);
                    }

                    _cidadeService.Update(model);
                    await _cidadeService.SaveChangeAsync();
                    return Ok();
                }
                else
                {
                    // Retorna erro caso não encontre o registro
                    return BadRequest("Não foi possível encontrar a cidade");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }

        }

        // DELETE api/5
        /// <summary>
        /// Remove uam cidade ao passar o ID
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <param id="id da cidade"></param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var cidade = await _cidadeService.GetCidadeById(id);
                if (cidade != null)
                {
                    _cidadeService.Delete(cidade);
                    await _cidadeService.SaveChangeAsync();
                    return Ok();
                }
                // Retorna erro caso não encontre o registro
                return BadRequest("Não foi possível encontrar a cidade");
            }
            catch (Exception ex)
            {

                return BadRequest($"Erro: {ex}");
            }
        }
    }
}
