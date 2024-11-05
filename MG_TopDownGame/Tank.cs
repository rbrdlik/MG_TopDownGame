using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MG_TopDownGame;
// ReSharper disable All

public class Tank
{
    private Texture2D _texture;
    private Vector2 _position;
    private float _speed;
    private float _rotation;
    private int _screenWidth;
    private int _screenHeight;
    private List<Missile> _missiles;
    private Texture2D _missileTexture;
    private bool _isShooting;

    public Vector2 Position => _position;

    public Tank(Texture2D texture, Vector2 position, float speed, int screenWidth, int screenHeight, List<Missile> missiles, Texture2D missileTexture)
    {
        _texture = texture;
        _position = position;
        _speed = speed;
        _screenWidth = screenWidth;
        _screenHeight = screenHeight;
        _missiles = missiles;
        _missileTexture = missileTexture;
        _rotation = 0f;
        _isShooting = false;
    }

    public void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        var keyboardState = Keyboard.GetState();
        var mouseState = Mouse.GetState();

        if (keyboardState.IsKeyDown(Keys.A))
        {
            _rotation -= 2f * dt;
        }
        if (keyboardState.IsKeyDown(Keys.D))
        {
            _rotation += 2f * dt;
        }

        Vector2 direction = new Vector2((float)System.Math.Cos(_rotation), (float)System.Math.Sin(_rotation));

        if (keyboardState.IsKeyDown(Keys.W))
        {
            _position += direction * _speed * dt;
        }
        if (keyboardState.IsKeyDown(Keys.S))
        {
            _position -= direction * _speed * dt;
        }

        _position.X = MathHelper.Clamp(_position.X, 0, _screenWidth - _texture.Width);
        _position.Y = MathHelper.Clamp(_position.Y, 0, _screenHeight - _texture.Height);

        if (mouseState.LeftButton == ButtonState.Pressed && !_isShooting)
        {
            Shoot(new Vector2(mouseState.X, mouseState.Y));
            _isShooting = true;
        }
        else if (mouseState.LeftButton == ButtonState.Released)
        {
            _isShooting = false;
        }
    }

    private void Shoot(Vector2 targetPosition)
    {
        Vector2 tankRightCenter = new Vector2(_position.X + _texture.Width / 2, _position.Y + _texture.Height / 2);
        Vector2 direction = targetPosition - tankRightCenter;
        direction.Normalize();

        _missiles.Add(new Missile(tankRightCenter, direction, _missileTexture));
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, null, Color.White, _rotation, new Vector2(_texture.Width / 2, _texture.Height / 2), 1.0f, SpriteEffects.None, 0f);
    }
}