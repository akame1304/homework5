using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homework3_Csh
{
    public partial class Form1 : Form
    {
        private Point mouseDownLocation;
        private bool isDragging = false;


        private Bitmap graphBitmap;

        public Form1()
        {
            InitializeComponent();
            /*Adversary(int m, int n, int T, int N, float lambda)this.numberOfSystem = m;
            this.numberOfAttacks = n;

            this.timeInterval = T;
            this.subtimeInterval = N;

            this.lambda = lambda;*/

            //generate attack
            Adversary adv = new Adversary(10, 200, 6, 10, 0.9f);
            adv.generateAttacks();
            //retrieve data

            List<List<int>> data2 = adv.GetLineChart2AttackList();
                        

            LineChart chart2 = new LineChart(this, pictureBox2);
            chart2.DrawChart(this, data2);

            int k = 20;

            Dictionary<int, int> histogramDistChart2 = adv.createHistoDistrib(data2, k);


            //Histogram histogram1 = new Histogram(this, chart1, histogramDistChart1, k);
            Histogram histogram2 = new Histogram(this, chart2, histogramDistChart2, k);

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
