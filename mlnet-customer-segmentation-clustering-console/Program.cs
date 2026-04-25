using MachineLearning.ML;
using MachineLearning.Models;


ProjetoClustering();

void ProjetoClustering()
{
     var trainer = new ClienteModelTrainer();
     trainer.CarregarDadosCSV(Path.Combine(AppContext.BaseDirectory, "clientes_agrupamento.csv"));
     trainer.TreinarModelo();
     trainer.AnalisarGrupos();
     trainer.AvaliarModelo();
    
    var pathModelo = Path.Combine(AppContext.BaseDirectory, "modelo-clustering.zip");
    trainer.SalvarModelo(pathModelo);

    var predictor = new ClienteModelPredictor();
    predictor.CarregarModelo(pathModelo);

    var novoCliente = new ClienteInputData()
    {
        CompraMes = 10,
        ValorMedioGasto = 1200,
        VisitasSemana = 3
    };

    var resultado = predictor.Prever(novoCliente);
    Console.WriteLine($"Novo cliente pertence ao grupo: {resultado.GrupoPrevisto}");
}