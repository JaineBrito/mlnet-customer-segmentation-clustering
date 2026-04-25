# ML.NET Customer Segmentation Clustering

Idioma: **Portugues** | [English](README.en.md)

Projeto de estudo em C# com ML.NET para segmentacao de clientes com clustering (K-Means).

O objetivo e praticar um fluxo completo de Machine Learning nao supervisionado no .NET:
- carregar dados de clientes;
- treinar modelo de clustering;
- analisar os grupos encontrados;
- avaliar metricas de qualidade do agrupamento;
- salvar/carregar modelo;
- prever o grupo de novos clientes.

## Estrutura do projeto

- `mlnet-customer-segmentation-clustering`
  Biblioteca com a logica de ML (treino, analise, avaliacao, persistencia e predicao).
- `mlnet-customer-segmentation-clustering-console`
  Aplicacao de console que executa o fluxo completo.

### Arquivos principais

- `mlnet-customer-segmentation-clustering-console/Program.cs`
  Orquestra todo o processo (carregar dados -> treinar -> analisar -> avaliar -> salvar -> prever).
- `mlnet-customer-segmentation-clustering/ML/ClienteModelTrainer.cs`
  Contem os metodos de carga de dados, treino K-Means, analise de grupos, avaliacao e salvamento.
- `mlnet-customer-segmentation-clustering/ML/ClienteModelPredictor.cs`
  Carrega o modelo salvo e faz previsao de grupo para novos clientes.
- `mlnet-customer-segmentation-clustering/Models/ClienteInputData.cs`
  Define as colunas de entrada do CSV.
- `mlnet-customer-segmentation-clustering/Models/ClienteComGrupo.cs`
  Define os dados com o grupo previsto para analise dos clusters.
- `mlnet-customer-segmentation-clustering/Models/ClientePredictionResult.cs`
  Define a saida da predicao (`GrupoPrevisto`).

## Como os dados sao lidos

O dataset CSV e carregado por `LoadFromTextFile<ClienteInputData>()` com:
- cabecalho (`hasHeader: true`);
- separador virgula (`separatorChar: ','`).

Mapeamento de colunas no `ClienteInputData`:
- coluna `0`: `CompraMes`;
- coluna `1`: `ValorMedioGasto`;
- coluna `2`: `VisitasSemana`.

## Pipeline de treino

No `ClienteModelTrainer`:

1. Concatena os campos de entrada em `Features`.
2. Normaliza com `NormalizeMinMax`.
3. Treina clustering com `KMeans` usando `numberOfClusters: 3`.
4. Gera previsoes para os dados e agrupa resultados para analise por cluster.

## Analise e avaliacao do clustering

O metodo `AnalisarGrupos()`:
- calcula medias de compras, gasto e visitas por grupo;
- mostra quantidade de registros por cluster.

O metodo `AvaliarModelo()` calcula metricas de clustering:
- `AverageDistance`;
- `DaviesBouldinIndex`.

## Salvar e carregar modelo

- `SalvarModelo(path)` salva o modelo treinado em arquivo `.zip`.
- `CarregarModelo(path)` recupera esse modelo para uso futuro.

Isso permite separar treino e inferencia.

## Predicao

A predicao e feita com:
- `CreatePredictionEngine<ClienteInputData, ClientePredictionResult>()`
- `Predict(novoCliente)`

Exemplo do projeto:
- entrada: cliente com `CompraMes`, `ValorMedioGasto` e `VisitasSemana`;
- saida: `GrupoPrevisto`.

## Como executar

Na raiz do repositorio:

```bash
dotnet restore "mlnet-customer-segmentation-clustering.sln"
dotnet build "mlnet-customer-segmentation-clustering.sln" -c Debug
dotnet run --project "mlnet-customer-segmentation-clustering-console/mlnet-customer-segmentation-clustering-console.csproj"
```

## Dependencias

No projeto de biblioteca:
- `Microsoft.ML`
- `Microsoft.ML.LightGbm`
- `Microsoft.ML.AutoML`

## Observacoes de estudo

- O projeto e focado em aprendizado, nao em producao.
- Para evoluir, voce pode:
  - testar diferentes valores de `numberOfClusters`;
  - usar mais variaveis de comportamento do cliente;
  - comparar metricas para diferentes pipelines de normalizacao;
  - criar um processo de monitoramento dos grupos ao longo do tempo.