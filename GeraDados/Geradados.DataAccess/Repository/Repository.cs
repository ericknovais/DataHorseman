﻿using Geradados.DataAccess.DB;
using GeraDados.DataModel.models;
using GeraDados.DataModel.repository;

namespace Geradados.DataAccess.Repository;

public class Repository : IRepository
{
    ContextoDataBase ctx;
    public Repository()
    {
        ctx = new ContextoDataBase();
    }

    IPessoaRepository? pessoa;
    public IPessoaRepository Pessoa { get { return pessoa ?? (pessoa = new PessoaRepository(ctx)); } }

    IEnderecoRepository? endereco;
    public IEnderecoRepository Endereco { get { return endereco ?? (endereco = new EnderecoRepository(ctx)); } }

    IContatoRepository? contato;
    public IContatoRepository Contato { get { return contato ?? (contato = new ContatoRepository(ctx)); } }

    ITipoContatoRepository? tipoContato;
    public ITipoContatoRepository TipoContato { get { return tipoContato ?? (tipoContato = new TipoContatoRepository(ctx)); } }

    ITipoDeAtivoRepository? tipoDeAtivo;
    public ITipoDeAtivoRepository TipoDeAtivo { get { return tipoDeAtivo ?? (tipoDeAtivo = new TipoDeAtivoRepository(ctx)); } }

    IAtivoRepository? ativo;
    public IAtivoRepository Ativo { get { return ativo ?? (ativo = new AtivoRepository(ctx)); } }

    ICarteiraRepository? carteira;
    public ICarteiraRepository Carteira  {get { return carteira ?? (carteira = new CarteiraRepository(ctx)); } }

    public void SaveChanges()
    {
        ctx.SaveChanges();
    }
}