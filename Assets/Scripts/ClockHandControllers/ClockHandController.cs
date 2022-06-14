using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClockHandController : MonoBehaviour
{
    [SerializeField] protected AlarmManger alarmManger;
    [SerializeField] protected AlarmInputFieldManager alarmInputFieldManager;
    [HideInInspector] public bool isActive = false;
    [SerializeField] protected CameraDistanceManager cameraDistanceManager;
    // Start is called before the first frame update

    private void Awake()
    {

    }
    void Start()
    {
        
    }

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
