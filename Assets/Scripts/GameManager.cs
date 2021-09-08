using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GAME;
    public static int MAPWIDTH, MAPHEIGHT;
    public static bool PAUSED;

    public UI UI;

    public bool poisoned, bleeding, manaUser, equippedSpell, equippedBow, hasGem, hasSkull, hasBone, hasBook, hasCandle;
    public int isStunned = 0, attkMode = 0;
    public int ForestLevel, playerLevel, numHP_pot, numMP_pot, num_Food, numPoison_pot, num_Bleed_pot, num_arrows, gold;
    public float playerHP, playerMP, playerFood, maxHP, maxMP, maxFood, playerXP, playerXPNNL, playerXPmod;
    public GameObject equipped_weapon, equipped_armor, equipped_amulet, equipped_shield;

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
}
