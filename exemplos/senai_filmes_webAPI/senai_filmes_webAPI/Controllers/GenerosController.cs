using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai_filmes_webAPI.Domains;
using senai_filmes_webAPI.Interfaces;
using senai_filmes_webAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Controller responsável pelos endpoints(rotas) referentes aos gêneros.
/// </summary>
namespace senai_filmes_webAPI.Controllers
{
    // Define que o tipo de resposta da API será no formato JSON
    [Produces("application/json")]

    // Define que uma rota de requisição será no formato dominio/api/nomeController.
    // Ex: http://localhost:5000/api/generos
    [Route("api/[controller]")]
    // Define que é um controlador de API.
    [ApiController]

    public class GenerosController : ControllerBase
    {
        private IGeneroRepository _generoRepository { get; set; }

        public GenerosController()
        {
            _generoRepository = new GeneroRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<GeneroDomain> listaGeneros = _generoRepository.ListarTodos();

            return Ok(listaGeneros);
        }

        [HttpPost]
        public IActionResult Post(GeneroDomain novoGenero)
        {
            _generoRepository.Cadastrar(novoGenero);

            return StatusCode(201);
        }
    }
}
