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

        if (Input.GetButtonUp("UpArrow")) Move(Vector2.up);
        if (Input.GetButtonUp("RightArrow")) Move(Vector2.right);
        if (Input.GetButtonUp("DownArrow")) Move(Vector2.down);
        if (Input.GetButtonUp("LeftArrow")) Move(Vector2.left);

    }

    void Move(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1.0f, moveLayerMask);
        if(hit.collider == null)
        {
            transform.Translate(dir);
        }
    }
}
