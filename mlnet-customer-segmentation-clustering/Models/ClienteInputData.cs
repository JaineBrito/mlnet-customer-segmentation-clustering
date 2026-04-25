using System;
using Microsoft.ML.Data;

namespace MachineLearning.Models;

public class ClienteInputData
{
    [LoadColumn(0)]
    public float CompraMes { get; set; }
    
    [LoadColumn(1)]
    public float ValorMedioGasto { get; set; }

    [LoadColumn(2)]
    public float VisitasSemana { get; set; }

}
