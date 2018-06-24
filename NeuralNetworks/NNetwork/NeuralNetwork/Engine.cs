using NeuralNetwork.Helpers;
using NeuralNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class Engine
    {
        private static int _numInputParameters;
        private static int _numHiddenLayers;
        private static int[] _hiddenNeurons;
        private static int _numOutputParameters;
        private static Network _network;
        private static List<DataSet> _dataSets;

        public static void Run()
        {
            InitialMenu();
        }

        private static void Greet()
        {
            Console.WriteLine("C# Neural Network Manager");
            Console.WriteLine("Created by Trent Sartain (trentsartain on GitHub)");
            PrintUnderline();
            Console.WriteLine();
        }

        private static void InitialMenu()
        {
            Console.Clear();
            Greet();
            Console.WriteLine();
            Console.WriteLine("Main Menu");
            PrintUnderline();
            Console.WriteLine("\t1. New Network");
            Console.WriteLine("\t2. Import Network");
            Console.WriteLine("\t3. Exit");
            Console.WriteLine();

            switch (GetInput("\tYour Choice: ", 1, 3))
            {
                case 1:
                    if (SetupNetwork())
                    {
                        DatasetMenu();
                    }
                    else
                    {
                        InitialMenu();
                    }
                    break;
                case 2:
                    ImportNetwork();
                    DatasetMenu();
                    break;
                case 3:
                    Exit();
                    break;
            }
        }

        private static void DatasetMenu()
        {
            Console.Clear();
            Greet();
            Console.WriteLine();
            Console.WriteLine("Dataset Menu");
            PrintUnderline();
            Console.WriteLine("\t1. Type Dataset");
            Console.WriteLine("\t2. Import Dataset");
            Console.WriteLine("\t3. Test Network");
            Console.WriteLine("\t4. Export Network");
            Console.WriteLine("\t5. Main Menu");
            Console.WriteLine("\t6. Exit");
            Console.WriteLine();

            switch (GetInput("\tYour Choice: ", 1, 6))
            {
                case 1:
                    if (GetTrainingData()) { NetworkMenu(); }
                    else DatasetMenu();
                    break;
                case 2:
                    ImportDatasets();
                    NetworkMenu();
                    break;
                case 3:
                    TestNetwork();
                    DatasetMenu();
                    break;
                case 4:
                    ExportNetwork();
                    DatasetMenu();
                    break;
                case 5:
                    InitialMenu();
                    break;
                case 6:
                    Exit();
                    break;
            }
        }

        private static void NetworkMenu()
        {
            Console.Clear();
            Greet();
            Console.WriteLine();
            Console.WriteLine("Network Menu");
            PrintUnderline();
            Console.WriteLine("\t1. Train Network");
            Console.WriteLine("\t2. Test Network");
            Console.WriteLine("\t3. Export Network");
            Console.WriteLine("\t4. Export Dataset");
            Console.WriteLine("\t5. Dataset Menu");
            Console.WriteLine("\t6. Main Menu");
            Console.WriteLine("\t7. Exit");
            Console.WriteLine();

            switch (GetInput("\tYour Choice: ", 1, 7))
            {
                case 1:
                    Train();
                    NetworkMenu();
                    break;
                case 2:
                    TestNetwork();
                    NetworkMenu();
                    break;
                case 3:
                    ExportNetwork();
                    NetworkMenu();
                    break;
                case 4:
                    ExportDatasets();
                    NetworkMenu();
                    break;
                case 5:
                    DatasetMenu();
                    break;
                case 6:
                    InitialMenu();
                    break;
                case 7:
                    Exit();
                    break;
            }
        }

        private static bool SetupNetwork()
        {
            Console.Clear();
            Greet();
            Console.WriteLine();
            Console.WriteLine("Network Setup");
            PrintUnderline();

            SetNumInputParameters();
            if (_numInputParameters == 0)
            {
                return false;
            }

            SetNumNeuronsInHiddenLayer();
            if (_numHiddenLayers == 0)
            {
                return false;
            }

            SetNumOutputParameters();
            if (_numInputParameters == 0)
            {
                return false;
            }

            Console.WriteLine("\tCreating Network...");
            _network = new Network(_numInputParameters, _hiddenNeurons, _numOutputParameters);
            Console.WriteLine("\t**Network Created!**");
            Console.WriteLine();
            return true;
        }

        private static void SetNumInputParameters()
        {
            Console.WriteLine("\tHow many input parameters will there be? (2 or more)");
            _numInputParameters = GetInput("\tInput Parameters: ", 2, int.MaxValue) ?? 0;
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void SetNumNeuronsInHiddenLayer()
        {
            Console.WriteLine("\tHow many hidden layers? (1 or more)");
            _numHiddenLayers = GetInput("\tHidden Layers: ", 1, int.MaxValue) ?? 0;

            Console.WriteLine("\tHow many neurons in the hidden layers? (2 or more)");
            _hiddenNeurons = GetArrayInput("\tNeurons in layer", 2, _numHiddenLayers);
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void SetNumOutputParameters()
        {
            Console.WriteLine("\tHow many output parameters will there be? (1 or more)");
            _numOutputParameters = GetInput("\tOutput Parameters: ", 1, int.MaxValue) ?? 0;
            Console.WriteLine();
            Console.WriteLine();
        }

        private static bool GetTrainingData()
        {
            PrintUnderline(50);
            Console.WriteLine("\tManually Enter the Datasets. Type 'menu' at any time to go back.");
            Console.WriteLine();

            var numDataSets = GetInput("\tHow many datasets are you going to enter? ", 1, int.MaxValue);

            var newDatasets = new List<DataSet>();
            for (var i = 0; i < numDataSets; i++)
            {
                var values = GetInputData($"\tData Set {i + 1}: ");
                if (values == null)
                {
                    Console.WriteLine();
                    return false;
                }

                var expectedResult = GetExpectedResult($"\tExpected Result for Data Set {i + 1}: ");
                if (expectedResult == null)
                {
                    Console.WriteLine();
                    return false;
                }

                newDatasets.Add(new DataSet(values, expectedResult));
            }

            _dataSets = newDatasets;
            Console.WriteLine();
            return true;
        }

        private static double[] GetInputData(string message)
        {
            Console.Write(message);
            var line = GetLine();

            if (line.Equals("menu", StringComparison.InvariantCultureIgnoreCase)) return null;

            while (line == null || line.Split(' ').Length != _numInputParameters)
            {
                Console.WriteLine($"\t{_numInputParameters} inputs are required.");
                Console.WriteLine();
                Console.WriteLine(message);
                line = GetLine();
            }

            var values = new double[_numInputParameters];
            var lineNums = line.Split(' ');
            for (var i = 0; i < lineNums.Length; i++)
            {
                if (double.TryParse(lineNums[i], out double num))
                {
                    values[i] = num;
                }
                else
                {
                    Console.WriteLine("\tYou entered an invalid number. Try again");
                    Console.WriteLine();
                    Console.WriteLine();
                    return GetInputData(message);
                }
            }

            return values;
        }

        private static double[] GetExpectedResult(string message)
        {
            Console.Write(message);
            var line = GetLine();

            if (line != null && line.Equals("menu", StringComparison.InvariantCultureIgnoreCase)) return null;

            while (line == null || line.Split(' ').Length != _numOutputParameters)
            {
                Console.WriteLine($"\t{_numOutputParameters} outputs are required.");
                Console.WriteLine();
                Console.WriteLine(message);
                line = GetLine();
            }

            var values = new double[_numOutputParameters];
            var lineNums = line.Split(' ');
            for (var i = 0; i < lineNums.Length; i++)
            {
                if (int.TryParse(lineNums[i], out int num) && (num == 0 || num == 1))
                {
                    values[i] = num;
                }
                else
                {
                    Console.WriteLine("\tYou must enter 1s and 0s!");
                    Console.WriteLine();
                    Console.WriteLine();
                    return GetExpectedResult(message);
                }
            }

            return values;
        }

        private static void TestNetwork()
        {
            Console.WriteLine("\tTesting Network");
            Console.WriteLine("\tType 'menu' at any time to return to the previous menu.");
            Console.WriteLine();

            while (true)
            {
                PrintUnderline(50);
                var values = GetInputData($"\tType {_numInputParameters} inputs (or 'menu' to exit): ");
                if (values == null)
                {
                    Console.WriteLine();
                    return;
                }

                var results = _network.Compute(values);
                Console.WriteLine();

                foreach (var result in results)
                {
                    Console.WriteLine($"\tOutput: {result}");
                }

                Console.WriteLine();
            }
        }

        private static void Train()
        {
            Console.WriteLine("Network Training");
            PrintUnderline(50);
            Console.WriteLine("\t1. Train to minimum error");
            Console.WriteLine("\t2. Train to max epoch");
            Console.WriteLine("\t3. Network Menu");
            Console.WriteLine();
            switch (GetInput("\tYour Choice: ", 1, 3))
            {
                case 1:
                    var minError = GetDouble("\tMinimum Error: ", 0.000000001, 1.0);
                    Console.WriteLine();
                    Console.WriteLine("\tTraining...");
                    _network.Train(_dataSets, minError);
                    Console.WriteLine("\t**Training Complete**");
                    Console.WriteLine();
                    NetworkMenu();
                    break;
                case 2:
                    var maxEpoch = GetInput("\tMax Epoch: ", 1, int.MaxValue);
                    if (!maxEpoch.HasValue)
                    {
                        Console.WriteLine();
                        NetworkMenu();
                        return;
                    }
                    Console.WriteLine();
                    Console.WriteLine("\tTraining...");
                    _network.Train(_dataSets, maxEpoch.Value);
                    Console.WriteLine("\t**Training Complete**");
                    Console.WriteLine();
                    break;
                case 3:
                    NetworkMenu();
                    break;
            }

            Console.WriteLine();
        }

        private static void ImportNetwork()
        {
            Console.WriteLine();
            _network = ImportHelper.ImportNetwork();
            if (_network == null)
            {
                WriteError("\t****Something went wrong while importing your network.****");
                return;
            }

            _numInputParameters = _network.InputLayer.Count;
            _hiddenNeurons = new int[_network.HiddenLayers.Count];
            _numOutputParameters = _network.OutputLayer.Count;

            Console.WriteLine("\t**Network successfully imported.**");
            Console.WriteLine();
        }

        private static void ExportNetwork()
        {
            Console.WriteLine();
            Console.WriteLine("\tExporting Network...");
            ExportHelper.ExportNetwork(_network);
            Console.WriteLine("\t**Exporting Complete!**");
            Console.WriteLine();
        }

        private static void ImportDatasets()
        {
            Console.WriteLine();
            _dataSets = ImportHelper.ImportDatasets();
            if (_dataSets == null)
            {
                WriteError("\t--Something went wrong while importing your datasets.--");
                return;
            }

            if (_dataSets.Any(x => x.Values.Length != _numInputParameters || _dataSets.Any(y => y.Targets.Length != _numOutputParameters)))
            {
                WriteError($"\t--The dataset does not fit the network.  Network requires datasets that have {_numInputParameters} inputs and {_numOutputParameters} outputs.--");
                return;
            }

            Console.WriteLine("\t**Datasets successfully imported.**");
            Console.WriteLine();
        }

        private static void ExportDatasets()
        {
            Console.WriteLine();
            Console.WriteLine("\tExporting Datasets...");
            ExportHelper.ExportDatasets(_dataSets);
            Console.WriteLine("\t**Exporting Complete!**");
            Console.WriteLine();
        }

        private static string GetLine()
        {
            var line = Console.ReadLine();
            return line?.Trim() ?? string.Empty;
        }

        private static int? GetInput(string message, int min, int max)
        {
            Console.Write(message);
            var num = GetNumber();
            if (!num.HasValue)
            {
                return null;
            }

            while (!num.HasValue || num < min || num > max)
            {
                Console.Write(message);
                num = GetNumber();
            }

            return num.Value;
        }

        private static double GetDouble(string message, double min, double max)
        {
            Console.Write(message);
            var num = GetDouble();

            while (num < min || num > max)
            {
                Console.Write(message);
                num = GetDouble();

            }

            return num;
        }

        private static int[] GetArrayInput(string message, int min, int numToGet)
        {
            var nums = new int[numToGet];

            for (var i = 0; i < numToGet; i++)
            {
                int? num = null;
                while (!num.HasValue || num < min)
                {
                    Console.Write(message + " " + (i + 1) + ": ");
                    num = GetNumber();
                }

                nums[i] = num.Value;
            }

            return nums;
        }

        private static int? GetNumber()
        {
            var line = GetLine();

            if (line.Equals("menu", StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
            }

            return int.TryParse(line, out int num) ? num : 0;
        }

        private static double GetDouble()
        {
            var line = GetLine();
            return line != null && double.TryParse(line, out double num) ? num : 0;
        }

        private static void PrintUnderline(int numUnderlines = 50)
        {
            Console.WriteLine(new string('-', numUnderlines));
            Console.WriteLine();
        }

        private static void WriteError(string error)
        {
            Console.WriteLine(error);
            Exit();
        }

        private static void Exit()
        {
            Console.WriteLine("Exiting...");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}
