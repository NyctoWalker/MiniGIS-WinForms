using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GIS_labs.Classes
{
    public abstract class MapObject
    {
        public MapLayer Layer { get; set; } = null;
        public enum SelectionPriority
        {
            Point,
            Text,
            Line,
            Multiline,
            Polygon,
            None
        }

        public abstract void DrawSelf(PaintEventArgs e);

        public abstract SelectionPriority Priority { get; }
        public abstract bool IsHit(PointF screenPoint, double tolerance);
        public bool IsSelected { get; set; }

        public virtual double CalculateLength() { return 0d; }
        public virtual double CalculateArea() { return 0d; }
    }
}
