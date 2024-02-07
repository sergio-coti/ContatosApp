using ContatosApp.API.Models;
using ContatosApp.Data.Entities;
using ContatosApp.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContatosApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatosController : ControllerBase
    {
        /// <summary>
        /// Método para cadastro do contato na API
        /// </summary>
        [HttpPost]
        public IActionResult Post(ContatosPostModel model)
        {
            try
            {
                var contato = new Contato
                {
                    Id = Guid.NewGuid(),
                    Nome = model.Nome,
                    Email = model.Email,
                    Telefone = model.Telefone,
                    DataHoraCadastro = DateTime.Now,
                    Ativo = 1
                };

                var contatoRepository = new ContatoRepository();
                contatoRepository.Insert(contato);

                //HTTP 201 - CREATED
                return StatusCode(201, new 
                { 
                    message = "Contato cadastrado com sucesso." ,
                    contato
                });
            }
            catch(Exception e)
            {
                //HTTP 500 - INTERNAL SERVER ERROR
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpPut]
        public IActionResult Put(ContatosPutModel model)
        {
            try
            {
                //consultar o contato no banco de dados através do ID
                var contatoRepository = new ContatoRepository();
                var contato = contatoRepository.GetById(model.Id.Value);

                //verificar se o contato foi encontrado
                if(contato != null)
                {
                    //modificando os dados do contato
                    contato.Nome = model.Nome;
                    contato.Email = model.Email;
                    contato.Telefone = model.Telefone;

                    //atualizar os dados do contato
                    contatoRepository.Update(contato);

                    return StatusCode(200, new 
                    { 
                        message = "Contato atualizado com sucesso.",
                        contato
                    });
                }
                else
                {
                    return StatusCode(400, new { message = "Contato não encontrado. Verifique o ID." });
                }
            }
            catch(Exception e)
            {
                //HTTP 500 - INTERNAL SERVER ERROR
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id) 
        {
            try
            {
                //consultando o contato através do ID
                var contatoRepository = new ContatoRepository();
                var contato = contatoRepository.GetById(id);

                //verificando se o contato foi encontrado
                if(contato != null)
                {
                    //excluir o contato
                    contatoRepository.Delete(contato);
                    return StatusCode(200, new 
                    { 
                        message = "Contato excluído com sucesso.",
                        contato
                    });
                }
                else
                {
                    return StatusCode(400, new { message = "Contato não encontrado. Verifique o ID." });
                }
            }
            catch(Exception e)
            {
                //HTTP 500 - INTERNAL SERVER ERROR
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var contatoRepository = new ContatoRepository();
                var contatos = contatoRepository.GetAll();

                return StatusCode(200, contatos);
            }
            catch(Exception e)
            {
                //HTTP 500 - INTERNAL SERVER ERROR
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var contatoRepository = new ContatoRepository();
                var contato = contatoRepository.GetById(id);

                if(contato != null)
                {
                    //HTTP 200 (OK)
                    return StatusCode(200, contato);
                }
                else
                {
                    return NoContent(); //HTTP 204
                }
            }
            catch(Exception e)
            {
                //HTTP 500 - INTERNAL SERVER ERROR
                return StatusCode(500, new { e.Message });
            }
        }
    }
}
