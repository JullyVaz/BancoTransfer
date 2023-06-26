
namespace BancoAPI.Entities
{
    public class Conta
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Cpf { get; set; }
    public decimal Saldo { get; set; }
    public TipoContaEnum TipoConta { get; set; }
}

}