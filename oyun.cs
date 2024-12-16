using System;
using System.Numerics;
using Raylib_cs;

public class oyun
{
    BossEnemy bossEnemy;
    Texture2D BackGround;
    SpaceShip player;
	List<enemy> enemies = new List<enemy>();
	bool isGameOver = false;
    CollisionDetector collisionDetector;
    bool isStartGame = true;
    public int score = 0;
    string UserName;
    string file = @"C:\Users\abdal\Desktop\score.txt";
    bool isDataStored = false;
    public oyun()
	{
		this.player = new SpaceShip();
        this.collisionDetector = new CollisionDetector();
	}



    public void drawEnemys()
    {
        foreach (var enemy in enemies)
        {
            if (enemy.spwanX > -100)
            {
                enemy.move(player.Getx(), player.Gety());
                Vector2 currentpoint = new Vector2(enemy.spwanX, enemy.spwanY);
                Raylib.DrawTextureV(enemy.enemyShip, currentpoint, Color.WHITE);
            }
        }


    }

        public void StartGame()
	{
        Raylib.InitWindow(1200, 800, "230229017 Abdullah Han");
        BackGround = Raylib.LoadTexture("Background.png");
        Texture2D ship = Raylib.LoadTexture("SpaceShip.png");
        player.spaceship = ship;
        for (int i = 0; i < 10; i++)
        {
            if (i <= 4)
            {
                BasicEnemy basicEnemy = new BasicEnemy();
                basicEnemy.enemyShip = Raylib.LoadTexture("enemy1.png");
                basicEnemy.speed = 0.05f;
                basicEnemy.spwanX = 900 + i*50;
                basicEnemy.spwanY =  i * 65;
                enemies.Add(basicEnemy);
            }
            else if (i >4  && i < 8)
            {
                FastEnemy fastEnemy = new FastEnemy();
                fastEnemy.enemyShip = Raylib.LoadTexture("enemy2.png");
                fastEnemy.speed = 0.1f;
                fastEnemy.spwanX = 1600;
                fastEnemy.spwanY =  i * 65 + i*50;
                enemies.Add(fastEnemy);
            }
            else
            {
                StrongEnemy strongEnemy = new StrongEnemy();
                strongEnemy.enemyShip = Raylib.LoadTexture("enemy3.png");
                strongEnemy.speed = 0.07f;
                strongEnemy.spwanX = 1500 + i*80;
                strongEnemy.spwanY = 500 - i * 50;
                enemies.Add(strongEnemy);
            }


           // BossEnemy here 
        }

         bossEnemy = new BossEnemy();
        bossEnemy.enemyShip = Raylib.LoadTexture("enemy4.png");
        bossEnemy.speed = 0.03f;
        bossEnemy.spwanX =1900;
        bossEnemy.spwanY = 300;
        enemies.Add(bossEnemy);

    }

	public void UpdateGame()
	{
        while (!Raylib.WindowShouldClose())
        {
            CheckCollisions();

            if (enemies.IndexOf(bossEnemy) == -1)
            {
                bossEnemy.bullets.Clear();
            }
            

            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                    player.shoot();

              

            if (enemies.Count == 0 || player.getHealth() <= 0)
            { 
                isGameOver = true;
            }

            if(bossEnemy.spwanX < 1200)
            {
                bossEnemy.attack();
            }


			if(Raylib.IsKeyDown(KeyboardKey.KEY_W))
			{
                player.move(true);
			}
			else if(Raylib.IsKeyDown(KeyboardKey.KEY_S))
			{
                player.move(false);
			}

			Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);


            if(isStartGame)
            {
                UserName += Raylib.GetKeyPressed() == 0 ? "": (char)Raylib.GetCharPressed();
                Raylib.DrawTexture(BackGround, 0, 0, Color.WHITE);
                Raylib.DrawText("OYUNA BASLAMAK ICIN SPACE TUSUNA BASIN", 200, 300, 30, Color.YELLOW);
                if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
                    isStartGame = false;
                Raylib.DrawText($"PLAYER:{UserName}", 200, 200, 30, Color.YELLOW);
            }
            else if (!isGameOver)
            {
                Raylib.DrawTexture(BackGround, 0, 0, Color.WHITE);
                player.drawSpaceShip(score);
                player.drawBullets();
                drawEnemys();
                bossEnemy.drawBullets();
            }
            else
            {
               
                if(!isDataStored)
                {
                    File.AppendAllText(file, $"\n {UserName}:{score}");
                    isDataStored = true;
                }   


                Raylib.DrawText($"UserName:{UserName}", 400, 100, 30, Color.YELLOW);
                Raylib.DrawText($"socr:{score}", 400, 200, 30, Color.YELLOW);
                string text = File.ReadAllText(file);
                Console.WriteLine(text);
                Raylib.DrawText($"{text}",400,300,30,Color.YELLOW);
                Raylib.DrawText("Oyun yapmak zaman kaybı gibi siz yapmayın gidin ai,backend calisin \n ABDULLAH HAN", 400, 50, 20, Color.LIME);
            }



            Raylib.EndDrawing();
        }
        EndGame();
    }

	public void CheckCollisions()
	{
        collisionDetector.CheckBulletCollision(player.bullets, enemies, ref score);
        collisionDetector.CheckBossBulletCollision(player, bossEnemy.bullets);
        collisionDetector.CheckCollision(player, enemies);
    }

	public void EndGame()
	{
        Raylib.CloseWindow();
        Raylib.UnloadTexture(BackGround);
        Raylib.UnloadTexture(player.spaceship);

        foreach (var item in enemies)
        {
            Raylib.UnloadTexture(item.enemyShip);
        }
    }
   
}
