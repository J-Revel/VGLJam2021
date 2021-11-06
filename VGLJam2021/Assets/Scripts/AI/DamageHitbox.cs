using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHitbox : MonoBehaviour
{
    void Start()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
