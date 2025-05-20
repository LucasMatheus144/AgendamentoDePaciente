using Aplication.Domain.Entities;
using Aplication.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Aplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly PacienteService servico;

        public PacienteController(PacienteService servico)
        {
            this.servico = servico;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(servico.AllPaciente());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(servico.ListarPorId(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Paciente obj)
        {
            var resultado = servico.Cadastra(obj, out List<MensagemErro> erros);

            if (resultado)
            {
                return Ok(obj);
            }

            return UnprocessableEntity(erros);
        }

        [HttpPut]
        public IActionResult Put(Paciente obj)
        {
            var resultado = servico.Editar(obj);

            if (resultado != null)
            {
                return Ok(obj);
            }

            return NotFound(resultado);
        }


        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var resultado = servico.Excluir(id);

            if (resultado)
            {
                return Ok(resultado);
            }

            return NotFound();
        }
    }
}
