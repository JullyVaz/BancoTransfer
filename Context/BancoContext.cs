using Microsoft.EntityFrameworkCore;
using BancoAPI.Entities;

namespace BancoAPI.Context
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {

        }

        public DbSet<Conta> Contas { get; set; } = null!;
        public DbSet<Transfer> Transfer {get; set;} = null!;
       
    }
}

