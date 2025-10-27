using System;
using MinimalApi.Dominio.DTOs;
using MinimalApi.Dominio.Entidades;

namespace MinimalApi.Dominio.Interfaces;

public interface IAdministradorServico
{
    Administrador? Login(LoginDTO loginDTO);
    Administrador Incluir(AdministradorDTO administradorDTO);
    List<Administrador> ObterTodos(int pagina);
    Administrador? ObterPorId(int id);
}