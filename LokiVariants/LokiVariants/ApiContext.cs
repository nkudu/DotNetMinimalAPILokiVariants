namespace LokiVariants
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    public class ApiContext : DbContext
    {
        public DbSet<Variant> Variants { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        { }
    }
}
