using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAtAnimEnd : MonoBehaviour
{
    public AnimatedSprite animatedSprite;

    void Update()
    {
        if(animatedSprite.isAnimationFinished)
            Destroy(gameObject);        
    }
}
