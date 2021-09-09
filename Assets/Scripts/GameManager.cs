using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GAME;
    public static int MAPWIDTH, MAPHEIGHT;
    public static bool PAUSED;

    public UI UI;

    public bool poisoned, bleeding, manaUser, hasGem, hasSkull, hasBone, hasBook, hasCandle;
    public int isStunned = 0, attkMode = 0;
    public int ForestLevel, playerLevel, numHP_pot, numMP_pot, num_Food, numPoison_pot, num_Bleed_pot, num_arrows, gold;
    public float playerHP, playerMP, playerFood, maxHP, maxMP, maxFood, playerXP, playerXPNNL, playerXPmod;
    public GameObject equipped_melee, equipped_bow, equipped_spell, equipped_armor, equipped_amulet, equipped_shield;

    private void Awake()
    {
        if (GAME != null && GAME != this)
        {
            Destroy(gameObject);
            return;
        }

        GAME = this;
        DontDestroyOnLoad(gameObject);
        PAUSED = false;
    }

    public void SwitchAttackMode()
    {
        attkMode++;
        if (attkMode == 1 && equipped_bow == null) attkMode = 2;
        if (attkMode == 2 && equipped_spell == null) attkMode = 0;
        if (attkMode == 3) attkMode = 0;        
    }

    public void Drink_Potion(string pot)
    {
        if (FindObjectOfType<Player>().canMove) // Can't drink potions when dazed
        {
            if (pot == "HP" && numHP_pot > 0 && playerHP < maxHP) //Health potions heals 10 hp
            {
                numHP_pot--;
                playerHP += 10;
                if (playerHP > maxHP) playerHP = maxHP;
            }
            if (pot == "MP" && numMP_pot > 0 && playerMP < maxMP)//Magic potions heal 25 mana
            {
                numMP_pot--;
                playerMP += 25;
                if (playerMP > maxMP) playerMP = maxMP;
            }
            if(pot == "Poison" && numPoison_pot >0 && poisoned)//Poison potions cure poison
            {
                numPoison_pot--;
                poisoned = false;
            }
            if(pot == "Bleed" && num_Bleed_pot > 0 && bleeding)//Bleed potions cure bleeding
            {
                num_Bleed_pot--;
                bleeding = false;
            }
            if(pot == "Food" && num_Food > 0 && playerFood < maxFood) //food fills food bar
            {
                num_Food--;
                playerFood += 5;
                if (playerFood > maxFood) playerFood = maxFood;
            }
        }
    }
}
