using BancoAPI.Enums;

namespace BancoAPI.Entities
{
    public class Transfer
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }

        public EnumTipoTransacao TipoTransacao { get; set; }
        
    }
    
    public class TransferenciaModel
    {
        public int IdContaOrigem { get; set; }
        public int IdContaDestino { get; set; }
        public decimal Valor { get; set; }
    }

    public class SaqueModel
    {
        public int IdConta { get; set; }
        public decimal Valor { get; set; }
    }

    public class DepositoModel
    {
        public int IdConta { get; set; }
        public decimal Valor { get; set; }
    }
}
