using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer_Controller : MonoBehaviour
{
    public TMP_Text txtTime; 
    public float CD_CurrentTime = 0f;
    public float CD_StartingTime = 6f;

    public float T_CurrentTime = 0f;
    public float T_StartTime = 181f;

    public bool CDrunning = true;
    public bool Trunning = false;

    void Start()
    {
        CD_CurrentTime = CD_StartingTime;
        T_CurrentTime = T_StartTime;
    }

    private void Update()
    {
        if (CDrunning) // Countdown
        {
            CD_CurrentTime -= Time.deltaTime;
            string temp = CD_CurrentTime.ToString("0");
            if (CD_CurrentTime == 0 || temp == "0") //Show Start at 0 and restar counter
            {
                temp = "Start";
                txtTime.color = Color.green;
                
            }
            if (temp =="-1")
            {
                CDrunning = false;
                Trunning = true;
                CD_CurrentTime = 0;
                CD_CurrentTime = CD_StartingTime;
                txtTime.color = Color.white;
            }
            txtTime.text = temp;
        }
        else if(Trunning) // Timer
        {

            T_CurrentTime -= Time.deltaTime;
            DisplayTime(T_CurrentTime);
        }
        
    }
    void DisplayTime(float timetoDisplay)
    {
        if(timetoDisplay<0)
        {
            timetoDisplay = 0;
        }
        float minutes = Mathf.FloorToInt(timetoDisplay / 60);
        float seconds = Mathf.FloorToInt(timetoDisplay % 60);
        txtTime.text = string.Format("{0:0}:{1:00}",minutes,seconds);
    }

}
