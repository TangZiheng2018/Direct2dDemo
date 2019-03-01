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
        public LineParam lp { set; get; }
        public AxisLine(CanvasParam _cp, LineParam lp)
        {
            cp = _cp;
            this.lp = lp;
        }
        public override void Draw(RawColor4 color)
        {   
            calculate();
            RawVector2 rs= new RawVector2(StartPointX,StartPointY);
            RawVector2 re = new RawVector2(EndPointX, EndPointY);
            var brush = new SharpDX.Direct2D1.SolidColorBrush(cp._renderTarget,color);
            cp._renderTarget.BeginDraw();
            cp._renderTarget.DrawLine(rs,re,brush,0.5F);
            arrow(color);
            if (ShowVirtualLine.Visible == lp.showVirtualLine)//绘制虚线
            {
                VirtualLine vl = new VirtualLine(cp, lp);
                vl.Draw(color);
            }
            LineScale ls = new LineScale(cp, lp);//刻度线
            ls.Draw(color);
            cp._renderTarget.EndDraw();
        }

        private void arrow(RawColor4 color)
        {
            float unitX = 0;
            float unitY = 0;
            float arrowLength = cp.ArrowLength;
            RawVector2[] points = new RawVector2[3];
            points[0].X = EndPointX;
            points[0].Y = EndPointY;
            if (LineDirection.Vertical == lp.Direction)
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
            var unitbrush=new SharpDX.Direct2D1.SolidColorBrush(cp._renderTarget, new RawColor4(0,0,0,1));
            var textformat = new SharpDX.DirectWrite.TextFormat(cp.dwFactory, "Arial", 12);
            unitX = points[0].X;
            unitY = points[0].Y;
            if (lp.lineLocation==LineLocation.Left)
            {
                unitX = points[0].X - 30;
                unitY = points[0].Y+2;
            }
            if (lp.Direction==LineDirection.Horizontal)
            {
                unitX = points[0].X - 20;
                unitY = points[0].Y + 2;
            }
            cp._renderTarget.DrawText(lp.Caption, textformat, new RawRectangleF(unitX, unitY, unitX + 200, unitY + 200), unitbrush);

        }
        private void calculate()
        {
            StartPointX = cp.OriginX;
            StartPointY = cp.OriginY;
            if (lp.Direction == LineDirection.Vertical)
            {
                EndPointX = cp.OriginX;
                EndPointY = StartPointY - cp.VerticalLength;
            }
            else
            {
                EndPointX = cp.OriginX + cp.HorizontalLength;
                EndPointY = cp.OriginY;
            }
        }
    }
}
