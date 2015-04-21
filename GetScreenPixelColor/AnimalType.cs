using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace GetScreenPixelColor
{
    static public class AnimalType
    {
        public const int Blue_Elephant = 0;
        public const int Red_Monkey = 1;
        public const int Green_Frog = 2;
        public const int Purple_Hippo = 3;
        public const int Orange_Lion = 4;
        public const int Yellow_Giraffe = 5;
        public const int Black_Panda = 6;
        public const int Color_Random = 7;
        public const int Unknown = 8;

        static public Color GetColorPattern(int type)
        {
            switch (type)
            {
                case -1:
                    return Color.FromRgb(0, 0, 0);
                    break;
                /*
            case AnimalType.Black_Panda:
                return Color.FromRgb(242, 241, 241);
                break;
            case AnimalType.Blue_Elephant:
                return Color.FromRgb(128, 200, 248);
                break;
            case AnimalType.Green_Frog:
                return Color.FromRgb(67, 198, 0);
                break;
            case AnimalType.Orange_Lion:
                return Color.FromRgb(229, 146, 5);
                break;
            case AnimalType.Purple_Hippo:
                return Color.FromRgb(208, 112, 168);
                break;
            case AnimalType.Red_Monkey:
                return Color.FromRgb(248, 96, 70);
                break;
            case AnimalType.Yellow_Giraffe:
                return Color.FromRgb(248, 230, 6);
                break;
                 * */

                    /* Sample level 3*/
                /*case AnimalType.Black_Panda:
                    return Color.FromRgb(110, 109, 109);
                    break;
                case AnimalType.Blue_Elephant:
                    return Color.FromRgb(105, 163, 217);
                    break;
                case AnimalType.Green_Frog:
                    return Color.FromRgb(64, 133, 43);
                    break;
                case AnimalType.Orange_Lion:
                    return Color.FromRgb(203, 142, 4);
                    break;
                case AnimalType.Purple_Hippo:
                    return Color.FromRgb(166, 98, 144);
                    break;
                case AnimalType.Red_Monkey:
                    return Color.FromRgb(134, 37, 22);
                    break;
                case AnimalType.Yellow_Giraffe:
                    return Color.FromRgb(128, 93, 36);
                    break;
                case AnimalType.Color_Random:
                    return Color.FromRgb(0, 255, 0);    //unknown
                    break;
                case AnimalType.Unknown:
                    return Color.FromRgb(255, 0, 0);    //unknown
                    break;*/

                /* Sample level 8*/
                case AnimalType.Black_Panda:
                    return Color.FromRgb(147, 147, 147);
                    break;
                case AnimalType.Blue_Elephant:
                    return Color.FromRgb(96, 157, 214);
                    break;
                case AnimalType.Green_Frog:
                    return Color.FromRgb(67, 166, 23);
                    break;
                case AnimalType.Orange_Lion:
                    return Color.FromRgb(170, 109, 2);
                    break;
                case AnimalType.Purple_Hippo:
                    return Color.FromRgb(191, 101, 159);
                    break;
                case AnimalType.Red_Monkey:
                    return Color.FromRgb(194, 69, 44);
                    break;
                case AnimalType.Yellow_Giraffe:
                    return Color.FromRgb(190, 164, 27);
                    break;
                case AnimalType.Color_Random:
                    return Color.FromRgb(0, 255, 0);    //unknown
                    break;
                case AnimalType.Unknown:
                    return Color.FromRgb(255, 0, 0);    //unknown
                    break;
            }
            return Color.FromRgb(255, 0, 0);    //unknown
        }
    }
}
