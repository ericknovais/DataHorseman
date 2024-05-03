﻿namespace GeraDados.DataModel.repository;

public interface IRepository
{
    void SaveChanges();
    public IPessoaRepository Pessoa { get;}
    public IEnderecoRepository Endereco { get;}
    public IContatoRepository Contato { get;}
    public ITipoContatoRepository TipoContato { get;}
    public ITipoDeAtivoRepository TipoDeAtivo { get;}
    public IAtivoRepository Ativo { get;}
}