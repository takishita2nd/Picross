using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross
{
    class Resource
    {
        private static asd.Texture2D _picrossTexture = null;
        public static asd.Texture2D GetPicrossTexture()
        {
            if (_picrossTexture == null)
            {
                _picrossTexture = asd.Engine.Graphics.CreateTexture2D("square.png");
            }
            return _picrossTexture;
        }
    }
}
