using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorPosition : MonoBehaviour {

    public Transform transformToMirror;

    public Vector2Int offset = Vector2Int.zero;

    public bool x;
    public bool y;

    private void Update()
    {
        this.transform.position = new Vector3(
            offset.x + (x ? transformToMirror.position.x : this.transform.position.x),
            offset.y + (y ? transformToMirror.position.y : this.transform.position.y)
            );
    }
}
