using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{    
    public string color, monsterName;

    public int HP, maxHP, XP, turns, block, dodge, min, max;
    public Color c;
    public float skip, move, attack, randomness;
    public bool stun, poison, bleed, ranged, dropMeat, dropCoin;

    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }
    private void OnMouseDown()
    {
        player.PlayerClickedOnMonster(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool _dodged = false;
        int _damage = 0;

        if (collision.tag == "arrow" || collision.tag == "spell") 
        {
            collision.GetComponent<Projectile>().ProjectileHit();

            if (Random.Range(0, dodge) > collision.GetComponent<Projectile>().damage) _dodged = true; //if the enemy dodges, they take no damage

            if(!_dodged) _damage = collision.GetComponent<Projectile>().damage - Random.Range(0, block); //if the enemy doesn't dodge, damage is reduced by block roll

            if (!collision.GetComponent<Projectile>().poison) if (_damage < 0) _damage = 0; //Poison can heal. I don't know why. Edgy?

            TakeDamage(_damage, collision.GetComponent<Projectile>().fire, collision.GetComponent<Projectile>().ice); //apply damage

            //STILL NEED TO OUTPUT FEEDBACK
        }
    }

    public void AttackEnemy(Pickup weapon)
    {        
        int _damage = Random.Range(weapon.min, weapon.max + 1);
        bool _dodged = Random.Range(0, dodge) > _damage ? true : false;

        _damage -= Random.Range(0, block);
        if (_damage < 0) _damage = 0;
        
        if(!_dodged) TakeDamage(_damage);

        //OUTPUT FEEDBACK
        Debug.Log("Dodged? " + _dodged + " damage? " + _damage);
    }

    private void TakeDamage(int _dam, bool fire = false, bool ice = false)
    {
        Debug.Log(this.name + " takes " + _dam + " damage.");
        this.HP -= _dam;
        if (this.HP > this.maxHP) this.HP = this.maxHP;
        if (this.HP <= 0) EnemyDies();
    }

    private void EnemyDies()
    {
        Debug.Log(this.name + " dies");
        if (GameManager.GAME.MonsterList.Contains(gameObject)) 
            GameManager.GAME.MonsterList.Remove(gameObject);        
        Destroy(this.gameObject);
    }

    public void EnemyTurn()
    {
        //Debug.Log(this.name + " takes its turn.");
        //GameManager.GAME.EnemyTurnEnd();
        
    }
}
