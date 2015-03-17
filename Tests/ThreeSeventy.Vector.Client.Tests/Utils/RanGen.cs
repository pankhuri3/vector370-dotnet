using System;
using System.Globalization;

namespace ThreeSeventy.Vector.Client.Tests
{
    public static class RanGen
    {
        public static string Str
        {
            get
            {
                var rand = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
                var rand1 = rand.Next(1, 10000);

                var returnStr = "";
                for (int i = 0; i < 4; i++)
                {
                    rand = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
                    var ranChar = rand.Next(65, 90);
                    returnStr += (char)ranChar;
                }
                return returnStr + rand1.ToString(CultureInfo.InvariantCulture);
            }
        }

        public static string Word
        {
            get
            {
                var returnStr = "";
                for (int i = 0; i < 4; i++)
                {
                    var rand = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
                    var ranChar = rand.Next(65, 90);
                    returnStr += (char)ranChar;
                }
                return returnStr;
            }
        }

        public static int Int
        {
            get
            {
                var rand = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
                return rand.Next(1, 1000);
            }
        }

        public static double Double
        {
            get
            {
                var rand = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
                var part1 = rand.Next(1, 1000);

                var rand2 = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
                var part2 = rand2.Next(0, 100);

                return (double)part1 + ((double)part2 / 100);
            }
        }

        public static DateTime DateTime
        {
            get
            {
                var start = new DateTime(1995, 1, 1);
                var rand = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));

                var range = (DateTime.Today - start).Days;
                return start.AddDays(rand.Next(range));
            }
        }

        public static bool Boolean
        {
            get
            {
                var rand = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
                return rand.Next(0, 10) > 5;
            }
        }

        public static string BinaryStr
        {
            get
            {
                var str = "";
                for (var i = 0; i < 10; i++)
                {
                    var rand = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
                    str += ((rand.Next() % 2 == 0) ? "0" : "1");
                }
                return str;
            }
        }

        public static int GetIntInRange(int min, int max)
        {
            var rand = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            return rand.Next(min, max);
        }

        public static string GetWord(int lenght)
        {
            var returnStr = "";
            for (int i = 0; i < lenght; i++)
            {
                var rand = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
                var ranChar = rand.Next(65, 90);
                returnStr += (char)ranChar;
            }
            return returnStr;
        }

        public static string Password
        {
            get { return GetWord(7) + Int + GetSpecialSymbolsStr(2); }
        }

        public static string GetSpecialSymbolsStr(int lenght)
        {
            var returnStr = "";
            for (int i = 0; i < lenght; i++)
            {
                var rand = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
                var ranChar = rand.Next(33, 46);
                returnStr += (char)ranChar;
            }
            return returnStr;
        }

        public static string UsaNumber
        {
            get
            {
                //var areas = new int[]{325, 330, 234, 518, 229, 957, 505, 320, 730, 618, 657, 909};

                //var rand = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
                //var area = rand.Next(0, areas.Length);

                //rand = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
                //var oper = rand.Next(100, 999);

                var rand = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
                var numb = rand.Next(1000, 9999);

                return string.Concat("+1234200", numb);
            }
        }

        public static string Url
        {
            get { return string.Concat("http://www.", Str, ".com"); }
        }

        public static string Email
        {
            get { return string.Concat(Str, "@", Str, ".com"); }
        }

        public static string StringBlock(int size)
        {
            var message = "";
            var symbol = GetWord(1);
            for (int i = 0; i < size; i++) message += symbol;
            return message;
        }
    }
}
