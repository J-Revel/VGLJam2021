using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX_blood_rand : MonoBehaviour
{
    private int rand;
    
    public Sprite[] Sprite_Pic;

    // Start is called before the first frame update
    void Start ()
    {
        rand = Random.Range(0,Sprite_Pic.Length);
        GetComponent<SpriteRenderer>().sprite = Sprite_Pic[rand];
    }

    
}
