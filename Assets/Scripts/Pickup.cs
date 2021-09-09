using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum type 
    { food, arrow, gold, bleed_pot, heal_pot, magic_pot, poison_pot, 
      gem_of_sight, ritual_bone, ritual_skull, ritual_book, ritual_candle,
      amulet, armor_heavy, armor_light, axe, bow, dagger, robe, shield, staff, sword, spell}

    public type itemType;
    public string itemName;
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
            if (itemType == type.amulet)
            {
                GameManager.GAME.focus_item = this.gameObject; //log the item for further operations

                if (GameManager.GAME.equipped_amulet != null) //is an amulet already equipped?
                {
                    GameManager.GAME.UI.OpenReplaceMessage(GameManager.GAME.equipped_amulet.GetComponent<Pickup>().itemName + "(" +
                        GameManager.GAME.equipped_amulet.GetComponent<Pickup>().min + ", " +
                        GameManager.GAME.equipped_amulet.GetComponent<Pickup>().max +
                        ") is currently equipped. Replace it with " + this.itemName +
                        "(" + min + ", " + max + ")?");
                }
                else
                {
                    GameManager.GAME.equipped_amulet = Instantiate(this.gameObject, new Vector3(0, 0, -100), Quaternion.identity);
                    Destroy(gameObject);
                }
            }

            if (itemType == type.armor_heavy || itemType == type.armor_light || itemType == type.robe)
            {
                GameManager.GAME.focus_item = this.gameObject; //log the item for further operations

                if (GameManager.GAME.equipped_armor != null) //is armor already equipped?
                {
                    GameManager.GAME.UI.OpenReplaceMessage(GameManager.GAME.equipped_armor.GetComponent<Pickup>().itemName + "(" +
                        GameManager.GAME.equipped_armor.GetComponent<Pickup>().min + ", " +
                        GameManager.GAME.equipped_armor.GetComponent<Pickup>().max +
                        ") is currently equipped. Replace it with " + this.itemName +
                        "(" + min + ", " + max + ")?");
                }
                else
                {
                    GameManager.GAME.equipped_armor = Instantiate(this.gameObject, new Vector3(0, 0, -100), Quaternion.identity);
                    Destroy(gameObject);
                }
            }

            if (itemType == type.shield)
            {
                GameManager.GAME.focus_item = this.gameObject; //log the item for further operations

                if (GameManager.GAME.equipped_shield != null) //is a shield already equipped?
                {
                    GameManager.GAME.UI.OpenReplaceMessage(GameManager.GAME.equipped_shield.GetComponent<Pickup>().itemName + "(" +
                        GameManager.GAME.equipped_shield.GetComponent<Pickup>().min + ", " +
                        GameManager.GAME.equipped_shield.GetComponent<Pickup>().max +
                        ") is currently equipped. Replace it with " + this.itemName +
                        "(" + min + ", " + max + ")?");
                }
                else
                {
                    GameManager.GAME.equipped_shield = Instantiate(this.gameObject, new Vector3(0, 0, -100), Quaternion.identity);
                    Destroy(gameObject);
                }
            }

            if (itemType == type.axe || itemType == type.dagger || itemType == type.staff || itemType == type.sword)
            {
                GameManager.GAME.focus_item = this.gameObject; //log the item for further operations

                if (GameManager.GAME.equipped_melee != null) //is a weapon already equipped?
                {
                    GameManager.GAME.UI.OpenReplaceMessage(GameManager.GAME.equipped_melee.GetComponent<Pickup>().itemName + "(" +
                        GameManager.GAME.equipped_melee.GetComponent<Pickup>().min + ", " +
                        GameManager.GAME.equipped_melee.GetComponent<Pickup>().max +
                        ") is currently equipped. Replace it with " + this.itemName +
                        "(" + min + ", " + max + ")?");
                }
                else
                {
                    GameManager.GAME.equipped_melee = Instantiate(this.gameObject, new Vector3(0, 0, -100), Quaternion.identity);
                    Destroy(gameObject);
                }
            }

            if (itemType == type.bow)
            {
                GameManager.GAME.focus_item = this.gameObject; //log the item for further operations

                if (GameManager.GAME.equipped_bow != null) //is a bow already equipped?
                {
                    GameManager.GAME.UI.OpenReplaceMessage(GameManager.GAME.equipped_bow.GetComponent<Pickup>().itemName + "(" +
                        GameManager.GAME.equipped_bow.GetComponent<Pickup>().min + ", " +
                        GameManager.GAME.equipped_bow.GetComponent<Pickup>().max +
                        ") is currently equipped. Replace it with " + this.itemName +
                        "(" + min + ", " + max + ")?");
                }
                else
                {
                    GameManager.GAME.equipped_bow = Instantiate(this.gameObject, new Vector3(0, 0, -100), Quaternion.identity);
                    Destroy(gameObject);
                }
            }

            if(itemType == type.gem_of_sight)
            {
                Destroy(gameObject);
                GameManager.GAME.hasGem[GameManager.GAME.ForestLevel] = true;
            }
        }
    }
}
