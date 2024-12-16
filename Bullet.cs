using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

public class Bullet
{
    float speed ;
    int damage = 50;
    public float direction;
    float y;
    public Vector2 size = new Vector2(30,7);
    public Vector2 postion;
    public Rectangle Collision;
    public Bullet(float x,float y,float speed) 
    {
        direction = x;
        this.y = y;
        this.speed = speed; 
    }
    public void move()
    {
     
            direction += speed;

            var random = new Random();

            if (random.Next(2) == 1)
                postion = new Vector2(direction, y);
            else
                postion = new Vector2(direction, y + 40);

            Collision = new Rectangle(direction, y, 7, 30);
      
        
    }

   

    public int OnHit()
    {
        return damage;  
    }




}

