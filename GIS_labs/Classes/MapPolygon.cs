using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GIS_labs.Classes.Styles;

namespace GIS_labs.Classes
{
    public class MapPolygon : MapObject
    {
        private List<MapPoint> pointsCollection;
        public List<MapPoint> PointsCollection
        {
            get { return pointsCollection; }
            set { pointsCollection = value; }
        }

        public MapPolygon(List<MapPoint> points)
        { PointsCollection = points; }

        private PolygonStyle polygonStyle { get { return this.Layer.Style.PolygonStyle; } }
        public override SelectionPriority Priority => SelectionPriority.Polygon;

        public void AddPoint(MapPoint point)
        { PointsCollection.Add(point); }

        public override bool IsHit(PointF screenPoint, double tolerance)
        { return IsHitInside(screenPoint, tolerance) || IsHitLine(screenPoint, tolerance); }

        public bool IsHitInside(PointF screenPoint, double tolerance)
        {
            List<PointF> screenPoints = PointsCollection.Select(p => Layer.Map.ConvertMapToScreen(p)).ToList();
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddPolygon(screenPoints.ToArray());
                return path.IsVisible(screenPoint);
            }
        }

        public bool IsHitLine(PointF screenPoint, double tolerance)
        {
            if (PointsCollection == null || PointsCollection.Count < 2)
                return false;

            double minDistance = tolerance * 10;

            // Итерация по каждому сегменту "кривой"
            for (int i = 0; i < PointsCollection.Count; i++)
            {
                PointF p1Screen = Layer.Map.ConvertMapToScreen(PointsCollection[i]);
                PointF p2Screen = Layer.Map.ConvertMapToScreen(PointsCollection[(i + 1) % PointsCollection.Count]);

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
            int n = PointsCollection.Count;

            for (int i = 0; i < n; i++)
            {
                double dx = PointsCollection[i].X - PointsCollection[(i + 1) % n].X;
                double dy = PointsCollection[i].Y - PointsCollection[(i + 1) % n].Y;

                length += Math.Sqrt(dx * dx + dy * dy);
            }

            return length;
        }

        public override double CalculateArea()
        {
            double area = 0;
            int n = PointsCollection.Count;

            if (n < 3) return 0;

            for (int i = 0; i < n; i++)
            {
                double x1 = PointsCollection[i].X;
                double y1 = PointsCollection[i].Y;
                double x2 = PointsCollection[(i + 1) % n].X; 
                double y2 = PointsCollection[(i + 1) % n].Y;

                area += (x1 * y2) - (x2 * y1);
            }

            return Math.Abs(area) / 2.0;
        }


        public override void DrawSelf(PaintEventArgs e)
        {
            if (pointsCollection != null && pointsCollection.Count >= 3)
            {
                PointF[] points = pointsCollection.Select(p => Layer.Map.ConvertMapToScreen(p)).ToArray();
                SolidBrush areaBrush = IsSelected ? new SolidBrush(Color.YellowGreen) : polygonStyle.AreaBrush;
                Pen borderPen = IsSelected ? new Pen(Color.Brown, 3) : polygonStyle.BorderPen;

                e.Graphics.FillPolygon(areaBrush, points);
                e.Graphics.DrawPolygon(borderPen, points);

                if (IsSelected)
                {
                    areaBrush.Dispose();
                    borderPen.Dispose();
                }
            }
        }
    }
}
