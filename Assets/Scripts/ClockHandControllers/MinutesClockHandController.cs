using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinutesClockHandController : ClockHandController
{
    protected override void ChangeValue()
    {
        alarmManger.AlarmClockTime.Minutes = 60 - (int)Mathf.Round(this.transform.rotation.eulerAngles.z / 6.0f);
        alarmInputFieldManager.InputFieldValue = alarmManger.AlarmClockTime.Minutes.ToString();
    }
}
