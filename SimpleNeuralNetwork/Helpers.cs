using System;
using System.Collections.Generic;

namespace SimpleNetwork
{
    public class Helpers
    {

        public static void Print(List<Neuron> neurons)
        {
             foreach (var neuron in neurons)
            {
                System.Console.Write(neuron.Sum.ToString("F2") + " ");
            };
        }

        public static void PrintWithBias(List<Neuron> neurons)
        {
            foreach (var neuron in neurons)
            {
                System.Console.Write(neuron.Output.ToString("F2") + " ");
            };
        }

        public static double SigmoidFunction(double x)
        {
            if (x < -45.0) return 0.0;
            else if (x > 45.0) return 1.0;
            else return 1.0 / (1.0 + Math.Exp(-x));
        }

        public static double HyperTanFunction(double x)
        {
            if (x < -10.0) return -1.0;
            else if (x > 10.0) return 1.0;
            else return Math.Tanh(x);
        }
    }
}