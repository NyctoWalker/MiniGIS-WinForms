using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GIS_labs.Classes.Styles;

namespace GIS_labs.Classes
{
    public class Style
    {
        private TextStyle textStyle = new();
        public TextStyle TextStyle
        {
            get { return textStyle; }
            set { textStyle = value; }
        }

        private PointStyle pointStyle = new();
        public PointStyle PointStyle
        {
            get { return pointStyle; }
            set { pointStyle = value; }
        }

        private LineStyle lineStyle = new();
        public LineStyle LineStyle
        {
            get { return lineStyle; }
            set { lineStyle = value; }
        }

        private PolygonStyle polygonStyle = new();
        public PolygonStyle PolygonStyle
        {
            get { return polygonStyle; }
            set { polygonStyle = value; }
        }

        public Style() { }
    }
}
