using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum type 
    { food, arrow, gold, bleed_pot, heal_pot, magic_pot, poison_pot, 
      gem_of_sight, ritual_bone, ritual_skull, ritual_book, ritual_candle,
      amulet, armor_heavy, armor_light, axe, bow, dagger, robe, shield, staff, sword}

    public type itemType;
    public int min, max, value;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(itemType == type.food && GameManager.GAME.num_Food < 99)
            {
                GameManager.GAME.num_Food++;
                Destroy(gameObject);
            }
            if(itemType == type.arrow && GameManager.GAME.num_arrows < 99)
            {
                GameManager.GAME.num_arrows++;
                Destroy(gameObject);
            }
            if(itemType == type.gold && GameManager.GAME.gold < 999999999)
            {
                GameManager.GAME.gold += Random.Range(1, GameManager.GAME.ForestLevel * 5);
                Destroy(gameObject);
            }
            if(itemType == type.bleed_pot && GameManager.GAME.num_Bleed_pot < 999)
            {
                GameManager.GAME.num_Bleed_pot++;
                Destroy(gameObject);
            }
            if(itemType == type.heal_pot && GameManager.GAME.numHP_pot < 999)
            {
                GameManager.GAME.numHP_pot++;
                Destroy(gameObject);
            }
            if(itemType == type.magic_pot && GameManager.GAME.numMP_pot < 999 && GameManager.GAME.manaUser)
            {
                GameManager.GAME.numMP_pot++;
                Destroy(gameObject);
            }
            if(itemType == type.poison_pot && GameManager.GAME.numPoison_pot < 999)
            {
                GameManager.GAME.numPoison_pot++;
                Destroy(gameObject);
            }
        }
    }
}
