using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Rental.WebApi.Domains;
using Senai.Rental.WebApi.Interfaces;
using Senai.Rental.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Controllers
{
    [Produces("application/json")]

    //Define que a rota de uma requisição será no formato domino/api/nomeController.
    // ex: http://localhost:5000/api/clientes
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private IClienteRepository _clienteRepository { get; set; }
        public ClientesController()
        {
            _clienteRepository = new ClienteRepository();
        }

        [HttpPost]
        public IActionResult Cadastrar(ClienteDomain novoCliente)
        {
            try
            {
                _clienteRepository.Cadastrar(novoCliente);

                return StatusCode(201);
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
            
        }

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                List<ClienteDomain> listaClientes = _clienteRepository.ListarTodos();

                return Ok(listaClientes);
            }
            catch (Exception codErro)
            {

                return BadRequest(codErro);
            }
            
        }

        [HttpGet("{id}")]
        public IActionResult Buscar(int id)
        {
            ClienteDomain clienteBuscado = _clienteRepository.BuscarPorId(id);

            if (clienteBuscado == null)
            {
                return NotFound("Nenhum cliente foi encontrado");
            }
            return Ok(clienteBuscado);
        }

        [HttpPut]
        public IActionResult Atualizar(ClienteDomain clienteAtualizado)
        {
            if (clienteAtualizado.nomeCliente == null || clienteAtualizado.idCliente <= 0 || clienteAtualizado.sobrenomeCliente == null || clienteAtualizado.CNH == null)
            {
                return BadRequest(
                    new
                    {
                        mensagem = "Dados não informados",
                    }
                    );
            }

            ClienteDomain clienteBuscado = _clienteRepository.BuscarPorId(clienteAtualizado.idCliente);

            if (clienteBuscado != null)
            {
                try
                {
                    _clienteRepository.AtualizarCliente(clienteAtualizado);

                    return NoContent();
                }
                catch (Exception erro)
                {
                    return BadRequest(erro);
                }
            }

            return NotFound(
                new
                {
                    mensagem = "Cliente não encontrado",
                    errorStatus = true
                }
                );
        }

        [HttpDelete("deletar/{id}")]
        public IActionResult Deletar(int id)
        {
            ClienteDomain clienteBuscado = _clienteRepository.BuscarPorId(id);

            if (clienteBuscado != null)
            {
                try
                {
                    _clienteRepository.Deletar(id);
                }
                catch (Exception erro)
                {

                    return BadRequest(erro);
                }

                return NoContent();
            }

            return NotFound("Nenhum cliente foi encontrado para ser deletado");
        }
    }
}
