using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum type 
    { food, arrow, gold, bleed_pot, heal_pot, magic_pot, poison_pot, 
      gem_of_sight, ritual_bone, ritual_skull, ritual_book, ritual_candle,
      amulet1, amulet2, armor_heavy1, armor_light1, axe1, axe2, bow1, bow2, dagger1, dagger2, robe1, shield1, shield2, staff1, staff2, sword1, sword2,
      amulet3, amulet4, armor_heavy2, armor_light2, axe3, axe4, bow3, bow4, dagger3, dagger4, robe2, shield3, shield4, staff3, staff4, sword3, sword4,
      amulet5, amulet6, armor_heavy3, armor_light3, axe5, axe6, bow5, bow6, dagger5, dagger6, robe3, shield5, shield6, staff5, staff6, sword5, sword6,
      amulet7, amulet8, armor_heavy4, armor_light4, axe7, axe8, bow7, bow8, dagger7, dagger8, robe4, shield7, shield8, staff7, staff8, sword7, sword8
    }

    public type itemType;

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
