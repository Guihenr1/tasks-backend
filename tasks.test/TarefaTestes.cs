using System;
using System.Collections.Generic;
using tasks.application.Services;
using tasks.domain.Entities;
using tasks.domain.Enums;
using Xunit;

namespace tasks.test
{
    public class TarefaTestes
    {
        [Fact(DisplayName = "Adicionar Tarefa - Data Deve ser Maior que Hoje")]
        public void AdicionarTarefa_NovaTarefa_DataDeveSerMaiorQueHoje()
        {
            // Arrange
            var tarefaService = new TarefaService();
            var tarefa = new Tarefa(){
                Descricao = "Tarefa Teste",
                Status = TarefaStatus.Pendente,
                Estimado = DateTime.Now.AddHours(1)
            };

            // Act
            tarefaService.Adicionar(tarefa);
        
            // Assert
            Assert.True(DateTime.Now < tarefaService.Estimado);
        }

        [Fact(DisplayName = "Adicionar Tarefa - Status Deve ser Pendente")]
        public void AdicionarTarefa_NovaTarefa_StatusDeveSerPendente()
        {
            // Arrange
            var tarefaService = new TarefaService();
            var tarefa = new Tarefa(){
                Descricao = "Tarefa Teste",
                Status = TarefaStatus.Pendente,
                Estimado = DateTime.Now.AddHours(1)
            };
        
            // Act
            tarefaService.Adicionar(tarefa);
        
            // Assert
            Assert.Equal(TarefaStatus.Pendente, tarefaService.Status);      
        }

        [Fact(DisplayName = "Fechar Tarefa - Data Concluida deve ser igual a hoje")]
        public void FecharTarefa_TarefaExistente_DataDeveSerIgualHoje()
        {
            // Arrange
            var dataHoje = DateTime.Now;
            var tarefaService = new TarefaService();
            var tarefa = new Tarefa(){
                Descricao = "Tarefa Teste",
                Status = TarefaStatus.Concluido,
                Estimado = DateTime.Now.AddHours(-1),
                Concluido = dataHoje
            };
        
            // Act
            tarefaService.Adicionar(tarefa);
        
            // Assert
            Assert.Equal(tarefaService.Concluido, dataHoje);
        }

        [Fact(DisplayName = "Fechar Tarefa - Status Deve ser Concluido")]
        public void FecharTarefa_TarefaExistente_StatusDeveSerConcluido()
        {
            // Arrange
            var tarefaService = new TarefaService();
            var tarefa = new Tarefa(){
                Descricao = "Tarefa Teste",
                Status = TarefaStatus.Concluido,
                Estimado = DateTime.Now.AddHours(-1),
                Concluido = DateTime.Now
            };
        
            // Act
            tarefaService.Adicionar(tarefa);
        
            // Assert
            Assert.Equal(tarefaService.Status, TarefaStatus.Concluido);
        }

        [Fact(DisplayName = "Fechar Tarefa - Data Estimada Deve Ser Menor que a Concluida")]
        public void FecharTarefa_TarefaExistente_DataEstimadaDeveSerMenorQueConcluida()
        {
            // Arrange
            var tarefaService = new TarefaService();
            var tarefa = new Tarefa(){
                Descricao = "Tarefa Teste",
                Status = TarefaStatus.Concluido,
                Estimado = DateTime.Now.AddHours(-1),
                Concluido = DateTime.Now
            };
        
            // Act
            tarefaService.Adicionar(tarefa);
        
            // Assert
            Assert.True(tarefaService.Concluido > tarefaService.Estimado);
        }
    }
}