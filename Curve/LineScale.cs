using SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curve
{
    public class LineScale : BaseLine
    {
        private List<RawVector2> listpointS = new List<RawVector2>();
        private List<RawVector2> listpointE = new List<RawVector2>();
        private LineParam lp { set; get; }
        float Hlength { set; get; }//纵向减去箭头的长度
        float Vlength { set; get; }//横向向减去箭头的长度
        private float ScaleCount { set; get; }
        private float cellLength { set; get; }
        public LineScale(CanvasParam _cp, LineParam lp)
        {
            cp = _cp;
            this.lp = lp;
            StartPointX = cp.OriginX;
            StartPointY = cp.OriginY;
            ScaleCount = (lp.MaxScale - lp.MinScale) / lp.CellScale;//计算多少刻度
            Hlength = cp.HorizontalLength - cp.ArrowLength - cp.BlankLegend;//减去箭头的长度
            Vlength = cp.VerticalLength - cp.ArrowLength - cp.BlankLegend;
        }
        public override void Draw(RawColor4 color)
        {
            calculate();
            if (listpointE.Count == 0)
            {
                return;
            }
            var textformat = new SharpDX.DirectWrite.TextFormat(cp.dwFactory, "Arial",12);
            //textformat.ReadingDirection = ReadingDirection.RightToLeft;
            var brush = new SharpDX.Direct2D1.SolidColorBrush(cp._renderTarget,color);
            string strValue = "";
            float showVlaue = 0;
            for (int i = 0; i < (ScaleCount + 1); i++)
            {
                cp._renderTarget.DrawLine(listpointS[i], listpointE[i],brush,1);
                if (lp.Direction == LineDirection.Vertical)
                {

                    cp._renderTarget.DrawText((lp.MinScale + i * lp.CellScale).ToString(), textformat,new RawRectangleF(listpointE[i].X-4, listpointE[i].Y, listpointE[i].X+1200, listpointE[i].Y+1200),brush);
                }
                else
                {
                    showVlaue = lp.MinScale + i * lp.CellScale;
                    if (lp.lineLocation == LineLocation.Left)
                    {
                        textformat.FlowDirection =FlowDirection.TopToBottom;
                        textformat.ReadingDirection = ReadingDirection.RightToLeft;
                        strValue = showVlaue < 0 ? Math.Abs(showVlaue).ToString() + "-" : (showVlaue).ToString();
                        //cp._renderTarget.DrawText(strValue, textformat, new RawRectangleF(listpointE[i].X-cp.ScalePadding-200, listpointE[i].Y-7-200, listpointE[i].X - cp.ScalePadding, listpointE[i].Y - 7),brush);
                        cp._renderTarget.DrawText(strValue, textformat, new RawRectangleF(listpointE[i].X - cp.ScalePadding-200, listpointE[i].Y - 7+200, listpointE[i].X - cp.ScalePadding, listpointE[i].Y - 7), brush);
                    }
                    else
                    {
                        cp._renderTarget.DrawText(showVlaue.ToString(), textformat, new RawRectangleF(listpointE[i].X + cp.ScalePadding, listpointE[i].Y - 7, listpointE[i].X + cp.ScalePadding + 1200, listpointE[i].Y - 7 + 1200), brush);
                    }
                }
            }
        }
        //竖向
        private void calculateHorizontal()
        {
            cellLength = Vlength / ScaleCount;//求单位长度
            if (lp.lineLocation == LineLocation.Left)
            {
                calculateHorizontalLeft();
            }
            else
            {
                calculateHorizontalRight();
            }
        }
        private void calculateHorizontalRight()
        {
            cellLength = Vlength / ScaleCount;//求单位长度
            for (int i = 0; i < (ScaleCount + 1); i++)
            {
                RawVector2 pfS = new RawVector2();
                RawVector2 pfE = new RawVector2();
                pfS.X = StartPointX;
                pfS.Y = StartPointY - cellLength * (i + 1);
                pfE.X = StartPointX + cp.ScaleLength;
                pfE.Y = StartPointY - cellLength * (i + 1);
                listpointS.Add(pfS);
                listpointE.Add(pfE);
            }
        }
        private void calculateHorizontalLeft()
        {
            cellLength = Vlength / ScaleCount;//求单位长度
            for (int i = 0; i < (ScaleCount + 1); i++)
            {
                RawVector2 pfS = new RawVector2();
                RawVector2 pfE = new RawVector2();
                pfS.X = StartPointX;
                pfS.Y = StartPointY - cellLength * i;
                pfE.X = StartPointX - cp.ScaleLength;
                pfE.Y = StartPointY - cellLength * i;
                listpointS.Add(pfS);
                listpointE.Add(pfE);
            }
        }
        //横向刻度
        private void calculateVertical()
        {
            cellLength = Hlength / ScaleCount;//求单位长度
            for (int i = 0; i < (ScaleCount + 1); i++)
            {
                RawVector2 pfS = new RawVector2();
                RawVector2 pfE = new RawVector2();
                pfS.X = StartPointX + cellLength * i;
                pfS.Y = StartPointY;
                pfE.X = StartPointX + cellLength * i;
                pfE.Y = StartPointY + cp.ScaleLength;
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
