using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AlarmInputFieldManager : MonoBehaviour
{
    [SerializeField] private AlarmManger alarmManger;
    [SerializeField] private int minValue;
    [SerializeField] private int maxValue;
    [SerializeField] private TMP_InputField input;

    //Method for non-user changes of field value
    public string InputFieldValue
    {
        get
        {
            return input.text;
        }
        set
        {
            isUserInput = false;
            input.text = value;
            isUserInput = true;
        }
    }

    private bool negativeAllowed;
    private bool isUserInput = true;

    private void Awake()
    {
        if (minValue < maxValue && minValue < 0)
        {
            negativeAllowed = true;
        }
        else
        {
            negativeAllowed = false;
        }
    }

    //Called every time then user change field value
    //Remove '-' if negative is not allowed
    public void FixInput()
    {
        if (alarmManger.IsAlarmClockMode && isUserInput)
        {
            if (!negativeAllowed && input.text.Contains("-"))
            {
                InputFieldValue = InputFieldValue.Replace("-", "");
            }
            else if (input.text.Length != 0)
            {
                {
                    int value = int.Parse(input.text);
                    if (value < minValue)
                    {
                        input.text = minValue.ToString();
                    }
                    else if (value > maxValue)
                    {
                        input.text = maxValue.ToString();
                    }
                }
            }
            alarmManger.ReadImputTimeFields();
        }
    }
    

}
