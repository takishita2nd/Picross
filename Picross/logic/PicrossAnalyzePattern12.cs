using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Picross.logic
{
    partial class PicrossAnalyze
    {
        // 解析パターンその１２
        // 確実に塗れない空白部分をマスクする
        private void pattern12()
        {
            // Row
            pattern12Row();
            // Col
            pattern12Col();
        }

        private void pattern12Row()
        {
            int row = 0;
            foreach (var rowlist in rowNumbers)
            {
                if (rowlist.IsAnalyzed())
                {
                    row++;
                    continue;
                }
                rowlist.AnalyzeDatas.Reverse();

                // マスクされていない連続したマスを数える
                var data = getSquareDataListUnMaskedRow(row);
                if(data == null)
                {
                    row++;
                    rowlist.AnalyzeDatas.Reverse();
                    continue;
                }

                // 塗られているマスがあるところを確認する
                int i = 0;
                bool[] flg = new bool[data.Count];
                for(i = 0; i < data.Count; i++)
                {
                    flg[i] = false;
                }
                i = 0;
                int count = 0;
                foreach(var dataList in data)
                {
                    foreach(var p in dataList)
                    {
                        if (p.IsPainted())
                        {
                            flg[i] = true;
                            count++;
                            break;
                        }
                    }
                    i++;
                }

                // 数字の数とflgの数を比較
                if(rowlist.AnalyzeDatas.Count == count)
                {
                    // flgが立っていないマスをマスクする
                    i = 0;
                    foreach(var dataList in data)
                    {
                        if(flg[i] == false)
                        {
                            foreach(var p in dataList)
                            {
                                p.Mask();
                            }
                        }
                        i++;
                    }
                }

                rowlist.AnalyzeDatas.Reverse();
                row++;
            }
        }

        private void pattern12Col()
        {
            int col = 0;
            foreach (var collist in colNumbers)
            {
                if (collist.IsAnalyzed())
                {
                    col++;
                    continue;
                }
                collist.AnalyzeDatas.Reverse();

                // マスクされていない連続したマスを数える
                var data = getSquareDataListUnMaskedCol(col);
                if (data == null)
                {
                    col++;
                    collist.AnalyzeDatas.Reverse();
                    continue;
                }

                // 塗られているマスがあるところを確認する
                int i = 0;
                bool[] flg = new bool[data.Count];
                for (i = 0; i < data.Count; i++)
                {
                    flg[i] = false;
                }
                i = 0;
                int count = 0;
                foreach (var dataList in data)
                {
                    foreach (var p in dataList)
                    {
                        if (p.IsPainted())
                        {
                            flg[i] = true;
                            count++;
                            break;
                        }
                    }
                    i++;
                }

                // 数字の数とflgの数を比較
                if (collist.AnalyzeDatas.Count == count)
                {
                    // flgが立っていないマスをマスクする
                    i = 0;
                    foreach (var dataList in data)
                    {
                        if (flg[i] == false)
                        {
                            foreach (var p in dataList)
                            {
                                p.Mask();
                            }
                        }
                        i++;
                    }
                }
                collist.AnalyzeDatas.Reverse();
                col++;
            }
        }
    }
}
