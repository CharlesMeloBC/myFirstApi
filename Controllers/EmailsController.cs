using DeverDeCasa.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
namespace DeverDeCasa.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class EmailsController : ControllerBase

    {
        [HttpGet("texto")]
        public ActionResult<string> GetTexto([FromQuery] string mensagem)
        {
            if (mensagem.Length < 10)
            {
                return BadRequest("Descrição precisa ter mais de 10 caracters");
            }

            return $"Você enviou {mensagem}";
        }

        [HttpGet("ola")]
        public ActionResult<string> GetSaudacao([FromQuery] string name)
        {
            if (name.Length < 10)
            {
                return BadRequest(" precisa ter mais de 10 caracters");
            }
            return $"Hello {name}";
        }

        [HttpPost("mensagem")]
        public ActionResult<string> PostMensagem([FromBody] MensagemModel mensagem)
        {
            if (mensagem == null || string.IsNullOrEmpty(mensagem.Name) || string.IsNullOrEmpty(mensagem.Descricao))
            {
                return BadRequest("Os campos Name e Descrição são obrigatórios.");
            }
            if (mensagem.Name.Length < 10 || mensagem.Descricao.Length < 10)
            {
                return BadRequest(" precisa ter mais de 10 caracters");
            }

            return $"Olá {mensagem.Name}, sua descrição é: {mensagem.Descricao}.";
        }

        private static List<MensagemModel> posts = new List<MensagemModel>();

        [HttpPost]
        public ActionResult<MensagemModel> CreatePost([FromBody] MensagemModel mensagem)
        {
            if (mensagem == null)
            {
                return BadRequest("Post não pode ser nulo.");
            }
            if (mensagem.Id >=0 )
            {
                mensagem.Id = mensagem.Id++;
            }

            posts.Add(mensagem);
            return CreatedAtAction(nameof(GetPosts), new { titulo = mensagem.Name }, mensagem);
        }

        [HttpGet]
        public ActionResult<List<MensagemModel>> GetPosts()
        {
            return Ok(posts);
        }
    }

    public class MensagemModel

    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Descricao { get; set; }
    }
}
