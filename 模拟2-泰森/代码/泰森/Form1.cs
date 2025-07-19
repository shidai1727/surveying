using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace 泰森
{
    public partial class Form1 : Form
    {
        List<Point> points = new List<Point>();
        public Form1()
        {
            InitializeComponent();
        }

        private void 导入文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                points = File.LoadFile(filePath);

                dataGridView1.Rows.Clear();
                chart1.Series["Point"].Points.Clear();
                foreach (var point in points)
                {
                    dataGridView1.Rows.Add(point.ID, point.X, point.Y);
                    chart1.Series["Point"].Points.AddXY(point.X, point.Y);
                }

                var convexHull = ConvexHull.GetConvexHull(points);
                chart1.Series["ConvexHull"].Points.Clear();
                foreach (var point in convexHull)
                {
                    chart1.Series["ConvexHull"].Points.AddXY(point.X, point.Y);
                }
                chart1.Series["ConvexHull"].Points.AddXY(convexHull[0].X, convexHull[0].Y);

                var delaunayTriangles = Delaunay.BowyerWaston(points).OrderBy(o => o.area).ToList();
                
                foreach(var triangle in delaunayTriangles)
                {
                    chart1.Series["Delaunay"].Points.AddXY(triangle.Points[0].X, triangle.Points[0].Y);
                    chart1.Series["Delaunay"].Points.AddXY(triangle.Points[1].X, triangle.Points[1].Y);
                    chart1.Series["Delaunay"].Points.AddXY(triangle.Points[2].X, triangle.Points[2].Y);
                    chart1.Series["Delaunay"].Points.AddXY(triangle.Points[0].X, triangle.Points[0].Y);
                    chart1.Series["Delaunay"].Points.AddXY(0, double.NaN);
                }

                var voronoiPolygons = Voronoi.GetVoronoi(points, convexHull).OrderBy(o => o.area).ToList();
                foreach (var Polygon in voronoiPolygons)
                {
                    foreach (var point in Polygon.Points)
                    {
                        chart1.Series["Voronoi"].Points.AddXY(point.X, point.Y);
                    }
                    chart1.Series["Voronoi"].Points.AddXY(Polygon.Points[0].X, Polygon.Points[0].Y);
                    chart1.Series["Voronoi"].Points.AddXY(0, double.NaN);
                }

                chart1.ChartAreas[0].AxisX.Minimum = points.Min(o => o.X);
                chart1.ChartAreas[0].AxisX.Maximum = points.Max(o => o.X);
                chart1.ChartAreas[0].AxisY.Minimum = points.Min(o => o.Y);
                chart1.ChartAreas[0].AxisY.Maximum = points.Max(o => o.Y);
            }
        }

    }
}
