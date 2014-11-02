using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
	

   public class Camera
	    {
	    Rectangle cameraView;

	  
       
       public Camera()
	        {
	            Reset();
            }
	   
       
       public void Reset()
	        {
            cameraView = new Rectangle(0, 0, GameEnvironment.Screen.X, GameEnvironment.Screen.Y);
	        }
	    
       
       public Rectangle CameraView
        {
           get { return cameraView; }
	            set { cameraView = value; }
	        }
	    }
