using System;
using Moq.AutoMock;
using tasks.application.Interfaces;
using tasks.application.Services;
using tasks.application.ViewModels;
using tasks.domain.Entities;
using Xunit;

namespace tasks.test
{
    public class TarefaTestes
    {
        private readonly TarefaService _tarefaService;
        AutoMocker mocker;
        public TarefaTestes()
        {
            mocker = new AutoMocker();
            _tarefaService = mocker.CreateInstance<TarefaService>();
        }

        [Fact(DisplayName = "Adicionar Tarefa - Data Deve ser Maior que Hoje")]
        public void AdicionarTarefa_NovaTarefa_DataDeveSerMaiorQueHoje()
        {
            // Arrange
            var tarefa = new TarefaViewModel()
            {
                Descricao = "Tarefa Teste",
                Estimado = DateTime.Now.AddHours(1)
            };

            // Act
            _tarefaService.Adicionar(tarefa);

            // Assert
            Assert.True(DateTime.Now < _tarefaService.Estimado);
            mocker.VerifyAll();
        }

        [Fact(DisplayName = "Fechar Tarefa - Data Concluida deve ser igual a hoje")]
        public void FecharTarefa_TarefaExistente_DataDeveSerIgualHoje()
        {
            // Arrange
            var tarefa = new TarefaViewModel()
            {
                Descricao = "Tarefa Teste",
                Estimado = DateTime.Now.AddHours(-1)
            };
        
            // Act
            _tarefaService.Adicionar(tarefa);
            _tarefaService.Fechar(tarefa);
        
            // Assert
            Assert.Equal(_tarefaService.Concluido.Value.Hour, DateTime.Now.Hour);
        }

        [Fact(DisplayName = "Fechar Tarefa - Data Estimada Deve Ser Menor que a Concluida")]
        public void FecharTarefa_TarefaExistente_DataEstimadaDeveSerMenorQueConcluida()
        {
            // Arrange
            var tarefa = new TarefaViewModel()
            {
                Descricao = "Tarefa Teste",
                Estimado = DateTime.Now.AddHours(-1)
            };
        
            // Act
            _tarefaService.Adicionar(tarefa);
            _tarefaService.Fechar(tarefa);
        
            // Assert
            Assert.True(_tarefaService.Concluido > _tarefaService.Estimado);
        }

        [Fact(DisplayName = "Validar Tarefa - Descrição vazia")]
        public void AdicionarTarefa_Validar_DescricaoVaziaDeveRetornarErro()
        {
            // Arrange
            var ex = new Tarefa(
                Guid.NewGuid(), 
                string.Empty, 
                DateTime.Now.AddHours(1),
                null
            );

            // Act
            ex.EhValido();

            // Assert
            Assert.False(ex.ValidationResult.IsValid);
        }

        [Fact(DisplayName = "Validar Tarefa - Horário anterior a agora")]
        public void AdicionarTarefa_Validar_HoraAnteriorAHojeDeveRetornarErro()
        {
            // Arrange
            var ex = new Tarefa(
                Guid.NewGuid(), 
                string.Empty, 
                DateTime.Now.AddHours(-1),
                null
            );

            // Act
            ex.EhValido();

            // Assert
            Assert.False(ex.ValidationResult.IsValid);
        }
    }
}
