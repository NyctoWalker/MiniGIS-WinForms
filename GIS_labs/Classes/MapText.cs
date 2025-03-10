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
    public class MapText : MapObject
    {
        private MapPoint origin;
        public MapPoint Origin 
        {
            get { return origin; }
            set { origin = value; }
        }

        private TextStyle textStyle { get { return this.Layer.Style.TextStyle; } }
        public override SelectionPriority Priority => SelectionPriority.Text;

        private string text;
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public MapText(MapPoint origin, string text)
        {
            this.Origin = origin;
            this.Text = text;
        }

        public override bool IsHit(PointF screenPoint, double tolerance)
        {
            PointF objScreenPoint = Layer.Map.ConvertMapToScreen(new MapPoint(origin.X, origin.Y));
            float dx = screenPoint.X - objScreenPoint.X;
            float dy = screenPoint.Y - objScreenPoint.Y;
            double distance = Math.Sqrt(dx * dx + dy * dy);
            return distance <= tolerance; //* Layer.Map.MapScale
        }

        public override void DrawSelf(PaintEventArgs e)
        {
            SolidBrush brush = IsSelected ? new SolidBrush(Color.MediumPurple) : textStyle.TextBrush;
            
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(text, textStyle.Font, brush, 
                Layer.Map.ConvertMapToScreen(Origin), sf);
            
            if (IsSelected)
                brush.Dispose();
        }
    }
}
