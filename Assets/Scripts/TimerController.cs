using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI timerText;
    
    public static float timeRemaining;

    private void Start()
    {
        timeRemaining = 10f;
    }

    private void Update()
    {
        if (GameController.state == State.Gameplay)
        {
            timeRemaining -= Time.deltaTime;

            switch (Mathf.Ceil(timeRemaining))
            {
                case 10:
                    timerText.text = "10";
                    break;
                case 9:
                    timerText.text = "9";
                    break;
                case 8:
                    timerText.text = "8";
                    break;
                case 7:
                    timerText.text = "7";
                    break;
                case 6:
                    timerText.text = "6";
                    break;
                case 5:
                    timerText.text = "5";
                    break;
                case 4:
                    timerText.text = "4";
                    break;
                case 3:
                    timerText.text = "3";
                    break;
                case 2:
                    timerText.text = "2";
                    break;
                case 1:
                    timerText.text = "1";
                    break;
                case 0:
                    timerText.text = "0";
                    break;
            }
        }
    }
}
