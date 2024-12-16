using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;


public class SpaceShip
{
    public Texture2D spaceship;
    static float x = 5;
    static float y = 400;
    int health = 100;
    int damage = 50;
    public List<Bullet> bullets = new List<Bullet>();
    public Rectangle Collision;  
    public void move(bool direction)
    {

        if (y < 750 && !direction)
            y += 0.3f;
        else if (y > 0 && direction)
            y -= 0.3f;
    }

    public void shoot()
    {
        Bullet bullet = new Bullet(x,y,0.5f);
        bullets.Add(bullet);
        bullet.move();
    }

    public void TakeDamage(int amount)
    {
        this.health -= amount;
    }

    public void drawBullets()
    {
        foreach (var bullet in bullets.ToList())
        {
                if(bullet.direction > 1100f)
                    {
                        bullets.Remove(bullet);
                    }
                else
                   {
                        Task.Delay(1);
                         bullet.move();
                         Raylib.DrawRectangleV(bullet.postion, bullet.size, Color.BLUE);
                    }
                
        }
    }

    public void drawSpaceShip(int scor)
    {
        Vector2 Vector2 = new Vector2(x, y);
        Raylib.DrawTextureV(spaceship,Vector2, Color.WHITE);
        string score = $"scor:{scor}\n can:{health}";
        Raylib.DrawText(score, (int)x+40,(int)y-40, 20, Color.YELLOW);
        Collision = new Rectangle(x, y, 55, 55);
    }
    public int getHealth()
    {
        return health;
    }   

    public float Getx()
    {
        return x;
    }

    public float Gety()
    {
        return y;
    }
}

