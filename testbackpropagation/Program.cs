using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;


namespace testbackpropagation
{
    class Program
    {
        static void Main(string[] args)
        {
            train();
        }

        class sigmoid
        {
            public static double output(double x)
            {
                return 1.0 / (1.0 + Math.Exp(-x));
            }

            public static double derivative(double x)
            {
                return x * (1 - x);
            }
        }

        class Neuron
        {
            public double[] inputs = new double[5];
            public double[] weights = new double[4];
            public string[] types = new string[4];
            public double error;

            public double biasWeight;

            private Random r = new Random();

            public double output
            {
                get { return sigmoid.output(weights[0] * inputs[0] + weights[1] * inputs[1]  + weights[2] * inputs[2] + weights[3] * inputs[3] + biasWeight); }
            }

            public void randomizeWeights()
            {
                weights[0] = r.NextDouble();
                weights[1] = r.NextDouble();
                weights[2] = r.NextDouble();
                weights[3] = r.NextDouble();
                biasWeight = r.NextDouble();
            }

            public void adjustWeights()
            {
                weights[0] += error * inputs[0];
                weights[1] += error * inputs[1];
                weights[2] += error * inputs[2];
                weights[3] += error * inputs[3];
                biasWeight += error;
            }
        }

        private static void train()
        {
            string pathweight = @"C:\Users\Q\source\repos\testbackpropagation\testbackpropagation\data\weight.txt";
            string pathTrainData = @"C:\Users\Q\source\repos\testbackpropagation\testbackpropagation\data\startdata.txt";
            int epoch = 0;
            // the input values from file
            // if (File.Exists(path))
            //{
            string[] readText = File.ReadAllLines(pathTrainData);
            int j = 0;
            int lenghtt = readText.Length;
            double[] types1 = new double[lenghtt];
            double[] types2 = new double[lenghtt];
            double[] types3 = new double[lenghtt];
            double[] types4 = new double[lenghtt];
            double[,] startdataSAVE = new double[lenghtt, 4];
            double[,] start_data = new double[lenghtt, 4];
            string[] arrayOneuser;
            string arrayOneuserresult;
            string[] arrayOneuserresults;
            foreach (string s in readText)
                {
                
                if (s.Contains(';'))
                {
                    arrayOneuser = s.ToString().Split(';');
                    arrayOneuserresult = arrayOneuser[1];
                    arrayOneuserresult = arrayOneuserresult.Replace(" ", "");
                }
                else
                {
                    arrayOneuserresult = s.ToString();
                    
                }
                

                arrayOneuserresults = arrayOneuserresult.ToString().Split(',');
                    types1[j] = Convert.ToDouble(arrayOneuserresults[0]); 
                    types2[j] = Convert.ToDouble(arrayOneuserresults[1]);
                    types3[j] = Convert.ToDouble(arrayOneuserresults[2]);
                    types4[j] = Convert.ToDouble(arrayOneuserresults[3]);


                    arrayOneuser = arrayOneuserresult.ToString().Split(',');
                    for (int i = 0; i < arrayOneuser.Length; i++) {
                        start_data[j,i] = Convert.ToDouble(arrayOneuser[i]);
                    startdataSAVE[j, i] = Convert.ToDouble(arrayOneuser[i]);
                    }
                    j++;
                }


            // }


            //data Normalization for correct working machine lerning alghoritm
            
            double[,] inputs= start_data;
            double[] arrayValue = new double[lenghtt];
                for (int i = 0; i < 4; i++) {
                for (int qwee = 0; qwee < lenghtt; qwee++)
                {
                    arrayValue[qwee] = start_data[qwee, i];
                }

                double avgValue = arrayValue.Sum() / lenghtt;
                    double diperancy = Math.Sqrt((Math.Pow((arrayValue[0] - avgValue), 2) + Math.Pow((arrayValue[1] - avgValue), 2) + Math.Pow((arrayValue[2] - avgValue), 2) + Math.Pow((arrayValue[3] - avgValue), 2) + Math.Pow((arrayValue[3] - avgValue), 2)) / lenghtt);
                for (int qq = 0; qq < lenghtt; qq++)
                {
                    inputs[qq, i] = Math.Sqrt((Math.Pow((arrayValue[qq] - avgValue) / diperancy, 2)));
                }

 
            }
            
            
            // types of users
            //string[] types = { "student", "traveler", "doctor", "major" };
            
            // desired results
            double[] resultsfor1output = types1;
            double[] resultsfor2output = types2;
            double[] resultsfor3output = types3;
            double[] resultsfor4output = types4;

            // creating the neurons
            Neuron hiddenNeuron1 = new Neuron();
            Neuron hiddenNeuron2 = new Neuron();
            Neuron hiddenNeuron3 = new Neuron();
            Neuron hiddenNeuron4 = new Neuron();
            Neuron outputNeuron1 = new Neuron();
            Neuron outputNeuron2 = new Neuron();
            Neuron outputNeuron3 = new Neuron();
            Neuron outputNeuron4 = new Neuron();










            string[] WeightLines = File.ReadAllLines(pathweight);
            if (WeightLines.Length != 0)
            {
                string[] arrayWeightforOneNeuron;
                int qq = 0;
                foreach (string s in WeightLines)
                {
                    arrayWeightforOneNeuron = s.ToString().Split(';');
                    if (qq == 0)
                    {
                        for (int i = 0; i < arrayWeightforOneNeuron.Length - 2; i++)
                        {
                            hiddenNeuron1.weights[i] = Convert.ToDouble(arrayWeightforOneNeuron[i]);
                        }
                        hiddenNeuron1.biasWeight = Convert.ToDouble(arrayWeightforOneNeuron[arrayWeightforOneNeuron.Length - 2]);
                        hiddenNeuron1.error = Convert.ToDouble(arrayWeightforOneNeuron[arrayWeightforOneNeuron.Length - 1]);

                    }
                    if (qq == 1)
                    {
                        for (int i = 0; i < arrayWeightforOneNeuron.Length - 2; i++)
                        {
                            hiddenNeuron2.weights[i] = Convert.ToDouble(arrayWeightforOneNeuron[i]);
                        }
                        hiddenNeuron2.biasWeight = Convert.ToDouble(arrayWeightforOneNeuron[arrayWeightforOneNeuron.Length - 2]);
                        hiddenNeuron2.error = Convert.ToDouble(arrayWeightforOneNeuron[arrayWeightforOneNeuron.Length - 1]);

                    }
                    if (qq == 2)
                    {
                        for (int i = 0; i < arrayWeightforOneNeuron.Length - 2; i++)
                        {
                            hiddenNeuron3.weights[i] = Convert.ToDouble(arrayWeightforOneNeuron[i]);
                        }
                        hiddenNeuron3.biasWeight = Convert.ToDouble(arrayWeightforOneNeuron[arrayWeightforOneNeuron.Length - 2]);
                        hiddenNeuron3.error = Convert.ToDouble(arrayWeightforOneNeuron[arrayWeightforOneNeuron.Length - 1]);

                    }
                    if (qq == 3)
                    {
                        for (int i = 0; i < arrayWeightforOneNeuron.Length - 2; i++)
                        {
                            hiddenNeuron4.weights[i] = Convert.ToDouble(arrayWeightforOneNeuron[i]);
                        }
                        hiddenNeuron4.biasWeight = Convert.ToDouble(arrayWeightforOneNeuron[arrayWeightforOneNeuron.Length - 2]);
                        hiddenNeuron4.error = Convert.ToDouble(arrayWeightforOneNeuron[arrayWeightforOneNeuron.Length - 1]);
                    }
                    if (qq == 4)
                    {
                        for (int i = 0; i < arrayWeightforOneNeuron.Length - 2; i++)
                        {
                            outputNeuron1.weights[i] = Convert.ToDouble(arrayWeightforOneNeuron[i]);
                        }
                        outputNeuron1.biasWeight = Convert.ToDouble(arrayWeightforOneNeuron[arrayWeightforOneNeuron.Length - 2]);
                            outputNeuron1.error = Convert.ToDouble(arrayWeightforOneNeuron[arrayWeightforOneNeuron.Length - 1]);
                        
                    }
                    if (qq == 5)
                    {
                        for (int i = 0; i < arrayWeightforOneNeuron.Length - 2; i++)
                        {
                            outputNeuron2.weights[i] = Convert.ToDouble(arrayWeightforOneNeuron[i]);
                    }
                    outputNeuron2.biasWeight = Convert.ToDouble(arrayWeightforOneNeuron[arrayWeightforOneNeuron.Length - 2]);
                            outputNeuron2.error = Convert.ToDouble(arrayWeightforOneNeuron[arrayWeightforOneNeuron.Length - 1]);
                            
                    }
                    if (qq == 6)
                    {
                        for (int i = 0; i < arrayWeightforOneNeuron.Length - 2; i++)
                        {
                            outputNeuron3.weights[i] = Convert.ToDouble(arrayWeightforOneNeuron[i]);
                        }
                    outputNeuron3.biasWeight = Convert.ToDouble(arrayWeightforOneNeuron[arrayWeightforOneNeuron.Length - 2]);
                            outputNeuron3.error = Convert.ToDouble(arrayWeightforOneNeuron[arrayWeightforOneNeuron.Length - 1]);
                                
                    }
                    if (qq == 7)
                    {
                        for (int i = 0; i < arrayWeightforOneNeuron.Length - 2; i++)
                        {
                            outputNeuron4.weights[i] = Convert.ToDouble(arrayWeightforOneNeuron[i]);
                    }
                    outputNeuron4.biasWeight = Convert.ToDouble(arrayWeightforOneNeuron[arrayWeightforOneNeuron.Length - 2]);
                            outputNeuron4.error = Convert.ToDouble(arrayWeightforOneNeuron[arrayWeightforOneNeuron.Length - 1]);
                                    
                    }
                                   
                                            qq++;

                }
                for (int i = 0; i < lenghtt; i++)  // very important, do NOT train for only one example
                {
                    // 1) forward propagation (calculates output)
                    hiddenNeuron1.inputs = new double[] { inputs[i, 0], inputs[i, 1], inputs[i, 2], inputs[i, 3] };
                    hiddenNeuron2.inputs = new double[] { inputs[i, 0], inputs[i, 1], inputs[i, 2], inputs[i, 3] };
                    hiddenNeuron3.inputs = new double[] { inputs[i, 0], inputs[i, 1], inputs[i, 2], inputs[i, 3] };
                    hiddenNeuron4.inputs = new double[] { inputs[i, 0], inputs[i, 1], inputs[i, 2], inputs[i, 3]};
                    outputNeuron1.inputs = new double[] { hiddenNeuron1.output, hiddenNeuron2.output, hiddenNeuron3.output, hiddenNeuron4.output };
                    outputNeuron2.inputs = new double[] { hiddenNeuron1.output, hiddenNeuron2.output, hiddenNeuron3.output, hiddenNeuron4.output };
                    outputNeuron3.inputs = new double[] { hiddenNeuron1.output, hiddenNeuron2.output, hiddenNeuron3.output, hiddenNeuron4.output };
                    outputNeuron4.inputs = new double[] { hiddenNeuron1.output, hiddenNeuron2.output, hiddenNeuron3.output, hiddenNeuron4.output };

                    string maxValue = "0, 0, 0, 0";
                    double[] qwe = { outputNeuron1.output, outputNeuron2.output, outputNeuron3.output, outputNeuron4.output };
                    if (outputNeuron1.output < qwe.Max())
                    {

                        if (outputNeuron2.output < qwe.Max())
                        {

                            if (outputNeuron3.output < qwe.Max())
                            {

                                if (outputNeuron4.output < qwe.Max())
                                {

                                }
                                else
                                {
                                    maxValue = "0, 0, 0,1 ";
                                }
                            }
                            else
                            {
                                maxValue = "0, 0, 1, 0 ";
                            }
                        }
                        else
                        {
                            maxValue = "0, 1, 0, 0 ";
                        }

                    }
                    else
                    {
                        maxValue = "1, 0, 0, 0 ";
                    }

                    Console.WriteLine("{0}, {1}, {2}, {3} ={4} ", startdataSAVE[i, 0], startdataSAVE[i, 1], startdataSAVE[i, 2], startdataSAVE[i, 3], maxValue);
                }
            }
            else
            {

                // random weights
                hiddenNeuron1.randomizeWeights();
                hiddenNeuron2.randomizeWeights();
                hiddenNeuron3.randomizeWeights();
                hiddenNeuron4.randomizeWeights();
                outputNeuron1.randomizeWeights();
                outputNeuron2.randomizeWeights();
                outputNeuron3.randomizeWeights();
                outputNeuron4.randomizeWeights();



            Retry:
                epoch++;
                for (int i = 0; i < lenghtt; i++)  // very important, do NOT train for only one example
                {
                    // 1) forward propagation (calculates output)
                    hiddenNeuron1.inputs = new double[] { inputs[i, 0], inputs[i, 1], inputs[i, 2], inputs[i, 3] };
                    hiddenNeuron2.inputs = new double[] { inputs[i, 0], inputs[i, 1], inputs[i, 2], inputs[i, 3] };
                    hiddenNeuron3.inputs = new double[] { inputs[i, 0], inputs[i, 1], inputs[i, 2], inputs[i, 3]};
                    hiddenNeuron4.inputs = new double[] { inputs[i, 0], inputs[i, 1], inputs[i, 2], inputs[i, 3] };
                    outputNeuron1.inputs = new double[] { hiddenNeuron1.output, hiddenNeuron2.output, hiddenNeuron3.output, hiddenNeuron4.output };
                    outputNeuron2.inputs = new double[] { hiddenNeuron1.output, hiddenNeuron2.output, hiddenNeuron3.output, hiddenNeuron4.output };
                    outputNeuron3.inputs = new double[] { hiddenNeuron1.output, hiddenNeuron2.output, hiddenNeuron3.output, hiddenNeuron4.output };
                    outputNeuron4.inputs = new double[] { hiddenNeuron1.output, hiddenNeuron2.output, hiddenNeuron3.output, hiddenNeuron4.output };
                    string maxValue= "0, 0, 0, 0";
                    double[] qwe = { outputNeuron1.output, outputNeuron2.output, outputNeuron3.output, outputNeuron4.output };
                    if (outputNeuron1.output < qwe.Max())
                    {
                        
                        if (outputNeuron2.output < qwe.Max())
                        {
                            
                            if (outputNeuron3.output < qwe.Max())
                            {
                                
                                if (outputNeuron4.output < qwe.Max())
                                {

                                }
                                else
                                {
                                    maxValue = "0, 0, 0,1 ";
                                }
                            }
                            else
                            {
                                maxValue =  "0, 0, 1, 0 ";
                            }
                        }
                        else
                        {
                            maxValue = "0, 1, 0, 0 ";
                        }

                    }
                    else
                    {
                        maxValue = "1, 0, 0, 0 ";
                    }
                    
                     Console.WriteLine("{0}, {1}, {2}, {3} ={4} ", startdataSAVE[i, 0], startdataSAVE[i, 1], startdataSAVE[i, 2], startdataSAVE[i, 3], maxValue);

                     //Console.WriteLine("{0}, {1}, {2}, {3} ={4},{5},{6},{7} ", inputs[i, 0], inputs[i, 1], inputs[i, 2], inputs[i, 3], outputNeuron1.output, outputNeuron2.output, outputNeuron3.output, outputNeuron4.output);
                    // 2) back propagation (adjusts weights)

                    // adjusts the weight of the output neuron, based on its error
                    outputNeuron1.error = sigmoid.derivative(outputNeuron1.output) * (resultsfor1output[i] - outputNeuron1.output) ;
                    outputNeuron2.error = sigmoid.derivative(outputNeuron2.output)* (resultsfor2output[i] - outputNeuron2.output)  ;
                    outputNeuron3.error = sigmoid.derivative(outputNeuron3.output)*  (resultsfor3output[i] - outputNeuron3.output) ;
                    outputNeuron4.error = sigmoid.derivative(outputNeuron4.output)  * (resultsfor4output[i] - outputNeuron4.output);
                    outputNeuron1.adjustWeights();
                    outputNeuron2.adjustWeights();
                    outputNeuron3.adjustWeights();
                    outputNeuron4.adjustWeights();

                    // then adjusts the hidden neurons' weights, based on their errors
                    hiddenNeuron1.error = sigmoid.derivative(hiddenNeuron1.output) * outputNeuron1.error * outputNeuron1.weights[0] * outputNeuron1.error * outputNeuron1.weights[1] * outputNeuron1.error * outputNeuron1.weights[2] * outputNeuron1.error * outputNeuron1.weights[3];
                    hiddenNeuron2.error = sigmoid.derivative(hiddenNeuron2.output) * outputNeuron2.error * outputNeuron2.weights[0] * outputNeuron2.error * outputNeuron2.weights[1] * outputNeuron2.error * outputNeuron2.weights[2] * outputNeuron2.error * outputNeuron2.weights[3];
                    hiddenNeuron3.error = sigmoid.derivative(hiddenNeuron3.output) * outputNeuron3.error * outputNeuron3.weights[0] * outputNeuron3.error * outputNeuron3.weights[1] * outputNeuron3.error * outputNeuron3.weights[2] * outputNeuron3.error * outputNeuron3.weights[3]; //weights[2 and 3]=0 all time. why neuron 1*weights[0] and why neuron 2*weights[1] ?????
                    hiddenNeuron4.error = sigmoid.derivative(hiddenNeuron4.output) * outputNeuron4.error * outputNeuron4.weights[0] * outputNeuron4.error * outputNeuron4.weights[1] * outputNeuron4.error * outputNeuron4.weights[2] * outputNeuron4.error * outputNeuron4.weights[3]; //how to do weights[2]!=0 and weights[2]!=0?

                    hiddenNeuron1.adjustWeights();
                    hiddenNeuron2.adjustWeights();
                    hiddenNeuron3.adjustWeights();
                    hiddenNeuron4.adjustWeights();
                }
                if (epoch < 1000)
                    goto Retry;
            }




            //writing all weights to file C:\Users\Q\source\repos\testbackpropagation\testbackpropagation\data\weight.txt
                   if (WeightLines.Length == 0)
                   {
                       string[] arrayWeight = {
                       hiddenNeuron1 .weights[0].ToString()+"; " +hiddenNeuron1.weights[1].ToString()+"; "+hiddenNeuron1.weights[2].ToString()+"; "+ hiddenNeuron1.weights[3].ToString()+"; "+ hiddenNeuron1.biasWeight.ToString()+"; "+ hiddenNeuron1.error.ToString()  ,
                       hiddenNeuron2 .weights[0].ToString()+"; " +hiddenNeuron2.weights[1].ToString()+"; "+hiddenNeuron2.weights[2].ToString()+"; "+ hiddenNeuron2.weights[3].ToString()+"; "+ hiddenNeuron2.biasWeight.ToString() +"; "+ hiddenNeuron1.error.ToString() ,
                       hiddenNeuron3 .weights[0].ToString()+"; " +hiddenNeuron3.weights[1].ToString()+"; "+hiddenNeuron3.weights[2].ToString()+"; "+ hiddenNeuron3.weights[3].ToString()+"; "+ hiddenNeuron3.biasWeight.ToString()+"; "+ hiddenNeuron1.error.ToString()  ,
                       hiddenNeuron4 .weights[0].ToString()+"; " +hiddenNeuron4.weights[1].ToString()+"; "+hiddenNeuron4.weights[2].ToString()+"; "+ hiddenNeuron4.weights[3].ToString()+"; "+ hiddenNeuron4.biasWeight.ToString()+"; "+ hiddenNeuron1.error.ToString()  ,
                       outputNeuron1 .weights[0].ToString()+"; " +outputNeuron1.weights[1].ToString()+"; "+outputNeuron1.weights[2].ToString()+"; "+ outputNeuron1.weights[3].ToString()+"; "+ outputNeuron1.biasWeight.ToString()+"; "+ outputNeuron1.error.ToString()  ,
                       outputNeuron2 .weights[0].ToString()+"; " +outputNeuron2.weights[1].ToString()+"; "+outputNeuron2.weights[2].ToString()+"; "+ outputNeuron2.weights[3].ToString()+"; "+ outputNeuron2.biasWeight.ToString()+"; "+ outputNeuron2.error.ToString()  ,
                       outputNeuron3 .weights[0].ToString()+"; " +outputNeuron3.weights[1].ToString()+"; "+outputNeuron3.weights[2].ToString()+"; "+ outputNeuron3.weights[3].ToString()+"; "+ outputNeuron3.biasWeight.ToString()+"; "+ outputNeuron3.error.ToString()  ,
                       outputNeuron4 .weights[0].ToString()+"; " +outputNeuron4.weights[1].ToString()+"; "+outputNeuron4.weights[2].ToString()+"; "+ outputNeuron4.weights[3].ToString()+"; "+ outputNeuron4.biasWeight.ToString()+"; "+ outputNeuron4.error.ToString()  ,
                       };
                       File.WriteAllLines(pathweight, arrayWeight); 
                   }
                   


            Console.ReadLine();

        }
    }
}