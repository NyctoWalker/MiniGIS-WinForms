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
    public class MapLine : MapObject
    {
        private MapPoint p1;
        public MapPoint P1
        {
            get {return p1; }
            set { p1 = value; }
        }

        private MapPoint p2;
        public MapPoint P2
        {
            get { return p2; }
            set { p2 = value; }
        }

        private LineStyle lineStyle { get { return this.Layer.Style.LineStyle; } }
        public override SelectionPriority Priority => SelectionPriority.Line;

        public MapLine(MapPoint p1, MapPoint p2)
        {
            P1 = p1;
            P2 = p2;
        }

        public override bool IsHit(PointF screenPoint, double tolerance)
        {
            PointF p1Screen = Layer.Map.ConvertMapToScreen(P1);
            PointF p2Screen = Layer.Map.ConvertMapToScreen(P2);
            double distance = DistanceFromPointToLine(screenPoint, p1Screen, p2Screen);
            return distance <= tolerance;
        }

        private double DistanceFromPointToLine(PointF point, PointF lineStart, PointF lineEnd)
        {
            float A = lineEnd.Y - lineStart.Y; // dY
            float B = lineStart.X - lineEnd.X; // -dX
            float C = (lineEnd.X * lineStart.Y) - (lineStart.X * lineEnd.Y); // x2*y1 - x1*y2

            double distance = Math.Abs(A * point.X + B * point.Y + C) / (float)Math.Sqrt(A * A + B * B);

            // Проверка, попадает ли перпендикуляр в граничные точки линии
            float dotProduct1 = (point.X - lineStart.X) * (lineEnd.X - lineStart.X) +
                                (point.Y - lineStart.Y) * (lineEnd.Y - lineStart.Y);
            float dotProduct2 = (point.X - lineEnd.X) * (lineStart.X - lineEnd.X) +
                                (point.Y - lineEnd.Y) * (lineStart.Y - lineEnd.Y);

            if (dotProduct1 >= 0 && dotProduct2 >= 0)
            {
                // Попадание на линию
                return distance;
            }
            else
            {
                // Возвращаем минимальное расстояния до одного из концов линии
                float distanceToStart = (float)Math.Sqrt(Math.Pow(point.X - lineStart.X, 2) + Math.Pow(point.Y - lineStart.Y, 2));
                float distanceToEnd = (float)Math.Sqrt(Math.Pow(point.X - lineEnd.X, 2) + Math.Pow(point.Y - lineEnd.Y, 2));
                return Math.Min(distanceToStart, distanceToEnd);
            }
        }

        public override double CalculateLength()
        {
            double dx = P1.X - P2.X;
            double dy = P1.Y - P2.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public override void DrawSelf(PaintEventArgs e)
        {
            Pen pen = IsSelected ? new Pen(Color.MediumPurple, 3) : lineStyle.LinePen;
            e.Graphics.DrawLine(pen, 
                                Layer.Map.ConvertMapToScreen(p1), 
                                Layer.Map.ConvertMapToScreen(p2));
            if (IsSelected)
                pen.Dispose();
        }
    }
}
