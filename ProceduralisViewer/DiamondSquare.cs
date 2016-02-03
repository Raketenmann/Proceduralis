using System;

namespace ProceduralisViewer
{
    internal class DiamondSquare
    {
        private int size;
        private Random _random = new Random();

        double[,] heights;
        public DiamondSquare(int size)
        {
            this.size = size;
        }
        
        public static double[,] Get(int size)
        {
            DiamondSquare ds = new DiamondSquare(size);
            return ds.GetData();
        }

        public double[,] GetData()
        {
             heights = new double[size, size];
            return CalculateDiamondSquare();
        }
        

        private double[,] CalculateDiamondSquare()
        {

            //Init
            heights[0, 0] = (float)_random.NextDouble();

            heights[0, size/2] = (float)_random.NextDouble();
            heights[0, size / 2] = (float)_random.NextDouble();
            heights[size / 2, size / 2] = (float)_random.NextDouble();

            int s = size/4;
            float persistence = 1f;
            while (s > 0)
            {
                DiamondSquareStep(s, persistence, size);
                s /= 2;
                persistence *= 0.5f;
            }
            return heights;
        }

        private void DiamondSquareStep(int stepSize, float persistence, int size)
        {
            //Diamond
            for (int x = stepSize; x < size; x += stepSize * 2)
            {
                for (int y = stepSize; y < size; y += stepSize * 2)
                {
                    //Console.Out.WriteLine("Diamond " + x + ":" + y);
                    double avg = GetDiamondAverage(x, y, stepSize);
                    heights[x, y] = Clamp01(avg + GetRandom() * persistence * avg);
                }
            }

            //Square
            for (int x = stepSize; x < size; x += stepSize * 2)
            {
                for (int y = stepSize; y < size; y += stepSize * 2)
                {
                    CalcSquareValue(x, y - stepSize, stepSize, persistence);//top
                    CalcSquareValue(x - stepSize, y, stepSize, persistence);//left
                                                                            //CalcSquareValue(x + stepSize, y, stepSize, persistence);//right
                                                                            //CalcSquareValue(x, y + stepSize, stepSize, persistence);//bottom

                    /*
                   if (x == stepSize)
                   {
                       CalcSquareValue(size - 1, y, stepSize, persistence);//right
                       calcs++;
                   }*/
                }
                //CalcSquareValue(x, size-1, stepSize, persistence);//bottom

            }
        }
        private double Clamp01(double val)
        {
            return Math.Min(1, Math.Max(0, val));
        }
        private double GetDiamondAverage(int x, int y, int range)
        {
            return (Sample((x - range), (y - range))
                + Sample((x + range), wrap(y - range))
                + Sample((x + range), wrap(y + range))
                + Sample(wrap(x - range), wrap(y + range))) / 4f;
        }
        private void CalcSquareValue(int x, int y, int stepSize, float persistence)
        {
            double avg;
            //Console.Out.WriteLine("Square " + x + ":" + y);
            if (x == 0 || x == size - 1)
                avg = GetColumnAverage(x, y, stepSize);
            else if (y == 0 || y == size - 1)
                avg = GetRowAverage(x, y, stepSize);
            else
                avg = GetSquareAverage(x, y, stepSize);
            heights[x, y] = Clamp01(avg + GetRandom() * persistence * avg);
        }
        private double GetSquareAverage(int x, int y, int range)
        {
            return (Sample(x, (y - range)) + Sample((x + range), y) + Sample(x, (y + range)) + Sample((x - range), y)) / 4f;
        }
        private double GetColumnAverage(int x, int y, int range)
        {
            return (Sample(x, (y - range)) + Sample(x, (y + range))) / 2f;
        }
        private double GetRowAverage(int x, int y, int range)
        {
            return (Sample((x + range), y) + Sample((x - range), y)) / 2f;
        }
        private double Sample(int x, int y)
        {
            //Console.Out.WriteLine("sample " + x +","+ y + " -> " + wrap(x) + ", " + wrap(y));
            return heights[wrap(x), wrap(y)];
        }
        private int wrap(int p)
        {
            if (p < 0)
                return p + size;
            if (p >= size)
                return p - size;
            return p;
        }
        private float GetRandom()
        {
            return ((float)_random.NextDouble() * 2f) - 1f;
            //return (float)_random.NextDouble();
        }
    }
}