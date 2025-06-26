using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data;

public class SessionDbContext : DbContext
{
    public SessionDbContext(DbContextOptions<SessionDbContext> options)
    : base(options) { }

    public DbSet<Session> Sessions => Set<Session>();
}