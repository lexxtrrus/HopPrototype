using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour // вместо масштабирования будет подкрашивать в зелёный и обратно в чёрный через две секунды
{
    private float startTime;
    Renderer rend;

    void Awake()
    {
        startTime = Time.time;
        rend = this.GetComponent<Renderer>();
        rend.material.color = Color.green;
    }

    void Update()
    {
        if(Time.time - startTime >= 1f)
        {
            rend = this.GetComponent<Renderer>();
            rend.material.color = Color.black;
            Destroy(this);
        }  
    }
}
