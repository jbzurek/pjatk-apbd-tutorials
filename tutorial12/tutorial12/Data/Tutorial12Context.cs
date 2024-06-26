using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tutorial12.Models;

namespace tutorial12.Data;

public class Tutorial12Context : DbContext
{
    public Tutorial12Context (DbContextOptions<Tutorial12Context> options)
        : base(options)
    {
    }

    public DbSet<tutorial12.Models.Movie> Movie { get; set; } = default!;
}