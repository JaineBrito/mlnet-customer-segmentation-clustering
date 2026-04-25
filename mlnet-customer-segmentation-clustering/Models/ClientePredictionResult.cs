using System;
using Microsoft.ML.Data;

namespace MachineLearning.Models;

public class ClientePredictionResult
{
    [ColumnName("PredictedLabel")]
    public uint GrupoPrevisto { get; set; }

}
