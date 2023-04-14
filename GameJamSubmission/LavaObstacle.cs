using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameJamSubmission; 

public class LavaObstacle : IEntity {
    private const int Speed = 1;
    private static readonly Color ObstacleColor = Color.Red;
    private Rectangle _area;
    private Texture2D _texture;

    public LavaObstacle(Rectangle area) {
        _area = area;
    }
    
    public void Initialize(Rectangle windowSize) {
    }

    public void LoadContent(Game game, ContentManager content) {
        Color[] data = new Color[_area.Width * _area.Height];
        for (int i = 0; i < data.Length; i++) {
            data[i] = ObstacleColor;
        }

        _texture = new Texture2D(game.GraphicsDevice, _area.Width, _area.Height);
        _texture.SetData(data);
    }

    public bool Update(Rectangle windowSize) {
        _area = new Rectangle(_area.X, _area.Y + Speed, _area.Width, _area.Height);
        return _area.Y > windowSize.Height;
    }

    public void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics) {
        spriteBatch.Draw(_texture, new Vector2(_area.X, _area.Y), Color.White);
    }

    public int GetHighestRenderablePixel() {
        return _area.Y;
    }
    
    public Rectangle GetArea() {
        return _area;
    }
}