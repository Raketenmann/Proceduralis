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
        Erosion erosion;
        private void button1_Click(object sender, EventArgs e)
        {
            bitmap = new Bitmap(size, size, PixelFormat.Format24bppRgb);
            //bitmap.SetData<Format24BppRgb>(GetImageData(DiamondSquare()));
            double[,] heights = Rebuild();
            erosion = new Erosion(size, heights);
            
            bitmap.SetData<Format24BppRgb>(GetImageData(erosion.GetData()));
            pictureBox1.Image = bitmap;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                erosion.Erode();
            }
            bitmap.SetData<Format24BppRgb>(GetImageData(erosion.GetData()));
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
        private double[,] Rebuild()
        {

            double[,] diamondSquare = DiamondSquare.Get(size);
            double[,] voronoi = Voronoi.Get(size);

            double[,] heights = new double[size, size];
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    heights[x, y] = 0.67f * diamondSquare[x, y] + 0.33f * voronoi[x, y];
                }
            }
            return Perturbation(heights);


        }
        private IEnumerable<Format24BppRgb> GetImageData(double[,] numericData)
        {
            foreach (var item in numericData)
            {
                yield return new Format24BppRgb((byte)(255*item));
            }
        }

        private double[,] Perturbation(double[,] heights)
        {
            double[,] noise1 = DiamondSquare.Get(size);
            double[,] noise2 = DiamondSquare.Get(size);

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
