using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;


class Program
{
    static RenderWindow window;

    static Texture stickTexture;
    static Texture ballTexture;
    static Texture block_01Texture;
    static Texture block_02Texture;
    static Texture block_03Texture;
    static Texture hearth_icon;
    static Texture winTexture;

    static Sprite stick;
    static Sprite win;
    static Sprite[] hearts;
    static Sprite[] blocks;
    static int[] blockType;

    static Ball ball;

    static int level = 1;
    static int lives = 3;

    public static void SetStartPosition()
    {
        int index = 0;

        if (level == 1)
        {
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 14; x++)
                {
                    blocks[index].Position = new Vector2f(x * (blocks[index].TextureRect.Width + 0) + blocks[index].TextureRect.Width, y * (blocks[index].TextureRect.Height + 0) + blocks[index].TextureRect.Height);
                    index++;
                }
            }
        }

        if (level == 2)
        {
            for (int y = 0; y < 15; y++)
            {
                for (int x = 0; x < 14; x++)
                {
                    blocks[index].Position = new Vector2f(x * (blocks[index].TextureRect.Width + 0) + blocks[index].TextureRect.Width, y * (blocks[index].TextureRect.Height + 0) + blocks[index].TextureRect.Height);
                    index++;
                }
            }
        }

        if (level == 3)
        {
            for (int y = 0; y < 20; y++)
            {
                for (int x = 0; x < 14; x++)
                {
                    blocks[index].Position = new Vector2f(x * (blocks[index].TextureRect.Width + 0) + blocks[index].TextureRect.Width, y * (blocks[index].TextureRect.Height + 0) + blocks[index].TextureRect.Height);
                    index++;
                }
            }
        }

        ball.sprite.Position = new Vector2f(1600 / 2 - ball.sprite.Texture.Size.X, 700);
        stick.Position = new Vector2f(1600 / 2 - stick.Texture.Size.X / 2, 850);
    }

    public static void ReplaceOrRemoveBlock(int index)
    {
        if (blockType[index] == 1) blocks[index].Position = new Vector2f(2000, 2000);
        if (blockType[index] == 2) blockType[index]--;
        if (blockType[index] == 3) blockType[index]--;
    }

    public static void BuildingLevels()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            if (level == 1)
            {
                if (i < 28 || i > 111 || i % 14 == 0 || i % 14 == 13) blockType[i] = 2;
                else blockType[i] = 1;
                if (i > 56 && i < 69 || i > 70 && i < 83) blockType[i] = 3;
            }

            if (level == 2)
            {
                if (i < 14 || i > 195 || i % 14 == 0 || i % 14 == 13) blockType[i] = 3;
                else blockType[i] = 2;
            }

            if (level == 3)
            {
                blockType[i] = 3;
            }
        }
    }

    public static void InitializationMassive()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            if (blockType[i] == 1) blocks[i] = new Sprite(block_01Texture);
            if (blockType[i] == 2) blocks[i] = new Sprite(block_02Texture);
            if (blockType[i] == 3) blocks[i] = new Sprite(block_03Texture);
        }
    }

    public static void InitLevel()
    {
        if (level == 1)
        {
            blocks = new Sprite[140];
            blockType = new int[140];
        }

        if (level == 2)
        {
            blocks = new Sprite[210];
            blockType = new int[210];
        }

        if (level == 3)
        {
            blocks = new Sprite[280];
            blockType = new int[280];
        }
    }

    public static void IsDefeat()
    {
        if (ball.sprite.Position.Y < 900) return;

        ball.sprite.Position = new Vector2f(1600 / 2 - ball.sprite.Texture.Size.X, 700);
        ball.speed = 0;
        lives--;

        if (lives >= 1) return;

        lives = 3;

        InitLevel();

        BuildingLevels();

        InitializationMassive();

        SetStartPosition();
    }

    public static void DrawHearts()
    {
        switch (lives)
        {
            case 3:
                for (int i = 0; i < hearts.Length; i++) window.Draw(hearts[i]);
                break;
            case 2:
                for (int i = 0; i < hearts.Length - 1; i++) window.Draw(hearts[i]);
                break;
            case 1:
                for (int i = 0; i < hearts.Length - 2; i++) window.Draw(hearts[i]);
                break;
        }
    }

    public static void CheckVictory()
    {
        for (int i = 0; i < blocks.Length; i++) if (blocks[i].Position.X != 2000) return;

        window.Draw(win);
    }

    static void Main(string[] args)
    {
        window = new RenderWindow(new VideoMode(1600, 900), "Arkanoid");
        window.Closed += Window_Closed;
        window.SetFramerateLimit(60);

        stickTexture = new Texture("Stick.png");
        ballTexture = new Texture("Ball.png");
        block_01Texture = new Texture("Block_01.png");
        block_02Texture = new Texture("Block_02.png");
        block_03Texture = new Texture("Block_03.png");
        hearth_icon = new Texture("hearth_icon.png");
        winTexture = new Texture("congratulations.png");

        win = new Sprite(winTexture);
        win.Position = new Vector2f(1600 / 2 - win.Texture.Size.X / 2, 300);

        ball = new Ball(ballTexture);
        stick = new Sprite(stickTexture);
        hearts = new Sprite[3];
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i] = new Sprite(hearth_icon);
            hearts[i].Position = new Vector2f(60 * i, 840);
        }

        InitLevel();

        BuildingLevels();

        InitializationMassive();

        SetStartPosition();

        while (window.IsOpen == true)
        {
            window.Clear();

            window.DispatchEvents();

            // Логика
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                ball.Stert(5, new Vector2f(0, 1));
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num1))
            {
                level = 1;
                ball.speed = 0;
                lives = 3;

                InitLevel();

                BuildingLevels();

                InitializationMassive();

                SetStartPosition();
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num2))
            {
                level = 2;
                ball.speed = 0;
                lives = 3;

                InitLevel();

                BuildingLevels();

                InitializationMassive();

                SetStartPosition();
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num3))
            {
                level = 3;
                ball.speed = 0;
                lives = 3;

                InitLevel();

                BuildingLevels();

                InitializationMassive();

                SetStartPosition();
            }

            IsDefeat();

            // Ball
            ball.Move(new Vector2i(0, 0), new Vector2i(1600, 900));

            ball.CheckCollision(stick, "stick");
            for (int i = 0; i < blocks.Length; i++)
            {
                ball.CheckCollision(blocks[i], "block");

                if (ball.CheckCollision(blocks[i], "block") == true)
                {
                    ball.CheckCollision(blocks[i], "block");

                    ReplaceOrRemoveBlock(i);

                    break;
                }
            }

            // Stick
            stick.Position = new Vector2f(Mouse.GetPosition(window).X - stick.Texture.Size.X / 2, stick.Position.Y);
            if (stick.Position.X < 0) stick.Position = new Vector2f(0, stick.Position.Y);
            if (stick.Position.X > 1600 - stick.Texture.Size.X) stick.Position = new Vector2f(1600 - stick.Texture.Size.X, stick.Position.Y);

            // Отрисовка
            window.Draw(ball.sprite);
            window.Draw(stick);
            for (int i = 0; i < blocks.Length; i++) window.Draw(blocks[i]);
            CheckVictory();
            DrawHearts();

            window.Display();
        }
    }

    private static void Window_Closed(object sender, EventArgs e)
    {
        window.Close();
    }
}