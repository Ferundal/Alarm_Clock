using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Track resolution changes and change camera distance. Also change screen center coordinates
public class CameraDistanceManager : MonoBehaviour
{
    private int width;
    private int height;
    private float startDistance;
    Vector3 cameraPosition;
    public Vector2 ScreenCenter { get; private set; }

    // Start is called before the first frame update
    private void Awake()
    {
        startDistance = transform.position.z;
        cameraPosition = transform.position;
        width = Screen.width;
        height = Screen.height;
        FixCameraPosition();
        ScreenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    void Update()
    {
        //If resolution changed - change camera position and center
        if (width != Screen.width || height != Screen.height)
        {
            Debug.Log("Screen.width: " + Screen.width + "Screen.height :" + Screen.height);
            FixCameraPosition();
            ScreenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
            width = Screen.width;
            height = Screen.height;
        }
    }
    private void FixCameraPosition()
    {
        if (Screen.width < Screen.height)
        {
            cameraPosition.z = startDistance / Screen.width * Screen.height;
        }
        else
        {
            cameraPosition.z = startDistance;
        }
        this.gameObject.transform.position = cameraPosition;
    }
}
