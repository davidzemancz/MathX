using Base.Api;
using Base.UI.Api.Controls;
using MathX.Primitives;
using MathX.Processes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathX.UI.Forms
{
    public partial class FormMathXGraph : BaseForm
    {
        #region PROPS

        private int AxisSpace 
        {
            get { return _axisSpace; }
            set { _axisSpace = value >= 15 ? value : 15; }
        }

        #endregion

        #region FIELDS

        private int _axisSpace = 30;

        private int _xMin;
        private int _xMax;

        private int _yMin;
        private int _yMax;

        private Bitmap _btmpGraph;
        private Graphics _graphics;

        private Process _currentProcess;

        #endregion

        #region CONTRUCTORS

        public FormMathXGraph()
        {
            InitializeComponent();
            pcbGraph.MouseWheel += pcbGraph_MouseWheel;
        }

        #endregion

        #region PRIVATE METHODS

        #region Actions
        
        private void DrawAxis()
        {
            Pen penAxis = null;
            Pen penAxisDark = null;
            try
            {
                int width = pcbContainer.Width;
                int midWidth = width / 2;
                int height = pcbContainer.Height;
                int midHeight = height / 2;

                _btmpGraph = new Bitmap(width, height);
                _graphics = Graphics.FromImage(_btmpGraph);

                penAxis = new Pen(Color.Gray);
                penAxisDark = new Pen(Color.DarkGray);
                Font fontAxis = this.Font;
                Brush brushAxis = Brushes.White;

                bool drawStrings = AxisSpace >= 15;

                // X-axis
                int numX = 0;
                _graphics.DrawLine(penAxis, 0, midHeight, width, midHeight);
                if (drawStrings) _graphics.DrawString($"{numX}", fontAxis, brushAxis, midWidth, midHeight);

                for (int x = AxisSpace; x <= width / 2; x += AxisSpace)
                {
                    _graphics.DrawLine(penAxisDark, midWidth + x, 0, midWidth + x, height);
                    if (drawStrings) _graphics.DrawString($"{++numX}", fontAxis, brushAxis, midWidth + x, midHeight);

                    _graphics.DrawLine(penAxisDark, midWidth - x, 0, midWidth - x, height);
                    if (drawStrings) _graphics.DrawString($"{numX}", fontAxis, brushAxis, midWidth - x, midHeight);
                }

                _xMin = -numX;
                _xMax = numX;


                // Y-axis
                int numY = 0;
                _graphics.DrawLine(penAxis, midWidth, 0, midWidth, height);

                for (int y = AxisSpace; y <= height / 2; y += AxisSpace)
                {
                    _graphics.DrawLine(penAxisDark, 0, midHeight + y, width, midHeight + y);
                    if (drawStrings) _graphics.DrawString($"{++numY}", fontAxis, brushAxis, midWidth, midHeight + y);

                    _graphics.DrawLine(penAxisDark, 0, midHeight - y, width, midHeight - y);
                    if (drawStrings) _graphics.DrawString($"{numY}", fontAxis, brushAxis, midWidth, midHeight - y);
                }

                _yMin = -numY;
                _yMax = numY;
            }
            catch(Exception ex)
            {
                throw;
            }
            finally
            {
                penAxis?.Dispose();
                penAxisDark?.Dispose();
            }
        }

        private void DrawFunction(Function function)
        {
            Pen penCurve = null;
            try
            {
                int width = pcbContainer.Width;
                float widthHalf = width / 2;

                int height = pcbContainer.Height;
                float heightHalf = height / 2;

                int xWidth = _xMax - _xMin;
                float xRatio = (width / xWidth);

                int yHeight = _yMax - _yMin;
                float yRatio = (height / yHeight);

                penCurve = new Pen(Color.Aquamarine, 2);
                List<PointF> points = new List<PointF>();

                for (double x = _xMin; x <= _xMax; x += 0.05)
                {
                    Variable var = function.Call(new[] { new Variable(Variable.DataTypeEnum.Double, "_", x) }, out BaseStatus status);

                    float value = Convert.ToSingle(x);
                    float result = Convert.ToSingle(var.Value);

                    PointF point = new PointF(widthHalf + (value * xRatio), heightHalf - (result * yRatio));
                    points.Add(point);

                    status.ThrowIfError();
                }

                _graphics.SmoothingMode = SmoothingMode.HighQuality;
                _graphics.DrawCurve(penCurve, points.ToArray());
                _graphics.SmoothingMode = SmoothingMode.Default;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                penCurve?.Dispose();
            }
        }

        private void LoadProcesses()
        {
            cbxProcesses.Items.Clear();
            foreach (KeyValuePair<string, Process> kvp in ProcessManager.Processes)
            {
                cbxProcesses.Items.Add(kvp.Value);
            }
            if (cbxProcesses.Items.Count > 0) cbxProcesses.SelectedIndex = 0;
        }

        private void Redraw()
        {
            DrawAxis();
            if (cbxFunctions.SelectedItem != null)
            {
                DrawFunction((Function)cbxFunctions.SelectedItem);
            }
            pcbGraph.Image = _btmpGraph;
        }

        #endregion

        #region Form

        private void FormMathXGraph_Activated(object sender, EventArgs e)
        {
            LoadProcesses();
        }

        private void FormMathXGraph_Load(object sender, EventArgs e)
        {
            LoadProcesses();
            Redraw();
        }

        private void cbxProcesses_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentProcess = (Process)cbxProcesses.SelectedItem;
            
            cbxFunctions.Items.Clear();
            foreach (var function in _currentProcess.Functions.Where(f => f.Value.ParametersNames.Length == 1))
            {
                cbxFunctions.Items.Add(function.Value);
            }

            if (cbxFunctions.Items.Count > 0) cbxFunctions.SelectedIndex = 0;
        }

        private void cbxFunctions_SelectedIndexChanged(object sender, EventArgs e)
        {
            Redraw();
        }

        private void pcbGraph_SizeChanged(object sender, EventArgs e)
        {
            Redraw();
        }

        private void pcbGraph_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                AxisSpace += 5;
            }
            else if (e.Delta < 0)
            {
                AxisSpace -= 5;
            }
            Redraw();
        }

        #endregion

        #endregion
    }
}
