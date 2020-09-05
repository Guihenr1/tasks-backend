using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using tasks.application.Services;
using tasks.domain.Entities;
using tasks.domain.Enums;
using tasks.domain.Interfaces;
using tasks.domain.ViewModels;
using tasks.domain.ViewModels.Validacao;
using Xunit;

namespace tasks.test
{
    public class TarefaTestes
    {
        Mock<IMapper> _mapper;
        public TarefaTestes()
        {
            _mapper = new Mock<IMapper>();
        }

        [Fact(DisplayName = "Adicionar Tarefa - Data Deve ser Maior que Hoje")]
        public async Task AdicionarTarefa_NovaTarefa_DataDeveSerMaiorQueHoje()
        {
            // Arrange
            var tarefa = new TarefaRequestViewModel()
            {
                Descricao = "Tarefa Teste",
                Estimado = DateTime.Now.AddHours(1)
            };

            // Act
            var mock = new Mock<ITarefaRepository>();
            mock.Setup(c => c.Adicionar(It.IsAny<Tarefa>()));
            mock.Setup(c => c.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            var service = new TarefaService(mock.Object, _mapper.Object);
            await service.Adicionar(tarefa);

            // Assert
            Assert.True(DateTime.Now < service.Estimado);
            mock.VerifyAll();
        }

        [Fact(DisplayName = "Fechar Tarefa")]
        public async Task FecharTarefa_TarefaExistente()
        {
            // Arrange
            var tarefaVM = new FecharTarefaRequestViewModel()
            {
                Id = Guid.NewGuid(),
                Descricao = "Tarefa Teste",
                Estimado = DateTime.Now.AddHours(-1)
            };
            var tarefa = new Tarefa(tarefaVM.Id, tarefaVM.Descricao, tarefaVM.Estimado);
        
            // Act
            var mock = new Mock<ITarefaRepository>();
            var mapper = new Mock<IMapper>();
            mock.Setup(c => c.Fechar(It.IsAny<Tarefa>()));
            mock.Setup(c => c.UnitOfWork.Commit()).Returns(Task.FromResult(true));
            mapper.Setup(c => c.Map<Tarefa>(tarefaVM)).Returns(tarefa);

            var service = new TarefaService(mock.Object, mapper.Object);
            await service.Fechar(tarefaVM);
        
            // Assert
            mock.VerifyAll();
        }

        [Fact(DisplayName = "Validar Tarefa - Descrição vazia")]
        public void AdicionarTarefa_Validar_DescricaoVaziaDeveRetornarErro()
        {
            // Arrange
            var ex = new TarefaRequestViewModel(){
                Descricao = string.Empty,
                Estimado = DateTime.Now.AddHours(1)
            };

            // Act
            ex.EhValido();

            // Assert
            Assert.False(ex.ValidationResult.IsValid);
        }

        [Fact(DisplayName = "Validar Tarefa - Horário anterior a agora")]
        public void AdicionarTarefa_Validar_HoraAnteriorAHojeDeveRetornarErro()
        {
            // Arrange
            var ex = new TarefaRequestViewModel(){
                Descricao = string.Empty,
                Estimado = DateTime.Now.AddHours(-1)
            };

            // Act
            ex.EhValido();

            // Assert
            Assert.False(ex.ValidationResult.IsValid);
        }

        [Fact(DisplayName = "Obter Todos - Deve retornar uma lista")]
        public async Task ObterTodos_()
        {
            // Arrange
            IEnumerable<Tarefa> lista = new List<Tarefa>(){
                new Tarefa(Guid.NewGuid(), "tarefa", DateTime.Now)
            };
        
            // Act
            var mock = new Mock<ITarefaRepository>();
            mock.Setup(t => t.ObterTodos()).Returns(Task.FromResult(lista));

            var service = new TarefaService(mock.Object, _mapper.Object);
            await service.ObterTodos();
        
            // Assert
            mock.VerifyAll();
        }
    }
}
