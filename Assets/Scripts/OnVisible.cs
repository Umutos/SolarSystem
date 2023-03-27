using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnVisible : MonoBehaviour
{
    [SerializeField] private VectorPoint point;
    // Start is called before the first frame update
    private void OnBecameVisible()
    {
        point.enabled = true;
    }

    // Update is called once per frame
    private void OnBecameInvisible()
    {
        point.enabled = false;
    }
}
