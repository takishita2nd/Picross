using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross.logic
{
    partial class PicrossAnalyze
    {
        // 解析パターンその５
        // 端っこ（先頭）が塗られていたら塗る。
        private void pattern5()
        {
            // Row
            pattern5Row();
            // Col
            pattern5Col();
        }

        private void pattern5Row()
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

                // 塗った場所が端っこならそこを塗る
                // マスクされていない部分をリスト化して取得する
                List<List<BitmapData>> data = new List<List<BitmapData>>();
                {
                    List<BitmapData> dataList = new List<BitmapData>();
                    for (int col = 0; col < colNumbers.Count; col++)
                    {
                        if (_bitmapData[row, col].IsMasked() == false)
                        {
                            dataList.Add(_bitmapData[row, col]);
                        }
                        else
                        {
                            if (dataList.Count != 0)
                            {
                                data.Add(dataList);
                                dataList = new List<BitmapData>();
                            }
                        }
                    }
                    if (dataList.Count != 0)
                    {
                        data.Add(dataList);
                    }

                    if (data.Count == 0)
                    {
                        row++;
                        continue;
                    }
                }

                int rowNumberIndex = 0;
                foreach (var dataList in data)
                {
                    // 端っこが塗られているか？
                    if (dataList[0].IsPainted())
                    {
                        // すでに処理済みか？
                        if (rowlist.AnalyzeDatas[rowNumberIndex].IsAnalyzed())
                        {
                            rowNumberIndex++;
                            continue;
                        }

                        // 数字に従ってマスを塗る
                        int count = 0;
                        foreach (var s in dataList)
                        {
                            if (count < rowlist.AnalyzeDatas[rowNumberIndex].Value)
                            {
                                s.Paint();
                                count++;
                            }
                            else
                            {
                                if (s.IsMasked() == false)
                                {
                                    s.Mask();
                                }
                                rowlist.AnalyzeDatas[rowNumberIndex].Analyzed();
                                rowlist.CheckAnalyze();
                                break;
                            }
                        }
                    }
                }
                row++;
            }
        }

        private void pattern5Col()
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

                // 塗った場所が端っこならそこを塗る
                // マスクされていない部分をリスト化して取得する
                List<List<BitmapData>> data = new List<List<BitmapData>>();
                {
                    List<BitmapData> dataList = new List<BitmapData>();
                    for (int row = 0; row < rowNumbers.Count; row++)
                    {
                        if (_bitmapData[row, col].IsMasked() == false)
                        {
                            dataList.Add(_bitmapData[row, col]);
                        }
                        else
                        {
                            if (dataList.Count != 0)
                            {
                                data.Add(dataList);
                                dataList = new List<BitmapData>();
                            }
                        }
                    }
                    if (dataList.Count != 0)
                    {
                        data.Add(dataList);
                    }

                    if (data.Count == 0)
                    {
                        col++;
                        continue;
                    }
                }

                int colNumberIndex = 0;
                foreach (var dataList in data)
                {
                    // 端っこが塗られているか？
                    if (dataList[0].IsPainted())
                    {
                        // すでに処理済みか？
                        if (collist.AnalyzeDatas[colNumberIndex].IsAnalyzed())
                        {
                            colNumberIndex++;
                            continue;
                        }

                        // 数字に従ってマスを塗る
                        int count = 0;
                        foreach (var s in dataList)
                        {
                            if (count < collist.AnalyzeDatas[colNumberIndex].Value)
                            {
                                s.Paint();
                                count++;
                            }
                            else
                            {
                                if (s.IsMasked() == false)
                                {
                                    s.Mask();
                                }
                                collist.AnalyzeDatas[colNumberIndex].Analyzed();
                                collist.CheckAnalyze();
                                break;
                            }
                        }
                    }
                }
                col++;
            }
        }
    }
}
