using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace WpfDrawing
{
    public abstract class Editor
    {
        public abstract void Init(DrawHost dh);
        public abstract void Deinit(DrawHost dh);
        public abstract List<Point> get_Points();

    }
    public class PointEditor : Editor
    {
        DrawHost drawhost;
        List<Point> points;//тоже много точек, которые сами добавляли
        List<Point> points_toget;//те же points, но не удаляются по завершению рисования
        PointVisual fossil;
        Pen pen;
        public PointEditor(Pen pen1)
        {
            pen = pen1;
        }
        public override void Init(DrawHost dh)
        {
            drawhost = dh;
            drawhost.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(drawhost_MouseLeftButtonDown);
        }
        

        void drawhost_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (points == null)
            {
                points = new List<Point>();
                fossil = new PointVisual(pen);
                drawhost.Add(fossil);
                
            }
            points.Add(e.GetPosition(drawhost));
            fossil.Points = points;
            points_toget = points;
            fossil.Redraw();
        }

        

        public override void Deinit(DrawHost dh)
        {
            drawhost.MouseLeftButtonDown -= drawhost_MouseLeftButtonDown;
            points = null;
        }
        public override List<Point> get_Points()
        {
            return points_toget;
        }
    }
    public class ScissorEditor : Editor
    {
        DrawHost drawhost;
        List<Point> points;//много точек
        List<Point> sub_points;
        List<Point> points_toget;
        FossilVisual fossil;
        Graph gr;//наш граф
        Point seed;
       
        public override void Init(DrawHost dh)
        {
            drawhost = dh;
            drawhost.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(drawhost_MouseLeftButtonDown);

            drawhost.MouseRightButtonDown += new System.Windows.Input.MouseButtonEventHandler(drawhost_MouseRightButtonDown);

            
        }
        public ScissorEditor(byte[,] bites)
        {
           
            gr = new Graph(bites);
            //в создание графа входит: подсчет градиента изображения, нахождение максимума градиента, подсчет Лапласиана
            
        }
        void drawhost_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //points_toget = points;
            drawhost.MouseMove -= drawhost_MouseMove;
            points_toget = points;
            points = null;
            fossil = null;
        }

        void drawhost_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (points != null)
            {
                
                if (Math.Abs(seed.X - e.GetPosition(drawhost).X) >= 49 || Math.Abs(seed.Y - e.GetPosition(drawhost).Y) >= 49)
                {
                    //seed = e.GetPosition(drawhost);
                    set_seed(e.GetPosition(drawhost));//если вышли за пределы расчетов
                    unit(points, sub_points);
                }
                else
                    sub_points = gr.return_path((int)e.GetPosition(drawhost).X, (int)e.GetPosition(drawhost).Y);
                List<Point> pts = concat(points, sub_points);

                fossil.Points = pts;
                fossil.Redraw();
            }
            
        }
        void set_seed(Point x)
        {
            //здесь нужно установить seed и посчитать пути
            seed = x;
            gr.set_seed((int)x.X, (int)x.Y);
        }
        void drawhost_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (points == null)
            {
                points = new List<Point>();
                sub_points = new List<Point>();
                drawhost.MouseMove += new System.Windows.Input.MouseEventHandler(drawhost_MouseMove);
                fossil = new FossilVisual();
                drawhost.Add(fossil);
            }

            //отмасштабировали
            //gr.set_seed((int)(e.GetPosition(drawhost).X), (int)(e.GetPosition(drawhost).Y ));//можно добавить масштабирование
            //sub_points.Add(e.GetPosition(drawhost));//чтобы эта точка точно тут была

            unit(points, sub_points);//добавили все точки промежуточные,которые рисовались до этого момента
            points.Add(e.GetPosition(drawhost));//чтобы точно попала
            //seed = e.GetPosition(drawhost);
            set_seed(e.GetPosition(drawhost));
            
            fossil.Points = points;
         
            fossil.Redraw();
        }
        /// <summary>
        /// объединение двух листов точек: постоянных и промежуточных в другой лист-промежуточное рисование
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <returns></returns>
        private List<Point> concat(List<Point> list1, List<Point> list2)
        {
            List<Point> list3 = new List<Point>();
            foreach (Point p in list1)
                list3.Add(p);
            foreach (Point p in list2)
                list3.Add(p);
            return list3;
        }
        /// <summary>
        /// будет выполняться, когда в постоянные точки нужно добавить
        ///еще точки из временного листа на постоянное место жительства
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        private void unit(List<Point> list1, List<Point> list2)
        {
            if (list2.Count>0)
            foreach (Point p in list2)
                list1.Add(p);
            
        }
        public override void Deinit(DrawHost dh)
        {
            drawhost.MouseLeftButtonDown -= drawhost_MouseLeftButtonDown;

            drawhost.MouseRightButtonDown -= drawhost_MouseRightButtonDown;
        }

        
        public override List<Point> get_Points()
        {
            return points_toget;
        }
       
    }
    /// <summary>
    /// рисование прямоугольника
    /// </summary>
    public class RectEditor : Editor
    {
        DrawHost drawhost;
        List<Point> points;//2 точки прямоугольника
        List<Point> points_toget;//те же points, но не удаляются по завершению рисования
        FossilVisual fossil;
        public override void Init(DrawHost dh)
        {
            drawhost = dh;
            drawhost.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(drawhost_MouseLeftButtonDown);
        }

        void drawhost_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            drawhost.MouseMove -= drawhost_MouseMove;
            points_toget = points;
            points = null;
            fossil = null;
        }

        void drawhost_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (points != null && points.Count > 1)
            {
                points[points.Count - 1] = e.GetPosition(drawhost);
                fossil.Points = calcRectPoints();

                fossil.Redraw();
            }
        }

        void drawhost_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (points != null)
            {
                points_toget = points;
                points = null;
                fossil = null;
                drawhost.MouseMove -= drawhost_MouseMove;
                return;
            }
            else
            {
                points_toget = null;
                points = new List<Point>();
                points.Add(e.GetPosition(drawhost));
                drawhost.MouseMove += new System.Windows.Input.MouseEventHandler(drawhost_MouseMove);
                fossil = new FossilVisual();
                drawhost.Add(fossil);
            }

            points.Add(e.GetPosition(drawhost));


            fossil.Points = calcRectPoints();
            fossil.Redraw();
        }

        public override void Deinit(DrawHost dh)
        {
            drawhost.MouseLeftButtonDown -= drawhost_MouseLeftButtonDown;


        }
        private List<Point> calcRectPoints()
        {
            List<Point> pts = new List<Point>();
            pts.Add(points[0]);
            pts.Add(new Point(points[1].X, points[0].Y));
            pts.Add(points[1]);
            pts.Add(new Point(points[0].X, points[1].Y));
            return pts;
        }
        public override List<Point> get_Points()
        {
            return points_toget;
            
        }
    }
    public class SplineEditor : Editor//сплайнами
    {
        DrawHost drawhost;
        List<Point> points;//много точек
        List<Point> points_toget;//те же points, но не удаляются по завершению рисования
        FossilVisual fossil;
        public override void Init(DrawHost dh)
        {
            drawhost = dh;
            drawhost.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(drawhost_MouseLeftButtonDown);
            
            drawhost.MouseRightButtonDown += new System.Windows.Input.MouseButtonEventHandler(drawhost_MouseRightButtonDown);
        }

        void drawhost_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //points_toget = points;
            drawhost.MouseMove -= drawhost_MouseMove;
            points = null;
            fossil = null;
        }

        void drawhost_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (points != null && points.Count > 1)
            {
                points[points.Count - 1] = e.GetPosition(drawhost);
                List<Point> pts = CalcSpline(points);
                points_toget = pts;
                fossil.Points = pts;
                fossil.Redraw();
            }
        }

        void drawhost_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (points == null)
            {
                points = new List<Point>();
                points.Add(e.GetPosition(drawhost));
                drawhost.MouseMove += new System.Windows.Input.MouseEventHandler(drawhost_MouseMove);
                fossil = new FossilVisual();
                drawhost.Add(fossil);
            }
            points.Add(e.GetPosition(drawhost));
            List<Point> pts = CalcSpline(points);
            fossil.Points = pts;
            points_toget = pts;
            fossil.Redraw();
        }

        public override void Deinit(DrawHost dh)
        {
            drawhost.MouseLeftButtonDown -= drawhost_MouseLeftButtonDown;
           
            drawhost.MouseRightButtonDown -= drawhost_MouseRightButtonDown;
        }

        private static List<Point> cardinalSpline(Point p0, Point p1, Point p2, Point p3)
        {
            double t, t2, t3;
            double step = 0.05;
            double mx_1, my_1, mx_2, my_2;
            double h = p2.X - p1.X;
            mx_1 = (p2.X - p0.X) / 2;
            mx_2 = (p3.X - p1.X) / 2;
            my_1 = (p2.Y - p0.Y) / 2;
            my_2 = (p3.Y - p1.Y) / 2;

            List<Point> points = new List<Point>();
            if (p1.X == p2.X && p1.Y == p2.Y)
                return points;
            double diag = Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y));
            //if(diag 
            if (diag > 1)
                step = 1 / diag;
            //if (diag > 10)
            //    step = 1 / diag;
            for (t = 0.0; t <= 1.0; t += step)
            {
                Point p = new Point();
                t2 = t * t;
                t3 = t2 * t;

                p.X = (2 * t3 - 3 * t2 + 1) * p1.X +
                    (t3 - 2 * t2 + t) * mx_1 +
                    (-2 * t3 + 3 * t2) * p2.X +
                    (t3 - t2) * mx_2;
                p.Y = (2 * t3 - 3 * t2 + 1) * p1.Y +
                    (t3 - 2 * t2 + t) * my_1 +
                    (-2 * t3 + 3 * t2) * p2.Y +
                    (t3 - t2) * my_2;
                points.Add(p);
            }
            
            return points;
        }
        /// <summary>
        /// Построение сплайна проходящего через заданные точки.
        /// </summary>
        /// <param name="points">Контрольные точки.</param>
        /// <returns>Точки сплайна.</returns>
        private static List<Point> CalcSpline(List<Point> points)
        {
            List<Point> tmp;
            int n = points.Count;
            List<Point> SplinePoints = new List<Point>();
            for (int i = 0; i < n - 1; i++)
            {
                if (i == 0)
                {
                    if (n > 2)
                        tmp = cardinalSpline(points[n - 1], points[i], points[i + 1], points[i + 2]);
                    else
                        tmp = cardinalSpline(points[n - 1], points[i], points[i + 1], points[i + 1]);
                }
                else
                {
                    if (i < n - 2)
                        tmp = cardinalSpline(points[i - 1], points[i], points[i + 1], points[i + 2]);
                    else
                        tmp = cardinalSpline(points[i - 1], points[i], points[i + 1], points[0]);
                }

                SplinePoints.AddRange(tmp);
            }
            if (n > 1)
            {
                tmp = cardinalSpline(points[n - 2], points[n - 1], points[0], points[1]);
                SplinePoints.AddRange(tmp);
            }
            return SplinePoints;

        }
        public override List<Point> get_Points()
        {
            return points_toget;
        }
    }
    public class FillEditor : Editor//отмечание фона и объекта
    {
        DrawHost drawhost;
        List<Point> points;//тоже много точек, которые сами добавляли
        List<Point> points_toget;//те же points, но не удаляются по завершению рисования
        AreaVisual area;
        Pen pen;
        public FillEditor(Pen pen1)
        {
            pen = pen1;
        }
        public override void Init(DrawHost dh)
        {
            drawhost = dh;
            drawhost.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(drawhost_MouseLeftButtonDown);
        }
        void drawhost_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (points != null && points.Count > 0)
            {
                Point p = e.GetPosition(drawhost);
                //points_toget.Add(p);
                if (Math.Sqrt((p.X - points[points.Count - 1].X) * (p.X - points[points.Count - 1].X) + (p.Y - points[points.Count - 1].Y) * (p.Y - points[points.Count - 1].Y)) > 1)
                {
                    points.Add(p);
                    area.Redraw();
                }
            }
        }

        void drawhost_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (points == null)
            {
                drawhost.MouseMove += new System.Windows.Input.MouseEventHandler(drawhost_MouseMove);
                drawhost.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(drawhost_MouseLeftButtonUp);
                points = new List<Point>();
                points.Add(e.GetPosition(drawhost));
                points.Add(e.GetPosition(drawhost));
                Brush br = Brushes.LightGreen;
                area = new AreaVisual(pen);
                drawhost.Add(area);
                area.Points = points;
                points_toget = points;
                area.Redraw();
            }
        }

        void drawhost_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //points_toget = points;
            points = null;
            area = null;
            drawhost.MouseLeftButtonUp -= drawhost_MouseLeftButtonUp;
            drawhost.MouseMove -= drawhost_MouseMove;
            return;
        }

        public override void Deinit(DrawHost dh)
        {
            drawhost.MouseLeftButtonDown -= drawhost_MouseLeftButtonDown;
        }
        public override List<Point> get_Points()
        {
            return points_toget;
        }
    }
   

}
