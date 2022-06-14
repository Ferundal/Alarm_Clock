using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlarmManger : MonoBehaviour
{
    [SerializeField] private ClockManager clockManager;
    [SerializeField] private Button alarmModeButton;
    [SerializeField] private AlarmInputFieldManager alarmHoursInputFieldManager;
    [SerializeField] private AlarmInputFieldManager alarmMinutesInputFieldManager;
    [SerializeField] private AlarmInputFieldManager alarmSecondsInputFieldManager;

    [SerializeField] private GameObject secondsHand;
    [SerializeField] private GameObject minutesHand;
    [SerializeField] private GameObject hoursHand;

    [SerializeField] private GameObject alarmReminder;
    public ClockTime AlarmClockTime { get; private set; }

    private bool b_IsAlarmClockMode = false;
    public bool IsAlarmClockMode
    {
        get
        {
            return b_IsAlarmClockMode;
        }
        private set
        {
            alarmReminder.SetActive(false);
            UpdateAlarmClock();
            b_IsAlarmClockMode = value;
        }
    }
    // Start is called before the first frame update
    public void SwitchMode()
    {
        if (IsAlarmClockMode)
        {
            alarmModeButton.GetComponentInChildren<Text>().text = "Alarm Mode";
            alarmHoursInputFieldManager.gameObject.SetActive(false);
            alarmMinutesInputFieldManager.gameObject.SetActive(false);
            alarmSecondsInputFieldManager.gameObject.SetActive(false);
            IsAlarmClockMode = false;
        }
        else
        {
            if (AlarmClockTime == null)
            {
                AlarmClockTime = new ClockTime(clockManager.ClockTime);
                alarmHoursInputFieldManager.InputFieldValue = AlarmClockTime.Hours.ToString();
                alarmMinutesInputFieldManager.InputFieldValue = AlarmClockTime.Minutes.ToString();
                alarmSecondsInputFieldManager.InputFieldValue = AlarmClockTime.Seconds.ToString();
            }
            alarmHoursInputFieldManager.gameObject.SetActive(true);
            alarmMinutesInputFieldManager.gameObject.SetActive(true);
            alarmSecondsInputFieldManager.gameObject.SetActive(true);
            alarmModeButton.GetComponentInChildren<Text>().text = "Set Alarm";
            IsAlarmClockMode = true;
        }
    }

    private int ReadImputTimeField(AlarmInputFieldManager alarmInputFieldManager)
    {
        if (alarmInputFieldManager.InputFieldValue.Length == 0)
        {
            return 0;
        } else
        {
            return int.Parse(alarmInputFieldManager.InputFieldValue);
        }
    }
    public void ReadImputTimeFields()
    {
        AlarmClockTime.Hours = ReadImputTimeField(alarmHoursInputFieldManager);
        AlarmClockTime.Minutes = ReadImputTimeField(alarmMinutesInputFieldManager);
        AlarmClockTime.Seconds = ReadImputTimeField(alarmSecondsInputFieldManager);
        UpdateAlarmClock();
    }

    public void UpdateAlarmClock()
    {
        float secondsAngle = -6.0f * AlarmClockTime.Seconds;
        secondsHand.transform.rotation = Quaternion.AngleAxis(secondsAngle, Vector3.forward);
        float minutesAngle = -6.0f * AlarmClockTime.Minutes;
        minutesHand.transform.rotation = Quaternion.AngleAxis(minutesAngle, Vector3.forward);
        float hoursAngle = -30.0f * (AlarmClockTime.Hours % 12);
        hoursHand.transform.rotation = Quaternion.AngleAxis(hoursAngle, Vector3.forward);
    }

    public void ShowAlarmReminder()
    {
        alarmReminder.SetActive(true);
    }
}
