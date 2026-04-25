using System;
using Microsoft.ML.Data;

namespace MachineLearning.Models;

public class ClienteComGrupo
{
    public float CompraMes { get; set; }

    public float ValorMedioGasto { get; set; }

    public float VisitasSemana { get; set; }

   [ColumnName("PredictedLabel")]
    public uint GrupoPrevisto { get; set; }


}
