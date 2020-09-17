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
        public async Task ObterTodos_DeveRetornarUmaLista()
        {
            // Arrange
            IEnumerable<Tarefa> lista = new List<Tarefa>(){
                new Tarefa(Guid.NewGuid(), "tarefa", DateTime.Now, Guid.NewGuid())
            };
        
            // Act
            var mock = new Mock<ITarefaRepository>();
            mock.Setup(t => t.ObterTodos()).Returns(Task.FromResult(lista));

            var service = new TarefaService(mock.Object, _mapper.Object);
            await service.ObterTodos();
        
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

        [Fact(DisplayName = "Fechar Tarefa - Dados inválidos")]
        public async Task FecharTarefa_Validacao_DeveDarErro()
        {
            // Arrange
            var tarefa = new FecharTarefaRequestViewModel{
                Descricao = "",
                Estimado = DateTime.MinValue,
                Id = Guid.Empty
            };
        
            // Act
            var service = new TarefaService(_mock.Object, _mapper.Object);
            var fechar = await service.Fechar(tarefa);
        
            // Assert
            Assert.False(fechar);
            Assert.False(tarefa.ValidationResult.IsValid);
            Assert.Equal(tarefa.ValidationResult.Errors.Count, 3);
        }

        [Fact(DisplayName = "Fechar Tarefa - Fechar tarefa com sucesso")]
        public async Task FecharTarefa_FecharTarefaComSucesso()
        {
            // Arrange
            var tarefaViewModel = new FecharTarefaRequestViewModel{
                Descricao = "xxx",
                Estimado = DateTime.Now.AddDays(1),
                Id = Guid.NewGuid()
            };
            var tarefa = new Tarefa(Guid.NewGuid(), "xxx", DateTime.Now, Guid.NewGuid());
        
            // Act
            var mock = new Mock<ITarefaRepository>();
            var mapper = new Mock<IMapper>();
            mapper.Setup(c => c.Map<Tarefa>(tarefaViewModel)).Returns(tarefa);
            mock.Setup(c => c.Fechar(tarefa));
            mock.Setup(c => c.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            var service = new TarefaService(mock.Object, mapper.Object);
            var fechar = await service.Fechar(tarefaViewModel);
        
            // Assert
            Assert.True(fechar);
        }
    }
}
