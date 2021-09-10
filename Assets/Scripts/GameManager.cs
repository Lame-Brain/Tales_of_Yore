using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GAME;
    public static int MAPWIDTH, MAPHEIGHT;
    public static bool PAUSED, ENDTURN;

    public UI UI;
    public GenerationManager GenManager;

    public bool poisoned, bleeding, manaUser, hasSkull, hasBone, hasBook, hasCandle;
    public bool[] hasGem;
    public int isStunned = 0, attkMode = 0;
    public int ForestLevel, playerLevel, numHP_pot, numMP_pot, num_Food, numPoison_pot, num_Bleed_pot, num_arrows, gold;
    public float playerHP, playerMP, playerFood, maxHP, maxMP, maxFood, playerXP, playerXPNNL, playerXPmod;
    public GameObject equipped_melee, equipped_bow, equipped_spell, equipped_armor, equipped_amulet, equipped_shield, focus_item;
    public Transform Pool;

    //[HideInInspector] 
    public List<Enemy> MonsterList = new List<Enemy>();
    [HideInInspector] public int EnemiesDoneWithTurn;

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
        hasGem = new bool[20]; for (int i = 0; i < 20; i++) hasGem[i] = false;
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

    public void ReplaceEquip()
    {
        GameObject _leftBehind = null;
        if (focus_item.GetComponent<Pickup>().itemType == Pickup.type.amulet) _leftBehind = equipped_amulet;
        if (focus_item.GetComponent<Pickup>().itemType == Pickup.type.armor_heavy) _leftBehind = equipped_armor;
        if (focus_item.GetComponent<Pickup>().itemType == Pickup.type.armor_light) _leftBehind = equipped_armor;
        if (focus_item.GetComponent<Pickup>().itemType == Pickup.type.robe) _leftBehind = equipped_armor;
        if (focus_item.GetComponent<Pickup>().itemType == Pickup.type.shield) _leftBehind = equipped_shield;
        if (focus_item.GetComponent<Pickup>().itemType == Pickup.type.axe) _leftBehind = equipped_melee;
        if (focus_item.GetComponent<Pickup>().itemType == Pickup.type.bow) _leftBehind = equipped_bow;
        if (focus_item.GetComponent<Pickup>().itemType == Pickup.type.dagger) _leftBehind = equipped_melee;
        if (focus_item.GetComponent<Pickup>().itemType == Pickup.type.staff) _leftBehind = equipped_melee;
        if (focus_item.GetComponent<Pickup>().itemType == Pickup.type.sword) _leftBehind = equipped_melee;

        if (focus_item.GetComponent<Pickup>().itemType == Pickup.type.amulet) equipped_amulet = Instantiate(focus_item, new Vector3(0, 0, -100), Quaternion.identity);
        if (focus_item.GetComponent<Pickup>().itemType == Pickup.type.armor_heavy) equipped_armor = Instantiate(focus_item, new Vector3(0, 0, -100), Quaternion.identity);
        if (focus_item.GetComponent<Pickup>().itemType == Pickup.type.armor_light) equipped_armor = Instantiate(focus_item, new Vector3(0, 0, -100), Quaternion.identity);
        if (focus_item.GetComponent<Pickup>().itemType == Pickup.type.robe) equipped_armor = Instantiate(focus_item, new Vector3(0, 0, -100), Quaternion.identity);
        if (focus_item.GetComponent<Pickup>().itemType == Pickup.type.shield) equipped_shield = Instantiate(focus_item, new Vector3(0, 0, -100), Quaternion.identity);
        if (focus_item.GetComponent<Pickup>().itemType == Pickup.type.axe) equipped_melee = Instantiate(focus_item, new Vector3(0, 0, -100), Quaternion.identity);
        if (focus_item.GetComponent<Pickup>().itemType == Pickup.type.bow) equipped_bow = Instantiate(focus_item, new Vector3(0, 0, -100), Quaternion.identity);
        if (focus_item.GetComponent<Pickup>().itemType == Pickup.type.dagger) equipped_melee = Instantiate(focus_item, new Vector3(0, 0, -100), Quaternion.identity);
        if (focus_item.GetComponent<Pickup>().itemType == Pickup.type.staff) equipped_melee = Instantiate(focus_item, new Vector3(0, 0, -100), Quaternion.identity);
        if (focus_item.GetComponent<Pickup>().itemType == Pickup.type.sword) equipped_melee = Instantiate(focus_item, new Vector3(0, 0, -100), Quaternion.identity);

        focus_item.GetComponentInChildren<SpriteRenderer>().sprite = _leftBehind.GetComponentInChildren<SpriteRenderer>().sprite;
        focus_item.GetComponent<Pickup>().itemName = _leftBehind.GetComponent<Pickup>().itemName;
        focus_item.GetComponent<Pickup>().min = _leftBehind.GetComponent<Pickup>().min;
        focus_item.GetComponent<Pickup>().max = _leftBehind.GetComponent<Pickup>().max;
        focus_item.GetComponent<Pickup>().value = _leftBehind.GetComponent<Pickup>().value;
        UI.CloseMessage();
    }

    public void TravelToAnotherMap()
    {
        if(focus_item.GetComponent<Sign>() != null) ForestLevel = focus_item.GetComponent<Sign>().destination;
        if (focus_item.GetComponent<Sign>() == null) ForestLevel = 0;
        focus_item = null;
        //StartCoroutine(WaitforLevelToLoadToLoadNextLevel());
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        UI.CloseMessage();
    }

    public void PlayerTurnEnd()
    {
        ENDTURN = true;
        EnemiesDoneWithTurn = 0;
        foreach (Enemy _enemy in MonsterList) _enemy.EnemyTurn();
    }

    public void EnemyTurnEnd()
    {
        EnemiesDoneWithTurn++;
        if(EnemiesDoneWithTurn >= MonsterList.Count)
        {
            ENDTURN = false;
        }
    }
}
