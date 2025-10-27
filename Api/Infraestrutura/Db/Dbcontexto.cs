using Microsoft.EntityFrameworkCore;
using  MinimalApi.Dominio.Entidades;


namespace MinimalApi.Infraestrutura.Db;

public class Dbcontexto : DbContext
{
    private readonly IConfiguration _configuracaoAppSettings;
    public Dbcontexto(IConfiguration configuracaoAppSettings) 
    {
        _configuracaoAppSettings = configuracaoAppSettings;
    }
    public DbSet<Administrador> Administradores { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Administrador>().HasData(new Administrador
        {
            Id = 1,
            Email = "admin@example.com",
            Senha = "123456",
            Perfil = "Adm"
            
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {


            var stringDeConexao = _configuracaoAppSettings.GetConnectionString("mysql")?.ToString();
            if (!string.IsNullOrEmpty(stringDeConexao))
            {
                optionsBuilder.UseMySql(
                    stringDeConexao,
                     ServerVersion.AutoDetect(stringDeConexao)
                );

            }
        }
       
    }
    public DbSet<Veiculo> Veiculos { get; set; } = default!;

}