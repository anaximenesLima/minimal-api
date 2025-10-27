using System;
using MinimalApi.Dominio.DTOs;
using MinimalApi.Dominio.Entidades;

namespace MinimalApi.Dominio.Interfaces;

public interface IveiculoServico
{
    void Incluir(Veiculo veiculo);
    List<Veiculo> ObterTodos();
    Veiculo ObterPorId(int id);
    void Atualizar(Veiculo veiculo);

    void Excluir(Veiculo veiculo);
}