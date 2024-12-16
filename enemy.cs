using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


public abstract class enemy
{
    public Texture2D enemyShip;
    public int health;
    public  float speed = 0.1f;
    public int damage;
    public float spwanX;
    public float spwanY;
    public Rectangle Collision;
    public bool isDestroyed = false;
    abstract public void move(float x, float y);
    abstract public void attack();
    
    public void TakeDamage(int amount)
    {

        if (this.health > 0)
        {
            this.health -= amount;
        }
        else
        {
            Destroy();
        }
    }

    public void Destroy()
    {
        isDestroyed = true;
    }

}


public class BasicEnemy:enemy
{
    public BasicEnemy()
    {
        this.health = 150;
        this.damage = 50;
    }
   
    public override void move(float x, float y)
    {
        spwanX -= speed;
        Collision = new Rectangle(spwanX, spwanY, 100, 100);
    }


    public override void  attack()
    {

    }


}


public class FastEnemy : enemy
{
    public FastEnemy()
    {
        this.health = 70;
        this.damage = 25;
    }
    public override void move(float x , float y)
    {

        float deltaX =spwanX -  x;
        float deltaY =spwanY - y;

   
        float mesafe = (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

    
        float yönX = deltaX / mesafe;
        float yönY = deltaY / mesafe;

      
        float x_artış = yönX * speed;
        float y_artış = yönY * speed;
       

        spwanX -= x_artış;
        spwanY -= y_artış;
        Collision = new Rectangle(spwanX, spwanY, 100, 100);
    }

    public override void attack()
    {

    }


}




public class StrongEnemy : enemy
{
    public StrongEnemy()
    {
        this.health = 200;
        this.damage = 100;
    }
    public override void move(float x, float y)
    {
        float deltaX = spwanX - x;
        float deltaY = spwanY - y;


        float mesafe = (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);


        float yönX = deltaX / mesafe;
        float yönY = deltaY / mesafe;


        float x_artış = yönX * speed;
        float y_artış = yönY * speed;


        spwanX -= x_artış;
        spwanY -= y_artış;
        Collision = new Rectangle(spwanX, spwanY, 100, 100);
    }

    public override void attack()
    {

    }


}



public class BossEnemy : enemy
{
    public List<Bullet> bullets = new List<Bullet>();
    private float bulletDelay = 0.7f; 
    private float currentDelay = 0.0f;
    public BossEnemy()
    {

        this.health = 550;
        this.damage = 200;
    }

    public void drawBullets()
    {
        foreach (var bullet in bullets.ToList())
        {
            if (bullet.direction < -10f || bullet.direction > 1100)
            {
                bullets.Remove(bullet);
            }
            else
            {
               
                bullet.move();
                Raylib.DrawRectangleV(bullet.postion, bullet.size, Color.RED);
            }

        }
    }

    public void shoot()
    {
        currentDelay += Raylib.GetFrameTime();


        if (currentDelay >= bulletDelay)
        {
            Bullet bullet = new Bullet(spwanX, spwanY + 80, -0.2f);
            bullets.Add(bullet);
            bullet.move();


            currentDelay = 0;
        }

    }


    public override void move(float x, float y)
    {  
        spwanX -= speed;
        Collision = new Rectangle(spwanX, spwanY, 170, 170);
    }

    public override void attack()
    {
        this.shoot();
    }


}

