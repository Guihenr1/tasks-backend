using System;
using System.Threading.Tasks;
using Moq;
using Moq.AutoMock;
using tasks.application.Services;
using tasks.application.ViewModels;
using tasks.domain.Entities;
using tasks.domain.Interfaces;
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
        public async Task AdicionarTarefa_NovaTarefa_DataDeveSerMaiorQueHoje()
        {
            // Arrange
            var tarefa = new TarefaViewModel()
            {
                Descricao = "Tarefa Teste",
                Estimado = DateTime.Now.AddHours(1)
            };

            // Act
            var mock = new Mock<ITarefaRepository>();
            mock.Setup(c => c.UnitOfWork.Commit()).Returns(() => Task.FromResult(true)).Verifiable();
            await _tarefaService.Adicionar(tarefa);

            // Assert
            Assert.True(DateTime.Now < _tarefaService.Estimado);
            mocker.VerifyAll();
        }

        [Fact(DisplayName = "Fechar Tarefa - Data Concluida deve ser igual a hoje")]
        public async Task FecharTarefa_TarefaExistente_DataDeveSerIgualHoje()
        {
            // Arrange
            var tarefa = new TarefaViewModel()
            {
                Descricao = "Tarefa Teste",
                Estimado = DateTime.Now.AddHours(-1)
            };
        
            // Act
            await _tarefaService.Adicionar(tarefa);
            await _tarefaService.Fechar(tarefa);
        
            // Assert
            Assert.Equal(_tarefaService.Concluido.Value.Hour, DateTime.Now.Hour);
            mocker.VerifyAll();
        }

        [Fact(DisplayName = "Fechar Tarefa - Data Estimada Deve Ser Menor que a Concluida")]
        public async Task FecharTarefa_TarefaExistente_DataEstimadaDeveSerMenorQueConcluida()
        {
            // Arrange
            var tarefa = new TarefaViewModel()
            {
                Descricao = "Tarefa Teste",
                Estimado = DateTime.Now.AddHours(-1)
            };
        
            // Act
            await _tarefaService.Adicionar(tarefa);
            await _tarefaService.Fechar(tarefa);
        
            // Assert
            Assert.True(_tarefaService.Concluido > _tarefaService.Estimado);
            mocker.VerifyAll();
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
            mocker.VerifyAll();
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
            mocker.VerifyAll();
        }
    }
}
