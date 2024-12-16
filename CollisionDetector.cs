using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

public class CollisionDetector
{
    public void CheckBossBulletCollision(SpaceShip player, List<Bullet> bullets)
    {
        foreach (var bullet in bullets.ToList())
        {
            if (Raylib.CheckCollisionRecs(player.Collision, bullet.Collision))
            {
                var damage = bullet.OnHit();
                player.TakeDamage(damage);
                bullets.Remove(bullet);
            }
        }
    }

    public void CheckCollision(SpaceShip player, List<enemy> enemies)
    {
        foreach (var enemy in enemies.ToList())
        {
            if (Raylib.CheckCollisionRecs(player.Collision, enemy.Collision))
            {
                player.TakeDamage(enemy.damage);
                enemies.Remove(enemy); 
            }
        }
    }

    public void CheckBulletCollision(List<Bullet> bullets, List<enemy> enemies,ref int score)
    {
        foreach (var bull in bullets.ToList())
        {
            foreach (var enemy in enemies.ToList())
            {
                if(Raylib.CheckCollisionRecs(bull.Collision,enemy.Collision))
                {
                    var damage = bull.OnHit();
                    enemy.TakeDamage(damage);
                    bullets.Remove(bull);
                    if (enemy.isDestroyed)
                    {
                        enemies.Remove(enemy);
                        score += enemy.damage;
                    }
                   
                        
                }
            }
        }
    }
}

