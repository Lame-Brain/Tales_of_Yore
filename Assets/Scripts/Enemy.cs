using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int defense;
    public int HP, maxHP;

    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }
    private void OnMouseDown()
    {
        player.PlayerClickedOnMonster(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "arrow" || collision.tag == "spell") 
        {
            collision.GetComponent<Projectile>().ProjectileHit();
            int _damage = collision.GetComponent<Projectile>().damage - defense;
            if (!collision.GetComponent<Projectile>().poison) if (_damage < 0) _damage = 0; //Poison can heal. I don't know why. Edgy?
            TakeDamage(_damage, collision.GetComponent<Projectile>().fire, collision.GetComponent<Projectile>().ice);
        }
    }

    public void AttackEnemy(Pickup weapon)
    {
        int _damage = Random.Range(weapon.min, weapon.max + 1) - defense;
        if (_damage < 0) _damage = 0;
        TakeDamage(_damage);
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
        if (GameManager.GAME.MonsterList.Contains(GetComponent<Enemy>())) 
            GameManager.GAME.MonsterList.Remove(GetComponent<Enemy>());
        Destroy(this.gameObject);
    }
    public void EnemyTurn()
    {
        Debug.Log(this.name + " takes its turn.");
        GameManager.GAME.EnemyTurnEnd();
    }


}
