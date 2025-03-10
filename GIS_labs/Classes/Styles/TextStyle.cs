using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIS_labs.Classes.Styles
{
    public class TextStyle
    {
        private Font font;
        public Font Font {get { return font; }}

        private SolidBrush textBrush;
        public SolidBrush TextBrush { get { return textBrush; } }

        public TextStyle(Font font, SolidBrush brush)
        {
            this.font = font;
            this.textBrush = brush;
        }

        public TextStyle()
        {
            font = new Font(new FontFamily("Wingdings"), 18);
            textBrush = new SolidBrush(Color.DarkBlue);
        }
    }
}
