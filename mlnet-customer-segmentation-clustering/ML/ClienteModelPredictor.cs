using System;
using MachineLearning.Models;
using Microsoft.ML;

namespace MachineLearning.ML;

public class ClienteModelPredictor
{
    private MLContext mLContext = new MLContext();

    private ITransformer modeloCarregado;
   
   public void CarregarModelo(string path)
    {
        DataViewSchema modeloSchema;
        modeloCarregado = mLContext.Model.Load(path, out modeloSchema);
    }

    public ClientePredictionResult Prever(
        ClienteInputData novoCliente
    )
    {
        var predEgine = mLContext.Model.CreatePredictionEngine<ClienteInputData, ClientePredictionResult>(
         modeloCarregado
        );
        return predEgine.Predict(novoCliente);
    }

}
