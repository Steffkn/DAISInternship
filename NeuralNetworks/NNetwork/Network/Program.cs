using CustomNetwork;
using Network.ActivationFunctions;
using Network.Factories;

namespace Network
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var numberOfInputNeurons = 2;
            var numberOfOutputNeurons = 1;
            var network = new NNetwork(numberOfInputNeurons, numberOfOutputNeurons, 1);

            var layerFactory = new NeuralLayerFactory();

            //// TODO: Mix these 2 lines. eliminate the foreach
            //var inputLayer = layerFactory.CreateNeuralLayer(numberOfInputNeurons, new RectifiedActivationFuncion());
            //foreach (var x in inputLayer.Neurons)
            //{
            //    x.AddInputSynapse(0);
            //}

            //network.AddLayer(inputLayer);
            //network.AddLayer(layerFactory.CreateNeuralLayer(3, new SigmoidActivationFunction(0.7)));
            //network.AddLayer(layerFactory.CreateNeuralLayer(1, new SigmoidActivationFunction(0.7)));

            //network.PushExpectedValues(
            //    new double[][] {
            //    new double[] { 0 },
            //    new double[] { 1 },
            //    new double[] { 1 },
            //    new double[] { 1 },
            //    });

            //network.Train(
            //    new double[][] {
            //    new double[] { 0, 0},
            //    new double[] { 0, 1},
            //    new double[] { 1, 0},
            //    new double[] { 1, 1},
            //    }, 10000);

            //network.PushInputValues(new double[] { 1, 1 });
            //var outputs = network.GetOutput();

            //foreach (var output in outputs)
            //{
            //    System.Console.WriteLine(output);
            //}
        }
    }
}
