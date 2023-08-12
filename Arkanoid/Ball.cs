using SFML.Graphics;
using SFML.System;

class Ball
{
    public Sprite sprite;

    public float speed;
    public Vector2f direction;

    public Ball(Texture texture)
    {
        sprite = new Sprite(texture);
    }

    public void Stert(float speed, Vector2f direction)
    {
        if (this.speed != 0) return;
        
        this.speed = speed;
        this.direction = direction;
    }

    public void Move(Vector2i boundPosition, Vector2i boundSize)
    {
        sprite.Position += direction * speed;

        if (sprite.Position.X + sprite.Texture.Size.X > boundSize.X || sprite.Position.X < boundPosition.X)
        {
            direction.X *= -1;
        }

        if (sprite.Position.Y < boundPosition.Y)
        {
            direction.Y *= -1;
        }
    }
    
    public bool CheckCollision(Sprite sprite, string tag)
    {
        if (this.sprite.GetGlobalBounds().Intersects( sprite.GetGlobalBounds() ) == true)
        {

            if (tag == "stick")
            {
                direction.Y = -1;
                float deviation = ( (this.sprite.Position.X + this.sprite.Texture.Size.X / 2) - (sprite.Position.X + sprite.Texture.Size.X / 2) ) / sprite.Texture.Size.X;
                direction.X = deviation * 3;
            }

            if (tag == "block")
            {
                direction.Y *= -1;
            }

            return true;
        }

        return false;
    }
}
