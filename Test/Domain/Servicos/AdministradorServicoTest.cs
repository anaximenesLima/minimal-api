
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Infraestrutura.Db;
namespace Test.Domain.Servicos;

[TestClass]
public class AdministradorServicoTest
{
    [TestMethod]
    public void TestandoSalvarAdministrador()
    {
        // Arrange
        var administrador = new Administrador();
        administrador.Id = 1;
        administrador.Email = "admin@test.com";
        administrador.Senha = "teste";
        administrador.Perfil = "Admin";
        // Act
       var contexto = new Dbcontexto();

        contexto.Administradores.Add(administrador);
        contexto.SaveChanges();

        // Assert
        Assert.AreEqual(1, administrador.Id);
        Assert.AreEqual("admin@test.com", administrador.Email);
        Assert.AreEqual("teste", administrador.Senha);
        Assert.AreEqual("Admin", administrador.Perfil);
    }
}