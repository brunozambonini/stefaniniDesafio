using Application.Models;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Stefanini.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly IPessoasService _pessoasService;

        public PessoasController(IPessoasService pessoaService)
        {
            _pessoasService = pessoaService;
        }

        // GET: api/Pessoas
        /// <summary>
        /// Retorna todos lista de todas pessoas, sem filtro
        /// </summary>
        /// <returns>Retorna lista de pessoas</returns>
        /// <response code="200">Retorna lista de pessoas</response>
        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var pessoas = await _pessoasService.GetAll("");
                if (pessoas != null)
                {
                    return Ok(pessoas);
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
        /// Retornar lista de Pessoa filtrando pelo nome
        /// </summary>
        /// <returns>Retorna lista de pessoas</returns>
        /// <response code="200">Retorna lista de pessoas</response>
        /// <param filter="termo usado para filtrar"></param>
        [HttpGet("{filter}")]
        public async Task<IActionResult> GetAllFiltering(string filter)
        {
            try
            {
                var pessoas = await _pessoasService.GetAll(filter);
                if (pessoas != null)
                {
                    return Ok(pessoas);
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

        //POST api/Pessoa
        /// <summary>
        /// Cria uma nova Pessoa
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <param model="Objeto pessoa (Id, Nome, CPF, Idade, Id_Cidade)"></param>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Pessoas model)
        {
            try
            {
                string erros = _pessoasService.PessoaValidate(model);
                if (!string.IsNullOrWhiteSpace(erros))
                {
                    // Retorna erro caso não seja válido
                    return BadRequest(erros);
                }

                // Salva no BD
                _pessoasService.Add(model);
                if (await _pessoasService.SaveChangeAsync())
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
        /// Edita os dados da pessoa ao passar o objeto do Pessoa pelo Body
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <param model="Objeto pessoa (Id, Nome, CPF, Idade, Id_Cidade)"></param>
        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] Pessoas model)
        {
            try
            {
                var pessoa = await _pessoasService.GetPessoaById(model.Id);
                if (pessoa != null)
                {
                    string errors = _pessoasService.PessoaValidate(model);
                    if (!string.IsNullOrWhiteSpace(errors))
                    {
                        // Retorna erro caso não seja válido
                        return BadRequest(errors);
                    }

                    _pessoasService.Update(model);
                    await _pessoasService.SaveChangeAsync();
                    return Ok();
                }
                else
                {
                    // Retorna erro caso não encontre o registro
                    return BadRequest("Não foi possível encontrar a pessoa");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }

        }

        // DELETE api/5
        /// <summary>
        /// Remove um pessoa ao passar o ID
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <param id="id da pessoa"></param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var pessoa = await _pessoasService.GetPessoaById(id);
                if (pessoa != null)
                {
                    _pessoasService.Delete(pessoa);
                    await _pessoasService.SaveChangeAsync();
                    return Ok();
                }
                // Retorna erro caso não encontre o registro
                return BadRequest("Não foi possível encontrar o pessoa");
            }
            catch (Exception ex)
            {

                return BadRequest($"Erro: {ex}");
            }
        }
    }
}
