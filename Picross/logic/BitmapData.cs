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
        private bool _isMask;

        public BitmapData(int row, int col)
        {
            Row = row;
            Col = col;
            _isPaint = false;
            _isMask = false;
        }

        public bool IsPainted()
        {
            return _isPaint;
        }

        public bool IsMasked()
        {
            return _isMask;
        }

        public bool IsValid()
        {
            if(_isPaint | _isMask)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Paint()
        {
            if(_isMask)
            {
                return false;
            }
            _isPaint = true;
            return true;
        }

        public bool Mask()
        {
            if (_isPaint)
            {
                return false;
            }
            _isMask = true;
            return true;
        }
    }
}
