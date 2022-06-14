using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : MonoBehaviour
{
    [SerializeField] private AlarmManger alarmManger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

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
