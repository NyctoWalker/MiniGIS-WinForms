using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GIS_labs.Classes
{
    public class MapLayer
    {
        public string Name { get; set; }
        public bool IsVisible { get; set; } = true;
        private List<MapObject> objects { get; set; } = new List<MapObject>();
        public List<MapObject> Objects { get { return objects; } }

        public Map Map { get; set; } = null;

        private Style style = new();
        public Style Style
        {
            get { return style; }
            set { style = value; }
        }

        public MapLayer(List<MapObject> objectsList, string name="Без имени", bool isvisible = true)
        {
            Name = name;
            foreach (MapObject obj in objectsList)
            { obj.Layer = this; }
            objects = objectsList;
            IsVisible = isvisible;
        }

        public MapLayer(string name="Без имени", bool isvisible = true)
        {
            Name = name;
            objects = new List<MapObject>();
            IsVisible = isvisible;
        }

        public void AddMapObject(MapObject mapObject)
        {
            mapObject.Layer = this;
            objects.Add(mapObject); 
        }

        public void DeleteMapObject(MapObject selectedObject)
        { objects.Remove(selectedObject); }

        public void DrawObjects(PaintEventArgs e)
        {
            foreach (MapObject o in Objects)
            {
                o.DrawSelf(e);
            }
        }
    }
}
