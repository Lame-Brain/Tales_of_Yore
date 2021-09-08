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
        
    }

    // Update is called once per frame
    void Update()
    {
        canMove = !GameManager.PAUSED;
        if (canMove)
        {
            if (Input.GetButtonUp("UpArrow")) Move(Vector2.up);
            if (Input.GetButtonUp("RightArrow")) Move(Vector2.right);
            if (Input.GetButtonUp("DownArrow")) Move(Vector2.down);
            if (Input.GetButtonUp("LeftArrow")) Move(Vector2.left);
        }
    }

    void Move(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1.0f, moveLayerMask);
        if(hit.collider == null) //Run into ground tile. (move)
        {
            transform.Translate(dir);
        }

        if (hit.collider != null && hit.collider.CompareTag("Sign") && GameManager.GAME.hasGem == false) //Run into a sign with no gem
        {
            GameManager.GAME.UI.OpenMessage("This Sign Post is shrouded in Darkness.");
        }


        if (hit.collider != null && hit.collider.CompareTag("Chest"))
        {
            hit.collider.GetComponent<Chest>().PopTheChest();
        }

    }
}
