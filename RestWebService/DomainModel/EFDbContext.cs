using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class EFDbContext : DbContext
    {
        public DbSet<Mail> Mails { get; set; }
        public DbSet<Client> Clients { get; set; }
    }
}
