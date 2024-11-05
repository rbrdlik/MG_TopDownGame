using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
// ReSharper disable All

namespace MG_TopDownGame;

public class Missile
{
    private Vector2 _position;
    private Vector2 _direction;
    private float _speed = 300f;
    private const int MissileSize = 10;
    private Texture2D _missileTexture;

    public bool IsVisible => _position.X >= 0 && _position.X <= 1280 && _position.Y >= 0 && _position.Y <= 720;
    
    public Missile(Vector2 startPosition, Vector2 direction, Texture2D missileTexture)
    {
        _position = startPosition;
        _direction = direction;
        _missileTexture = missileTexture;
    }

    public void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        _position += _direction * _speed * dt;

        Console.WriteLine($"Missile Position: {_position}");
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_missileTexture, new Rectangle((int)_position.X, (int)_position.Y, MissileSize, MissileSize), Color.Red);
    }
}