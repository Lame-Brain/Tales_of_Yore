using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GAME;
    public static int MAPWIDTH, MAPHEIGHT;

    public bool poisoned, bleeding, manaUser, hasGem;
    public int ForestLevel, playerLevel, playerATK, playerDEF, numHP_pot, numMP_pot, num_Food, numPoison_pot, num_Bleed_pot;
    public float playerHP, playerMP, playerFood, maxHP, maxMP, maxFood, playerXP, playerXPNNL, playerXPmod;

    private void Awake()
    {
        if (GAME != null && GAME != this)
        {
            Destroy(gameObject);
            return;
        }

        GAME = this;
        DontDestroyOnLoad(gameObject);
    }
}
