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
    public GameObject messagePanel, attack_text_panel, ammo_text_panel;
    public TextMeshProUGUI panelMessage;

    private void Awake()
    {
        messagePanel.SetActive(false);
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
        attack_icon.sprite = GameManager.GAME.equipped_weapon.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        Debug.Log("THIS ->" + GameManager.GAME.equipped_weapon.transform.GetChild(0).name);
        attack_string.text = "Atk: " + GameManager.GAME.equipped_weapon.GetComponent<Pickup>().min + "-" + GameManager.GAME.equipped_weapon.GetComponent<Pickup>().max;
        if (GameManager.GAME.attkMode == 0 || GameManager.GAME.attkMode == 2) 
            ammo_text_panel.SetActive(false);
        else        
        {
            ammo_text_panel.SetActive(true);
            ammo_string.text = "Ammo: " + GameManager.GAME.num_arrows;
        }

        //Input for when message panel is active
        if (Input.GetButtonUp("Fire1") || Input.GetButtonUp("Fire2"))
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
