using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Classes who use the properties in the GameData class must
 * implement this interface and its function, save and load.
 * 
 * For example if we want to save and load the player's health,
 * that is to say if the player's health is a property in the
 * GameData class, the Player class should implement this interface.
 * 
 * Example code:
 * public class Player : MonoBehaviour, IDataPersistence
 * {
 *     private int health;
 *     
 *     public void LoadData(GameData data)
 *     {
 *         health = data.health;
 *     }
 *     
 *     public void SaveData(ref GameData data)
 *     {
 *         data.health = health;
 *     }
 * }
 */
public interface IDataPersistence
{
    void LoadData(GameData data);
    void SaveData(ref GameData data);
}
