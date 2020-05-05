using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross.ui.dialog
{
    class Dialog
    {
        private asd.TextureObject2D _texture;

        public Dialog()
        {
            _texture = new asd.TextureObject2D();
            _texture.Position = new asd.Vector2DF(300, 300);
        }

        public void SetEngine()
        {
            asd.Engine.AddObject2D(_texture);
        }

        public void Show()
        {
            _texture.Texture = Resource.getDialogTexture();
        }

        public void Hide()
        {
            _texture.Texture = null;
        }
    }
}
