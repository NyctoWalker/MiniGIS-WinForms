using GIS_labs.Classes.Styles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GIS_labs.Classes
{
    public class MapMultiLine : MapObject
    {
        private List<MapPoint> pointsCollection;
        public List<MapPoint> PointsCollection
        {
            get { return pointsCollection; }
            set { pointsCollection = value; }
        }

        private LineStyle lineStyle { get { return this.Layer.Style.LineStyle; } }
        public override SelectionPriority Priority => SelectionPriority.Multiline;

        public MapMultiLine(List<MapPoint> points)
        { PointsCollection = points; }

        public void AddPoint(MapPoint point)
        { PointsCollection.Add(point); }

        public override bool IsHit(PointF screenPoint, double tolerance)
        {
            if (PointsCollection == null || PointsCollection.Count < 2)
                return false;

            double minDistance = tolerance * 10;

            // Итерация по каждому сегменту "кривой"
            for (int i = 0; i < PointsCollection.Count - 1; i++)
            {
                PointF p1Screen = Layer.Map.ConvertMapToScreen(PointsCollection[i]);
                PointF p2Screen = Layer.Map.ConvertMapToScreen(PointsCollection[i + 1]);

                double distance = DistanceFromPointToLine(screenPoint, p1Screen, p2Screen);
                minDistance = distance < minDistance ? distance : minDistance;
            }

            if (minDistance <= tolerance)
                return true;

            return false;
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
            double length = 0;

            for (int i = 0; i < PointsCollection.Count - 1; i++)
            {
                double dx = PointsCollection[i].X - PointsCollection[i + 1].X;
                double dy = PointsCollection[i].Y - PointsCollection[i + 1].Y;

                length += Math.Sqrt(dx * dx + dy * dy);
            }

            return length;
        }

        public override void DrawSelf(PaintEventArgs e)
        {
            if (pointsCollection != null && pointsCollection.Count >= 2)
            {
                Pen pen = IsSelected ? new Pen(Color.MediumPurple, 3) : lineStyle.LinePen;

                PointF[] points = pointsCollection.Select(p => Layer.Map.ConvertMapToScreen(p)).ToArray();
                e.Graphics.DrawLines(pen, points);

                if (IsSelected)
                    pen.Dispose();
            }
        }
    }
}
