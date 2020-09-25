using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using tasks.application.Services;
using tasks.domain.Entities;
using tasks.domain.Interfaces;
using tasks.domain.ViewModels;
using Xunit;

namespace tasks.test
{
    public class TarefaTestes
    {
        Mock<IMapper> _mapper;
        Mock<ITarefaRepository> _mock;
        public TarefaTestes()
        {
            _mapper = new Mock<IMapper>();
            _mock = new Mock<ITarefaRepository>();
        }

        [Fact(DisplayName = "Obter Todos - Deve retornar uma lista")]
        public void ObterTodos_DeveRetornarUmaLista()
        {
            // Arrange
            IEnumerable<Tarefa> lista = new List<Tarefa>(){
                new Tarefa(Guid.NewGuid(), "tarefa", DateTime.Now, Guid.NewGuid())
            };
            var clientId = Guid.NewGuid();
            var data = DateTime.Now;
        
            // Act
            var mock = new Mock<ITarefaRepository>();
            mock.Setup(t => t.ObterTodos(clientId, data)).Returns(lista);

            var service = new TarefaService(mock.Object, _mapper.Object);
            service.ObterTodos(clientId, data);
        
            // Assert
            mock.VerifyAll();
        }

        [Theory(DisplayName = "Validar Tarefa - Dados inválidos")]
        [InlineData("", null)]
        [InlineData("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", "2019-10-11")]
        public async Task AdicionarTarefa_Validacao_DeveDarErro(string descricao, string estimado)
        {
            // Arrange
            var tarefa = new TarefaRequestViewModel{
                Descricao = descricao,
                Estimado = Convert.ToDateTime(estimado)
            };
        
            // Act
            var service = new TarefaService(_mock.Object, _mapper.Object);
            var adicionar = await service.Adicionar(tarefa, Guid.NewGuid());
        
            // Assert
            Assert.False(adicionar);
            Assert.False(tarefa.ValidationResult.IsValid);
        }

        [Fact(DisplayName = "Adicionar Tarefa - Tarefa inserida com sucesso")]
        public async Task AdicionarTarefa_CriarTarefaComSucesso()
        {
            // Arrange
            var tarefaViewModel = new TarefaRequestViewModel{
                Descricao = "abc",
                Estimado = DateTime.Now.AddDays(1)
            };
            var tarefa = new Tarefa(Guid.NewGuid(), "abc", DateTime.Now.AddDays(1), Guid.NewGuid());
        
            // Act
            var mock = new Mock<ITarefaRepository>();
            mock.Setup(c => c.Adicionar(tarefa));
            mock.Setup(c => c.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            var service = new TarefaService(mock.Object, _mapper.Object);
            var adicionar = await service.Adicionar(tarefaViewModel, Guid.NewGuid());
        
            // Assert
            Assert.True(adicionar);
        }

        [Fact(DisplayName = "Alternar status tarefa - Tarefa não encontrada")]
        public void AlternarStatusTarefa_TarefaNaoEncontrada_DeveDarErro()
        {
            // Act
            var mock = new Mock<ITarefaRepository>();
            mock.Setup(x => x.ObterPorId(Guid.NewGuid())).Returns<object>(null);

            var service = new TarefaService(mock.Object, _mapper.Object);
        
            // Assert
            Assert.ThrowsAsync<Exception>(() => service.Alternar(Guid.NewGuid()));
        }

        [Fact(DisplayName = "Alternar Status Tarefa - Alternar status com sucesso")]
        public async Task AlternarStatusTarefa_AlternarTarefaComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            var tarefa = new Tarefa(Guid.NewGuid(), "xxx", DateTime.Now, Guid.NewGuid());
        
            // Act
            var mock = new Mock<ITarefaRepository>();
            mock.Setup(x => x.ObterPorId(id)).Returns(Task.FromResult(tarefa));
            mock.Setup(c => c.Alternar(tarefa));
            mock.Setup(c => c.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            var service = new TarefaService(mock.Object, _mapper.Object);
            var alternar = await service.Alternar(id);
        
            // Assert
            Assert.True(alternar);
        }
    }
}
