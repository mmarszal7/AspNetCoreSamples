using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace myApp
{
    class SpikePrediction
    {
        [VectorType(3)]
        public double[] Prediction { get; set; }
    }

    class Program
    {
        private static string TrainingDataPath = ".csv";

        private static string signalName = "";

        static void Main()
        {
            var mlContext = new MLContext(seed: 0);

            var columns = new List<TextLoader.Column>() {
                new TextLoader.Column("Timestamp", DataKind.DateTime, 0),
                new TextLoader.Column(signalName, DataKind.Single, 1)
                };

            var dataView = mlContext.Data.LoadFromTextFile(
               TrainingDataPath,
               columns.ToArray(),
               separatorChar: ',',
               hasHeader: true);
            var preview = dataView.Preview();

            DetectAnomalies(mlContext, dataView);
            Console.Read();
        }

        public static void DetectAnomalies(MLContext mlContext, IDataView dataView)
        {
            // Train
            const int PValueSize = 30;
            const int SeasonalitySize = 30;
            const int TrainingSize = 90;
            const int ConfidenceInterval = 98;

            string outputColumnName = nameof(SpikePrediction.Prediction);
            string inputColumnName = signalName;

            var trainigPipeLine = mlContext.Transforms.DetectSpikeBySsa(
                outputColumnName,
                inputColumnName,
                confidence: ConfidenceInterval,
                pvalueHistoryLength: PValueSize,
                trainingWindowSize: TrainingSize,
                seasonalityWindowSize: SeasonalitySize);

            ITransformer trainedModel = trainigPipeLine.Fit(dataView);

            // Predict
            var transformedData = trainedModel.Transform(dataView);

            IEnumerable<SpikePrediction> predictions =
                mlContext.Data.CreateEnumerable<SpikePrediction>(transformedData, false);

            var colCDN = dataView.GetColumn<float>(signalName).ToArray();
            var colTime = dataView.GetColumn<DateTime>("Timestamp").ToArray();

            Display(predictions, colCDN, colTime);
        }

        private static void Display(IEnumerable<SpikePrediction> predictions, float[] colCDN, DateTime[] colTime)
        {
            Console.WriteLine("Date              \tReadingDiff\tAlert\tScore\tP-Value");
            int i = 0;
            foreach (var p in predictions)
            {
                if (p.Prediction[0] == 1)
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine("{0}\t{1:0.0000}\t{2:0.00}\t{3:0.00}\t{4:0.00}",
                    colTime[i], colCDN[i],
                    p.Prediction[0], p.Prediction[1], p.Prediction[2]);
                Console.ResetColor();
                i++;
            }
        }
    }
}