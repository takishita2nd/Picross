using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross.logic
{
    class BitmapData
    {
        public int Row { get; }
        public int Col { get; }
        // 塗るで確定
        private bool _isPaint;
        // 塗らないで確定
        private bool _isFilter;

        public BitmapData(int row, int col)
        {
            Row = row;
            Col = col;
            _isPaint = false;
            _isFilter = false;
        }

        public bool IsPainted()
        {
            return _isPaint;
        }

        public bool IsFilted()
        {
            return _isFilter;
        }

        public bool IsValid()
        {
            if(_isPaint | _isFilter)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
