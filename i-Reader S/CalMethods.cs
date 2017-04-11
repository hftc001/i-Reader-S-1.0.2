using System;
using System.Collections.Generic;
using System.Linq;

namespace i_Reader_S
{
    internal class CalMethods
    {
        /// <summary>
        /// CCD数据分析工具：正常样本片分析
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>

        public static string CalCcdod(int[,] data)
        {
            try
            {
            //1、找到中间线
            int midY = 0;
            //如果是灰度片，需固定中间线位置为1016/2-80
            if (ReaderS.LocationCcd == -1)
            {
                midY = 428;
            }
            //普通片需要查找中间线
            else
            {
                int[] data1 = new int[data.GetLength(0)];
                for (int i = 0; i < data.GetLength(0); i++)
                {
                    for (int j = 0; j < data.GetLength(1); j++)
                    {
                        data1[i] += data[i, j];
                    }
                    data1[i] /= data.GetLength(1);
                }

                int UpX = 0;
                int DownX = 0;
                for (int i = 0; i < data1.Length; i++)
                {
                    if (data1[i] < data1.Min() + data1.Max() / 4 - data1.Min() / 4)
                    {
                        UpX = i;
                        break;
                    }
                }
                if (UpX == 0) return "-9";
                for (int i = data1.Length - 1; i > -1; i--)
                {
                    if (data1[i] < data1.Min() + data1.Max() / 4 - data1.Min() / 4)
                    {
                        DownX = i;
                        break;
                    }
                }
                if (DownX == 0) return "-9";
                midY = DownX / 2 + UpX / 2-80;
            }
            //有效区域取平均
            int[] data2 = new int[data.GetLength(1)];
            for (int i = 0; i < data2.Length; i++)
            {
                for (int j = midY; j < midY + 160; j++)
                {
                    data2[i] += data[j, i];
                }
                data2[i] /= 160;
            }

            int c1X=0;
            int c1Y=0;
            int c2X=0;
            int c2Y=0;
            int tx=0;
            int ty=0;
            int minx = 0;
            int maxx = 0;
            int miny = 5000;
            int maxy = 0;

            if (ReaderS.LocationCcd == -1)
            {
                c1X = 338;
                tx = 638;
                c2X = 938;
                c1Y = (int)(data2.Take(c1X + 10).Reverse().Take(20).Average());
                c2Y = (int)(data2.Take(c2X + 10).Reverse().Take(20).Average());
                ty = (int)(data2.Take(tx + 10).Reverse().Take(20).Average());
            }
            else
            {
                int LeftX = 0;
                int RightX = 0;

                for (int i = 100; i < data2.Length-100; i++)
                {
                    if (data2.Take(i + 50).Reverse().Take(150).Min() == data2[i])
                    {
                        LeftX = i;break;
                    }
                }

                if (LeftX == 0) return "-9";
                
                for (int i = data2.Length- 101; i >100; i--)
                {
                    if (data2.Take(i + 100).Reverse().Take(150).Min() == data2[i])
                    {
                        RightX = i; break;
                    }
                }

                if (RightX < LeftX+600) return "-9";

                for (int i = 0; i < RightX / 3 - LeftX / 3; i++)
                {
                    if (data2[LeftX + i] > c1Y)
                    {
                        c1Y = data2[LeftX + i];
                        c1X = LeftX + i;
                    }
                    if (data2[LeftX + RightX / 3 - LeftX / 3 + i] > ty)
                    {
                        ty = data2[LeftX + RightX / 3 - LeftX / 3 + i];
                        tx = LeftX + RightX / 3 - LeftX / 3 + i;
                    }

                    if (data2[LeftX + RightX / 3 - LeftX / 3 + i] < miny)
                    {
                        minx = LeftX + RightX / 3 - LeftX / 3 + i;
                        miny = data2[minx];
                    }

                    if (data2[LeftX + RightX / 3 - LeftX / 3 + RightX / 3 - LeftX / 3 + i] > c2Y)
                    {
                        c2Y = data2[LeftX+ RightX / 3 - LeftX / 3+ RightX / 3 - LeftX / 3 + i];
                        c2X = LeftX+ RightX / 3 - LeftX / 3+ RightX / 3 - LeftX / 3 + i;
                    }
                }

                for (int i = 0; i < data2.Length; i++)
                {
                    if (data2[i] > maxy)
                    {
                        maxy = data2[i];
                        maxx = i;
                    }
                }

            }
            var str = "";
            if (c1Y + c2Y - miny * 2 < 1000 & ReaderS.LocationCcd!=-1) str = "-11^";

            //Max(71, 4020); Min(481, 566); C1(323, 2192); T(619, 1040); C2(925, 2152); nstartY(425)
            return str+$"Max({maxx}, {maxy}); Min({minx}, {miny}); C1({c1X}, {c1Y}); T({tx}, {ty}); C2({c2X}, {c2Y}); nstartY({midY})";
            }
            catch (Exception)
            {
                return "-9";
            }
        }

        public static string CalTurn(int[,] data, string str)
        {
            try
            {
                var tx = str.Substring(str.IndexOf("T(", StringComparison.Ordinal) + 2);
                int Tx = int.Parse(tx.Substring(0, tx.IndexOf(",", StringComparison.Ordinal)));

                int h = data.GetLength(0);

                int[] datarow = MyMath.MyMath.RowMean(data);
                int[] datacol = MyMath.MyMath.ColMean(data);
                int data1 = datarow.Max() / 4 + 3 * datarow.Min() / 4;
                int data2 = datacol.Max() / 4 + 3 * datacol.Min() / 4;
                int[,] subrow1 = MyMath.MyMath.SubArray(data, 0, h - 1, Tx - 210, Tx - 190);
                int[,] subrow2 = MyMath.MyMath.SubArray(data, 0, h - 1, Tx + 190, Tx + 210);
                int[] subrow11 = MyMath.MyMath.ColMean(subrow1);
                int[] subrow22 = MyMath.MyMath.ColMean(subrow2);
                int cx11 = 0;
                int cx21 = 0;
                int cx12 = 0;
                int cx22 = 0;
                for (int i = 0; i < subrow11.Length; i++)
                {
                    if (subrow11[i] < data1)
                    {
                        cx11 = i;
                        break;
                    }
                }
                for (int i = subrow11.Length - 1; i > -1; i--)
                {
                    if (subrow11[i] < data1)
                    {
                        cx21 = i;
                        break;
                    }
                }
                for (int i = 0; i < subrow22.Length; i++)
                {
                    if (subrow22[i] < data1)
                    {
                        cx12 = i;
                        break;
                    }
                }
                for (int i = subrow22.Length - 1; i > -1; i--)
                {
                    if (subrow11[i] < data1)
                    {
                        cx22 = i;
                        break;
                    }
                }
                int Rdiff = (cx11 + cx21) / 2 - (cx12 + cx22) / 2;

                return str + $"; Rdiff({Rdiff})";
            }
            catch (Exception)
            {
                return "-9";
            }
        }





        /// <summary>
        /// 荧光计算
        /// </summary>
        /// <param name="fluodata">荧光数据</param>
        /// <returns></returns>
        public static string CalFluo(List<double> fluodata)
        {
            try
            {
                var dataCount = fluodata.Count;
                var point = dataCount / 20;
                var cx = 0;
                var tx = 0;
                double cy = 0;
                double ty = 0;
                var tcspan = dataCount * 90 / 180;
                for (var i = dataCount / 10; i < dataCount * 9 / 10; i++)
                {
                    if (fluodata[i] > cy)
                    {
                        cx = i;
                        cy = fluodata[i];
                    }
                }
                if (cx > dataCount / 2 & cy >= fluodata[cx + 1])
                {
                    tx = cx;
                    ty = cy;
                    cx = 0;
                    cy = 0;
                    for (var i = Math.Max(0, tx - tcspan - 2 * point); i < tx - tcspan + 2 * point; i++)
                    {
                        if (fluodata[i] > cy)
                        {
                            cy = fluodata[i];
                            cx = i;
                        }
                    }
                    if (cx == Math.Max(0, tx - tcspan - 2 * point) | cx == tx - tcspan + 2 * point - 1)
                    {
                        cx = tx - tcspan;
                        cy = fluodata[cx];
                    }
                }
                else if (cx < dataCount / 2 & cy >= fluodata[cx - 1])
                {
                    for (var i = cx + tcspan - 2 * point; i < Math.Min(cx + tcspan + 2 * point, fluodata.Count); i++)
                    {
                        if (fluodata[i] > ty)
                        {
                            ty = fluodata[i];
                            tx = i;
                        }
                    }
                    if (cx == tx - tcspan - 2 * point | cx == Math.Min(cx + tcspan + 2 * point, fluodata.Count) - 1)
                    {
                        tx = cx + tcspan;
                        ty = fluodata[tx];
                    }
                }
                double sumC = 0;
                double sumT = 0;
                for (var i = -point; i < point; i++)
                {
                    sumC += fluodata[cx + i];
                    sumT += fluodata[tx + i];
                }
                double base1 = 0;
                double base2 = 0;
                double base3 = 0;
                double base4 = 0;
                for (var i = cx + point * 2; i < cx + point * 3; i++)
                {
                    base2 += fluodata[i];
                }
                for (var i = tx - point * 3; i < tx - point * 2; i++)
                {
                    base3 += fluodata[i];
                }
                if (cx - point * 2 - Math.Max(0, cx - point * 3) < 1)
                {
                    base1 = base2;
                }
                else
                {
                    for (var i = Math.Max(0, cx - point * 3); i < cx - point * 2; i++)
                    {
                        base1 += fluodata[i];
                    }
                    base1 = base1 / (cx - point * 2 - Math.Max(0, cx - point * 3)) * point;
                }
                if (Math.Min(dataCount - 1, tx + point * 3) - tx - point * 2 < 1)
                {
                    base4 = base3;
                }
                else
                {
                    for (var i = tx + point * 2; i < Math.Min(dataCount - 1, tx + point * 3); i++)
                    {
                        base4 += fluodata[i];
                    }
                    base4 = base4 / (Math.Min(dataCount - 1, tx + point * 3) - tx - point * 2) * point;
                }
                var sumCBase = sumC - base1 - base2;
                var sumTBase = sumT - base3 - base4;
                var ii = 1;
                while (sumCBase < 0 & ii < 11)
                {
                    sumCBase += (Math.Max(base2, base1) - Math.Min(base2, base1)) * ii / 10;
                    ii++;
                }
                ii = 1;
                while (sumTBase < 0 & ii < 11)
                {
                    sumTBase += (Math.Max(base4, base3) - Math.Min(base4, base3)) * ii / 10;
                    ii++;
                }
                var strOd = "C(%1,%2); T(%3,%4); SumCBase(%5); SumTBase(%6)";
                strOd = strOd.Replace("%1", cx.ToString());
                strOd = strOd.Replace("%2", cy.ToString("F2"));
                strOd = strOd.Replace("%3", tx.ToString());
                strOd = strOd.Replace("%4", ty.ToString("F2"));
                strOd = strOd.Replace("%5", sumCBase.ToString("F2"));
                strOd = strOd.Replace("%6", sumTBase.ToString("F2"));
                return strOd;
            }
            catch (Exception)
            {
                return "Error:非正常测试片";
            }
        }
    }
}
