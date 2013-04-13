using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace WpfDrawing
{
    public class DrawHost:FrameworkElement
    {
        VisualCollection visuals = null;
        Editor editor;
        public DrawHost()
        {
            visuals = new VisualCollection(this);
        }
        protected override Visual GetVisualChild(int index)
        {
            return visuals[index];
        }
        protected override int VisualChildrenCount
        {
            get
            {
                return visuals.Count;
            }
        }
        public void Add(Visual v)
        {
            visuals.Add(v);
        }
        public void Remove(Visual v)
        {
            visuals.Remove(v);
        }
        public Editor CurrentEditor
        {
            get { return editor; }
            set
            {
                if (editor != null)
                    editor.Deinit(this);
                editor = value;
                if(editor!=null)
                    editor.Init(this);
            }
        }
        public void clear()
        {
            visuals.Clear();
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(Brushes.Transparent, null, new Rect(this.RenderSize));
            base.OnRender(drawingContext);
        }
        public List<Point> getPoints()
        {
            return editor.get_Points();
        }
    }
}
