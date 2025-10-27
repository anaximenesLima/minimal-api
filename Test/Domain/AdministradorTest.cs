
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinimalApi.Dominio.Entidades;
namespace Test.Domain.Entidades;

[TestClass]
public class AdministradorTest
{
    [TestMethod]
    public void TestGetSetPropriedades()
    {
        // Arrange
        var administrador = new Administrador();
       
        // Act
        administrador.Id = 1;
        administrador.Email = "admin@test.com";
        administrador.Senha = "teste";
        administrador.Perfil = "Admin";

        // Assert
        Assert.AreEqual(1, administrador.Id);
        Assert.AreEqual("admin@test.com", administrador.Email);
        Assert.AreEqual("teste", administrador.Senha);
        Assert.AreEqual("Admin", administrador.Perfil);
    }
}