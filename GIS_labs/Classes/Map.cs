using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GIS_labs.Classes
{
    public partial class Map : UserControl
    {
        private List<MapLayer> layers { get; set; } = new List<MapLayer>();
        public List<MapLayer> Layers { get { return layers; } }

        public MapPoint CenterPoint { get; set; } = new(0.0, 0.0);
        public double MapScale { get; set; } = 1.0;

        public Map(List<MapLayer> objectsList)
        { layers = objectsList; }

        public Map() { InitializeComponent(); }

        public PointF ConvertMapToScreen(MapPoint point)
        {
            double dX = point.X - CenterPoint.X;
            double dY = point.Y - CenterPoint.Y;
            double new_X = (dX * MapScale) + (Width / 2);
            double new_Y = (Height / 2) - (dY * MapScale);

            return new PointF((float)new_X, (float)new_Y);
        }

        public MapPoint ConvertScreenToMap(PointF point)
        {
            double dX = (point.X - Width / 2d) / MapScale;
            double dY = (Height / 2d - point.Y) / MapScale;
            double cX = dX + CenterPoint.X;
            double cY = dY + CenterPoint.Y;

            return new MapPoint(cX, cY);
        }

        public void AddMapLayers(MapLayer mapLayer)
        { 
            layers.Add(mapLayer);
            mapLayer.Map = this;
        }

        public void SetMapLayers(List<MapLayer> layers) 
        { 
            this.layers = layers;
            foreach (MapLayer layer in layers)
                layer.Map = this;
        }

        public void DeleteMapObject(MapLayer selectedLayer)
        { layers.Remove(selectedLayer); }

        public int MoveObjectUp(MapLayer selectedLayer)
        {
            int index = layers.IndexOf(selectedLayer);
            if (layers == null || index < 0 || index >= layers.Count - 1)
                return 0;

            layers[index] = layers[index + 1];
            layers[index + 1] = selectedLayer;
            return 1;
        }

        public int MoveObjectDown(MapLayer selectedLayer)
        {
            int index = layers.IndexOf(selectedLayer);
            if (layers == null || index <= 0 || index > layers.Count - 1)
                return 0;

            layers[index] = layers[index - 1];
            layers[index - 1] = selectedLayer;
            return -1;
        }

        public void DrawLayers(PaintEventArgs e)
        {
            foreach (MapLayer l in layers)
            {
                if (l.IsVisible)
                    l.DrawObjects(e);
            }
        }

        public void Map_Paint(object sender, PaintEventArgs e)
        {
            DrawLayers(e);
        }
    }
}
