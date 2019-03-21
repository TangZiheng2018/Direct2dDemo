using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using Curve;
namespace Direct2dDemo
{
    using D2D = SharpDX.Direct2D1;
    using WIC = SharpDX.WIC;
    using DW = SharpDX.DirectWrite;
    using DXGI = SharpDX.DXGI;
    using SharpDX.Mathematics.Interop;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        D2D.Factory factory = new D2D.Factory();
        DW.Factory dwfactory = new DW.Factory();
        D2D.PixelFormat pf = new PixelFormat(Format.B8G8R8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Ignore);
        D2D.RenderTargetProperties renderTargetProperties;
        D2D.HwndRenderTargetProperties hwndRenderTargetProperties = new D2D.HwndRenderTargetProperties();
        private D2D.RenderTarget _renderTarget;
        private void Form1_Load(object sender, EventArgs e)
        {
            mc = new MainCurve(panel1);
            renderTargetProperties = new D2D.RenderTargetProperties(D2D.RenderTargetType.Default, pf, 0, 0, D2D.RenderTargetUsage.None, D2D.FeatureLevel.Level_DEFAULT);
            hwndRenderTargetProperties.Hwnd = panel1.Handle;
            hwndRenderTargetProperties.PixelSize = new SharpDX.Size2(panel1.Width, panel1.Height);
            _renderTarget = new D2D.WindowRenderTarget(factory, renderTargetProperties, hwndRenderTargetProperties);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ellipse = new D2D.Ellipse(new SharpDX.Mathematics.Interop.RawVector2(100, 100), 10, 10);

            var brush = new D2D.SolidColorBrush(_renderTarget, new SharpDX.Mathematics.Interop.RawColor4(1, 0, 0, 1));
            _renderTarget.BeginDraw();
            _renderTarget.Clear(new RawColor4(0.752F, 0.862F, 0752F, 0));
            _renderTarget.DrawEllipse(ellipse, brush, 1);
            _renderTarget.FillEllipse(ellipse, brush);
            _renderTarget.EndDraw();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_renderTarget == null)
            {
                return;
            }
            _renderTarget.BeginDraw();
            var brush = new D2D.SolidColorBrush(_renderTarget, new SharpDX.Mathematics.Interop.RawColor4(0, 1, 0, 1));
            _renderTarget.Clear(new RawColor4(0.752F, 0.862F, 0752F, 0));
            _renderTarget.DrawLine(new RawVector2(10, 10), new RawVector2(100, 100), brush, 10);
            _renderTarget.EndDraw();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var strokeStylePropertiesFlat = new D2D.StrokeStyleProperties();
            strokeStylePropertiesFlat.StartCap = D2D.CapStyle.Flat;
            strokeStylePropertiesFlat.EndCap = D2D.CapStyle.Flat;
            var strokeStylePropertiesRound = new D2D.StrokeStyleProperties();
            strokeStylePropertiesRound.StartCap = D2D.CapStyle.Round;
            strokeStylePropertiesRound.EndCap = D2D.CapStyle.Round;
            var strokeStylePropertiesSquare = new D2D.StrokeStyleProperties();
            strokeStylePropertiesSquare.StartCap = D2D.CapStyle.Square;
            strokeStylePropertiesSquare.EndCap = D2D.CapStyle.Round;
            var strokeStylePropertiesTriangle = new D2D.StrokeStyleProperties();
            strokeStylePropertiesTriangle.StartCap = D2D.CapStyle.Triangle;
            strokeStylePropertiesTriangle.EndCap = D2D.CapStyle.Round;
            var strokeStyleFlat = new StrokeStyle(factory, strokeStylePropertiesFlat);
            var strokeStyleRound = new StrokeStyle(factory, strokeStylePropertiesRound);
            var strokeStyleSquare = new StrokeStyle(factory, strokeStylePropertiesSquare);
            var strokeStyleTriangle = new StrokeStyle(factory, strokeStylePropertiesTriangle);
            var brush = new D2D.SolidColorBrush(_renderTarget, new SharpDX.Mathematics.Interop.RawColor4(1, 0, 0, 1));
            _renderTarget.BeginDraw();
            _renderTarget.Clear(new RawColor4(0.752F, 0.862F, 0752F, 0));
            _renderTarget.DrawLine(new RawVector2(100, 50), new RawVector2(200, 50), brush, 10, strokeStyleFlat);
            _renderTarget.DrawLine(new RawVector2(100, 100), new RawVector2(200, 100), brush, 10, strokeStyleRound);
            _renderTarget.DrawLine(new RawVector2(100, 150), new RawVector2(200, 150), brush, 10, strokeStyleSquare);
            _renderTarget.DrawLine(new RawVector2(100, 200), new RawVector2(200, 200), brush, 10, strokeStyleTriangle);
            _renderTarget.EndDraw();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var strokeStyleProperties = new D2D.StrokeStyleProperties();
            float[] dashes = { 20,10};
   
            strokeStyleProperties.DashStyle = D2D.DashStyle.Custom;
            
            //strokeStyleProperties.DashCap = D2D.CapStyle.Square;
            //strokeStyleProperties.DashOffset = 5;
            var strokeStyle = new StrokeStyle(factory, strokeStyleProperties,dashes);
            strokeStyle.GetDashes(dashes, 2);
            var brush = new D2D.SolidColorBrush(_renderTarget, new SharpDX.Mathematics.Interop.RawColor4(0.439F,0.501F, 0.564F, 1));

            _renderTarget.BeginDraw();
            _renderTarget.Clear(new RawColor4(0.752F, 0.862F, 0752F, 0));
            _renderTarget.DrawLine(new RawVector2(100, 50), new RawVector2(500, 50), brush, 0.5F, strokeStyle);


            _renderTarget.EndDraw();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var textformat = new DW.TextFormat(dwfactory, "宋体", 20);
            var brush = new D2D.SolidColorBrush(_renderTarget, new SharpDX.Mathematics.Interop.RawColor4(1, 0, 0, 1));
            _renderTarget.BeginDraw();
            _renderTarget.Clear(new RawColor4(0.752F, 0.862F, 0752F, 0));
            textformat.FlowDirection = DW.FlowDirection.TopToBottom;
            textformat.ReadingDirection = DW.ReadingDirection.RightToLeft;
            _renderTarget.DrawText("-20", textformat, new RawRectangleF(0, 0,200, 200), brush);
            _renderTarget.EndDraw();
        }
        MainCurve mc = null;
        private void button6_Click(object sender, EventArgs e)
        {
            mc.Draw(100,100);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            //mc.Draw(e.Location.X, e.Location.Y);
        }

        private void panel1_Click(object sender, EventArgs e)
        {
           
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            //mc.Draw(e.Location.X, e.Location.Y);
        }

        private void 时间宽度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void 光标数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mc.canvasparam.showcursordata == ShowCursorData.Hide)
            {
                mc.canvasparam.showcursordata = ShowCursorData.Visible;
            }
            else
            {
                mc.canvasparam.showcursordata = ShowCursorData.Hide;
            }         
        }

        private void 数据秒点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mc.canvasparam.showdatapoint == ShowDataPoint.Hide)
            {
                mc.canvasparam.showdatapoint = ShowDataPoint.Visible;
            }
            else
            {
                mc.canvasparam.showdatapoint = ShowDataPoint.Hide;
            }
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
