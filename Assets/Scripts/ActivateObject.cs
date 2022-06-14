using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Activates clock hands with rays if alarm mode is on
public class ActivateObject : MonoBehaviour
{
    //We activate clock hands only in Alarm Mode from Alarm Manager
    [SerializeField] private AlarmManger alarmManger;

    // Update is called once per frame
    void Update()
    {
        if (alarmManger.IsAlarmClockMode)
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("ClockHand"))
                    {
                        Debug.Log("Touch");
                        ClockHandController clockHandController = hit.collider.gameObject.transform.GetComponentInParent<ClockHandController>();
                        clockHandController.isActive = true;
                    }
                }
            }
        }
    }
}
