using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using W9_assignment_template.Models;

namespace W9_assignment_template.Data;

public class GameContext : DbContext
{
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Ability> Abilities { get; set; }

    public GameContext(DbContextOptions<GameContext> options) : base(options)
    {
    }

//I keep getting this error and idk how to fix it: Cannot configure the discriminator value for entity type 'Player' because it doesn't derive from 'Character'.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Register derived types explicitly
    modelBuilder.Entity<Player>();
    modelBuilder.Entity<Goblin>();

    // Configure TPH for Character hierarchy
    modelBuilder.Entity<Character>()
        .HasDiscriminator<string>("Discriminator")
        .HasValue<Character>("Character")
        .HasValue<Player>("Player")
        .HasValue<Goblin>("Goblin");

    base.OnModelCreating(modelBuilder);
}

}