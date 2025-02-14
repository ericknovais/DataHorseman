﻿using Geradados.Infrastructure.Persistence;
using GeraDados.Domain.Entities;
using GeraDados.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geradados.Infrastructure.Intercafes
{
    public class EnderecoRepository : RepositoryBase<Endereco>, IEnderecoRepository
    {
        ContextoDataBase ctx = new ContextoDataBase();
        public EnderecoRepository(ContextoDataBase contexto) : base(contexto)
        {
            ctx = contexto;
        }

        public Endereco? ObtemEnderecoPorIdPessoa(int idPessoa)
        {
            return ctx.Enderecos.FirstOrDefault(endereco => endereco.Pessoa.ID.Equals(idPessoa));
        }
    }
}
