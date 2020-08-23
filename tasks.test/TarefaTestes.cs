using System;
using tasks.application.Services;
using tasks.domain.DomainException;
using tasks.domain.Entities;
using tasks.domain.Enums;
using Xunit;

namespace tasks.test
{
    public class TarefaTestes
    {
        private readonly TarefaService _tarefaService;
        public TarefaTestes()
        {
            _tarefaService = new TarefaService();
        }

        [Fact(DisplayName = "Adicionar Tarefa - Data Deve ser Maior que Hoje")]
        public void AdicionarTarefa_NovaTarefa_DataDeveSerMaiorQueHoje()
        {
            // Arrange
            var tarefa = new Tarefa(
                "Tarefa Teste",
                DateTime.Now.AddHours(1)
            );

            // Act
            _tarefaService.Adicionar(tarefa);
        
            // Assert
            Assert.True(DateTime.Now < _tarefaService.Estimado);
        }

        [Fact(DisplayName = "Adicionar Tarefa - Status Deve ser Pendente")]
        public void AdicionarTarefa_NovaTarefa_StatusDeveSerPendente()
        {
            // Arrange
            var tarefa = new Tarefa(
                "Tarefa Teste",
                DateTime.Now.AddHours(1)
            );
        
            // Act
            _tarefaService.Adicionar(tarefa);
        
            // Assert
            Assert.Equal(TarefaStatus.Pendente, _tarefaService.Status);      
        }

        [Fact(DisplayName = "Fechar Tarefa - Data Concluida deve ser igual a hoje")]
        public void FecharTarefa_TarefaExistente_DataDeveSerIgualHoje()
        {
            // Arrange
            var tarefa = new Tarefa(
                "Tarefa Teste",
                DateTime.Now.AddHours(-1)
            );
        
            // Act
            _tarefaService.Adicionar(tarefa);
            _tarefaService.Fechar(tarefa);
        
            // Assert
            Assert.Equal(_tarefaService.Concluido.Value.Hour, DateTime.Now.Hour);
        }

        [Fact(DisplayName = "Fechar Tarefa - Status Deve ser Concluido")]
        public void FecharTarefa_TarefaExistente_StatusDeveSerConcluido()
        {
            // Arrange
            var tarefa = new Tarefa(
                "Tarefa Teste",
                DateTime.Now.AddHours(-1)
            );
        
            // Act
            _tarefaService.Adicionar(tarefa);
            _tarefaService.Fechar(tarefa);
        
            // Assert
            Assert.Equal(_tarefaService.Status, TarefaStatus.Concluido);
        }

        [Fact(DisplayName = "Fechar Tarefa - Data Estimada Deve Ser Menor que a Concluida")]
        public void FecharTarefa_TarefaExistente_DataEstimadaDeveSerMenorQueConcluida()
        {
            // Arrange
            var tarefa = new Tarefa(
                "Tarefa Teste",
                DateTime.Now.AddHours(-1));
        
            // Act
            _tarefaService.Adicionar(tarefa);
            _tarefaService.Fechar(tarefa);
        
            // Assert
            Assert.True(_tarefaService.Concluido > _tarefaService.Estimado);
        }

        [Fact(DisplayName = "Fechar Tarefa - Tarefa inexistente")]
        public void FecharTarefa_TarefaInexistente_DeveRetornarException()
        {
            // Arrange
            var tarefa = new Tarefa(
                "Tarefa Teste",
                DateTime.Now.AddHours(-1)
            );
        
            // Act & Assert
            Assert.Throws<DomainException>(() => _tarefaService.Fechar(tarefa));
        }

        [Fact(DisplayName = "Remover Tarefa - Tarefa inexistente")]
        public void RemoverTarefa_TarefaInexistente_DeveRetornarException()
        {
            // Arrange
            var tarefa = new Tarefa(
                "Tarefa Teste",
                DateTime.Now.AddHours(-1)
            );
        
            // Act & Assert
            Assert.Throws<DomainException>(() => _tarefaService.Remover(tarefa));
        }

        [Fact(DisplayName = "Validar Tarefa - Descrição vazia")]
        public void AdicionarTarefa_Validar_DescricaoVaziaDeveRetornarErro()
        {
            // Arrange
            var ex = new Tarefa(string.Empty, DateTime.Now.AddHours(1));

            // Act
            ex.EhValido();

            // Assert
            Assert.False(ex.ValidationResult.IsValid);
        }

        [Fact(DisplayName = "Validar Tarefa - Horário anterior a agora")]
        public void AdicionarTarefa_Validar_HoraAnteriorAHojeDeveRetornarErro()
        {
            // Arrange
            var ex = new Tarefa("Teste", DateTime.Now.AddDays(-1));

            // Act
            ex.EhValido();

            // Assert
            Assert.False(ex.ValidationResult.IsValid);
        }
    }
}
