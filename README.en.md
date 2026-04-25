# ML.NET Customer Segmentation Clustering

Language: [Portugues](README.md) | **English**

Study project in C# with ML.NET for customer segmentation using clustering (K-Means).

The goal is to practice a full unsupervised Machine Learning workflow in .NET:
- load customer data;
- train a clustering model;
- analyze discovered groups;
- evaluate clustering quality metrics;
- save/load model artifacts;
- predict groups for new customers.

## Project structure

- `mlnet-customer-segmentation-clustering`
  Class library with ML logic (training, analysis, evaluation, persistence, and prediction).
- `mlnet-customer-segmentation-clustering-console`
  Console app that runs the end-to-end workflow.

### Main files

- `mlnet-customer-segmentation-clustering-console/Program.cs`
  Orchestrates the full process (load -> train -> analyze -> evaluate -> save -> predict).
- `mlnet-customer-segmentation-clustering/ML/ClienteModelTrainer.cs`
  Contains data loading, K-Means training, group analysis, evaluation, and model-saving methods.
- `mlnet-customer-segmentation-clustering/ML/ClienteModelPredictor.cs`
  Loads the saved model and predicts customer groups.
- `mlnet-customer-segmentation-clustering/Models/ClienteInputData.cs`
  Defines CSV input columns.
- `mlnet-customer-segmentation-clustering/Models/ClienteComGrupo.cs`
  Defines records enriched with predicted group for cluster analysis.
- `mlnet-customer-segmentation-clustering/Models/ClientePredictionResult.cs`
  Defines prediction output (`GrupoPrevisto`).

## How data is loaded

The CSV dataset is loaded with `LoadFromTextFile<ClienteInputData>()` using:
- header row (`hasHeader: true`);
- comma separator (`separatorChar: ','`).

Column mapping in `ClienteInputData`:
- column `0`: `CompraMes`;
- column `1`: `ValorMedioGasto`;
- column `2`: `VisitasSemana`.

## Training pipeline

Inside `ClienteModelTrainer`:

1. Concatenate input fields into `Features`.
2. Normalize features with `NormalizeMinMax`.
3. Train clustering with `KMeans` using `numberOfClusters: 3`.
4. Generate predictions and aggregate cluster-level statistics.

## Clustering analysis and evaluation

`AnalisarGrupos()`:
- computes average purchases, spending, and visits per group;
- prints record count per cluster.

`AvaliarModelo()` computes clustering metrics:
- `AverageDistance`;
- `DaviesBouldinIndex`.

## Save and load model

- `SalvarModelo(path)` saves the trained model to a `.zip` file.
- `CarregarModelo(path)` loads that model for future use.

This allows training and inference to be separated.

## Prediction

Prediction is done with:
- `CreatePredictionEngine<ClienteInputData, ClientePredictionResult>()`
- `Predict(novoCliente)`

Project example:
- input: customer profile with `CompraMes`, `ValorMedioGasto`, and `VisitasSemana`;
- output: `GrupoPrevisto`.

## How to run

From repository root:

```bash
dotnet restore "mlnet-customer-segmentation-clustering.sln"
dotnet build "mlnet-customer-segmentation-clustering.sln" -c Debug
dotnet run --project "mlnet-customer-segmentation-clustering-console/mlnet-customer-segmentation-clustering-console.csproj"
```

## Dependencies

In the class library project:
- `Microsoft.ML`
- `Microsoft.ML.LightGbm`
- `Microsoft.ML.AutoML`

## Study notes

- This project is learning-focused, not production-ready.
- To evolve it further, you can:
  - test different `numberOfClusters` values;
  - add more customer behavior features;
  - compare metrics across normalization strategies;
  - create a monitoring process for segment drift over time.
