using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameJamSubmission; 

public class Gameplay : IGameMode {
    private static Random Rng;
    private Player _player;
    private List<IEntity> _entities;
    private Rectangle _windowSize;
    private Game _game;
    private ContentManager _content;

    public void Initialize(Game game, Rectangle windowSize) {
        Rng = new Random();
        _windowSize = windowSize;
        _entities = new List<IEntity>();
        _player = new Player();
        _player.Initialize(windowSize);
        _game = game;
        _content = game.Content;
        GenerateNextScreen();
    }

    public void LoadContent(Game game, ContentManager content) {
        _player.LoadContent(game, content);
    }

    public void Update() {
        _player.Update(_windowSize);
        List<IEntity> temp = _entities.Where(e => e.Update(_windowSize)).ToList();

        if (_entities.Any(e => e.GetArea().Intersects(_player.GetArea()))) {
            Environment.Exit(0);
        }

        foreach (IEntity e in temp) {
            _entities.Remove(e);
        }

        if (_entities.Last().GetHighestRenderablePixel() >= 0) {
            GenerateNextScreen();
        }
    }

    public void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics) {
        _player.Draw(spriteBatch, graphics);
        foreach (IEntity e in _entities) {
            e.Draw(spriteBatch, graphics);
        }
    }

    private void GenerateNextScreen() {
        int numObstacles = Rng.Next(3, 6);
        for (int i = 0; i < numObstacles; i++) {
            int x = Rng.Next(_windowSize.Width * 2 / 3);
            int y = Rng.Next(-1*_windowSize.Height, -50);
            LavaObstacle o = new(new Rectangle(x, y, Rng.Next(_windowSize.Width / 3, _windowSize.Width-x), 50));
            o.Initialize(_windowSize);
            o.LoadContent(_game, _content);
            _entities.Add(o);
        }
        _entities.Sort((e2, e1) => e1.GetHighestRenderablePixel().CompareTo(e2.GetHighestRenderablePixel()));
    }
}