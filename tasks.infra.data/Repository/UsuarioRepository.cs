using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tasks.domain.Entities;
using tasks.domain.Interfaces;

namespace tasks.infra.data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly TarefaContext context;

        public UsuarioRepository(TarefaContext context)
        {
            this.context = context;
        }

        public IUnitOfWork UnitOfWork => context;

        public async Task Adicionar(Usuario usuario)
        {
            await context.AddAsync(usuario);
        }

        public void Dispose()
        {
            context?.Dispose();
        }

        public async Task<Usuario> ObterPorEmailESenha(Usuario usuario)
        {
            return await context.Usuarios.SingleOrDefaultAsync(
                    x => x.Email == usuario.Email && x.Senha == usuario.Senha
                );
        }

        public async Task<Usuario> ObterPorId(Guid id)
        {
            return await context.Usuarios.FindAsync(id);
        }
    }
}