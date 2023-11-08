using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework3_Csh
{
    internal class Adversary
    {
        // Attributes
        protected int numberOfAttacks { get; private set; }
        protected int numberOfSystem { get; private set; }
        protected int timeInterval { get; private set; } // T
        protected int subtimeInterval { get; private set; } // N

        protected float lambda { get; private set; }

        protected float probability { get; private set; }

        // This list represents the history of all N attacks for all M systems
        protected List<List<int>> lineChartValuesChart1 { get; private set; }
        protected List<List<int>> lineChartValuesChart2 { get; private set; }
        protected List<List<float>> lineChartValuesChart3 { get; private set; }
        protected List<List<float>> lineChartValuesChart4 { get; private set; }


        // Constructor
        public Adversary(int m, int n, int T, int N, float lambda)
        {
            this.numberOfSystem = m;
            this.numberOfAttacks = n;

            this.timeInterval = T;
            this.subtimeInterval = N;

            this.lambda = lambda;
            this.probability = lambda * ((float)timeInterval / subtimeInterval);

            lineChartValuesChart2 = new List<List<int>>();
        }

        // Method to generate attacks
        public bool generateAttacks()
        {
            Random random = new Random();

            for (int i = 0; i < numberOfSystem; i++)
            {
                List<int> valuesChart2 = new List<int>();

                for (int j = 0; j < numberOfAttacks; j++)
                {
                    // Generate random from (0, 1]
                    float randomNumber = (float)random.NextDouble();

                    if (randomNumber > this.probability)
                    {
                        // Attacck success
                        Debug.WriteLine("System " + i + ": Attack" + j + " succeeded");
                        valuesChart2.Add(1);
                    }
                    else
                    {
                        // Attacco failed
                        Debug.WriteLine("System " + i + ": Attack" + j + " failed");
                        valuesChart2.Add(0);
                    }
                }
                lineChartValuesChart2.Add(valuesChart2);
            }


            return true;
        }

        private bool createChart3Values() {

            for (int i = 0; i < lineChartValuesChart2.Count; i++)
            {
                List<float> systemIValues = new List<float>(); systemIValues.Add(0); 
                float sum = 0;
                //Debug.WriteLine("System " + i);
                for (int j = 0; j < lineChartValuesChart2[i].Count; j++)
                {
                    sum += lineChartValuesChart2[i][j];
                    systemIValues.Add(sum / (j + 1));
                    //Debug.WriteLine("Frequency (relative) values: " + sum / (j + 1));
                }
                lineChartValuesChart3.Add(systemIValues);
            }
            
            return true;
        }

        private bool createChart4Values()
        {

            for (int i = 0; i < lineChartValuesChart2.Count; i++)
            {
                List<float> systemIValues = new List<float>(); systemIValues.Add(0);
                float sum = 0;
                //Debug.WriteLine("System " + i);
                for (int j = 0; j < lineChartValuesChart2[i].Count; j++)
                {
                    sum += lineChartValuesChart2[i][j];
                    systemIValues.Add(sum / (float)Math.Sqrt(j + 1));
                    //Debug.WriteLine("Frequency (normalized) values: " + sum / Math.Sqrt(j + 1));
                }
                lineChartValuesChart4.Add(systemIValues);
            }

            return true;
        }

        public Dictionary<int, int> createHistoDistrib(List<List<int>> values, int k)
        {

            Dictionary<int, int> finalValues = new Dictionary<int, int>();

            for (int i = 0; i < values.Count; i++)
            {
                int sum = 0;
                for (int s = 0; s < values[i].Count; s++)
                {
                    sum += values[i][s];
                }
                //Debug.WriteLine("   System" + i + " final value = " + sum);
                finalValues.Add(i, sum);
            }

            return finalValues;
        }

        public Dictionary<int, float> createHistoDistrib(List<List<float>> values, int k)
        {

            Dictionary<int, float> finalValues = new Dictionary<int, float>();

            int i = 0;
            foreach (List<float> list in values)
            {
                finalValues.Add(i, list.Last());
                i++;
                Debug.WriteLine("last freq " + i + " : " + list.Last());
            }

            /*
            for (int i = 0; i < values.Count; i++)
            {
                float sum = 0;
                for (int s = 0; s < values[i].Count; s++)
                {
                    sum += values[i][s];
                }
                //Debug.WriteLine("   System" + i + " final value = " + sum);
                finalValues.Add(i, sum);
            }*/

            return finalValues;
        }

        public List<List<int>> GetLineChart1AttackList()
        {
            return lineChartValuesChart1;
        }

        public List<List<int>> GetLineChart2AttackList()
        {
            return lineChartValuesChart2;
        }
        
        public List<List<float>> GetLineChart3AttackList()
        {
            return lineChartValuesChart3;
        }
        
        public List<List<float>> GetLineChart4AttackList()
        {
            return lineChartValuesChart4;
        }
    }
}
