using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MouseKeyboardLibrary;
using System.Windows;
using System.Windows.Media;
using com.artgital.ajhsu;
using ScreenshotCaptureWithMouse.ScreenCapture;


namespace GetScreenPixelColor
{


    public class ZooReader
    {
        //當掃描完成時呼叫
        public event AGTEvent.AGTEventHandler ScanCompleted;

        private bool bIsTopLeftPosed = false;
        private bool bIsBottomRightPosed = false;
        //Home set
        private Point Position_TopLeft = new Point(523, 285);
        private Point Position_BottomRight = new Point(1056, 817);

        /*private Point Position_TopLeft = new Point(637, 295);
        private Point Position_BottomRight = new Point(1187, 843);*/


        private int[,] _resultArray = new int[8, 8];
        public int[,] ResultArray
        {
            get { return _resultArray; }
        }

        private Point[,] _positionArray = new Point[8, 8];
        public Point[,] PositionArray
        {
            get { return _positionArray; }
        }


        private System.Drawing.Bitmap bmp;

        private List<Color> _collect = new List<Color>();

        /// <summary>
        /// 設定左上角
        /// </summary>
        public void SetPosition_TopLeft(Point point)
        {
            Position_TopLeft = point;
            Console.WriteLine("TopLeft: " + Position_TopLeft.ToString());

            if (!bIsTopLeftPosed)
            {
                bIsTopLeftPosed = true;
                CheckIfBoardIsReady();
            }
        }

        /// <summary>
        /// 設定右下角
        /// </summary>
        public void SetPosition_BottomRight(Point point)
        {
            Position_BottomRight = point;
            Console.WriteLine("BottomRight: " + Position_BottomRight.ToString());

            if (!bIsBottomRightPosed)
            {
                bIsBottomRightPosed = true;
                CheckIfBoardIsReady();
            }
        }

        private void CheckIfBoardIsReady()
        {
            if (bIsTopLeftPosed && bIsBottomRightPosed)
            {
                //Scanning the board
                //UpdateChessboardMirror();
            }
        }

        private void GetScreen()
        {
            bmp = CaptureScreen.CaptureDesktop();
        }

        private Color GetPixel(Point p)
        {
            //Console.WriteLine(bmp.Width + "," + bmp.Height);
            System.Drawing.Color c = bmp.GetPixel((int)p.X, (int)p.Y);
            Color _c = Color.FromRgb(c.R, c.G, c.B);
            return _c;
        }

        /// <summary>
        /// 掃描座標 + 擷取顏色 + 分析動物
        /// </summary>
        public void UpdateChessboardMirror()
        {
            double BoardWidth = Math.Abs(Position_BottomRight.X - Position_TopLeft.X);
            double BoardHeight = Math.Abs(Position_BottomRight.Y - Position_TopLeft.Y);
            double PaddingX = BoardWidth / 8;
            double PaddingY = BoardHeight / 8;
            double OffsetX = PaddingX * 0.5;
            double OffsetY = PaddingY * 0.5;

            _resultArray = new int[8, 8];

            GetScreen();

            for (var _y = 0; _y < 8; _y++)
            {
                for (var _x = 0; _x < 8; _x++)
                {
                    Point p = new Point(Position_TopLeft.X + (PaddingX * _x) + OffsetX, Position_TopLeft.Y + (PaddingY * _y) + OffsetY);
                    _positionArray[_x, _y] = p;
                    MouseSimulator.Position = new System.Drawing.Point((int)p.X, (int)p.Y);
                    //Color c = AGTScreenPixelPicker.GetScreenPixelColor(p);
                    //Color c = GetPixel(p);
                    Color c = GetAverageColorOfRange(p, (int)(PaddingX*0.6), 8);

                    Console.WriteLine("{0},{1}: {2},{3},{4}", _x + 1, _y + 1, c.R, c.G, c.B);

                    int result = AnimalRecongize(c, 30);
                    //Console.Write(result + ",");
                    _resultArray[_x, _y] = result;  //把對應結果放進陣列

                }
                Console.Write("\n");
            }
            if (ScanCompleted != null)
            {
                ScanCompleted(this, new AGTEventArgs(_resultArray));
            }
        }

        #region CrazyMove
        public void CrazyMover(string cmd)
        {
            double BoardWidth = Math.Abs(Position_BottomRight.X - Position_TopLeft.X);
            double BoardHeight = Math.Abs(Position_BottomRight.Y - Position_TopLeft.Y);
            double PaddingX = BoardWidth / 8;
            double PaddingY = BoardHeight / 8;
            double OffsetX = PaddingX * 0.5;
            double OffsetY = PaddingY * 0.5;

            if (cmd == "A")
            {
                for (var _y = 0; _y < 8; _y++)
                {
                    for (var _x = 0; _x < 8; _x++)
                    {
                        Point p = new Point(Position_TopLeft.X + (PaddingX * _x) + OffsetX, Position_TopLeft.Y + (PaddingY * _y) + OffsetY);
                        MouseSimulator.Position = new System.Drawing.Point((int)p.X, (int)p.Y);
                        MouseSimulator.Click(MouseButton.Left);
                    }
                }
            }

            if (cmd == "B")
            {
                for (var _y = 0; _y < 8; _y++)
                {
                    for (var _x = 1; _x < 7; _x++)
                    {
                        Point p = new Point(Position_TopLeft.X + (PaddingX * _x) + OffsetX, Position_TopLeft.Y + (PaddingY * _y) + OffsetY);
                        MouseSimulator.Position = new System.Drawing.Point((int)p.X, (int)p.Y);
                        MouseSimulator.Click(MouseButton.Left);
                    }
                }
            }

            if (cmd == "C")
            {

                for (var _x = 0; _x < 8; _x++)
                {
                    for (var _y = 0; _y < 8; _y++)
                    {
                        Point p = new Point(Position_TopLeft.X + (PaddingX * _x) + OffsetX, Position_TopLeft.Y + (PaddingY * _y) + OffsetY);
                        MouseSimulator.Position = new System.Drawing.Point((int)p.X, (int)p.Y);
                        MouseSimulator.Click(MouseButton.Left);
                    }
                }
            }

            if (cmd == "D")
            {

                for (var _x = 0; _x < 8; _x++)
                {
                    for (var _y = 1; _y < 7; _y++)
                    {
                        Point p = new Point(Position_TopLeft.X + (PaddingX * _x) + OffsetX, Position_TopLeft.Y + (PaddingY * _y) + OffsetY);
                        MouseSimulator.Position = new System.Drawing.Point((int)p.X, (int)p.Y);
                        MouseSimulator.Click(MouseButton.Left);
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 以顏色分析動物
        /// </summary>
        private int AnimalRecongize(Color color, int tolerance)
        {
            int t = tolerance;

            for (int _c = 0; _c < 9; _c++)
            {
                if (_c == (int)AnimalType.Black_Panda)
                {
                    int average = (color.R + color.G + color.B) / 3;
                    if (color.R >= average - 3 && color.R <= average + 3)
                    {
                        return (int)AnimalType.Black_Panda;
                    }
                }

                Color c = AnimalType.GetColorPattern(_c);
                if (color.R >= c.R - t && color.R <= c.R + t)
                {
                    if (color.G >= c.G - t && color.G <= c.G + t)
                    {
                        if (color.B >= c.B - t && color.B <= c.B + t)
                        {
                            return _c;
                        }
                    }
                }
            }
            return -1;  //Nothing
        }

        /// <summary>
        /// 分析區域平均色
        /// </summary>
        private Color GetAverageColorOfRange(Point position, int range, int level)
        {
            int offset = range / (level-1);
            Point StartPosition = new Point(position.X - (range / 2), position.Y - (range / 2));

            int totalR = 0;
            int totalG = 0;
            int totalB = 0;

            for (var _y = 0; _y < level; _y++)
            {
                for (var _x = 0; _x < level; _x++)
                {
                    Point p = new Point(StartPosition.X + (_x * offset), StartPosition.Y + (_y * offset));
                    Color c = GetPixel(p);
                    totalR += c.R;
                    totalG += c.G;
                    totalB += c.B;
                }
            }
            int t = level * level;
            return Color.FromRgb((byte)(totalR / t), (byte)(totalG / t), (byte)(totalB / t));
        }

        /// <summary>
        /// 尋找特殊道具
        /// </summary>
        /*private Color CheckIfSpecialForce(Point position)
        {

            return null;
        }*/
    }
}
