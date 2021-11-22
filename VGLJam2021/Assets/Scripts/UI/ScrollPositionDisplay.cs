using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollPositionDisplay : MonoBehaviour
{
    public SnapScrollbox scrollbox;
    public int index;
    public Transform displayElement;
    void Start()
    {
        
    }

    void Update()
    {
        float clampedDistance = Mathf.Clamp(Mathf.Abs(scrollbox.value-index), 0, 1);
        float parentScale = Mathf.Clamp(1 - clampedDistance * 0.5f, 0, 1);
        float scale = 1 - clampedDistance;
        transform.localScale = new Vector3(parentScale, parentScale, parentScale);
        displayElement.localScale = new Vector3(scale, scale, scale);
    }
}
