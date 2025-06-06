﻿using DataHorseman.Domain.Entidades;
using DataHorseman.Domain.Enums;
using DataHorseman.Domain.Interfaces;
using DataHorseman.Infrastructure.Persistencia.DataContext;
using Microsoft.EntityFrameworkCore;

namespace DataHorseman.Infrastructure.Persistencia.Repositories
{
    public class AtivoRepository : RepositoryBase<Ativo>, IAtivoRepository
    {
        DataHorsemanDbContext _contexto = new DataHorsemanDbContext();
        public AtivoRepository(DataHorsemanDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public Task<List<Ativo>> ObtemAtivosPorTipoDeAtivoIDAsync(eTipoDeAtivo tipoDeAtivoID)
        {
             var ativos = _contexto.Ativos
                    .Where(ativo => ativo.TipoDeAtivoId == (int)tipoDeAtivoID)
                    .ToListAsync();
            return ativos;
        }
    }
}