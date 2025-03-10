using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIS_labs.Classes.Styles
{
    public class LineStyle
    {
        private Pen linePen;
        public Pen LinePen { get { return linePen; } }

        public LineStyle(Pen pen)
        { this.linePen = pen; }

        public LineStyle()
        { linePen = new Pen(Color.Red, 3); }
    }
}
