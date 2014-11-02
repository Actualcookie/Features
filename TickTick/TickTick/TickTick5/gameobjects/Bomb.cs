using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
class Bomb: AnimatedGameObject
    {
    private Vector2 startposition, velocity;
    SoundEffect soundeffect;
    Texture2D 

    public void bomb(ContentManager Content)
    {
        soundeffect = Content.Load<SoundEffect>("kaboom");

    }

    }
