using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
   public CommUniPython DataManager;
   private double val_bpm, val_spo2, val_state;
    private double rwWeight,rwTopLeft, rwTopRight, rwBotLeft, rwBotRight;
    private Vector3 aPos;
    public TMP_Text text_bpm, text_spo2, text_state;
    public RectTransform knob;
    public double y_pos, x_pos;
    public double val_top, val_bot, val_left, val_right;

    private void Start()
    {
        aPos = knob.anchoredPosition;
        y_pos = 0;
        x_pos = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    
        if (DataManager.data_Wii== null)
        {
            Debug.Log("No Data BIO Found!");
        }
        else
        {
            rwWeight = DataManager.data_Wii[0];
            rwTopLeft = DataManager.data_Wii[1];
            rwTopRight = DataManager.data_Wii[2];
            rwBotLeft = DataManager.data_Wii[3];
            rwBotRight = DataManager.data_Wii[4];
        }
        

        if (DataManager.data_ESP == null)
        {
            Debug.Log("No Data BIO Found!");
        }
        else
        {
            //Debug.Log("BPM,SPO2,STATE VALUES");
            //Debug.Log(DataManager.data_ESP[0].ToString() + "," + DataManager.data_ESP[1].ToString() + "," + DataManager.data_ESP[2].ToString());

            val_bpm = DataManager.data_ESP[0];
            val_spo2 = DataManager.data_ESP[1];
            val_state = DataManager.data_ESP[2];

            
        }

         

    }
    private void Update()
    {
        //Update GUI
        text_bpm.text = val_bpm.ToString();
        text_spo2.text = val_spo2.ToString() + '%';
        text_state.text = val_state.ToString();
        
        //Update Wii-Mat
        val_left = rwBotLeft + rwTopLeft;
        val_right = rwBotRight + rwTopRight;
        val_top = rwTopLeft + rwTopRight;
        val_bot = rwBotLeft + rwBotRight;

        //Mapping handling

        //y axis
        if (val_bot < val_top)
        {
            y_pos = val_top - val_bot;
        }
        else if (val_bot > val_top)
        {
            y_pos = val_bot - val_top;
            y_pos = y_pos * -1;
        }
        else
        {
            y_pos = 0;
        }

        //x axis
        if (val_left < val_right)
        {
            x_pos = val_right - val_left;
        }
        else if (val_left > val_right)
        {
            x_pos = val_left - val_right;
            x_pos = x_pos * -1;
        }
        else
        {
            x_pos = 0;
        }

        x_pos = x_pos * 2;
        aPos.y = Convert.ToSingle(y_pos);
        aPos.x = Convert.ToSingle(x_pos);
        knob.anchoredPosition = aPos;

 
    }
}
