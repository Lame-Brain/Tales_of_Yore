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

    }
}
