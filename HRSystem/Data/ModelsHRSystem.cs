using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HRSystem.Models;

    public class ModelsHRSystem : DbContext
    {
        public ModelsHRSystem (DbContextOptions<ModelsHRSystem> options)
            : base(options)
        {
        }

        public DbSet<HRSystem.Models.Division> Division { get; set; }
    }
