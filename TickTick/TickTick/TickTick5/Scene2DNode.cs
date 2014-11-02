using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Scene2DNode
    {
        private Texture2D texture;
        private Vector2 worldPosition;

        public Scene2DNode(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.worldPosition = position;
        }
        public void Draw(SpriteBatch renderer, Vector2 drawPosition)
        {
            renderer.Draw(texture, drawPosition, Color.White);
        }
        public Vector2 Position
        {
            get { return worldPosition; }
            set { worldPosition = value; }
        }
       

    }
