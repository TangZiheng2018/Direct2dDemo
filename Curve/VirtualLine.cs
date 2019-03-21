using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curve
{
  public   class VirtualLine : BaseLine
    {
        private List<RawVector2> listpointS = new List<RawVector2>();
        private List<RawVector2> listpointE = new List<RawVector2>();
        private AxisLineParam lp { set; get; }
        float Hlength { set; get; }//纵向减去箭头的长度
        float Vlength { set; get; }//横向向减去箭头的长度
        private float ScaleCount { set; get; }
        private float cellLength { set; get; }
        public VirtualLine(CanvasParam _cp, AxisLineParam lp):base(_cp)
        {
            cp = _cp;
            this.lp = lp;
            StartPointX = cp.OriginX;
            StartPointY = cp.OriginY;
            ScaleCount = (lp.MaxScale - lp.MinScale) / lp.CellScale;//计算多少刻度
            Hlength = cp.HorizontalLength - cp.ArrowLength - cp.BlankLegend;//减去箭头的长度
            Vlength = cp.VerticalLength - cp.ArrowLength - cp.BlankLegend;
        }
        public override void Draw()
        {
            calculate();
            var strokeStyleProperties = new SharpDX.Direct2D1.StrokeStyleProperties();
            strokeStyleProperties.DashStyle = SharpDX.Direct2D1.DashStyle.Custom;
            float[] dashes = { 10, 5 };
            //strokeStyleProperties.DashCap = SharpDX.Direct2D1.CapStyle.Square;
            //strokeStyleProperties.DashOffset = 0.2F;
            var strokeStyle = new StrokeStyle(cp.factory, strokeStyleProperties,dashes);
            var brush = new SharpDX.Direct2D1.SolidColorBrush(cp._renderTarget, new RawColor4(0.439F, 0.501F, 0.564F,1));
            for (int i = 0; i < ScaleCount; i++)
            {
                cp._renderTarget.DrawLine(listpointS[i], listpointE[i],brush, 0.5F, strokeStyle);
            }
        }
        //竖向
        private void calculateVertical()
        {
            cellLength = Vlength / ScaleCount;//求单位长度
            for (int i = 0; i < ScaleCount; i++)
            {
                RawVector2 pfS = new RawVector2();
                RawVector2 pfE = new RawVector2();
                pfS.X = StartPointX;
                pfS.Y = StartPointY - cellLength * (i + 1);
                pfE.X = StartPointX + Hlength;
                pfE.Y = StartPointY - cellLength * (i + 1);
                listpointS.Add(pfS);
                listpointE.Add(pfE);
            }
        }
        //横向
        private void calculateHorizontal()
        {
            cellLength = Hlength / ScaleCount;//求单位长度
            for (int i = 0; i < ScaleCount; i++)
            {
                RawVector2 pfS = new RawVector2();
                RawVector2 pfE = new RawVector2();
                pfS.X = StartPointX + cellLength * (i + 1);
                pfS.Y = StartPointY;
                pfE.X = StartPointX + cellLength * (i + 1);
                pfE.Y = StartPointY - Vlength;
                listpointS.Add(pfS);
                listpointE.Add(pfE);
            }
        }
        private void calculate()
        {
            if (lp.Direction == LineDirection.Vertical)
            {
                calculateVertical();
            }
            else
            {
                calculateHorizontal();
            }
        }
    }
}
