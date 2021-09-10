using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool active;
    public float speed, range;
    public int damage;
    public bool fire, poison, ice;

    private void Start()
    {
        this.active = false;
        this.transform.position = GameManager.GAME.Pool.position;
    }

    private void Update()
    {
        if (active) transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    public void FireProjectile(bool playershot = true)
    {
        StartCoroutine(FlightTime(playershot));
    }

    IEnumerator FlightTime(bool playershot)
    {
        GameManager.ENDTURN = true;
        this.active = true;
        int _min = 0, _max = 0;
        if (this.tag == "arrow")
        {
            _min = GameManager.GAME.equipped_bow.GetComponent<Pickup>().min;
            _max = GameManager.GAME.equipped_bow.GetComponent<Pickup>().max;
        }
        else if(this.tag == "spell")
        {
            _min = GameManager.GAME.equipped_spell.GetComponent<Pickup>().min;
            _max = GameManager.GAME.equipped_spell.GetComponent<Pickup>().max;
        }
        this.damage = Random.Range(_min, _max + 1);
        Debug.Log("min is " + _min + ", max is " + (_max + 1) + " result is " + damage);

        yield return new WaitForSeconds(range);

        this.active = false;
        this.transform.position = GameManager.GAME.Pool.position;
        this.transform.rotation = Quaternion.identity;
        this.damage = 0;

        if (playershot)
        {
            GameManager.GAME.PlayerTurnEnd();
        }

        if (!playershot)
        {

        }
    }

    public void ProjectileHit()
    {
        this.active = false;
        this.transform.position = GameManager.GAME.Pool.position;
        this.transform.rotation = Quaternion.identity;        
    }
}
