using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClockHandController : MonoBehaviour
{
    //Base class for all Clock Hands Controllers
    [SerializeField] protected AlarmManger alarmManger;
    [SerializeField] protected AlarmInputFieldManager alarmInputFieldManager;
    //Clock Hand trace finger touches only if it was activated my ray touch
    [HideInInspector] public bool isActive = false;
    //We need center of the screen position from CameraDistanceManager, it changed then we rorate screen
    [SerializeField] protected CameraDistanceManager cameraDistanceManager;

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (Input.touchCount == 1)
            {
                Touch screenTouch = Input.GetTouch(0);
                if (screenTouch.phase == TouchPhase.Moved)
                {
                    Vector2 directionFromCenterToCurrentPosition = screenTouch.position - cameraDistanceManager.ScreenCenter;
                    this.transform.rotation = Quaternion.AngleAxis(Vector2.SignedAngle(Vector2.up, directionFromCenterToCurrentPosition), Vector3.forward);
                    ChangeValue();
                }
                else if (screenTouch.phase == TouchPhase.Ended)
                {
                    isActive = false;
                    alarmManger.UpdateAlarmClock();
                }
            }
        }
    }
    protected abstract void ChangeValue();

}
