using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

    class Camera2D
    {
        private SpriteBatch spriteRend;
        private Vector2 camPos;

        public Camera2D(SpriteBatch renderer)
        {
            spriteRend = renderer;
            camPos = new Vector2(0, 0);
        }

        public void DrawNode(Scene2DNode node)
        {
            // get the screen position of the node
            Vector2 drawPosition = ApplyTransformations(node.Position);
            node.Draw(spriteRend, drawPosition);
        }
        private Vector2 ApplyTransformations(Vector2 nodePos)
        {
            Vector2 finalPosition = nodePos-camPos;
            return finalPosition;
        }   
            public Vector2 Position
        {
            get { return camPos; }
            set { camPos = value; }
        }
        public void Translate(Vector2 moveVector)
            {
            camPos += moveVector;
            }

    }
