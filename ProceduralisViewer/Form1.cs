using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProceduralisViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        const int size = 512;
        Bitmap bitmap;
        private void button1_Click(object sender, EventArgs e)
        {
            bitmap = new Bitmap(size, size, PixelFormat.Format24bppRgb);
            //bitmap.SetData<Format24BppRgb>(GetImageData(DiamondSquare()));
            Voronoi v = new Voronoi(size);
            DiamondSquare ds = new DiamondSquare(size);
            Rebuild();
            
            bitmap.SetData<Format24BppRgb>(GetImageData(heights));
            pictureBox1.Image = bitmap;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                Erode();
            }
            bitmap.SetData<Format24BppRgb>(GetImageData(heights));
            pictureBox1.Image = bitmap;
        }
        Random _random = new Random();
        private double[,] GetRandomImage()
        {
            
            double[,] result = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = _random.NextDouble();
                }
            }
            return result;
        }

        private IEnumerable<Format24BppRgb> GetImageData(double[,] numericData)
        {
            foreach (var item in numericData)
            {
                yield return new Format24BppRgb((byte)(255*item));
            }
        }
        private void Rebuild()
        {
            waters = new double[size, size];
            materials = new double[size, size];

            noise1 = DiamondSquare.Get(size);
            noise2 = DiamondSquare.Get(size);
            double[,] diamondSquare = DiamondSquare.Get(size);
            double[,] voronoi = Voronoi.Get(size);

            heights = new double[size, size];
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    heights[x, y] = 0.67f * diamondSquare[x, y] + 0.33f * voronoi[x, y];
                    //heights[x, y] = diamondSquare[x, y];
                    //heights[x, y] = voronoi[x, y];
                }
            }
            heights = Perturbation(heights);


        }
        void Erode()
        {
            heights = ThermalErosion(heights);
            heights = HydrolicErosion(heights);

        }

        private double[,] heights;
        private double[,] waters;
        private double[,] materials;
        private double[,] noise1;
        private double[,] noise2;
        private double Kr = 0.01f;
        private double Ks = 0.01f;
        private double Ke = 0.5f;
        private double Kc = 0.01f;
        double[,] altitudes;

        private double[,] HydrolicErosion(double[,] heights)
        {
            WaterAppereance();
            WaterEroding(heights);
            Capturing();
            Transportation(heights);
            Evaporation(heights);
            return heights;
        }

        private void Evaporation(double[,] heights)
        {

            for (int x = 0; x < waters.GetLength(0); x++)
                for (int y = 0; y < waters.GetLength(1); y++)
                    waters[x, y] = waters[x, y] * (1f - Ke);

            for (int x = 0; x < waters.GetLength(0); x++)
                for (int y = 0; y < waters.GetLength(1); y++)
                {
                    double deltaM = Math.Max(0, materials[x, y] - (Kc * waters[x, y]));
                    materials[x, y] -= deltaM;
                    heights[x, y] += deltaM;
                }
        }

        private void Transportation(double[,] heights)
        {
            double[,] deltaWaters = new double[size, size];
            double[,] deltaMaterials = new double[size, size];
            altitudes = (double[,])heights.Clone();
            for (int x = 0; x < altitudes.GetLength(0); x++)
                for (int y = 0; y < altitudes.GetLength(1); y++)
                    altitudes[x, y] += waters[x, y];
            double allTheDeltas = 0;

            for (int x = 0; x < altitudes.GetLength(0); x++)
                for (int y = 0; y < altitudes.GetLength(1); y++)
                {
                    double[] deltaWs = new double[4];
                    double[] deltas = new double[4];
                    double[] alts = new double[4];
                    double deltasTotal = 0;
                    alts[0] = altitudes[wrap(x), wrap(y - 1)];
                    alts[1] = altitudes[wrap(x + 1), wrap(y)];
                    alts[2] = altitudes[wrap(x), wrap(y + 1)];
                    alts[3] = altitudes[wrap(x - 1), wrap(y)];

                    for (int i = 0; i < 4; i++)
                        deltas[i] = altitudes[x, y] - alts[i];

                    int count = 1;
                    double altitudesTotal = altitudes[x, y];
                    for (int i = 0; i < 4; i++)
                        if (deltas[i] > 0)
                        {
                            deltasTotal += deltas[i];
                            altitudesTotal += alts[i];
                            count++;
                        }
                    double averageA = altitudesTotal / (double)count;
                    double deltaA = altitudes[x, y] - averageA;
                    double totalDeltaW = 0;
                    if (deltasTotal != 0)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (deltas[i] > 0)
                            {
                                deltaWs[i] = Math.Min(waters[x, y], deltaA) * (deltas[i] / deltasTotal);
                                totalDeltaW += deltaWs[i];
                            }
                        }
                    }

                    allTheDeltas += deltasTotal;

                    deltaWaters[x, y] -= totalDeltaW;
                    deltaWaters[wrap(x), wrap(y - 1)] += deltaWs[0];
                    deltaWaters[wrap(x + 1), wrap(y)] += deltaWs[1];
                    deltaWaters[wrap(x), wrap(y + 1)] += deltaWs[2];
                    deltaWaters[wrap(x - 1), wrap(y)] += deltaWs[3];

                    deltaMaterials[x, y] -= materials[x, y] * totalDeltaW / waters[x, y];
                    deltaMaterials[wrap(x), wrap(y - 1)] += materials[x, y] * deltaWs[0] / waters[x, y];
                    deltaMaterials[wrap(x + 1), wrap(y)] += materials[x, y] * deltaWs[1] / waters[x, y];
                    deltaMaterials[wrap(x), wrap(y + 1)] += materials[x, y] * deltaWs[2] / waters[x, y];
                    deltaMaterials[wrap(x - 1), wrap(y)] += materials[x, y] * deltaWs[3] / waters[x, y];

                }

            for (int x = 0; x < materials.GetLength(0); x++)
                for (int y = 0; y < materials.GetLength(1); y++)
                    //if (waters[x, y] > 0)
                    materials[x, y] += deltaMaterials[x, y];

            for (int x = 0; x < waters.GetLength(0); x++)
                for (int y = 0; y < waters.GetLength(1); y++)
                    waters[x, y] += deltaWaters[x, y];

        }

        private void Capturing()
        {
            for (int x = 0; x < materials.GetLength(0); x++)
                for (int y = 0; y < materials.GetLength(1); y++)
                    materials[x, y] += Ks * waters[x, y];
        }

        private void WaterEroding(double[,] heights)
        {
            for (int x = 0; x < heights.GetLength(0); x++)
                for (int y = 0; y < heights.GetLength(1); y++)
                    heights[x, y] -= Ks * waters[x, y];
        }

        private void WaterAppereance()
        {
            for (int x = 0; x < waters.GetLength(0); x++)
                for (int y = 0; y < waters.GetLength(1); y++)
                    waters[x, y] += Kr;

        }

        private double[,] ThermalErosion(double[,] heights)
        {


            double[,] newHeights = new double[heights.GetLength(0), heights.GetLength(1)];
            newHeights = (double[,])heights.Clone();
            for (int x = 0; x < heights.GetLength(0); x++)
            {
                for (int y = 0; y < heights.GetLength(1); y++)
                {
                    ThermalErosion(x, y, heights, newHeights);
                }

            }
            return newHeights;
        }

        private void ThermalErosion(int x, int y, double[,] heights, double[,] newHeights)
        {
            double Talus = 4 / size;
            double c = 0.5f;
            double[] deltas = new double[4];
            deltas[0] = heights[x, y] - heights[wrap(x), wrap(y - 1)];
            deltas[1] = heights[x, y] - heights[wrap(x + 1), wrap(y)];
            deltas[2] = heights[x, y] - heights[wrap(x), wrap(y + 1)];
            deltas[3] = heights[x, y] - heights[wrap(x - 1), wrap(y)];
            double deltaTotal = 0;
            double deltaMax = 0;
            for (int i = 0; i < 4; i++)
            {
                if (deltas[i] > Talus)
                {
                    deltaTotal += deltas[i];
                    deltaMax = Math.Max(deltaMax, deltas[i]);
                }
            }
            newHeights[x, y] -= (deltaMax - Talus) * c;
            newHeights[wrap(x), wrap(y - 1)] += deltas[0] > Talus ? c * (deltaMax - Talus) * (deltas[0] / deltaTotal) : 0;
            newHeights[wrap(x + 1), wrap(y)] += deltas[1] > Talus ? c * (deltaMax - Talus) * (deltas[1] / deltaTotal) : 0;
            newHeights[wrap(x), wrap(y + 1)] += deltas[2] > Talus ? c * (deltaMax - Talus) * (deltas[2] / deltaTotal) : 0;
            newHeights[wrap(x - 1), wrap(y)] += deltas[3] > Talus ? c * (deltaMax - Talus) * (deltas[3] / deltaTotal) : 0;
        }
        

        private double[,] Perturbation(double[,] heights)
        {
            double[,] result = new double[heights.GetLength(0), heights.GetLength(1)];

            for (int x = 0; x < result.GetLength(0); x++)
            {
                for (int y = 0; y < result.GetLength(1); y++)
                {
                    int tx = wrap((int)(x + (noise1[x, y] - 0.5f) * 0.5f * size));
                    int ty = wrap((int)(y + (noise2[x, y] - 0.5f) * 0.5f * size));
                    result[x, y] = heights[tx, ty];
                    //result[x, y] = noise1[x, y];
                }
            }

            return result;
        }
        private int wrap(int p)
        {
            if (p < 0)
                return p + size;
            if (p >= size)
                return p - size;
            return p;
        }

        
    }
}
