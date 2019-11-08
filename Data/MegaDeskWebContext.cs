using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MegaDeskWeb.Models
{
    public class MegaDeskWebContext : DbContext
    {
        public MegaDeskWebContext (DbContextOptions<MegaDeskWebContext> options)
            : base(options)
        {
        }

        public DbSet<MegaDeskWeb.Models.DeskQuote> DeskQuote { get; set; }
    }
}
