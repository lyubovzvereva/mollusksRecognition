using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;
using System.Collections.ObjectModel;

namespace WpfDrawing
{
    public class FossilVisual:DrawingVisual
    {
        List<Point> points;
        Pen pen;
        public FossilVisual()
        {
            pen = new Pen(Brushes.SpringGreen, 2);
            pen.Freeze();
            Redraw();
        }

       
        public void Redraw()
        {
            if (points!=null && points.Count > 0)
            {
                DrawingContext dc = this.RenderOpen();
                Draw(dc);
                dc.Close();
            }
        }

        private void Draw(DrawingContext dc)
        {
            StreamGeometry sg = new StreamGeometry();
            StreamGeometryContext sgc = sg.Open();
            sgc.BeginFigure(points[0], false, true);
            sgc.PolyLineTo(points, true, true);
            sgc.Close();
            dc.DrawGeometry(null, pen, sg);
        }
        public List<Point> Points
        {
            get { return points; }
            set { points = value; }
        }

    }
    public class AreaVisual : DrawingVisual
    {
        List<Point> points;
        Pen pen;
        public AreaVisual(Pen pen1)
        {
           // pen = new Pen(Brushes.SpringGreen, 10);
            pen = pen1;
            pen.Freeze();
            Redraw();
        }


        public void Redraw()
        {
            if (points != null && points.Count > 0)
            {
                DrawingContext dc = this.RenderOpen();
                Draw(dc);

                dc.Close();
            }
        }

        private void Draw(DrawingContext dc)
        {
            StreamGeometry sg = new StreamGeometry();
            StreamGeometryContext sgc = sg.Open();
            sgc.BeginFigure(points[0], false, false);
            sgc.PolyLineTo(points, true, true);
            sgc.Close();
            dc.DrawGeometry(null, pen, sg);
        }
        public List<Point> Points
        {
            get { return points; }
            set { points = value; }
        }

    }
    public class PointVisual : DrawingVisual
    {
        List<Point> points;
        Pen pen;
        public PointVisual(Pen pen1)
        {
            pen = pen1;
            pen.Freeze();
            Redraw();
        }


        public void Redraw()
        {
            if (points != null && points.Count > 0)
            {
                DrawingContext dc = this.RenderOpen();
                Draw(dc);

                dc.Close();
            }
        }

        private void Draw(DrawingContext dc)
        { 
            Brush brush = new SolidColorBrush(Colors.Indigo);
            foreach (Point point in points)
                dc.DrawEllipse(brush,pen,point,2,2);
           
        }
        public List<Point> Points
        {
            get { return points; }
            set { points = value; }
        }

    }
}
