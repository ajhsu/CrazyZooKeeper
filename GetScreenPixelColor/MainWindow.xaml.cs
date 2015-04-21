using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using MouseKeyboardLibrary;
using com.artgital.ajhsu;
using System.Timers;

namespace GetScreenPixelColor
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        private Timer RoundTimer;
        private Timer MoveTimer;

        private ZooReader reader;
        private ZooAnalyze analyze;
        private ZooMover mover;

        private Rectangle[,] _rectArray = new Rectangle[8, 8];

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            reader = new ZooReader();
            reader.ScanCompleted += new AGTEvent.AGTEventHandler(reader_ScanCompleted);

            analyze = new ZooAnalyze();

            mover = new ZooMover();
            mover.MoveCompleted += new EventHandler(mover_MoveCompleted);

            InitializeChessboardMirror();
            InitializeTimers();
        }

        private void InitializeTimers() {
            RoundTimer = new Timer(30000);
            RoundTimer.Elapsed += new ElapsedEventHandler(RoundTimer_Elapsed);

            MoveTimer = new Timer(250);
            MoveTimer.Elapsed += new ElapsedEventHandler(MoveTimer_Elapsed);
        }

        void MoveTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(reader.UpdateChessboardMirror), null);
        }

        void RoundTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            MoveTimer.Stop();
            RoundTimer.Stop();
            this.Dispatcher.BeginInvoke(new Action(GetFocus),null);
        }

        private void GetFocus()
        {
            this.Activate();
        }

        void mover_MoveCompleted(object sender, EventArgs e)
        {
            //reader.UpdateChessboardMirror();
        }

        private void reader_ScanCompleted(object sender, AGTEventArgs passObject)
        {
            int[,] r = passObject.PassObject as int[,];

            for (int _y = 0; _y < 8; _y++)
            {
                for (int _x = 0; _x < 8; _x++)
                {
                    _rectArray[_x, _y].Fill = new SolidColorBrush(AnimalType.GetColorPattern(r[_x, _y]));
                }
            }

            AnalyzeAndDecideToMove(r);
        }

        private int AnalyzeAndDecideToMove(int[,] result)
        {
            ChanceMap[,] _chanceMap = analyze.Analyze_Directly(result);
            List<Point> list = new List<Point>();

            for (int _y = 0; _y < 8; _y++)
            {
                for (int _x = 0; _x < 8; _x++)
                {
                    if (_chanceMap[_x, _y] != null)
                    {
                        if (_chanceMap[_x, _y].Dot.Count > 0)
                        {
                            list.Add(new Point(_x, _y));
                        }
                    }
                }
            }

            if (list.Count > 0)
            {
                Random r = new Random(DateTime.Now.Millisecond);
                Point _p = list.ElementAt(r.Next() % list.Count);

                Point p = _chanceMap[(int)_p.X, (int)_p.Y].Dot[0];
                mover.Move(reader.PositionArray[(int)_p.X, (int)_p.Y], reader.PositionArray[(int)_p.X + (int)p.X, (int)_p.Y + (int)p.Y]);
            }

            return 0;
        }



        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Q:
                    SetPosition_TopLeft();
                    break;
                case Key.S:
                    SetPosition_BottomRight();
                    break;

                case Key.G:
                    //this.Background = new SolidColorBrush(GetAverageColorOfRange(PointParse(MouseSimulator.Position), 30, 3));
                    break;
                case Key.V:
                    reader.CrazyMover("A");
                    break;
                case Key.B:
                    reader.CrazyMover("B");
                    break;
                case Key.N:
                    reader.CrazyMover("C");
                    break;
                case Key.M:
                    reader.CrazyMover("D");
                    break;
                case Key.Z:
                    reader.UpdateChessboardMirror();
                    break;
                case Key.R:
                    RoundTimer.Start();
                    MoveTimer.Start();
                    break;
            }
        }

        private void InitializeChessboardMirror()
        {
            for (var _y = 0; _y < 8; _y++)
            {
                for (var _x = 0; _x < 8; _x++)
                {
                    Rectangle r = new Rectangle();
                    r.Fill = Brushes.Black;
                    r.Width = 20;
                    r.Height = 20;
                    r.Margin = new Thickness((_x * 30) + 10, (_y * 30) + 10, 0, 0);
                    _rectArray[_x, _y] = r;

                    BoardWrapper.Children.Add(r);
                }
            }
        }

        private void SetPosition_TopLeft()
        {
            reader.SetPosition_TopLeft(AGTGeometric.DPoint2WPoint(MouseSimulator.Position));
        }

        private void SetPosition_BottomRight()
        {
            reader.SetPosition_BottomRight(AGTGeometric.DPoint2WPoint(MouseSimulator.Position));
        }

        private void UI_CheckBox_AlwaysTop_Changed(object sender, RoutedEventArgs e)
        {
            this.Topmost = UI_CheckBox_AlwaysTop.IsChecked.Value;
        }

    }


}
