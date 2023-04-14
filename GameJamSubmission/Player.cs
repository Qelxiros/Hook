using System;
using Devcade;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameJamSubmission; 

public class Player : IEntity {
    private int _y;
    private int _x;
    private int? _hookX;
    private int? _hookY;
    private int _hookVelocity;
    private bool _boxHeld;
    private const int Speed = 1;
    private Texture2D _texture;
    private Texture2D _hookTexture;
    private Rectangle _area;
    
    public void Initialize(Rectangle windowSize) {
        _x = windowSize.Width / 2-25;
        _y = (int)(windowSize.Height * 0.8);
        _hookX = null;
        _hookY = null;
        _hookVelocity = 0;
        _boxHeld = false;
        _area = new Rectangle(_x, _y, 50, 50);
    }

    public void LoadContent(Game game, ContentManager content) {
        Color[] data = new Color[_area.Width * _area.Height];
        for (int i = 0; i < _area.Width * _area.Height; i++) {
            data[i] = Color.Lime;
        }

        _texture = new Texture2D(game.GraphicsDevice, _area.Width, _area.Height);
        _texture.SetData(data);
        
        _hookTexture = new Texture2D(game.GraphicsDevice, 1, 1);
        _hookTexture.SetData(new []{Color.Lime});
    }

    public bool Update(Rectangle windowSize) {
        KeyboardState ks = Keyboard.GetState();
        
        if (_hookX != null && _hookY != null) {
            if (_hookX > windowSize.Left && _hookX < windowSize.Right) {
                _hookX += _hookVelocity;
                if (_hookX > windowSize.Right) {
                    _hookX = windowSize.Right;
                } else if (_hookX < windowSize.Left) {
                    _hookX = windowSize.Left;
                }
            } else {
                if (_hookX >= windowSize.Right) {
                    _x = windowSize.Right - 50;
                    _hookX = null;
                    _hookY = null;
                } else {
                    _x = 0;
                    _hookX = null;
                    _hookY = null;
                }
            }
        } else {
            if (ks.IsKeyDown(Keys.A) || Input.GetButton(1, Input.ArcadeButtons.A1) ||
                Input.GetButton(2, Input.ArcadeButtons.A1)) {
                _hookX = _x + 25;
                _hookY = _y + 25;
                _hookVelocity = -12;
            }
            
            if (ks.IsKeyDown(Keys.D) || Input.GetButton(1, Input.ArcadeButtons.A2) ||
                Input.GetButton(2, Input.ArcadeButtons.A2)) {
                _hookX = _x + 25;
                _hookY = _y + 25;
                _hookVelocity = 12;
            }
        }

        if (ks.IsKeyDown(Keys.Left) || Input.GetButton(1, Input.ArcadeButtons.StickLeft) ||
            Input.GetButton(2, Input.ArcadeButtons.StickLeft)) {
            _x = Math.Max(_x - Speed, 0);
            _area = new Rectangle(_x, _y, _area.Width, _area.Height);
        }
        
        if (ks.IsKeyDown(Keys.Right) || Input.GetButton(1, Input.ArcadeButtons.StickRight) ||
            Input.GetButton(2, Input.ArcadeButtons.StickRight)) {
            _x = Math.Min(_x + Speed, windowSize.Width - _area.Width);
            _area = new Rectangle(_x, _y, _area.Width, _area.Height);
        }
        
        return true;
    }

    public void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics) {
        if (_hookX != null && _hookY != null) {
            spriteBatch.Draw(_hookTexture, new Rectangle(Math.Min(_x + 25, _hookX.Value), _y+20, Math.Abs(_hookX.Value-_x-25), _hookY.Value-_y-15), Color.Gray);
        }
        spriteBatch.Draw(_texture, new Vector2(_x, _y), Color.White);
    }

    public int GetHighestRenderablePixel() {
        return _y;
    }

    public Rectangle GetArea() {
        return _area;
    }
}