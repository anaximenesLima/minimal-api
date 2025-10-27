using System;
using MinimalApi.Dominio.DTOs;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Infraestrutura.Db;

namespace MinimalApi.Dominio.Servicos;

public class AdministradorServico : IAdministradorServico
{
    private readonly Dbcontexto _contexto;

    public AdministradorServico(Dbcontexto contexto)
    {
        contexto = _contexto;
    }

    public Administrador Incluir(AdministradorDTO administradorDTO)
    {
        var adm = new Administrador
        {
            
            Email = administradorDTO.Email,
            Senha = administradorDTO.Senha,
            Perfil = administradorDTO.Perfil.ToString()
        };
        _contexto.Administradores.Add(adm);
        _contexto.SaveChanges();
        return adm;
    }

    public Administrador? Login (LoginDTO loginDTO)
    {
        var adm = _contexto.Administradores.Where(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha).FirstOrDefault();
        return adm;
    }

    public Administrador ObterPorId(int id)
    {
        var adm = _contexto.Administradores.Find(id);
        return adm;
    }

    public List<Administrador> ObterTodos(int pagina)
    {
        int tamanhoPagina = 10;
        var administradores = _contexto.Administradores
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToList();
        return administradores;
    }
}