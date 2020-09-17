using tasks.domain.DTOs;

namespace tasks.application.Interfaces
{
    public interface ISecurityService
    {
        string gerarJwtToken(UsuarioDto usuario);
    }
}