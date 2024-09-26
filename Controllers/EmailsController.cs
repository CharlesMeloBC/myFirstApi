using Microsoft.AspNetCore.Mvc;
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
            return $"Olá {mensagem.Name}, sua descrição é: {mensagem.Descricao}.";
        }
    }

    // Classe para o modelo
    public class MensagemModel
    {
        public string? Name { get; set; }
        public string? Descricao { get; set; }
    }
}