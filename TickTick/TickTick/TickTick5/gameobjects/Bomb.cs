using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
	
	  class Bomb : SpriteGameObject
      {
	        float speedbomb;
          
           public Bomb()
	            :base("Sprites/Player/", 0, "bullet")
	        {
	            speedbomb = 600f;
	            Velocity = new Vector2(speedbomb, 0);
	        }
	    }
	