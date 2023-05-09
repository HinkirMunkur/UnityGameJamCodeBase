/**
 * This class will store properties that needs to be saved and loaded.
 * This file should remain empty and should only be filled when which
 * properties to save and load are determined.
 */

[System.Serializable]
public class GameData
{
    /* Properties (for save/load):
     Properties defined in this class should be public. */
    
    // Example: public int health;
    public int a;
    public int b;
    public int c;


    /* Constructor:
     * Values defined in this constructor should be the initial values for
     * the properties, since the first time game is ran, no loading will happen.
     */
    public GameData()
    {
        // Example: this.health = 5;
        a = 31;
        b = 13;
        c = 0;
    }
}
