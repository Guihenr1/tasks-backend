using System;
using tasks.application.Dtos;
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
    }
}