using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
// ReSharper disable All

namespace MG_TopDownGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _backgroundTexture;
    private List<Missile> _missiles;
    private Texture2D _missileTexture;
    private Tank _playerTank;
    private EnemyTank _enemyTank;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = 1280; 
        _graphics.PreferredBackBufferHeight = 720; 
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _missiles = new List<Missile>();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _backgroundTexture = Content.Load<Texture2D>("background");
        
        _missileTexture = new Texture2D(GraphicsDevice, 1, 1);
        _missileTexture.SetData(new[] { Color.Red });

        Texture2D playerTexture = Content.Load<Texture2D>("playerTank");
        Texture2D enemyTexture = Content.Load<Texture2D>("enemyTank");

        _playerTank = new Tank(playerTexture, new Vector2(100, 100), 200f, 1280, 720, _missiles, _missileTexture);
        _enemyTank = new EnemyTank(enemyTexture, new Vector2(1180, 620), 150f, _missiles, _missileTexture);
    }
    
    protected override void Update(GameTime gameTime)
    {
        _playerTank.Update(gameTime);
        _enemyTank.Update(gameTime, _playerTank.Position);
        
        for (int i = _missiles.Count - 1; i >= 0; i--)
        {
            _missiles[i].Update(gameTime);
            if (!_missiles[i].IsVisible)
            {
                _missiles.RemoveAt(i);
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        
        _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);

        _playerTank.Draw(_spriteBatch);
        _enemyTank.Draw(_spriteBatch);

        foreach (var missile in _missiles)
        {
            missile.Draw(_spriteBatch);
        }

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}