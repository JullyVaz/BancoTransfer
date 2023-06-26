using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using BancoAPI.Entities;

namespace BancoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaController : ControllerBase
    {
        private readonly BancoAPI.Context.BancoContext _context;

        public ContaController(BancoAPI.Context.BancoContext context)
        {
            _context = context;
        }

        [HttpPost("CadastrarNovaConta")]
        public IActionResult CadastrarNovaConta(
            [Required] string Nome,
            [Required] string Cpf,
            [Required] [EnumDataType(typeof(TipoContaEnum))] TipoContaEnum TipoConta,
            [Required] int Saldo)
        {
            Conta conta = new Conta();
            conta.Nome = Nome;
            conta.Cpf = Cpf;
            conta.TipoConta = TipoConta;
            conta.Saldo = Saldo;

            _context.Add(conta);
            _context.SaveChanges();

            return CreatedAtAction(nameof(ConsultarConta), new { id = conta.Id }, conta);
        }

        [HttpGet("ConsultarConta/{id}")]
        public IActionResult ConsultarConta(int id)
        {
            var conta = _context.Contas.Find(id);

            if (conta is null)
                return NotFound();

            return Ok(conta);
        }

        [HttpDelete("DeletarConta/{id}")]
        public IActionResult DeletarConta(int id)
        {
            var conta = _context.Contas.Find(id);

            if (conta is null)
                return NotFound();

            _context.Contas.Remove(conta);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
