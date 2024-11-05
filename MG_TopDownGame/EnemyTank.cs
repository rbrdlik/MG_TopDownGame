using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
// ReSharper disable All

namespace MG_TopDownGame;

public class EnemyTank
{
    private Texture2D _texture;
    private Vector2 _position;
    private float _speed;
    private List<Missile> _missiles;
    private Texture2D _missileTexture;
    private float _fireRate = 1f;
    private float _fireTimer = 0f;

    public Vector2 Position => _position;

    public EnemyTank(Texture2D texture, Vector2 position, float speed, List<Missile> missiles, Texture2D missileTexture)
    {
        _texture = texture;
        _position = position;
        _speed = speed;
        _missiles = missiles;
        _missileTexture = missileTexture;
    }

    public void Update(GameTime gameTime, Vector2 playerPosition)
    {
        if (_position.Y < playerPosition.Y - 50)
        {
            _position.Y += _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        else if (_position.Y > playerPosition.Y + 50)
        {
            _position.Y -= _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        
        _fireTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_fireTimer <= 0)
        {
            Shoot(playerPosition);
            _fireTimer = _fireRate;
        }
    }

    private void Shoot(Vector2 targetPosition)
    {
        Vector2 enemyCenter = new Vector2(_position.X + _texture.Width / 2, _position.Y + _texture.Height / 2);
        Vector2 direction = targetPosition - enemyCenter;
        direction.Normalize();

        _missiles.Add(new Missile(enemyCenter, direction, _missileTexture));
        Console.WriteLine("Nepřátelský tank střílí na hráče!");
    }


    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, Color.White);
    }
}