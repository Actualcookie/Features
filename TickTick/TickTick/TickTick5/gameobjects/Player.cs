using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

partial class Player : AnimatedGameObject
{
    protected Vector2 startPosition;
    protected bool isOnTheGround;
    protected float previousYPosition;
    protected bool isAlive;
    protected bool exploded;
    protected bool finished;
    protected bool walkingOnIce, walkingOnHot;
    List<Bomb> bombList;
    
    
    public Player(Vector2 start) : base(2, "player")
    {
        this.LoadAnimation("Sprites/Player/spr_idle", "idle", true); 
        this.LoadAnimation("Sprites/Player/spr_run@13", "run", true, 0.05f);
        this.LoadAnimation("Sprites/Player/spr_jump@14", "jump", false, 0.05f); 
        this.LoadAnimation("Sprites/Player/spr_celebrate@14", "celebrate", false, 0.05f);
        this.LoadAnimation("Sprites/Player/spr_die@5", "die", false);
        this.LoadAnimation("Sprites/Player/spr_explode@5x5", "explode", false, 0.04f); 
        startPosition = start;
        Reset();
    }

    public override void Reset()
    {
        this.position = startPosition;
        this.velocity = Vector2.Zero;
        isOnTheGround = true;
        isAlive = true;
        exploded = false;
        finished = false;
        walkingOnIce = false;
        walkingOnHot = false;
        this.PlayAnimation("idle");
        previousYPosition = BoundingBox.Bottom;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        float walkingSpeed = 400;
        if (walkingOnIce)
            walkingSpeed *= 1.5f;
        if (!isAlive)
            return;
        if (inputHelper.IsKeyDown(Keys.Left))
            velocity.X = -walkingSpeed;
        else if (inputHelper.IsKeyDown(Keys.Right))
            velocity.X = walkingSpeed;
        else if (!walkingOnIce && isOnTheGround)
            velocity.X = 0.0f;
        if (velocity.X != 0.0f)
            Mirror = velocity.X < 0;
        if (inputHelper.KeyPressed(Keys.Up) && isOnTheGround)
            Jump();
        if (inputHelper.MouseLeftButtonPressed()&&  BoundingBox.Contains((int)inputHelper.MousePosition.X, (int)inputHelper.MousePosition.Y))
            Sounds();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
       if (!finished && isAlive)
        {
            if (isOnTheGround)
                if (velocity.X == 0)
                    this.PlayAnimation("idle");
                else
                    this.PlayAnimation("run");
            else if (velocity.Y < 0)
                this.PlayAnimation("jump");

            TimerGameObject timer = GameWorld.Find("timer") as TimerGameObject;
            if (walkingOnHot)
                timer.Multiplier = 2;
            else if (walkingOnIce)
                timer.Multiplier = 0.5;
            else
                timer.Multiplier = 1;

            TileField tiles = GameWorld.Find("tiles") as TileField;
            if (BoundingBox.Top >= tiles.Rows * tiles.CellHeight)
                this.Die(true);
        }

        DoPhysics();
    }

    public void Explode()
    {
        if (!isAlive || finished)
            return;
        isAlive = false;
        exploded = true;
        velocity = Vector2.Zero;
        position.Y += 15;
        this.PlayAnimation("explode");
    }

    public void Die(bool falling)
    {
        if (!isAlive || finished)
            return;
        isAlive = false;
        velocity.X = 0.0f;
        if (falling)
            GameEnvironment.AssetManager.PlaySound("Sounds/snd_player_fall");
        else
        {
            velocity.Y = -900;
            GameEnvironment.AssetManager.PlaySound("Sounds/snd_player_die");
        }
        this.PlayAnimation("die");
    }

     public void Shoot()
	    {
	        Bomb bomb = new Bomb();
	       bomb.Position= new Vector2(this.Position.X, this.Position.Y - BoundingBox.Height/2);
           if(Mirror)
	            bomb.Velocity *= -1;
	        else 
	            bomb.Velocity *= 1;
	
	        bombList.Add(bomb);
	        GameWorld.Add(bomb);
	
	    }

    public void Sounds()
    {
        Random r = new Random();
        int x = r.Next(0, 11);
        switch (x)
        {
            case 1:
                GameEnvironment.AssetManager.PlaySound("Sounds/snd_player_die");
                break;
            case 2:
                GameEnvironment.AssetManager.PlaySound("Sounds/snd_player_jump");
                break;
            case 3:
                GameEnvironment.AssetManager.PlaySound("Sounds/game_over");
                    break;
            case 4:
                    GameEnvironment.AssetManager.PlaySound("Sounds/gotta_hurt");
                    break;
            case 5:
                    GameEnvironment.AssetManager.PlaySound("Sounds/hail");
                    break;
            case 6:
                    GameEnvironment.AssetManager.PlaySound("Sounds/out_of_gum_x");
                    break;
            case 7:
                    GameEnvironment.AssetManager.PlaySound("Sounds/gotta_hurt");
                    break;
            case 8:
                    GameEnvironment.AssetManager.PlaySound("Sounds/be_back2");
                    break;
            case 9:
                    GameEnvironment.AssetManager.PlaySound("Sounds/ugly");
                    break;
            case 10:
                    GameEnvironment.AssetManager.PlaySound("Sounds/why_not");
                    break;
            default:
                break;

        }
    }

    public bool IsAlive
    {
        get { return isAlive; }
    }

    public bool Finished
    {
        get { return finished; }
    }

    public void LevelFinished()
    {
        finished = true;
        velocity.X = 0.0f;
        this.PlayAnimation("celebrate");
        GameEnvironment.AssetManager.PlaySound("Sounds/snd_player_won");
    }
   
     public List<Bomb> Bombs
	    {
	        get { return bombList; }
	    }
    
    public Vector2 Playerpos
    {
        get { return this.position; }
    }
}
