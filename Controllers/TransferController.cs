
using Microsoft.AspNetCore.Mvc;
using BancoAPI.Entities;


namespace BancoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransferController : ControllerBase
    {
        private readonly BancoAPI.Context.BancoContext _context;

             public TransferController(BancoAPI.Context.BancoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpPost]
        [Route("RealizarTransferenciasEntreContas")]
        public ActionResult<string> Transferir([FromBody] TransferenciaModel? transferencia)
        {
            if (transferencia == null)
            {
                return BadRequest("Objeto de transferência inválido");
            }

            List<Conta> contas = _context.Contas.ToList();

            Conta? contaOrigem = contas.FirstOrDefault(c => c.Id == transferencia.IdContaOrigem);
            Conta? contaDestino = contas.FirstOrDefault(c => c.Id == transferencia.IdContaDestino);

            if (contaOrigem != null && contaDestino != null)
            {
                if (TransferirValor(contaOrigem, contaDestino, transferencia.Valor))
                {
                    _context.SaveChanges();
                    return "Transferência realizada com sucesso";
                }
                else
                {
                    return "Saldo insuficiente para realizar a transferência";
                }
            }
            else
            {
                return "Conta de origem ou conta de destino não encontrada";
            }
        }

        private bool TransferirValor(Conta origem, Conta destino, decimal valor)
        {
            if (origem.Saldo >= valor)
            {
                origem.Saldo -= valor;
                destino.Saldo += valor;
                return true;
            }
            return false;
        }

        [HttpPost]
        [Route("RealizarSaque")]
        public ActionResult<string> Sacar([FromBody] SaqueModel? saque)
        {
            if (saque == null)
            {
                return BadRequest("Objeto de saque inválido");
            }

            List<Conta> contas = _context.Contas.ToList();

            Conta? conta = contas.FirstOrDefault(c => c.Id == saque.IdConta);

            if (conta != null)
            {
                if (SacarValor(conta, saque.Valor))
                {
                    _context.SaveChanges();
                    return "Saque realizado com sucesso";
                }
                else
                {
                    return "Saldo insuficiente para realizar o saque";
                }
            }
            else
            {
                return "Conta não encontrada";
            }
        }

        private bool SacarValor(Conta conta, decimal valor)
        {
            if (conta.Saldo >= valor)
            {
                conta.Saldo -= valor;
                return true;
            }
            return false;
        }

        [HttpPost]
        [Route("RealizarDeposito")]
        public ActionResult<string> Depositar([FromBody] DepositoModel? deposito)
        {
            if (deposito == null)
            {
                return BadRequest("Objeto de depósito inválido");
            }

            List<Conta> contas = _context.Contas.ToList();

            Conta? conta = contas.FirstOrDefault(c => c.Id == deposito.IdConta);

            if (conta != null)
            {
                DepositarValor(conta, deposito.Valor);
                _context.SaveChanges();
                return "Depósito realizado com sucesso";
            }
            else
            {
                return "Conta não encontrada";
            }
        }

        private void DepositarValor(Conta conta, decimal valor)
        {
            conta.Saldo += valor;
        }
    }
}
