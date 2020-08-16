using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCameraPosition : MonoBehaviour {
    public Vector2 bottomRightOffset = Vector2.zero;

    private void OnLevelWasLoaded()
    {
        this.transform.position = new Vector2(
            Camera.main.orthographicSize * Screen.width / Screen.height + bottomRightOffset.x,
            bottomRightOffset.y - Camera.main.orthographicSize
            );
    }
}
