using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Schema;

namespace homework3_Csh
{
    internal class Histogram
    {

        protected Form form;
        protected LineChart lineChart;

        protected Dictionary<int, int> data = new Dictionary<int, int>();
        protected Dictionary<int, float> dataf = new Dictionary<int, float>();
        protected int k;
        
        
        /*
        public Histogram(Form form, Bitmap bitmap, List<double> data, int k)
        {
            //future implementations
            this.form = form;
            this.data = data;
            this.k = k;
        }
        */

        public Histogram(Form form, LineChart lineChart, Dictionary<int, int> data, int k) {

            this.form = form;
            this.lineChart = lineChart;

            this.data = data;
            this.k = k;
         
            DrawVerticalHistogram();
            lineChart.pictureBox.Invalidate();
        }
        public Histogram(Form form, LineChart lineChart, Dictionary<int, float> dataf, int k)
        {

            this.form = form;
            this.lineChart = lineChart;

            this.dataf = dataf;
            this.k = k;

            DrawVerticalHistogramF();
            lineChart.pictureBox.Invalidate();
        }

        public bool DrawVerticalHistogram()
        {
            Bitmap bitmap = lineChart.graphBitmap;
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                int lineChartPadding = lineChart.getPADDING();
                int width = bitmap.Width - 2 * lineChartPadding;
                int height = bitmap.Height - 2 * lineChartPadding;

                float yMin = lineChart.yMin;
                float yMax = lineChart.yMax;
                float xMin = lineChart.xMin;
                float xMax = lineChart.xMax;

                int[] dist = computeDist(data);

                if (k > yMax && k < yMin) return false; 

                Pen pen = new Pen(Color.Violet, 2);

                // Disegna l'asse Y verticale
                g.DrawLine(pen, lineChartPadding + width, lineChartPadding + height, lineChartPadding + width, lineChartPadding);
                

                int numIntervallo = 0;
                // i è decide da dove partono i rettangoli
                for (float i = yMin + (yMax - yMin)/k; i < yMax; i += (yMax-yMin)/k)
                {
                    // NUMERI
                    float y = lineChartPadding + height - (i - yMin) * height / (yMax - yMin);
                    g.DrawLine(pen, lineChartPadding + width - 5, y, lineChartPadding + width + 5, y);
                    g.DrawString(i.ToString(), form.Font, Brushes.Black, lineChartPadding + width + 5, y - 5);

                    // DRAW OF K RECTANGLES

                    //float unit = height / (xMax - width / 2);
                    float rect_len =  (float)dist[numIntervallo]/(float)dist.Max() * height;
                    Debug.WriteLine("dist Max" + dist.Max());
                    float interval_len = height / k;

                    float x1 = lineChartPadding + width - rect_len;
                    float y1 = lineChartPadding + height - (i - yMin) * height / (yMax - yMin);

                    numIntervallo++;

                    SolidBrush brush = new SolidBrush(Color.FromArgb(128, 0, 0, 255));
                    
                    g.FillRectangle(brush, x1, y1, rect_len, interval_len);
                
                }
                
                
            }
            
            return true;
        }

        public bool DrawVerticalHistogramF()
        {
            Bitmap bitmap = lineChart.graphBitmap;
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                int lineChartPadding = lineChart.getPADDING();
                int width = bitmap.Width - 2 * lineChartPadding;
                int height = bitmap.Height - 2 * lineChartPadding;

                float yMin = lineChart.yMin;
                float yMax = lineChart.yMax;
                float xMin = lineChart.xMin;
                float xMax = lineChart.xMax;

                int[] dist = computeDist(dataf);

                if (k > yMax && k < yMin) return false;

                Pen pen = new Pen(Color.Violet, 2);

                // Disegna l'asse Y verticale
                g.DrawLine(pen, lineChartPadding + width, lineChartPadding + height, lineChartPadding + width, lineChartPadding);


                int numIntervallo = 0;
                // i è decide da dove partono i rettangoli
                for (float i = yMin + (yMax - yMin) / k; i < yMax; i += (yMax - yMin) / k)
                {
                    // NUMERI
                    float y = lineChartPadding + height - (i - yMin) * height / (yMax - yMin);
                    g.DrawLine(pen, lineChartPadding + width - 5, y, lineChartPadding + width + 5, y);
                    g.DrawString(i.ToString(), form.Font, Brushes.Black, lineChartPadding + width + 5, y - 5);

                    // DRAW OF K RECTANGLES

                    //float unit = height / (xMax - width / 2);
                    float rect_len = (float)dist[numIntervallo] / (float)dist.Max() * height;
                    //Debug.WriteLine("dist Max" + dist.Max());
                    float interval_len = height / k;

                    float x1 = lineChartPadding + width - rect_len;
                    float y1 = lineChartPadding + height - (i - yMin) * height / (yMax - yMin);

                    numIntervallo++;

                    SolidBrush brush = new SolidBrush(Color.FromArgb(128, 0, 0, 255));

                    g.FillRectangle(brush, x1, y1, rect_len, interval_len);

                }
            }

            return true;
        }

        public int[] computeDist(Dictionary<int, int> systemsFinalValues)
        {

            int[] dist = new int[k];

            float lenInterval = 2 * lineChart.yMax / k;
            Debug.WriteLine(lenInterval);


            float j = lineChart.yMin;

            for (int numIntervallo = 0; numIntervallo < k; numIntervallo++)
            {
                foreach (int key in systemsFinalValues.Keys)
                {
                    if (j <= systemsFinalValues[key] && systemsFinalValues[key] < j + lenInterval) dist[numIntervallo]++;
                }

                j += lenInterval;
            }
            
            return dist;
        }

        public int[] computeDist(Dictionary<int, float> systemsFinalValues)
        {

            int[] dist = new int[k];

            float lenInterval = lineChart.yMax / k;

            float j = 0; // 0
            for (int numIntervallo = 0; numIntervallo < k; numIntervallo++)
            {
                foreach (int key in systemsFinalValues.Keys)
                {

                    if (j <= systemsFinalValues[key] && systemsFinalValues[key] < j + lenInterval)
                    {
                        Debug.WriteLine("dist : " + systemsFinalValues[key]);
                        dist[numIntervallo]++;
                    }

                    
                }
                
                j += lenInterval;
            }

            return dist;
        }

    }
}
