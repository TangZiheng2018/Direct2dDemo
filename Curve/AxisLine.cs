using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Mathematics.Interop;

namespace Curve
{
    public class AxisLine : BaseLine
    {
        public AxisLineParam axislineparam { set; get; }
        public AxisLine(CanvasParam _cp, AxisLineParam axislineparam) :base(_cp)
        {
            cp = _cp;
            this.axislineparam = axislineparam;
        }
        public override void Draw()
        {
            if (axislineparam.lineVisible==LineVisiable.Hide)
            {
                return;
            }
            calculate();
            RawVector2 rs= new RawVector2(StartPointX,StartPointY);
            RawVector2 re = new RawVector2(EndPointX, EndPointY);
            var brush = new SharpDX.Direct2D1.SolidColorBrush(cp._renderTarget,color);
            cp._renderTarget.DrawLine(rs,re,brush,0.5F);
            arrow(color);
            if (ShowVirtualLine.Visible == axislineparam.showVirtualLine)//绘制虚线
            {
                VirtualLine vl = new VirtualLine(cp, axislineparam);
                vl.color = color;
                vl.Draw();
            }
            LineScale ls = new LineScale(cp, axislineparam);//刻度线
            ls.color = color;
            ls.Draw();
        }

        private void arrow(RawColor4 color)
        {
            float arrowLength = cp.ArrowLength;
            RawVector2[] points = new RawVector2[3];
            points[0].X = EndPointX;
            points[0].Y = EndPointY;
            if (LineDirection.Vertical == axislineparam.Direction)
            {
                points[1].X = EndPointX - arrowLength / 2;
                points[1].Y = EndPointY + arrowLength;
                points[2].X = EndPointX + arrowLength / 2;
                points[2].Y = EndPointY + arrowLength;
            }
            else
            {
                points[1].X = EndPointX - arrowLength;
                points[1].Y = EndPointY + arrowLength / 2;
                points[2].X = EndPointX - arrowLength;
                points[2].Y = EndPointY - arrowLength / 2;
            }
            var brush = new SharpDX.Direct2D1.SolidColorBrush(cp._renderTarget, color);
            cp._renderTarget.DrawLine(points[0], points[1], brush);
            cp._renderTarget.DrawLine(points[0], points[2], brush);
            DrawLegend(points[0].X, points[0].Y);
        }
        private void calculate()
        {
            if (axislineparam.Direction == LineDirection.Vertical)
            {
                if (axislineparam.lineLocation == LineLocation.Left)
                {
                    StartPointX = cp.OriginX;
                    StartPointY = cp.OriginY;
                    EndPointX = cp.OriginX;
                    EndPointY = StartPointY - cp.VerticalLength;
                }
                else
                {
                    StartPointX = cp.OriginX + Hlength + axislineparam.Index * 10;
                    StartPointY = cp.OriginY;
                    EndPointX = cp.OriginX + Hlength + axislineparam.Index * 10;
                    EndPointY = StartPointY - cp.VerticalLength;
                }
            }
            else
            {
                StartPointX = cp.OriginX;
                StartPointY = cp.OriginY;
                EndPointX = cp.OriginX + cp.HorizontalLength;
                EndPointY = cp.OriginY;
            }
        }
        private void DrawLegend(float PointX, float PointY)
        {
            float unitX = 0;
            float unitY = 0;
            var unitbrush = new SharpDX.Direct2D1.SolidColorBrush(cp._renderTarget, new RawColor4(0, 0, 0, 1));
            var textformat = new SharpDX.DirectWrite.TextFormat(cp.dwFactory, "Arial", 12);
            unitX = PointX;
            unitY = PointY;
            if (axislineparam.lineLocation == LineLocation.Left)
            {
                unitX = PointX - 30;
                unitY = PointY+2;
            }
            if (axislineparam.Direction == LineDirection.Horizontal)
            {
                unitX = PointX - 20;
                unitY = PointY+2;
            }
            cp._renderTarget.DrawText(axislineparam.Caption, textformat, new RawRectangleF(unitX, unitY, unitX + 200, unitY + 200), unitbrush);
        }
    }
}
