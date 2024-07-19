using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBlockScript : MonoBehaviour
{
    [SerializeField] float rotateZ = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //このオブジェクトを回転
        Transform transform = this.transform;
        float z = rotateZ * Time.deltaTime;
        transform.Rotate(0.0f, 0.0f, z, Space.Self);
    }
}