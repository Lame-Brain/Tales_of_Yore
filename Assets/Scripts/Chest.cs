using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public List<GameObject> loot = new List<GameObject>();

    public void PopTheChest()
    {
        Vector2 _pos, _targetPos; RaycastHit2D hit; bool placed = false;        
        _pos = this.transform.position; _targetPos = Vector2.zero;
        
        Instantiate(loot[0], transform.position, Quaternion.identity); //Place the first treasure where the chest was

        for (int _i = 1; _i < loot.Count; _i++)
        {
            for(int y = -1; y < 2; y++)
                for(int x = -1; x < 2; x++)
                {
                    hit = Physics2D.Raycast(new Vector2(_pos.x + x, _pos.y + y), Vector2.zero, 0.5f);
                    if (!hit)
                    {
                        placed = true;
                        _targetPos = new Vector2(_pos.x + x, _pos.y + y);
                    }
                }
            if (!placed)
            {
                for (int y = -2; y < 3; y++)
                    for (int x = -2; x < 3; x++)
                    {
                        hit = Physics2D.Raycast(new Vector2(_pos.x + x, _pos.y + y), Vector2.zero, 0.5f);
                        if (!hit)
                        {
                            placed = true;
                            _targetPos = new Vector2(_pos.x + x, _pos.y + y);
                        }
                    }
            }

            if (!placed) _targetPos = transform.position;
            Instantiate(loot[_i], _targetPos, Quaternion.identity); //Place the rest of the treasure            
        }
        Destroy(this.gameObject);
    }    
}
