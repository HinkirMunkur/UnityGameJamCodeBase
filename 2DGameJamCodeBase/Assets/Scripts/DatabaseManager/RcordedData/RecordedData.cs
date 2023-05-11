/**
 * This class will store properties that needs to be saved and loaded.
 * This file should remain empty and should only be filled when which
 * properties to save and load are determined.
 */

[System.Serializable]
public abstract class RecordedData
{
    /* Properties (for save/load):
     Properties defined in this class should be public. */
    
    // Example: public int health;

    [System.NonSerialized] public bool IsDirty;
    [System.NonSerialized] public bool IsLoaded;

    /* Constructor:
     * Values defined in this constructor should be the initial values for
     * the properties, since the first time game is ran, no loading will happen.
     */
    protected RecordedData()
    { 
        IsDirty = false;
        IsLoaded = false;
    }
}
