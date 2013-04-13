using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;

namespace WpfDrawing
{
    class Parametrs
    {
        public double wid;
        public double heid;
        public double maxx;
        public double minx;
        public double maxy;
        public double miny;

        public Parametrs()
        {
        }

        public double scale(PointCollection mypoints)//сделали ширину 100. а высоту трогать не надо
        {
            maxx = mypoints.Max(pet => pet.X);
            maxy = mypoints.Max(pet => pet.Y);
            minx = mypoints.Min(pet => pet.X);
            miny = mypoints.Min(pet => pet.Y);
            double shir = maxx - minx;
            double vis = maxy - miny;
            wid = shir;
            heid = vis;
            for (int i = 0; i < mypoints.Count; i++)
            {
                //mypoints[i] = new Point(((mypoints[i].X - minx) * 100 / shir) + minx, ((mypoints[i].Y - miny) * 100 / vis) + miny);
                mypoints[i] = new Point(((mypoints[i].X - minx) * 100 / shir) + minx, mypoints[i].Y );

            }
            double sq = square(mypoints);
            return sq;
        }
       
        public double square(PointCollection pol)//считаем площадь полигона
        {
            double s = 0;
            double res = 0;
            double sq = 0;
            int n = pol.Count;//кол-во вершин
            for (int i = 0; i < n; i++)
            {
                if (i == 0)
                {
                    s = pol[i].X * (pol[n - 1].Y - pol[i + 1].Y); //если i == 0, то y[i-1] заменяем на y[n-1]
                    res += s;
                }
                else
                    if (i == n - 1)
                    {
                        s = pol[i].X * (pol[i - 1].Y - pol[0].Y); // если i == n-1, то y[i+1] заменяем на y[0]
                        res += s;
                    }
                    else
                    {
                        s = pol[i].X * (pol[i - 1].Y - pol[i + 1].Y);
                        res += s;
                    }
            }

            sq = Math.Abs(res / 2);

            return sq;
        }
        public double stripes(List<Point> mypoints)//частота точек
        {
            if (mypoints.Count > 1)
            {
                double maxx = mypoints.Max(pet => pet.X);
                double maxy = mypoints.Max(pet => pet.Y);
                double minx = mypoints.Min(pet => pet.X);
                double miny = mypoints.Min(pet => pet.Y);
                double shir = maxx - minx;
                double vis = maxy - miny;
                double freq = mypoints.Count / (Math.Sqrt(shir * shir + vis * vis));
                return freq;
            }

            return 0;
        }
    }
}
