using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorPosition : MonoBehaviour {

    public Transform transformToMirror;

    public bool x;
    public bool y;

    private void Update()
    {
        this.transform.position = new Vector3(
            x ? transformToMirror.position.x : this.transform.position.x,
            y ? transformToMirror.position.y : this.transform.position.y
            );
    }
}
