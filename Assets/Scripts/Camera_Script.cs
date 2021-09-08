using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Script : MonoBehaviour
{
    public bool followPlayer;
    public float cameraMoveSpeed;

    Vector3 _targetPos;
    float shakeX = 0, shakeY = 0, shake = 0;

    void Update()
    {
        if (followPlayer && GameObject.FindGameObjectWithTag("Player") != null) 
        { 
            _targetPos.x = GameObject.FindGameObjectWithTag("Player").transform.position.x; 
            _targetPos.y = GameObject.FindGameObjectWithTag("Player").transform.position.y - 1;
            _targetPos.z = -10;
        }
        else
        {
            _targetPos.x = 0f;
            _targetPos.y = 0f;
            _targetPos.z = -10;
        }

        transform.position = Vector3.Lerp(transform.position, _targetPos, cameraMoveSpeed * Time.deltaTime);
        shakeX = Random.Range(-shake, shake); shakeY = Random.Range(-shake, shake);
        transform.position = new Vector3(this.transform.position.x + shakeX, this.transform.position.y + shakeY, _targetPos.z);
    }

    public void ScreenShake(float n)
    {
        shake = n;
        StartCoroutine(CeaseThyShaking());
    }

    IEnumerator CeaseThyShaking()
    {
        yield return new WaitForSeconds(0.25f);
        shake = 0;
    }
}
