using System;
using System.Collections.Generic;

namespace SimpleNetwork
{

    public class Neuron
    {

        public double Input { get; set; }
        public double Output { get; set; }
        public double Bias { get; set; }
        public double Sum { get; set; }
        public List<double> Weights { get; set; }

        public void InputValues()
        {
            System.Console.Write(this.Input.ToString("F2") + " ");
        }
    }

}