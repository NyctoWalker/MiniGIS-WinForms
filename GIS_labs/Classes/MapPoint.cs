using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GIS_labs.Classes.Styles;

namespace GIS_labs.Classes
{
    public class MapPoint : MapObject
    {
        private double x;
        public double X
        {
            get { return x; }
            set { x = value; }
        }

        private double y;
        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        private PointStyle pointStyle { get { return this.Layer.Style.PointStyle; } }
        public override SelectionPriority Priority => SelectionPriority.Point;

        public MapPoint(double x, double y) { X = x; Y = y; }

        public override bool IsHit(PointF screenPoint, double tolerance)
        {
            PointF objScreenPoint = Layer.Map.ConvertMapToScreen(new MapPoint(X, Y));
            float dx = screenPoint.X - objScreenPoint.X;
            float dy = screenPoint.Y - objScreenPoint.Y;
            double distance = Math.Sqrt(dx * dx + dy * dy);
            return distance <= tolerance; //*Layer.Map.MapScale
        }

        public override void DrawSelf(PaintEventArgs e)
        {
            Pen pen = IsSelected ? new Pen(Color.MediumPurple, 3) : pointStyle.PointPen;
            PointF point = Layer.Map.ConvertMapToScreen(this);
            e.Graphics.DrawEllipse(pen, 
                                   point.X-pen.Width/2, point.Y-pen.Width/2,
                                   pen.Width, pen.Width);
            if (IsSelected)
                pen.Dispose();
        }
    }
}
