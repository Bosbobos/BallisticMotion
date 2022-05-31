using System;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;

namespace BallisticMotion
{
    internal class ProgramLogic
    {
        public void SetupGraph(ZedGraphControl zedGraphControl)
        {
            GraphPane pane = zedGraphControl.GraphPane;
            pane.CurveList.Clear();
            pane.XAxis.Title.Text = "Расстояние, м";
            pane.YAxis.Title.Text = "Высота, м";
            pane.Title.Text = "Траектория движения";
            pane.XAxis.Scale.MinAuto = true;
            pane.XAxis.Scale.Max = 300;
            pane.YAxis.Scale.MinAuto = true;
            pane.YAxis.Scale.Max = 100;

            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
        }

        public void DrawGraph(ZedGraphControl zedGraphControl, System.Windows.Forms.Label height, System.Windows.Forms.Label length, System.Windows.Forms.Label time)
        {
            GraphPane pane = zedGraphControl.GraphPane;
            pane.CurveList.Clear();

            LineItem line = pane.AddCurve("Траектория", CalculatePoints(), Color.Blue, SymbolType.None);
            zedGraphControl.Invalidate();

            height.Text = "Максимальная высота: " + Math.Round(maxY, 1) + " м";
            length.Text = "Дальность полёта: " + Math.Round(L, 1) + " м";
            time.Text = "Время полёта: " + Math.Round(t, 1) + " с";
        }
    }
}
