using Geradados.Infrastructure.Persistence;
using GeraDados.Domain.Entities;
using GeraDados.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geradados.Infrastructure.Intercafes
{
    public class TipoDeAtivoRepository : RepositoryBase<TipoDeAtivo>, ITipoDeAtivoRepository
    {
        ContextoDataBase ctx = new ContextoDataBase();
        public TipoDeAtivoRepository(ContextoDataBase contexto) : base(contexto)
        {
            ctx = contexto;
        }
    }
}
