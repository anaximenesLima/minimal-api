using System;
using MinimalApi.Dominio.DTOs;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Infraestrutura.Db;

namespace MinimalApi.Dominio.Servicos;

public class VeiculoServico : IveiculoServico
{
    private readonly Dbcontexto _contexto;

    public VeiculoServico(Dbcontexto contexto)
    {
        _contexto = contexto;
    }

    public void Incluir(Veiculo veiculo)
    {
        _contexto.Veiculos.Add(veiculo);
        _contexto.SaveChanges();
    }

    public Veiculo ObterPorId(int id)
    {
        return _contexto.Veiculos.Find(id);
    }

    public List<Veiculo> ObterTodos()
    {
        return _contexto.Veiculos.ToList();
    }

    public void Atualizar(Veiculo veiculo)
    {
        _contexto.Veiculos.Update(veiculo);
        _contexto.SaveChanges();
    }
    public void Excluir(Veiculo veiculo)
    {
        _contexto.Veiculos.Remove(veiculo);
        _contexto.SaveChanges();
    }
}