using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
   public CommUniPython DataManager;
   private int val_bpm, val_spo2, val_state;
    public TMP_Text text_bpm, text_spo2, text_state;
    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.dataFinal == null)
        {
            Debug.Log("No Data Ref Found!");
            return;
        }
   
    }

    // Update is called once per frame
    void Update()
    {
        if (DataManager.dataFinal == null)
        {
            Debug.Log("No Data Ref Found!");
            return;
        }
        else
        {
            Debug.Log(DataManager.dataFinal[0]);
            Debug.Log(DataManager.dataFinal[1]);
            Debug.Log(DataManager.dataFinal[2]);

            val_bpm = DataManager.dataFinal[0];
            val_spo2 = DataManager.dataFinal[1];
            val_state = DataManager.dataFinal[2];

            //Update GUI
            text_bpm.text = val_bpm.ToString();
            text_spo2.text = val_spo2.ToString() + '%';
            text_state.text = val_state.ToString();
        }
    }
}
