using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject Poison_Icon, Bleed_Icon, Dazed_Icon, ManaPot_Icon, Poison_Pot_Icon, Bleed_Pot_Icon, Gem_Icon;
    public GameObject skull, bone, book, candle;
    public GameObject hpBar, mpBar, xpBar, foodBar;
    public TextMeshProUGUI Stats, numHP_pot, numMP_pot, num_Food, numPoison_pot, numBleed_pot, goldAmount, attack_string, ammo_string;
    public Image attack_icon;
    public GameObject messagePanel, attack_text_panel, ammo_text_panel, replaceMessagePanel, travelMessagePanel;
    public TextMeshProUGUI panelMessage, panelMessage_replace, panelMessage_travel;

    private void Awake()
    {
        CloseMessage();
    }

    private void Update()
    {
        //Status Effects
        Poison_Icon.SetActive(GameManager.GAME.poisoned);
        Bleed_Icon.SetActive(GameManager.GAME.bleeding);
        if (GameManager.GAME.isStunned > 0) 
            Dazed_Icon.SetActive(true);
        else
            Dazed_Icon.SetActive(false);

        //Status Bar
        ManaPot_Icon.SetActive(GameManager.GAME.manaUser);
        Poison_Pot_Icon.SetActive(GameManager.GAME.numPoison_pot > 0);
        Bleed_Pot_Icon.SetActive(GameManager.GAME.num_Bleed_pot > 0);
        Gem_Icon.SetActive(GameManager.GAME.hasGem[GameManager.GAME.ForestLevel]);
        hpBar.GetComponent<Image>().fillAmount = GameManager.GAME.playerHP / GameManager.GAME.maxHP;
        mpBar.GetComponent<Image>().fillAmount = GameManager.GAME.playerMP / GameManager.GAME.maxMP;
        foodBar.GetComponent<Image>().fillAmount = GameManager.GAME.playerFood / GameManager.GAME.maxFood;
        xpBar.GetComponent<Image>().fillAmount = GameManager.GAME.playerXP / GameManager.GAME.playerXPNNL;
        numHP_pot.text = GameManager.GAME.numHP_pot.ToString();
        numMP_pot.text = GameManager.GAME.numMP_pot.ToString();
        num_Food.text = GameManager.GAME.num_Food.ToString();
        numPoison_pot.text = GameManager.GAME.numPoison_pot.ToString();
        numBleed_pot.text = GameManager.GAME.num_Bleed_pot.ToString();        
        
        //Player Stats
        Stats.text = "LVL " + GameManager.GAME.playerLevel +
            " AC: " + CalculateAC() +            
            ", Ddg: " + CalculateDodge();        
        
        //Gold
        goldAmount.text = GameManager.GAME.gold + " gp";
        
        //Ritual Items
        skull.SetActive(GameManager.GAME.hasSkull);
        bone.SetActive(GameManager.GAME.hasBone);
        book.SetActive(GameManager.GAME.hasBook);
        candle.SetActive(GameManager.GAME.hasCandle);

        //Equipped Attack
        if (GameManager.GAME.attkMode == 0)
        {
            attack_icon.sprite = GameManager.GAME.equipped_melee.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            attack_string.text = GameManager.GAME.equipped_melee.GetComponent<Pickup>().itemName + "\nAtk: " + GameManager.GAME.equipped_melee.GetComponent<Pickup>().min + "-" + GameManager.GAME.equipped_melee.GetComponent<Pickup>().max;
            ammo_text_panel.SetActive(false);
        }
        if (GameManager.GAME.attkMode == 1 && GameManager.GAME.equipped_bow != null)
        {
            attack_icon.sprite = GameManager.GAME.equipped_bow.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            attack_string.text = "Atk: " + GameManager.GAME.equipped_bow.GetComponent<Pickup>().min + "-" + GameManager.GAME.equipped_bow.GetComponent<Pickup>().max;
            ammo_text_panel.SetActive(true);
            ammo_string.text = "Ammo: " + GameManager.GAME.num_arrows;
        }
        if (GameManager.GAME.attkMode == 2 && GameManager.GAME.equipped_spell != null)
        {
            attack_icon.sprite = GameManager.GAME.equipped_spell.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            attack_string.text = "Atk: " + GameManager.GAME.equipped_spell.GetComponent<Pickup>().min + "-" + GameManager.GAME.equipped_spell.GetComponent<Pickup>().max;
            ammo_text_panel.SetActive(false);
        }

        //Input for when message panel is active
        if (Input.GetButtonUp("Jump"))
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

    public void OpenReplaceMessage(string s)
    {
        replaceMessagePanel.SetActive(true);
        panelMessage_replace.text = s;
        GameManager.PAUSED = true;
    }

    public void OpenTravelMessage(string s)
    {
        travelMessagePanel.SetActive(true);
        panelMessage_travel.text = s;
        GameManager.PAUSED = true;
    }

    public void CloseMessage()
    {
        messagePanel.SetActive(false);
        replaceMessagePanel.SetActive(false);
        travelMessagePanel.SetActive(false);
        GameManager.PAUSED = false;        
    }

    public int CalculateAC()
    {
        ///this is a pickup armor-type's min value
        ///If a character has both shield and amulet, only the higer value is used
        int _ac = 0;
        if (GameManager.GAME.equipped_armor != null) _ac += GameManager.GAME.equipped_armor.GetComponent<Pickup>().min;
        if (GameManager.GAME.equipped_shield != null && GameManager.GAME.equipped_amulet == null) _ac += GameManager.GAME.equipped_shield.GetComponent<Pickup>().min;
        if (GameManager.GAME.equipped_shield == null && GameManager.GAME.equipped_amulet != null) _ac += GameManager.GAME.equipped_amulet.GetComponent<Pickup>().min;
        if (GameManager.GAME.equipped_shield != null && GameManager.GAME.equipped_amulet != null)
        {
            if (GameManager.GAME.equipped_shield.GetComponent<Pickup>().min > GameManager.GAME.equipped_amulet.GetComponent<Pickup>().min)
                _ac += GameManager.GAME.equipped_shield.GetComponent<Pickup>().min;
            else
                _ac += GameManager.GAME.equipped_amulet.GetComponent<Pickup>().min;
        }
        return _ac;
    }
    public int CalculateDodge()
    {
        ///this is a pickup armor-type's max value
        ///If a character has both shield and amulet, only the higher value is used
        int _ac = 0;
        if (GameManager.GAME.equipped_armor != null) _ac += GameManager.GAME.equipped_armor.GetComponent<Pickup>().max;
        if (GameManager.GAME.equipped_shield != null && GameManager.GAME.equipped_amulet == null) _ac += GameManager.GAME.equipped_shield.GetComponent<Pickup>().max;
        if (GameManager.GAME.equipped_shield == null && GameManager.GAME.equipped_amulet != null) _ac += GameManager.GAME.equipped_amulet.GetComponent<Pickup>().max;
        if (GameManager.GAME.equipped_shield != null && GameManager.GAME.equipped_amulet != null)
        {
            if (GameManager.GAME.equipped_shield.GetComponent<Pickup>().max > GameManager.GAME.equipped_amulet.GetComponent<Pickup>().max)
                _ac += GameManager.GAME.equipped_shield.GetComponent<Pickup>().max;
            else
                _ac += GameManager.GAME.equipped_amulet.GetComponent<Pickup>().max;
        }
        return _ac;
    }
}
