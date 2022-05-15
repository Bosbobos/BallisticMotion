using System;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;

namespace BallisticMotion
{
    public partial class MainWindow : Form
    {
        private double maxY;
        private double L;
        private double t;

        public MainWindow()
        {
            InitializeComponent();
            SetupGraph();  
        }

        private void SetupGraph()
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();
            pane.XAxis.Title.Text = "Расстояние, м";
            pane.YAxis.Title.Text = "Высота, м";
            pane.Title.Text = "Траектория движения";
            pane.XAxis.Scale.MinAuto = true;
            pane.XAxis.Scale.Max = 300;
            pane.YAxis.Scale.MinAuto = true;
            pane.YAxis.Scale.Max = 100;

            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private void DrawGraph()
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();

            LineItem line = pane.AddCurve("Траектория", CalculatePoints(), Color.Blue, SymbolType.None);
            zedGraphControl1.Invalidate();
            
            label3.Text = "Максимальная высота: " + Math.Round(maxY,1) + "м";
            label4.Text = "Дальность полёта: " + Math.Round(L,1) + "м";
            label5.Text = "Время полёта: " + Math.Round(t, 1) + "с";

        }

        private PointPairList CalculatePoints()
        {
            PointPairList list = new PointPairList();
            int V;
            double ang;
            if (textBox1.Text != "")
                V = Convert.ToInt32(textBox1.Text);
            else
                V = 50;
            if (textBox2.Text != "")
                ang = Convert.ToInt32(textBox2.Text);
            else
                ang = 45;
            double g = 10;
            double x, y;
            ang = ang * Math.PI / 180;

            t = (2 * V * Math.Sin(ang)) / g;

            L = V * V * Math.Sin(ang * 2) / g;
            maxY = V * V * Math.Pow(Math.Sin(ang), 2) / (2 * g);

            for (double i = 0; i < t; i+=0.001)
            {
                x = V * Math.Cos(ang) * i;
                y = V*Math.Sin(ang)*i - (g*i*i/2);

                list.Add(x, y);
            }

            return list;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char num = e.KeyChar;
            if (!Char.IsDigit(num) && num != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char num = e.KeyChar;
            if (!Char.IsDigit(num) && num != 8)
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && Convert.ToInt64(textBox2.Text) >= 90)
                MessageBox.Show("Угол должен быть меньше 90 градусов!", "Внимание!");
            else if (textBox1.Text != "" && Convert.ToInt64(textBox1.Text) >= 500)
                MessageBox.Show("Скорость должна быть меньше 500 метров в секунду!", "Внимание!");
            else
                DrawGraph();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            
            pane.XAxis.Scale.MinAuto = true;
            pane.XAxis.Scale.MaxAuto = true;
            pane.YAxis.Scale.MinAuto = true;
            pane.YAxis.Scale.MaxAuto = true;

            zedGraphControl1.AxisChange();

            if (pane.XAxis.Scale.Max > pane.YAxis.Scale.Max)
            {
                pane.YAxis.Scale.Max = pane.XAxis.Scale.Max / 3;
                pane.YAxis.Scale.Min = pane.XAxis.Scale.Min;
//                pane.YAxis.Scale.MajorStep = pane.XAxis.Scale.MajorStep;
            }
            else
            {
                pane.XAxis.Scale.Max = pane.YAxis.Scale.Max * 3;
                pane.XAxis.Scale.Min = pane.YAxis.Scale.Min;
                pane.XAxis.Scale.MajorStep = pane.YAxis.Scale.MajorStep;
            }

            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GraphPane pane = zedGraphControl1.GraphPane;

            pane.XAxis.Scale.MinAuto = true;
            pane.XAxis.Scale.Max = 300;
            pane.YAxis.Scale.MinAuto = true;
            pane.YAxis.Scale.Max = 100;

            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }
    }
}
