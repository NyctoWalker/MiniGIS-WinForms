using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GIS_labs.Classes;
using Newtonsoft.Json;

namespace GIS_labs
{
    public partial class Form1 : Form
    {
        // Map map;
        bool MapMove = false;
        private PointF initialMousePosition = new PointF(0, 0);
        private MapPoint initialMapCenter = new MapPoint(0, 0);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            map.SetMapLayers(new(
                new List<MapLayer>
                {
                    new MapLayer(
                        new List<MapObject>
                        {
                            new MapMultiLine(
                                new List<MapPoint>
                                {
                                    new MapPoint(-10.0, -10.0),
                                    new MapPoint(-10.0, -20.0),
                                    new MapPoint(-80.0, -40.0),
                                    new MapPoint(-45.0, -10.0)
                                }
                            ),
                            new MapText(
                                new MapPoint(-50.0, 50.0),
                                "A"
                            )
                        },
                        "Верхний слой"
                    ),
                    new MapLayer
                    (
                        new List<MapObject>
                        {
                            new MapPoint(2.5, 10.0),
                            new MapPolygon(
                                new List<MapPoint>
                                {
                                    new MapPoint(10.0, 10.0),
                                    new MapPoint(10.0, 20.0),
                                    new MapPoint(80.0, 40.0),
                                    new MapPoint(45.0, 10.0)
                                }
                            ),
                            new MapPolygon(
                                new List<MapPoint>
                                {
                                    new MapPoint(-10.0, -10.0),
                                    new MapPoint(-10.0, -20.0),
                                    new MapPoint(-20.0, -20.0),
                                    new MapPoint(-20.0, -10.0)
                                }
                            ),
                            new MapPolygon(
                                new List<MapPoint>
                                {
                                    new MapPoint(-40.0, -40.0),
                                    new MapPoint(-40.0, -50.0),
                                    new MapPoint(-50.0, -50.0)
                                }
                            ),
                            new MapLine(new MapPoint(0.0, 50.0),
                                        new MapPoint(90.0, 150.0))
                        },
                        "Слой с именем!"
                    ),
                    new MapLayer(
                        new List<MapObject>
                        {
                            new MapPolygon(
                                new List<MapPoint>
                                {
                                    new MapPoint(0.0, 0.0),
                                    new MapPoint(50.0, 0.0),
                                    new MapPoint(50.0, 30.0),
                                    new MapPoint(15.0, 30.0),
                                    new MapPoint(15.0, 20.0),
                                    new MapPoint(25.0, 20.0),
                                    new MapPoint(25.0, 10.0),
                                    new MapPoint(0.0, 10.0),
                                }
                            ),
                        },
                        "Тестовый полигон (P180, S1100)",
                        false  // Видимость слоя
                    )
                }
            ));

            map.MouseWheel += map_MouseWheel;

            // layersList.DataSource = map.Layers;
            // layersList.CheckOnClick = true;
            layersList.DisplayMember = "Name";
            RefreshLayers();
            
        }

        private void layersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            MapLayer layer = (MapLayer)layersList.SelectedItem;
            if (layer is not null)
            {
                layerElements.DataSource = layer.Objects;
                layerElements.DisplayMember = "Priority";
            }
        }

        private void layerElements_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var layer in map.Layers)
            {
                foreach (var obj in layer.Objects)
                {
                    obj.IsSelected = false;
                }
            }

            MapObject selectedObject = layerElements.SelectedItem as MapObject;
            if (selectedObject != null)
            {
                selectedObject.IsSelected = true;
                UpdateStatsLabel(selectedObject.Priority, selectedObject);
                map.Refresh();
            }
        }

        private void buttonLayerAdd_Click(object sender, EventArgs e)
        {
            MapLayer layer = new MapLayer("Новый слой");
            map.AddMapLayers(layer);
            RefreshLayers();
            // MessageBox.Show(map.Layers.Count().ToString());
        }

        private void layersList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            MapLayer layer = (MapLayer)layersList.Items[e.Index];
            if (e.NewValue == CheckState.Checked)
            {
                map.Layers[e.Index].IsVisible = true;
            }
            else
            {
                map.Layers[e.Index].IsVisible = false;
            }
            map.Refresh();
        }

        private void RefreshLayers()
        {
            /*layersList.DataSource = null;
            layersList.DataSource = map.Layers;
            layersList.DisplayMember = "Name";
            map.Invalidate();*/

            layersList.Items.Clear();
            foreach (var layer in map.Layers)
            {
                layersList.Items.Add(layer, layer.IsVisible);
            }
            map.Refresh();
        }

        private void buttonDeleteLayer_Click(object sender, EventArgs e)
        {
            MapLayer layer = (MapLayer)layersList.SelectedItem;

            if (layer is not null)
            {
                map.DeleteMapObject(layer);
                // MessageBox.Show(layer.Name + "\n" + layer.Objects);
            }
            RefreshLayers();
        }

        private void buttonLayerDown_Click(object sender, EventArgs e)
        {
            MapLayer layer = (MapLayer)layersList.SelectedItem;
            if (layer is not null)
                { layersList.SelectedIndex += map.MoveObjectUp(layer);} // Сверху вниз
            RefreshLayers();
        }

        private void buttonLayerUp_Click(object sender, EventArgs e)
        {
            MapLayer layer = (MapLayer)layersList.SelectedItem;
            if (layer is not null)
                { layersList.SelectedIndex += map.MoveObjectDown(layer); } // Сверху вниз
            RefreshLayers();
        }

        private void map_Paint(object sender, PaintEventArgs e)
        {
            map.Map_Paint(sender, e);
        }

        private void map_MouseMove(object sender, MouseEventArgs e)
        {
            MapPoint point = map.ConvertScreenToMap(e.Location);
            coordinatesLabel.Text = $"(X: {point.X:F2}; Y: {point.Y:F2}), Масштаб: {map.MapScale:F2}";

            if (MapMove)
            {
                PointF screenDiff = new PointF(e.Location.X - initialMousePosition.X,
                                               e.Location.Y - initialMousePosition.Y);

                double dX = -screenDiff.X / map.MapScale;
                double dY = screenDiff.Y / map.MapScale;
                map.CenterPoint = new MapPoint(initialMapCenter.X + dX, initialMapCenter.Y + dY);
                map.Refresh();
            }
        }

        private void map_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                MapMove = true;
                initialMousePosition = e.Location;
                initialMapCenter = new MapPoint(map.CenterPoint.X, map.CenterPoint.Y);
            }
            else if (e.Button == MouseButtons.Left)
            {
                HandleSelection(e.Location);
            }
        }

        private void map_MouseUp(object sender, MouseEventArgs e)
        {
            MapMove = false;
        }

        private void map_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                map.MapScale *= 1.1;
            else
                map.MapScale /= 1.1;

            MapPoint point = map.ConvertScreenToMap(e.Location);
            coordinatesLabel.Text = $"(X: {point.X:F2}; Y: {point.Y:F2}), Масштаб: {map.MapScale:F2}";

            map.Refresh();
        }

        private void HandleSelection(Point screenPoint)
        {
            const double baseTolerance = 7f; // Радиус выбора со стандартным масштабом
            // double tolerance = baseTolerance / map.MapScale;
            double tolerance = baseTolerance;
            List<MapObject> candidates = new List<MapObject>();

            // Проверка слоёв сверху вниз, т.е. сначала заполняем из верхних слоёв
            foreach (var layer in map.Layers.AsEnumerable().Reverse())
            {
                if (!layer.IsVisible) continue;

                foreach (var obj in layer.Objects.AsEnumerable().Reverse())
                {
                    if (obj.IsHit(screenPoint, tolerance) && !obj.IsSelected)
                        candidates.Add(obj);
                }
            }

            // Определение наивысшего приоритета (Точка приоритетнее полигона)
            MapObject.SelectionPriority highestPriority = MapObject.SelectionPriority.Polygon;
            foreach (var candidate in candidates)
            {
                if (candidate.Priority < highestPriority)
                    highestPriority = candidate.Priority;
            }

            // Выбор первого кандидата в порядке добавления с наивысшим приоритетом
            MapObject selectedObject = null;
            foreach (var candidate in candidates)
            {
                if (candidate.Priority == highestPriority)
                {
                    selectedObject = candidate;
                    break;
                }
            }

            // Очищаем старые выбранные объекты и устанавливаем новый
            foreach (var layer in map.Layers)
                foreach (var obj in layer.Objects)
                    obj.IsSelected = false;

            if (selectedObject != null)
            {
                selectedObject.IsSelected = true;
                // Выбираем слой и выбранный объект слоя
                layersList.SelectedItem = selectedObject.Layer;
                layerElements.SelectedItem = selectedObject;
                UpdateStatsLabel(selectedObject.Priority, selectedObject);
            }
            else
                UpdateStatsLabel(MapObject.SelectionPriority.None, null);

            map.Refresh();
        }

        private void UpdateStatsLabel(MapObject.SelectionPriority priority, MapObject? mapObject)
        {
            if (mapObject is null)
            {
                bigObjectStatsLabel.Text = $"{priority}";
                return;
            }

            if (priority == MapObject.SelectionPriority.Polygon)
                bigObjectStatsLabel.Text = $"{priority}: Периметр {mapObject.CalculateLength():F2}; Площадь {mapObject.CalculateArea():F2}";
            else if (priority == MapObject.SelectionPriority.Multiline)
                bigObjectStatsLabel.Text = $"{priority}: Длина {mapObject.CalculateLength():F2}";
            else if (priority == MapObject.SelectionPriority.Line)
                bigObjectStatsLabel.Text = $"{priority}: Длина {mapObject.CalculateLength():F2}";
            else
                bigObjectStatsLabel.Text = $"{priority}";
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "GeoJSON файлы|*.geojson;*.json";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string json = File.ReadAllText(ofd.FileName);
                        var featureCollection = JsonConvert.DeserializeObject<GeoJSON.Net.Feature.FeatureCollection>(json);

                        MapLayer newLayer = new MapLayer();
                        newLayer.Name = Path.GetFileNameWithoutExtension(ofd.FileName);

                        var reader = new GeoJSONReader();
                        foreach (var feature in featureCollection.Features)
                        {
                            var mapObjects = reader.ConvertGeoJsonFeatureToMapObjects(feature);
                            foreach (var obj in mapObjects)
                            {
                                newLayer.AddMapObject(obj);
                            }
                        }

                        map.AddMapLayers(newLayer);
                        RefreshLayers();

                        RecalculateLayersZoom(newLayer);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка импорта GeoJSON: {ex.Message}");
                    }
                }
            }
        }

        private void RecalculateLayersZoom(MapLayer layer)
        {
            RecalculateLayersZoom(new List<MapLayer> {layer});
        }

        private void RecalculateLayersZoom(List<MapLayer> layers)
        {
            double minX = double.MaxValue;
            double maxX = double.MinValue;
            double minY = double.MaxValue;
            double maxY = double.MinValue;

            // Подсчёт Bounding Box'ов для всех слоёв
            foreach (var layer in layers)
            {
                var (layerMinX, layerMaxX, layerMinY, layerMaxY) = CalculateBoundingBox(layer);
                minX = Math.Min(minX, layerMinX);
                maxX = Math.Max(maxX, layerMaxX);
                minY = Math.Min(minY, layerMinY);
                maxY = Math.Max(maxY, layerMaxY);
            }

            // Если вышли не нашли валидных bbox'ов, ничего не делаем
            if (minX == double.MaxValue)
                return;

            // Установка центр на центр общего bbox'а
            double centerX = (minX + maxX) / 2;
            double centerY = (minY + maxY) / 2;
            map.CenterPoint = new MapPoint(centerX, centerY);

            // Рассчитываем масштаб с учётом отступа
            double width = maxX - minX;
            double height = maxY - minY;
            if (width <= 0) width = 1;
            if (height <= 0) height = 1;

            double scaleX = map.Width / width;
            double scaleY = map.Height / height;
            double newScale = Math.Min(scaleX, scaleY) * 0.9; // 10% отступ

            map.MapScale = newScale;
            map.Refresh();
        }

        private (double minX, double maxX, double minY, double maxY) CalculateBoundingBox(MapLayer layer)
        {
            double minX = double.MaxValue;
            double maxX = double.MinValue;
            double minY = double.MaxValue;
            double maxY = double.MinValue;

            foreach (var obj in layer.Objects)
            {
                switch (obj)
                {
                    case MapPoint point:
                        UpdateMinMax(point.X, point.Y, ref minX, ref maxX, ref minY, ref maxY);
                        break;
                    case MapMultiLine multiLine:
                        foreach (var p in multiLine.PointsCollection)
                            UpdateMinMax(p.X, p.Y, ref minX, ref maxX, ref minY, ref maxY);
                        break;
                    case MapPolygon polygon:
                        foreach (var p in polygon.PointsCollection)
                            UpdateMinMax(p.X, p.Y, ref minX, ref maxX, ref minY, ref maxY);
                        break;
                    case MapLine line:
                        UpdateMinMax(line.P1.X, line.P1.Y, ref minX, ref maxX, ref minY, ref maxY);
                        UpdateMinMax(line.P2.X, line.P2.Y, ref minX, ref maxX, ref minY, ref maxY);
                        break;
                    case MapText text:
                        UpdateMinMax(text.Origin.X, text.Origin.Y, ref minX, ref maxX, ref minY, ref maxY);
                        break;
                }
            }

            return (minX, maxX, minY, maxY);
        }

        private void UpdateMinMax(double x, double y, ref double minX, ref double maxX, ref double minY, ref double maxY)
        {
            minX = Math.Min(minX, x);
            maxX = Math.Max(maxX, x);
            minY = Math.Min(minY, y);
            maxY = Math.Max(maxY, y);
        }

        private void buttonLayerZoom_Click(object sender, EventArgs e)
        {
            if (layersList.SelectedItem is not null)
                RecalculateLayersZoom((MapLayer)layersList.SelectedItem);
        }

        private void buttonMapZoom_Click(object sender, EventArgs e)
        {
            List<MapLayer> layers = map.Layers;
            if (layers.Count > 0)
                RecalculateLayersZoom(layers);
        }
    }
}
