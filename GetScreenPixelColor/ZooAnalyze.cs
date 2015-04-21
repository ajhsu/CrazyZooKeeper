using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace GetScreenPixelColor
{
    public class ChanceMap
    {
        private List<Point> _dot;
        public List<Point> Dot
        {
            get { return _dot; }
        }

        public ChanceMap()
        {
            _dot = new List<Point>();
        }
    }


    public class ZooAnalyze
    {

        private int[,] _rawArray = new int[8, 8];
        public int?[,] __fakeArray_mirrored__ = {{0,0,1,5,2,5,5,0},
                                                 {2,1,3,2,5,1,0,3},
                                                 {6,1,4,5,6,3,0,1},
                                                 {1,2,0,5,4,4,3,1},
                                                 {5,6,0,1,4,1,5,4},
                                                 {-1,3,2,3,1,2,5,3},
                                                 {6,6,0,-1,6,0,3,-1},
                                                 {1,3,0,6,-1,5,2,5}};

        //Actually, the array in code looks mirrored
        public int[,] _fakeArray = {{0,2,6,1,5,-1,6,1},
                                    {0,1,1,2,6,3,6,3},
                                    {1,3,4,0,0,2,0,0},
                                    {5,2,5,5,1,3,-1,6},
                                    {2,5,6,4,4,1,6,-1},
                                    {5,1,3,4,1,2,0,5},
                                    {5,0,0,3,5,5,3,2},
                                    {0,3,1,1,4,3,-1,5}};

        public ChanceMap[,] _chanceMap = new ChanceMap[8, 8];

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_data">Original Animal-Type Map</param>
        /// <returns>ChanceMap</returns>
        public ChanceMap[,] Analyze_Directly(int[,] _data)
        {
            _chanceMap = new ChanceMap[8, 8];

            int _currentValueX = 0;
            int?[,] _resultX = new int?[8, 8];

            Console.WriteLine("*************************");

            #region Analyzing 

            for (int _y = 0; _y < 8; _y++)
            {
                for (int _x = 0; _x < 8; _x++)
                {
                    _currentValueX = _data[_x, _y];
                    if (_currentValueX >= 0)    // Value != Negative
                    {
                        //***********找中洞*************
                        if (_x <= 5)
                        {
                            // O X O
                            if (_data[_x + 2, _y] == _currentValueX)    //發現右邊跳兩格一樣
                            {
                                //找上面
                                if (_y >= 1)
                                {
                                    if (_data[_x + 1, _y - 1] == _currentValueX)
                                    {
                                        //機會點 加入 相對機會點的位置
                                        DotCheck(ref _chanceMap[_x + 1, _y]).Dot.Add(new Point(0, -1));
                                    }
                                }

                                //找下面
                                if (_y <= 6)
                                {
                                    if (_data[_x + 1, _y + 1] == _currentValueX)
                                    {
                                        //機會點 加入 相對機會點的位置
                                        DotCheck(ref _chanceMap[_x + 1, _y]).Dot.Add(new Point(0, 1));
                                    }
                                }
                            }
                        }
                        //***********找中洞*************

                        //************找邊間************
                        if (_x <= 6) //防止碰到每行最右邊
                        {

                            // O O X
                            if (_data[_x + 1, _y] == _currentValueX)    //發現原來右邊也一樣
                            {
                                //寫入結果陣列
                                _resultX[_x, _y] = _currentValueX;
                                _resultX[_x + 1, _y] = _currentValueX;

                                //找出得分地圖

                                //先找左邊
                                if (_x >= 2)
                                {
                                    if (_data[_x - 2, _y] == _currentValueX)
                                    {
                                        //機會點 加入 相對機會點的位置
                                        DotCheck(ref _chanceMap[_x - 1, _y]).Dot.Add(new Point(-1, 0));
                                    }
                                }

                                //左上
                                if (_x >= 1 && _y >= 1)
                                {
                                    if (_data[_x - 1, _y - 1] == _currentValueX)
                                    {
                                        //機會點 加入 相對機會點的位置
                                        DotCheck(ref _chanceMap[_x - 1, _y]).Dot.Add(new Point(0, -1));
                                    }
                                }

                                //左下
                                if (_x >= 1 && _y <= 6)
                                {
                                    if (_data[_x - 1, _y + 1] == _currentValueX)
                                    {
                                        //機會點 加入 相對機會點的位置
                                        DotCheck(ref _chanceMap[_x - 1, _y]).Dot.Add(new Point(0, 1));
                                    }
                                }

                                //最左邊
                                if (_x <= 4)
                                {
                                    if (_data[_x + 3, _y] == _currentValueX)
                                    {
                                        DotCheck(ref _chanceMap[_x + 2, _y]).Dot.Add(new Point(1, 0));
                                    }
                                }

                                //左上
                                if (_x <= 5 && _y >= 1)
                                {
                                    if (_data[_x + 2, _y - 1] == _currentValueX)
                                    {
                                        DotCheck(ref _chanceMap[_x + 2, _y]).Dot.Add(new Point(0, -1));
                                    }
                                }

                                //左下
                                if (_x <= 5 && _y <= 6)
                                {
                                    if (_data[_x + 2, _y + 1] == _currentValueX)
                                    {
                                        DotCheck(ref _chanceMap[_x + 2, _y]).Dot.Add(new Point(0, 1));
                                    }
                                }
                            }
                        }
                        //************找邊間************
                    }
                }
            }

            //PrintArray(_resultX);

            //Console.WriteLine("*************************");

            int _currentValueY = 0;
            int?[,] _resultY = new int?[8, 8];


            for (int _x = 0; _x < 8; _x++)
            {
                for (int _y = 0; _y < 8; _y++)
                {
                    _currentValueY = _data[_x, _y];
                    if (_currentValueY >= 0)    //Value !== Negative
                    {
                        //***********找中洞*************
                        if (_y <= 5)
                        {
                            // O X O
                            if (_data[_x, _y + 2] == _currentValueY)
                            {
                                //找Left面
                                if (_x >= 1)
                                {
                                    if (_data[_x - 1, _y + 1] == _currentValueY)
                                    {
                                        //機會點 加入 相對機會點的位置
                                        DotCheck(ref _chanceMap[_x, _y + 1]).Dot.Add(new Point(-1, 0));
                                    }
                                }

                                //找下Right面
                                if (_x <= 6)
                                {
                                    if (_data[_x + 1, _y + 1] == _currentValueY)
                                    {
                                        //機會點 加入 相對機會點的位置
                                        DotCheck(ref _chanceMap[_x, _y + 1]).Dot.Add(new Point(1, 0));
                                    }
                                }
                            }
                        }
                        //***********找中洞*************

                        //************找邊間************
                        if (_y <= 6) //防止碰到每行最底邊
                        {

                            if (_data[_x, _y + 1] == _currentValueY)
                            {
                                //寫入結果陣列
                                _resultY[_x, _y] = _currentValueY;
                                _resultY[_x, _y + 1] = _currentValueY;

                                //先找頂邊
                                if (_y >= 2)
                                {
                                    if (_data[_x, _y - 2] == _currentValueY)
                                    {
                                        //機會點 加入 相對機會點的位置
                                        DotCheck(ref _chanceMap[_x, _y - 1]).Dot.Add(new Point(0, -1));
                                    }
                                }

                                //左上
                                if (_x >= 1 && _y >= 1)
                                {
                                    if (_data[_x - 1, _y - 1] == _currentValueY)
                                    {
                                        //機會點 加入 相對機會點的位置
                                        DotCheck(ref _chanceMap[_x, _y - 1]).Dot.Add(new Point(-1, 0));
                                    }
                                }

                                //右上
                                if (_x <= 6 && _y >= 1)
                                {
                                    if (_data[_x + 1, _y - 1] == _currentValueY)
                                    {
                                        //機會點 加入 相對機會點的位置
                                        DotCheck(ref _chanceMap[_x, _y - 1]).Dot.Add(new Point(1, 0));
                                    }
                                }

                                //最底邊
                                if (_y <= 4)
                                {
                                    if (_data[_x, _y + 3] == _currentValueY)
                                    {
                                        DotCheck(ref _chanceMap[_x, _y + 2]).Dot.Add(new Point(0, 1));
                                    }
                                }

                                //底左
                                if (_x >= 1 && _y <= 5)
                                {
                                    if (_data[_x - 1, _y + 2] == _currentValueY)
                                    {
                                        DotCheck(ref _chanceMap[_x, _y + 2]).Dot.Add(new Point(-1, 0));
                                    }
                                }

                                //底右
                                if (_x <= 6 && _y <= 5)
                                {
                                    if (_data[_x + 1, _y + 2] == _currentValueY)
                                    {
                                        DotCheck(ref _chanceMap[_x, _y + 2]).Dot.Add(new Point(1, 0));
                                    }
                                }
                            }
                        }
                        //************找邊間************
                    }
                }
            }

            //PrintArray(_resultY);

            //Console.WriteLine("*************************");

            #endregion

            PrintChanceMap(_chanceMap);

            return _chanceMap;
        }

        private ChanceMap DotCheck(ref ChanceMap map)
        {
            if (map == null)
            {
                map = new ChanceMap();
            }
            return map;
        }

        public void PrintArray(int?[,] _Data)
        {
            for (int _y = 0; _y < 8; _y++)
            {
                for (int _x = 0; _x < 8; _x++)
                {
                    Console.Write((_Data[_x, _y] != null ? _Data[_x, _y].Value.ToString() : "-") + " ");
                }
                Console.Write("\n");
            }
        }

        public void PrintChanceMap(ChanceMap[,] map)
        {
            Console.WriteLine("*************************");
            Console.WriteLine("機會地圖");

            for (int _y = 0; _y < 8; _y++)
            {
                for (int _x = 0; _x < 8; _x++)
                {

                    Console.Write(((map[_x, _y] != null) ? map[_x, _y].Dot.Count.ToString() : "-") + " ");

                }
                Console.Write("\n");
            }
        }
    }
}
