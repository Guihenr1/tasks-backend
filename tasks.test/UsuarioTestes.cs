using System;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using tasks.application.Interfaces;
using tasks.application.Services;
using tasks.domain.DTOs;
using tasks.domain.Entities;
using tasks.domain.Interfaces;
using tasks.domain.ViewModels;
using Xunit;

namespace tasks.test
{
    public class UsuarioTestes
    {
        Mock<IMapper> _mapper;
        Mock<IUsuarioRepository> _mockusuario;
        Mock<ISecurityService> _mockSecurity;
        public UsuarioTestes()
        {
            _mapper = new Mock<IMapper>();
            _mockusuario = new Mock<IUsuarioRepository>();
            _mockSecurity = new Mock<ISecurityService>();
        }

        [Fact(DisplayName = "Obter por Id - Deve retornar um usuário")]
        public async Task ObterPorId_DeveRetornarUmUsuario()
        {
            // Arrange
            var usuario = new Usuario(Guid.NewGuid(), "Guilherme", "ghp@email.com", "123456");
            var usuarioDto = new UsuarioDto();
            var id = Guid.NewGuid();

            // Act
            var mock = new Mock<IUsuarioRepository>();
            var mapper = new Mock<IMapper>();
            mock.Setup(c => c.ObterPorId(id));
            mapper.Setup(c => c.Map<UsuarioDto>(usuario)).Returns(usuarioDto);

            var service = new UsuarioService(mapper.Object, mock.Object, _mockSecurity.Object);
            await service.ObterPorId(id);
        
            // Assert
            mock.Verify(x => x.ObterPorId(id), Times.Once);
        }

        [Theory(DisplayName = "Validar Usuario - Dados inválidos")]
        [InlineData("")]
        [InlineData("asb")]
        public async Task AdicionarUsuario_Validar_DeveRetornarErroEmTodosOsCampos(string email)
        {
            // Arrange
            var usuario = new AdicionarUsuarioViewModel{
                Email = email
            };
        
            // Act
            var service = new UsuarioService(_mapper.Object, _mockusuario.Object, _mockSecurity.Object);
            var adicionar = await service.Adicionar(usuario);
        
            // Assert
            Assert.False(adicionar);
            Assert.False(usuario.ValidationResult.IsValid);
        }

        [Fact(DisplayName = "Adicionar Usuario - Inserir com sucesso")]
        public async Task AdicionarUsuario_DeveInserirComSucesso_RetornarTrue()
        {
            // Arrange
            var usuarioViewModel = new AdicionarUsuarioViewModel{
                Nome = "Guilherme",
                Email = "xxxxxx@gmail.com",
                Senha = "ASGFJS"
            };
            var usuario = new Usuario(Guid.NewGuid(), "Guilherme", "xxxxxx@gmail.com", "ASGFJS");
        
            // Act
            var mock = new Mock<IUsuarioRepository>();
            var mapper = new Mock<IMapper>();
            mock.Setup(c => c.Adicionar(usuario));
            mock.Setup(c => c.UnitOfWork.Commit()).Returns(Task.FromResult(true));
            mapper.Setup(c => c.Map<Usuario>(usuarioViewModel)).Returns(usuario);

            var service = new UsuarioService(mapper.Object, mock.Object, _mockSecurity.Object);
            var adicionar = await service.Adicionar(usuarioViewModel);
        
            // Assert
            Assert.True(usuarioViewModel.EhValido());
            Assert.True(adicionar);
        }

        [Theory(DisplayName = "Autenticar Usuario - Dados inválidos")]
        [InlineData("")]
        [InlineData("asb")]
        public async Task AutenticarUsuario_Validar_DeveRetornarErro(string email)
        {
            // Arrange
            var autenticacao = new AutenticacaoRequisicaoViewModel{
                Email = email,
                Senha = string.Empty
            };
        
            // Act
            var service = new UsuarioService(_mapper.Object, _mockusuario.Object, _mockSecurity.Object);
            var autenticar = await service.Autenticar(autenticacao);
        
            // Assert
            Assert.Null(autenticar);
            Assert.False(autenticacao.ValidationResult.IsValid);
        }

        [Fact(DisplayName = "Autenticar Usuario - Usuario não encontrado")]
        public async Task AutenticarUsuario_UsuarioInvalido_RetornarNull()
        {
            // Arrange
            var autenticacaoViewModel = new AutenticacaoRequisicaoViewModel();
            var autenticacao = new Usuario(Guid.NewGuid(), "Guilherme", "xxxx@gmail.com", "123456");
        
            // Act
            var mapper = new Mock<IMapper>();
            var mock = new Mock<IUsuarioRepository>();
            mapper.Setup(c => c.Map<Usuario>(autenticacaoViewModel)).Returns(autenticacao);
            mock.Setup(c => c.ObterPorEmailESenha(autenticacao)).Returns(Task.FromResult<Usuario>(null));

            var service = new UsuarioService(mapper.Object, mock.Object, _mockSecurity.Object);
            var adicionar = await service.Autenticar(autenticacaoViewModel);
        
            // Assert
            Assert.Null(adicionar);
        }

        [Fact(DisplayName = "Autenticar Usuario - Usuario autenticado")]
        public async Task Trocar_Nome_Metodo()
        {
            // Arrange
            var autenticacaoViewModel = new AutenticacaoRequisicaoViewModel{
                Email = "xxxx@gmail.com",
                Senha = "abc"
            };
            var autenticacaoRespostaViewModel = new AutenticacaoRespostaViewModel();
            var autenticacao = new Usuario(Guid.NewGuid(), "Guilherme", "xxxx@gmail.com", "123456");
            var usuarioDto = new UsuarioDto(){Id = Guid.NewGuid()};
        
            // Act
            var mapper = new Mock<IMapper>();
            var mockUsuario = new Mock<IUsuarioRepository>();
            var mockSecurity = new Mock<ISecurityService>();
            mapper.Setup(c => c.Map<Usuario>(autenticacaoViewModel)).Returns(autenticacao);
            mockUsuario.Setup(c => c.ObterPorEmailESenha(autenticacao)).Returns(Task.FromResult(autenticacao));
            mapper.Setup(c => c.Map<AutenticacaoRespostaViewModel>(autenticacao)).Returns(autenticacaoRespostaViewModel);
            mapper.Setup(c => c.Map<UsuarioDto>(autenticacao)).Returns(usuarioDto);
            mockSecurity.Setup(c => c.gerarJwtToken(usuarioDto)).Returns("asfgvbv");

            var service = new UsuarioService(mapper.Object, mockUsuario.Object, mockSecurity.Object);
            var adicionar = await service.Autenticar(autenticacaoViewModel);
        
            // Assert
            Assert.IsType(typeof(AutenticacaoRespostaViewModel), adicionar);
        }
    }
}
