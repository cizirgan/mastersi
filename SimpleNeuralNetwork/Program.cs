using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Neuron> inputLayer = new List<Neuron>(){
                new Neuron{Input = 1.0},
                new Neuron{Input = 2.0},
                new Neuron{Input = 3.0},
            };

            System.Console.WriteLine("input values:");
            foreach (var item in inputLayer)
            {
                item.InputValues();
            }
           Console.WriteLine(" ");           Console.WriteLine("");
            List<Neuron> hiddenLayer = new List<Neuron>(){
                new Neuron{Weights = new List<double>() { 0.1, 0.5, 0.9 }, Bias = -2.0},
                new Neuron{Weights = new List<double>() { 0.2, 0.6, 1.0 }, Bias = -6.0},
                new Neuron{Weights = new List<double>() { 0.3, 0.7, 1.1 }, Bias = -1.0},
                new Neuron{Weights = new List<double>() { 0.4, 0.8, 1.2 }, Bias = -7.0}
            };
 
            var firstLevel = RunIH(hiddenLayer, inputLayer);
            System.Console.WriteLine("input-to-hidden sum");
            Helpers.Print(firstLevel);
            Console.WriteLine("");
            System.Console.WriteLine("\ninput-to-hidden bias values");
            foreach (var item in hiddenLayer)
            {
                System.Console.Write(item.Bias.ToString("F2") + " ");
            }
            Console.WriteLine("");
            System.Console.WriteLine("\ninput-to-hidden with bias");
            Helpers.PrintWithBias(firstLevel);

            List<Neuron> outputLayer = new List<Neuron>(){
                new Neuron{Weights = new List<double>() { 1.3, 1.5,1.7,1.9}, Bias = -2.5},
                new Neuron{Weights = new List<double>() { 1.4, 1.6,1.8,2.0 }, Bias = -5.0},
            };
            var secondLevel = RunHO(outputLayer, firstLevel);
            System.Console.WriteLine("\n\nhidden-to-output sum");
            Helpers.Print(secondLevel);

            Console.WriteLine("");
            System.Console.WriteLine("\nhidden-to-output bias values");
            foreach (var item in outputLayer)
            {
                System.Console.Write(item.Bias.ToString("F") + " ");
            }

            Console.WriteLine("");
            System.Console.WriteLine("\nhidden-to-output with bias");
            Helpers.PrintWithBias(secondLevel);

        }

        static List<Neuron> RunIH(List<Neuron> hiddenLayer, List<Neuron> inputLayer)
        {
            for (int i = 0; i < hiddenLayer.Count; i++)
            {
                for (int j = 0; j < inputLayer.Count; j++)
                {
                    hiddenLayer[i].Sum += inputLayer[j].Input * hiddenLayer[i].Weights[j];
                }
                hiddenLayer[i].Output = hiddenLayer[i].Sum + hiddenLayer[i].Bias;
                hiddenLayer[i].Input = Helpers.SigmoidFunction(hiddenLayer[i].Output);
            }

            return hiddenLayer;
        }

        static List<Neuron> RunHO(List<Neuron> outputLayer, List<Neuron> hiddenLayer)
        {
            for (int i = 0; i < outputLayer.Count; i++)
            {
                for (int j = 0; j < hiddenLayer.Count; j++)
                {
                    outputLayer[i].Sum += (hiddenLayer[j].Input * outputLayer[i].Weights[j]);
                }
                outputLayer[i].Output = outputLayer[i].Sum + outputLayer[i].Bias;
                outputLayer[i].Output = Helpers.HyperTanFunction(outputLayer[i].Output);
            }
            return outputLayer;
        }
    }
}

