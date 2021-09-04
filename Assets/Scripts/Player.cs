using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public bool canMove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetAxisRaw("Vertical") > 0) transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
        if (Input.GetAxisRaw("Vertical") < 0) transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
        if (Input.GetAxisRaw("Horizontal") < 0) transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        if (Input.GetAxisRaw("Horizontal") > 0) transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        */
        if (Input.GetButtonUp("UpArrow")) Debug.Log("UP");
        if (Input.GetButtonUp("RightArrow")) Debug.Log("RIGHT");
        if (Input.GetButtonUp("DownArrow")) Debug.Log("DOWN");
        if (Input.GetButtonUp("LeftArrow")) Debug.Log("LEFT");
    }
}
