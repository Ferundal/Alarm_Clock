using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoursClockHandController : ClockHandController
{
    protected override void ChangeValue()
    {
        int newHoursClockHandValue = (12 - (int)Mathf.Round(this.transform.rotation.eulerAngles.z / 30.0f)) % 12;
        int diff = newHoursClockHandValue - alarmManger.AlarmClockTime.Hours % 12;
        if (diff > 6)
        {
            diff -= 12;
        }
        else if (diff < -6)
        {
            diff += 12;
        }
        Debug.Log(diff);
        alarmManger.AlarmClockTime.Hours += diff;
        alarmInputFieldManager.InputFieldValue = alarmManger.AlarmClockTime.Hours.ToString();
    }
}
