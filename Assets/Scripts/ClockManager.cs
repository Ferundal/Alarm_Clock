using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

//Time mode manager
//Update clock hands position if alarm mode is off
public class ClockManager : MonoBehaviour
{
    [SerializeField] private GameObject secondsHand;
    [SerializeField] private GameObject minutesHand;
    [SerializeField] private GameObject hoursHand;
    [SerializeField] private GameObject clockMark;
    [SerializeField] private AlarmManger alarmManger;
    [SerializeField] private TMP_InputField alarmInputField;

    public ClockTime ClockTime { get; private set; }
    private Coroutine clockCoroutine;

    private static readonly string WORLD_TIME_API = "http://worldtimeapi.org/api/ip";
    private static readonly string WORLD_CLOCK_API = "http://worldclockapi.com/api/json/utc/now";
    private void Awake()
    {
        StartCoroutine("StartingCoroutine");
    }

    //Not Proud of code duplication in UpdateClockTime() and StartingCoroutine() but do not know how to move it to functions
    private IEnumerator StartingCoroutine()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(WORLD_TIME_API);
        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            ClockTime = ClockTime.ParseWorldTimeAPI(webRequest.downloadHandler.text);
        }
        else
        {
            webRequest = UnityWebRequest.Get(WORLD_CLOCK_API);
            yield return webRequest.SendWebRequest();
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                ClockTime = ClockTime.ParseWorldClockAPI(webRequest.downloadHandler.text);
            }
            else
            {
                Debug.Log("Can't get start time");
                Application.Quit();
            }
        }
        Debug.Log(ClockTime.Seconds + " " + ClockTime.Minutes + " " + ClockTime.Hours);
        clockCoroutine = StartCoroutine("CountSeconds");
        InvokeRepeating("UpdateClockTime", 3600.0f, 3600.0f);
        SpawnMarks();
    }

    //Not Proud of code duplication in UpdateClockTime() and StartingCoroutine() but do not know how to move it to functions
    private IEnumerator UpdateClockTime()
    {
        ClockTime currentCloackTime = null;
        UnityWebRequest webRequest = UnityWebRequest.Get(WORLD_TIME_API);
        yield return new WaitForSeconds(3600);
        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            currentCloackTime = ClockTime.ParseWorldTimeAPI(webRequest.downloadHandler.text);
        }
        else
        {
            webRequest = UnityWebRequest.Get(WORLD_CLOCK_API);
            yield return webRequest.SendWebRequest();
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                currentCloackTime = ClockTime.ParseWorldClockAPI(webRequest.downloadHandler.text);
            }
        }
        if (currentCloackTime != null)
        {
            StopCoroutine(clockCoroutine);
            ClockTime = currentCloackTime;
            clockCoroutine = StartCoroutine("CountSeconds");
        }
    }

    //Corutine to change time
    private IEnumerator CountSeconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            ClockTime.AddSecond();
            if (!alarmManger.IsAlarmClockMode)
            {
                if (alarmManger.AlarmClockTime!= null && ClockTime.Equals(alarmManger.AlarmClockTime))
                {
                    alarmManger.ShowAlarmReminder();
                }
                UpdateClock();
            }
        }
    }
    //Set clock hands to position according clock time
    private void UpdateClock()
    {
        float secondsAngle = -6.0f * ClockTime.Seconds;
        this.secondsHand.transform.rotation = Quaternion.AngleAxis(secondsAngle, Vector3.forward);
        float minutesAngle = -6.0f * (ClockTime.Minutes + (float)ClockTime.Seconds / 60.0f);
        this.minutesHand.transform.rotation = Quaternion.AngleAxis(minutesAngle, Vector3.forward);
        float hoursAngle = -30.0f * (ClockTime.Hours % 24 + (float)ClockTime.Minutes / 60.0f + (float)ClockTime.Seconds / 3600.0f);
        this.hoursHand.transform.rotation = Quaternion.AngleAxis(hoursAngle, Vector3.forward);
    }
    //Hours marks spawner
    private void SpawnMarks()
    {
        for (int markCounter = 0; markCounter < 12; ++markCounter)
        {
            GameObject currentMark = Instantiate<GameObject>(clockMark);
            currentMark.transform.Rotate(0, 0, -30.0f * markCounter);
        }
    }
}
