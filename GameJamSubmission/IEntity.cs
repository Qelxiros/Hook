using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameJamSubmission;

public interface IEntity {
    /**
     * All non-content related initialization should be done here. Called immediately on startup.
     * This method should include the creation of all game objects.
     */
    public void Initialize(Rectangle windowSize);

    /**
    * All content for this game mode should be loaded here.
    */
    public void LoadContent(Game game, ContentManager content);

    /**
     * All update logic for this game mode should be done here. This will only be called when this game mode is the active game mode.
     * This will run before draw.
     */
    public bool Update(Rectangle windowSize);

    /**
     * All drawing logic for this game mode should be done here. This will only be called when this game mode is the active game mode.
     * This will run after update.
     */
    public void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics);

    public int GetHighestRenderablePixel();

    public Rectangle GetArea();
}
