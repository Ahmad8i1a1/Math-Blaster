using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TargetMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.DOLocalMove(new Vector3(0f, 0f, 0f), 4f);
        if(this.gameObject.transform.position.z <= 0.6 )
        {
            Destroy(this.gameObject);
        }
    }
}
