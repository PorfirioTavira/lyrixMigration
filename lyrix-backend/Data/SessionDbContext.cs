using Microsoft.EntityFrameworkCore;
using MyApi.Models;

namespace MyApi.Data;

public class SessionDbContext : DbContext
{
    public SessionDbContext(DbContextOptions<SessionDbContext> options)
    : base(options) { }

    public DbSet<Session> Sessions => Set<Session>();
}