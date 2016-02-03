using System;
using System.Collections.Generic;
using System.Linq;

namespace ProceduralisViewer
{
    internal class Voronoi
    {
        private int size;
        private List<FeaturePoint> _featurePoints = new List<FeaturePoint>();
        private Random _random = new Random();

        public Voronoi(int size)
        {
            this.size = size;
        }
        public double[,] GetData()
        {
            return Voronio();
        }
        public static double[,] Get(int size)
        {
            Voronoi voronoi = new Voronoi(size);
            return voronoi.GetData();
        }


        private class FeaturePoint
        {
            private double y;
            private double x;

            public FeaturePoint(double x, double y)
            {
                this.x = x;
                this.y = y;
            }

            internal double DistanceTo(double x, double y, int size)
            {
                double xdist = Math.Abs(this.x - x);
                double ydist = Math.Abs(this.y - y);

                // closer to wrap around?
                if (xdist > size / 2)
                {
                    if (this.x < x)
                    {
                        xdist = this.x + (size - x);
                    }
                    else
                    {
                        xdist = x + (size - this.x);
                    }
                }
                // closer to wrap around?
                if (ydist > size / 2)
                {
                    if (this.y < y)
                    {
                        ydist = this.y + (size - y);
                    }
                    else
                    {
                        ydist = y + (size - this.y);
                    }
                }
                return Math.Sqrt(xdist * xdist + ydist * ydist);
                //return (float)Math.Sqrt((x - this.x) * (x - this.x) + (y - this.y) * (y - this.y));
            }
        }
        private double[,] Voronio()
        {
            double[,] result = new double[size, size];
            _featurePoints = new List<FeaturePoint>();
            int cellsPerRow = 2;
            int cellSize = (size - 1) / cellsPerRow;
            for (int x = 0; x < cellsPerRow; x++)
            {
                for (int y = 0; y < cellsPerRow; y++)
                {
                    _featurePoints.Add(new FeaturePoint(cellSize * x + (int)(cellSize * (float)_random.NextDouble()), cellSize * y + (int)(cellSize * (float)_random.NextDouble())));
                }
            }
            double maxVal = 0;
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    double val = CalcVoronioDistance(x, y);
                    maxVal = Math.Max(maxVal, val);
                    result[x, y] = val;
                }
            }
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    result[x, y] = result[x, y] / maxVal;
                }
            }
            return result;
        }

        private double CalcVoronioDistance(int x, int y)
        {
            _featurePoints = _featurePoints.OrderBy<FeaturePoint, double>(each => each.DistanceTo(x, y, size)).ToList();
            float c1 = -1;
            float c2 = 1;
            return _featurePoints[0].DistanceTo(x, y, size) * c1 + _featurePoints[1].DistanceTo(x, y, size) * c2;
        }
    }
}