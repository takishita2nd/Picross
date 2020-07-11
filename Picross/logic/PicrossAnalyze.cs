using Picross.ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross.logic
{
    partial class PicrossAnalyze
    {
        private List<AnalyzeListData> rowNumbers;
        private List<AnalyzeListData> colNumbers;
        private BitmapData[,] _bitmapData;
        private int oldPaintedCount;

        public PicrossAnalyze(List<List<NumberSquare>> rowNumberSquares, List<List<NumberSquare>> colNumberSquares)
        {
            oldPaintedCount = 0;
            _bitmapData = new BitmapData[rowNumberSquares.Count, colNumberSquares.Count];
            for (int r = 0; r < rowNumberSquares.Count; r++)
            {
                for (int c = 0; c < colNumberSquares.Count; c++)
                {
                    _bitmapData[r, c] = new BitmapData(r, c);
                }
            }

            rowNumbers = new List<AnalyzeListData>();
            foreach (var rowList in rowNumberSquares)
            {
                AnalyzeListData list = new AnalyzeListData();
                foreach (var s in rowList)
                {
                    if (s.GetValue() != 0)
                    {
                        list.AnalyzeDatas.Add(new AnalyzeData(s.GetValue()));
                    }
                }
                rowNumbers.Add(list);
            }

            colNumbers = new List<AnalyzeListData>();
            foreach (var colList in colNumberSquares)
            {
                AnalyzeListData list = new AnalyzeListData();
                foreach (var s in colList)
                {
                    if (s.GetValue() != 0)
                    {
                        list.AnalyzeDatas.Add(new AnalyzeData(s.GetValue()));
                    }
                }
                colNumbers.Add(list);
            }
        }

        public BitmapData[,] Run()
        {
            int roop = 0;
            pattern1();
            //do
            //{
            //    pattern2();
            //    pattern3();
            //    pattern4();
            //    pattern5();
            //    pattern6();
            //    pattern7();
            //    pattern8();
            //    pattern9();
            //    pattern10();
            //    pattern11();
            //    pattern12();
            //    pattern13();
            //    pattern14();
            //    doMask();
            //    checkAnalyze();
            //    if (checkPainedCount() == false)
            //    {
            //        roop++;
            //    }
            //} while (roop < 3);
            //int roop = 0;
            while (roop < 7)
            {
                pattern2();
                pattern3();
                pattern4();
                pattern5();
                pattern6();
                pattern7();
                pattern8();
                pattern9();
                pattern10();
                pattern11();
                pattern12();
                pattern13();
                pattern14();
                doMask();
                checkAnalyze();
                roop++;
            }

            return _bitmapData;
        }

        private bool checkPainedCount()
        {
            int newPaintedCount = 0;
            for (int row = 0; row < rowNumbers.Count; row++)
            {
                for (int col = 0; col < colNumbers.Count; col++)
                {
                    if (_bitmapData[row, col].IsValid())
                    {
                        newPaintedCount++;
                    }
                }
            }
            if (oldPaintedCount == newPaintedCount)
            {
                return false;
            }
            oldPaintedCount = newPaintedCount;
            return true;
        }

        private void doMask()
        {
            doMaskRow();
            doMaskCol();
        }

        private void doMaskRow()
        {
            int row = 0;
            foreach (var list in rowNumbers)
            {
                if (list.IsAnalyzed())
                {
                    for (int col = 0; col < colNumbers.Count; col++)
                    {
                        if (_bitmapData[row, col].IsValid() == false)
                        {
                            _bitmapData[row, col].Mask();
                        }
                    }
                }
                row++;
            }
        }

        private void doMaskCol()
        {
            int col = 0;
            foreach (var list in colNumbers)
            {
                if (list.IsAnalyzed())
                {
                    for (int row = 0; row < rowNumbers.Count; row++)
                    {
                        if (_bitmapData[row, col].IsValid() == false)
                        {
                            _bitmapData[row, col].Mask();
                        }
                    }
                }
                col++;
            }
        }

        private void checkAnalyze()
        {
            checkAnalyzeRowBefore();
            checkAnalyzeRowAfter();
            checkAnalyzeColBefore();
            checkAnalyzeColAfter();
        }

        private void checkAnalyzeRowBefore()
        {
            int row = 0;
            foreach (var rowList in rowNumbers)
            {
                if (rowList.IsAnalyzed())
                {
                    row++;
                    continue;
                }

                rowList.AnalyzeDatas.Reverse();
                int rowIndex = 0;
                foreach(var rowData in rowList.AnalyzeDatas)
                {
                    if (rowData.IsAnalyzed())
                    {
                        rowIndex++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (rowIndex == rowList.AnalyzeDatas.Count)
                {
                    rowList.AnalyzeDatas.Reverse();
                    row++;
                    continue;
                }

                int painted = 0;
                int count = 0;
                for (int col = 0; col < colNumbers.Count; col++)
                {
                    if (_bitmapData[row, col].IsPainted())
                    {
                        painted++;
                    }
                    else if (_bitmapData[row, col].IsMasked())
                    {
                        if(count == rowIndex)
                        {
                            if (rowList.AnalyzeDatas[rowIndex].Value == painted)
                            {
                                rowList.AnalyzeDatas[rowIndex].Analyzed();
                                rowList.CheckAnalyze();
                                rowList.AnalyzeDatas.Reverse();
                                row++;
                                break;
                            }
                            else if (col + 1 == colNumbers.Count)
                            {
                                rowList.AnalyzeDatas.Reverse();
                                row++;
                            }
                        }
                        else if(painted != 0)
                        {
                            painted = 0;
                            count++;
                        }
                    }
                    else
                    {
                        rowList.AnalyzeDatas.Reverse();
                        row++;
                        break;
                    }
                }
            }
        }

        private void checkAnalyzeRowAfter()
        {
            int row = 0;
            foreach (var rowList in rowNumbers)
            {
                if (rowList.IsAnalyzed())
                {
                    row++;
                    continue;
                }

                int rowIndex = 0;
                foreach (var rowData in rowList.AnalyzeDatas)
                {
                    if (rowData.IsAnalyzed())
                    {
                        rowIndex++;
                    }
                    else
                    {
                        break;
                    }
                }
                if(rowIndex == rowList.AnalyzeDatas.Count)
                {
                    row++;
                    continue;
                }

                int painted = 0;
                int count = 0;
                for (int col = colNumbers.Count - 1; col >= 0; col--)
                {
                    if (_bitmapData[row, col].IsPainted())
                    {
                        painted++;
                    }
                    else if (_bitmapData[row, col].IsMasked())
                    {
                        if (count == rowIndex)
                        {
                            if (rowList.AnalyzeDatas[rowIndex].Value == painted)
                            {
                                rowList.AnalyzeDatas[rowIndex].Analyzed();
                                rowList.CheckAnalyze();
                                row++;
                                break;
                            }
                            else if (col == 0)
                            {
                                rowList.AnalyzeDatas.Reverse();
                                row++;
                            }
                        }
                        else if (painted != 0)
                        {
                            painted = 0;
                            count++;
                        }
                    }
                    else
                    {
                        row++;
                        break;
                    }
                }
            }
        }

        private void checkAnalyzeColBefore()
        {
            int col = 0;
            foreach (var colList in colNumbers)
            {
                if (colList.IsAnalyzed())
                {
                    col++;
                    continue;
                }

                colList.AnalyzeDatas.Reverse();
                int colIndex = 0;
                foreach (var colData in colList.AnalyzeDatas)
                {
                    if (colData.IsAnalyzed())
                    {
                        colIndex++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (colIndex == colList.AnalyzeDatas.Count)
                {
                    colList.AnalyzeDatas.Reverse();
                    col++;
                    continue;
                }
                int painted = 0;
                int count = 0;
                for (int row = 0; row < rowNumbers.Count; row++)
                {
                    if (_bitmapData[row, col].IsPainted())
                    {
                        painted++;
                    }
                    else if (_bitmapData[row, col].IsMasked())
                    {
                        if (count == colIndex)
                        {
                            if (colList.AnalyzeDatas[colIndex].Value == painted)
                            {
                                colList.AnalyzeDatas[colIndex].Analyzed();
                                colList.CheckAnalyze();
                                colList.AnalyzeDatas.Reverse();
                                col++;
                                break;
                            }
                            else if(row + 1 == rowNumbers.Count)
                            {
                                colList.AnalyzeDatas.Reverse();
                                col++;
                            }
                        }
                        else if (painted != 0)
                        {
                            painted = 0;
                            count++;
                        }
                    }
                    else
                    {
                        colList.AnalyzeDatas.Reverse();
                        col++;
                        break;
                    }
                }
            }
        }

        private void checkAnalyzeColAfter()
        {
            int col = 0;
            foreach (var colList in colNumbers)
            {
                if (colList.IsAnalyzed())
                {
                    col++;
                    continue;
                }

                int colIndex = 0;
                foreach (var colData in colList.AnalyzeDatas)
                {
                    if (colData.IsAnalyzed())
                    {
                        colIndex++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (colIndex == colList.AnalyzeDatas.Count)
                {
                    col++;
                    continue;
                }
                int painted = 0;
                int count = 0;
                for (int row = rowNumbers.Count - 1; row >= 0; row--)
                {
                    if (_bitmapData[row, col].IsPainted())
                    {
                        painted++;
                    }
                    else if (_bitmapData[row, col].IsMasked())
                    {
                        if (count == colIndex)
                        {
                            if (colList.AnalyzeDatas[colIndex].Value == painted)
                            {
                                colList.AnalyzeDatas[colIndex].Analyzed();
                                colList.CheckAnalyze();
                                col++;
                                break;
                            }
                            else if (row == 0)
                            {
                                colList.AnalyzeDatas.Reverse();
                                col++;
                            }
                        }
                        else if (painted != 0)
                        {
                            painted = 0;
                            count++;
                        }
                    }
                    else
                    {
                        col++;
                        break;
                    }
                }
            }
        }

        private List<List<BitmapData>> getSquareDataListUnMaskedRow(int row)
        {
            List<List<BitmapData>> data = new List<List<BitmapData>>();
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
                return null;
            }

            return data;
        }

        private List<List<BitmapData>> getSquareDataListUnMaskedCol(int col)
        {
            List<List<BitmapData>> data = new List<List<BitmapData>>();
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
                return null;
            }

            return data;
        }
    }
}
