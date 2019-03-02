using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curve
{
   public class DataLine: BaseLine
    {
        public List<LineDataModel> listData { set; get; }
        public List<AxisLineParam> listCP { set; get; }
        public string Attributes { set; get; }
        private List<RawVector2> data = new List<RawVector2>();
        public String Caption { set; get; }
        public RawColor4 color { set; get; }//线颜色
        public float lineWith { set; get; }//线粗细
        private AxisLineParam Vlp { set; get; }
        private AxisLineParam Hlp { set; get; }
        private float HUnit { set; get; }//横向单位刻度以分钟为基准
        private float VUnit { set; get; }//竖向单位以1位单位
        public DataLine(CanvasParam _cp, List<AxisLineParam> listCP, string Attributes) : base(_cp)
        {
            lineWith = 0;
            this.Attributes = Attributes;
            this.Hlp = listCP.Find(t => t.Attributes == "Time");
            this.Vlp = listCP.Find(t => t.Attributes == Attributes);
        }
        public float GetLineData(float x, float y)
        {
            for (int i = 1; i < data.Count; i++)
            {
                if ((x<data[i].X)&&(x>data[i-1].X))
                {
                    if (x >= (data[i].X - data[i - 1].X) / 2)
                    {
                        return listData[i].Values;
                    }
                    else
                    {
                        return listData[i-1].Values;
                    }
                }
            }
            return 0;
        }
        public override void Draw()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            calculate();
            stopwatch.Stop();
            Console.WriteLine("dataline数点计算：" + stopwatch.ElapsedMilliseconds);
            stopwatch.Restart();
            RawVector2[] pfdata = data.ToArray<RawVector2>();
            DrawDataLine(pfdata);
            DrawDataPoint(pfdata);
            stopwatch.Stop();
            Console.WriteLine("dataline绘制：" + stopwatch.ElapsedMilliseconds);
        }
        private void calculate()
        {
            data.Clear();
            if (listData == null)
            {
                return;
            }
            HUnit = Hlength / (Hlp.MaxScale - Hlp.MinScale);
            VUnit = Vlength / (Vlp.MaxScale - Vlp.MinScale);
            for (int i = 0; i < listData.Count; i++)
            {
                RawVector2 pf = new RawVector2();
                if (listData[i].dataUnit == DataUnit.M)
                {
                    pf.X = cp.OriginX + (i) * HUnit * listData[i].Times;
                }
                else if (listData[i].dataUnit == DataUnit.S)
                {
                    pf.X = cp.OriginX + (i) * (HUnit / 60) * listData[i].Times;
                }
                else
                {
                    pf.X = cp.OriginX + (i) * (HUnit * 60) * listData[i].Times;
                }
                pf.Y = cp.OriginY - (listData[i].Values - Vlp.MinScale) * VUnit;
                if (pf.X > Hlength + StartPointX)
                {
                    break;
                }
                data.Add(pf);
            }
        }
        private void DrawDataLine(RawVector2[] pfdata)
        {
            var brush = new SharpDX.Direct2D1.SolidColorBrush(cp._renderTarget, color);
            for (int i = 0; i < pfdata.Length - 1; i++)
            {
                cp._renderTarget.DrawLine(pfdata[i], pfdata[i + 1], brush, lineWith);
            }
        }
        private void DrawDataPoint(RawVector2[] pfdata)
        {
            if (cp.showdatapoint==ShowDataPoint.Hide)
            {
                return;
            }
            RawColor4 rc = new RawColor4(0,0.5F,0,1);
            var brush = new SolidColorBrush(cp._renderTarget, rc);
            for (int i = 0; i < pfdata.Length; i++)
            {
                var ellipse = new Ellipse(pfdata[i], 2, 2);
                cp._renderTarget.FillEllipse(ellipse, brush);
            }
        }
    }
}
