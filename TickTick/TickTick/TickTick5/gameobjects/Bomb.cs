using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;



  class Bomb:AnimatedGameObject
    {
    protected Vector2 bombPosition;
    public bool shoot;

    public Bomb(bool moveToLeft, Vector2 startPosition)
    {
        Player player = GameWorld.Find("player") as Player;
        this.LoadAnimation("Sprites/Player/Bomb", "default", true, 0.2f);
        this.PlayAnimation("default");
        this.Mirror = moveToLeft;
        this.bombPosition = Vector2.Zero;
        Reset();
        this.shoot = false;
    }

    public override void Reset()
    {
        this.Visible = false;
        this.position = new Vector2();
        this.velocity = Vector2.Zero;
        this.shoot = false;
    }
      public override void HandleInput(InputHelper inputHelper)
    {
          
         if (inputHelper.KeyPressed(Keys.Space) && !shoot)
         { shoot = true; }
    }

    public override void Update(GameTime gameTime)
    {
        Player player = GameWorld.Find("player") as Player;
        base.Update(gameTime);
         if (shoot)
        {
            this.Visible = true;
            this.velocity.X = 600;
        }
        this.bombPosition = player.Position;
        if (Mirror)
            this.velocity.X *= -1f;
        CheckEnemyCollision();
        // check if we are outside the screen
        Rectangle screenBox = new Rectangle(0, 0, GameEnvironment.Screen.X, GameEnvironment.Screen.Y);
        if (!screenBox.Intersects(this.BoundingBox))
            this.Reset();
    }

    public void CheckEnemyCollision()
    {
        Rocket rocket = GameWorld.Find("rocket") as Rocket;
        if (this.CollidesWith(rocket) && this.Visible)
        {
            rocket.die = false;
            this.Reset();
        }
    }
  }

