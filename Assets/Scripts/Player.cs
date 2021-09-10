using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public bool canMove;
        
    public LayerMask moveLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        //DEBUG. Putting this here ensures that it does not run until the level is loaded. (otherwise it deletes my rooms when the level finishes loading... annoying.)
        Random.InitState(42 + GameManager.GAME.ForestLevel);
        GameManager.GAME.GenManager.Generate();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.PAUSED || GameManager.ENDTURN)
            canMove = false;
        else
            canMove = true;
        

        if (canMove)
        {
            if (Input.GetButtonDown("UpArrow")) Move(Vector2.up);
            if (Input.GetButtonDown("RightArrow")) Move(Vector2.right);
            if (Input.GetButtonDown("DownArrow")) Move(Vector2.down);
            if (Input.GetButtonDown("LeftArrow")) Move(Vector2.left);
        }
    }

    void Move(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1.0f, moveLayerMask);
        if(hit.collider == null) //Run into ground tile. (move)
        {
            transform.Translate(dir);
        }

        if (hit.collider != null && hit.collider.CompareTag("Sign") && GameManager.GAME.hasGem[GameManager.GAME.ForestLevel] == false) //Run into a sign with no gem
        {
            GameManager.GAME.UI.OpenMessage("This Sign Post is shrouded in Darkness.");
        }
        if (hit.collider != null && hit.collider.CompareTag("Sign") && GameManager.GAME.hasGem[GameManager.GAME.ForestLevel] == true) //Run into a sign with the gem for this level
        {
            GameManager.GAME.focus_item = hit.collider.gameObject;
            GameManager.GAME.UI.OpenTravelMessage("The Gem lifts the darkness from the sign, showing you the way forward.");
        }


        if (hit.collider != null && hit.collider.CompareTag("Chest"))
        {
            hit.collider.GetComponent<Chest>().PopTheChest();
        }

        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            hit.collider.GetComponent<Enemy>().AttackEnemy(GameManager.GAME.equipped_melee.GetComponent<Pickup>());
        }

        GameManager.GAME.PlayerTurnEnd();
    }

    public void PlayerClickedOnMonster(GameObject monster)
    {        
        if (canMove && GameManager.GAME.attkMode == 1 && GameManager.GAME.num_arrows > 0)
        {
            GameManager.GAME.num_arrows--;
            for (int a = 0; a < GameManager.GAME.Pool.Find("ArrowPool").childCount; a++)
                if (!GameManager.GAME.Pool.Find("ArrowPool").GetChild(a).GetComponent<Projectile>().active)
                {
                    GameManager.GAME.Pool.Find("ArrowPool").GetChild(a).transform.position = this.transform.position;

                    Vector3 dir = monster.transform.position - transform.position;
                    float angle = (Mathf.Atan2(dir.y + Random.Range(-1.5f, 1.5f), dir.x + Random.Range(-1.5f, 1.5f)) * Mathf.Rad2Deg) - 90f;
                    GameManager.GAME.Pool.Find("ArrowPool").GetChild(a).transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                    GameManager.GAME.Pool.Find("ArrowPool").GetChild(a).GetComponent<Projectile>().FireProjectile();
                    return;
                }
        }
        //Spell Attack
        if (canMove && GameManager.GAME.attkMode == 2 && ThereIsEnoughMana(GameManager.GAME.equipped_spell.GetComponent<Pickup>()))
        {
            GameManager.GAME.playerMP -= GameManager.GAME.equipped_spell.GetComponent<Pickup>().cost;
            if (GameManager.GAME.playerMP < 0) GameManager.GAME.playerMP = 0;

            GameObject _obj = null;
            if (GameManager.GAME.equipped_spell.GetComponent<Pickup>().itemName == "Mage Blast") _obj = GameManager.GAME.Pool.Find("MageBlastPool").gameObject;
            if (GameManager.GAME.equipped_spell.GetComponent<Pickup>().itemName == "Fire Bolt") _obj = GameManager.GAME.Pool.Find("FireBoltPool").gameObject;
            if (GameManager.GAME.equipped_spell.GetComponent<Pickup>().itemName == "Frost Blast") _obj = GameManager.GAME.Pool.Find("FrostBlastPool").gameObject;
            if (GameManager.GAME.equipped_spell.GetComponent<Pickup>().itemName == "Poison Splash") _obj = GameManager.GAME.Pool.Find("PoisonSplashPool").gameObject;
            if (GameManager.GAME.equipped_spell.GetComponent<Pickup>().itemName == "Mage Missile") _obj = GameManager.GAME.Pool.Find("MageMissilePool").gameObject;
            if (GameManager.GAME.equipped_spell.GetComponent<Pickup>().itemName == "Fireball") _obj = GameManager.GAME.Pool.Find("FireballPool").gameObject;
            if (GameManager.GAME.equipped_spell.GetComponent<Pickup>().itemName == "Blizzard") _obj = GameManager.GAME.Pool.Find("BlizzardPool").gameObject;
            if (GameManager.GAME.equipped_spell.GetComponent<Pickup>().itemName == "Death Bolt") _obj = GameManager.GAME.Pool.Find("DeathTouchPool").gameObject;


            for (int a = 0; a < _obj.transform.childCount; a++)
                if (!_obj.transform.GetChild(a).GetComponent<Projectile>().active)
                {
                    _obj.transform.GetChild(a).transform.position = this.transform.position;

                    Vector3 dir = monster.transform.position - transform.position;
                    float angle = (Mathf.Atan2(dir.y + Random.Range(-1.5f, 1.5f), dir.x + Random.Range(-1.5f, 1.5f)) * Mathf.Rad2Deg) - 90f;
                    _obj.transform.GetChild(a).transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                    _obj.transform.GetChild(a).GetComponent<Projectile>().FireProjectile();
                    return;
                }
        }
    }

    private bool ThereIsEnoughMana(Pickup _spell)
    {
        //Debug.Log("spell costs " + _spell.cost + " and I have " + GameManager.GAME.playerMP);
        bool _result = false; 
        if (_spell != null && _spell.cost <= GameManager.GAME.playerMP) _result = true;
        return _result;
    }
}
