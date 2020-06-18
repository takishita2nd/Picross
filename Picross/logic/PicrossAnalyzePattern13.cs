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
        // 解析パターンその１３
        // 確実に塗れない空白部分をマスクする
        private void pattern13()
        {
            // Row
            pattern13RowFront();
            pattern13RowBack();
            // Col
            pattern13ColFront();
            pattern13ColBack();
        }

        private void pattern13RowFront()
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

                // 先頭の数字が解析済みであるか？
                if (rowlist.AnalyzeDatas[0].IsAnalyzed() == false)
                {
                    row++;
                    rowlist.AnalyzeDatas.Reverse();
                    continue;
                }

                // マスクされていない連続したマスを数える
                var dataList = getSquareDataListUnMaskedRow(row);
                if(dataList == null)
                {
                    row++;
                    rowlist.AnalyzeDatas.Reverse();
                    continue;
                }

                // 解析済みのマスを確認する
                foreach(var data in dataList)
                {
                    bool check = false;
                    int count = 0;
                    foreach(var p in data)
                    {
                        if (p.IsPainted())
                        {
                            count++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if(count == rowlist.AnalyzeDatas[0].Value)
                    {
                        foreach(var data2 in dataList)
                        {
                            if (data2.Equals(data) == false)
                            {
                                foreach(var p in data2)
                                {
                                    p.Mask();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        check = true;
                    }
                    if (check)
                    {
                        break;
                    }
                }

                rowlist.AnalyzeDatas.Reverse();
                row++;
            }
        }

        private void pattern13RowBack()
        {
            int row = 0;
            foreach (var rowlist in rowNumbers)
            {
                if (rowlist.IsAnalyzed())
                {
                    row++;
                    continue;
                }

                // 先頭の数字が解析済みであるか？
                if (rowlist.AnalyzeDatas[0].IsAnalyzed() == false)
                {
                    row++;
                    continue;
                }

                // マスクされていない連続したマスを数える
                var dataList = getSquareDataListUnMaskedRow(row);
                if (dataList == null)
                {
                    row++;
                    continue;
                }
                dataList.Reverse();

                // 解析済みのマスを確認する
                foreach (var data in dataList)
                {
                    bool check = false;
                    int count = 0;
                    foreach (var p in data)
                    {
                        if (p.IsPainted())
                        {
                            count++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (count == rowlist.AnalyzeDatas[0].Value)
                    {
                        foreach (var data2 in dataList)
                        {
                            if (data2.Equals(data) == false)
                            {
                                foreach (var p in data2)
                                {
                                    p.Mask();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        check = true;
                    }
                    if (check)
                    {
                        break;
                    }
                }

                row++;
            }
        }

        private void pattern13ColFront()
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

                // 先頭の数字が解析済みであるか？
                if (collist.AnalyzeDatas[0].IsAnalyzed() == false)
                {
                    col++;
                    collist.AnalyzeDatas.Reverse();
                    continue;
                }

                // マスクされていない連続したマスを数える
                var dataList = getSquareDataListUnMaskedCol(col);
                if (dataList == null)
                {
                    col++;
                    collist.AnalyzeDatas.Reverse();
                    continue;
                }

                // 解析済みのマスを確認する
                foreach (var data in dataList)
                {
                    bool check = false;
                    int count = 0;
                    foreach (var p in data)
                    {
                        if (p.IsPainted())
                        {
                            count++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (count == collist.AnalyzeDatas[0].Value)
                    {
                        foreach (var data2 in dataList)
                        {
                            if (data2.Equals(data) == false)
                            {
                                foreach (var p in data2)
                                {
                                    p.Mask();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        check = true;
                    }
                    if (check)
                    {
                        break;
                    }
                }

                collist.AnalyzeDatas.Reverse();
                col++;
            }
        }

        private void pattern13ColBack()
        {
            int col = 0;
            foreach (var collist in colNumbers)
            {
                if (collist.IsAnalyzed())
                {
                    col++;
                    continue;
                }

                // 先頭の数字が解析済みであるか？
                if (collist.AnalyzeDatas[0].IsAnalyzed() == false)
                {
                    col++;
                    continue;
                }

                // マスクされていない連続したマスを数える
                var dataList = getSquareDataListUnMaskedCol(col);
                if (dataList == null)
                {
                    col++;
                    continue;
                }
                dataList.Reverse();

                // 解析済みのマスを確認する
                foreach (var data in dataList)
                {
                    bool check = false;
                    int count = 0;
                    foreach (var p in data)
                    {
                        if (p.IsPainted())
                        {
                            count++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (count == collist.AnalyzeDatas[0].Value)
                    {
                        foreach (var data2 in dataList)
                        {
                            if (data2.Equals(data) == false)
                            {
                                foreach (var p in data2)
                                {
                                    p.Mask();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        check = true;
                    }
                    if (check)
                    {
                        break;
                    }
                }

                col++;
            }
        }
    }
}
