using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject Poison_Icon, Bleed_Icon, ManaPot_Icon, Poison_Pot_Icon, Bleed_Pot_Icon, Gem_Icon;
    public GameObject skull, bone, book, candle;
    public GameObject hpBar, mpBar, xpBar, foodBar;
    public TextMeshProUGUI Stats, numHP_pot, numMP_pot, num_Food, numPoison_pot, numBleed_pot, goldAmount;

    public GameObject messagePanel;
    public TextMeshProUGUI panelMessage;

    private void Awake()
    {
        messagePanel.SetActive(false);
    }

    private void Update()
    {
        Poison_Icon.SetActive(GameManager.GAME.poisoned);
        Bleed_Icon.SetActive(GameManager.GAME.bleeding);
        ManaPot_Icon.SetActive(GameManager.GAME.manaUser);
        Poison_Pot_Icon.SetActive(GameManager.GAME.numPoison_pot > 0);
        Bleed_Pot_Icon.SetActive(GameManager.GAME.num_Bleed_pot > 0);
        Gem_Icon.SetActive(GameManager.GAME.hasGem);
        hpBar.GetComponent<Image>().fillAmount = GameManager.GAME.playerHP / GameManager.GAME.maxHP;
        mpBar.GetComponent<Image>().fillAmount = GameManager.GAME.playerMP / GameManager.GAME.maxMP;
        foodBar.GetComponent<Image>().fillAmount = GameManager.GAME.playerFood / GameManager.GAME.maxFood;
        xpBar.GetComponent<Image>().fillAmount = GameManager.GAME.playerXP / GameManager.GAME.playerXPNNL;
        numHP_pot.text = GameManager.GAME.numHP_pot.ToString();
        numMP_pot.text = GameManager.GAME.numMP_pot.ToString();
        num_Food.text = GameManager.GAME.num_Food.ToString();
        numPoison_pot.text = GameManager.GAME.numPoison_pot.ToString();
        numBleed_pot.text = GameManager.GAME.num_Bleed_pot.ToString();
        Stats.text = "LVL " + GameManager.GAME.playerLevel +
            " ATK: " + GameManager.GAME.playerATK +
            " DEF: " + GameManager.GAME.playerDEF;
        goldAmount.text = GameManager.GAME.gold + " gp";
        skull.SetActive(GameManager.GAME.hasSkull);
        bone.SetActive(GameManager.GAME.hasBone);
        book.SetActive(GameManager.GAME.hasBook);
        candle.SetActive(GameManager.GAME.hasCandle);

        if(Input.GetButtonUp("Fire1") || Input.GetButtonUp("Fire2"))
        {
            if (messagePanel.activeSelf) CloseMessage();
        }
    }

    public void OpenMessage(string s)
    {
        messagePanel.SetActive(true);
        panelMessage.text = s;
        GameManager.PAUSED = true;
    }

    public void CloseMessage()
    {
        messagePanel.SetActive(false);
        GameManager.PAUSED = false;
    }
}
