using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Excercise_Beh : MonoBehaviour
{
    
    public GameObject target_R;
    public GameObject target_L;
    public Transform[] positions_R;
    public Transform[] positions_L;

    private Vector2[] pos_R;
    private Vector2[] pos_L;
    private Coroutine cooler;
    private int state = 0;
    private bool loader_flag = false;
    public int score = -1;
    public bool isenabled= true;
    

    
    void Start()
    {
        #region GetData and set initial conditions
        //Cambiamos el tamaño de array para no tener problemas con NullReference
        Array.Resize(ref pos_R, positions_R.Length);
        Array.Resize(ref pos_L, positions_L.Length);

        for (int i = 0; i<positions_R.Length; i++)
        {

            Vector2 temp_R = new Vector2(positions_R[i].position.x, positions_R[i].position.y);
            Vector2 temp_L = new Vector2(positions_L[i].position.x, positions_L[i].position.y);
            pos_R[i] = temp_R;
            pos_L[i] = temp_L;
        
        }

        //Set initial positions
        target_R.transform.position = pos_R[0];
        target_R.transform.position = pos_L[0];
        state++;

        #endregion
    }
    private void OnEnable()
    {
        if (cooler == null) return;
        StopCoroutine(cooler);
    }
    void Update()
    {
        if (isenabled)
        {
            if (!loader_flag)
            {
                if (!target_R.activeSelf && !target_L.activeSelf)
                {
                    score++;
                    cooler = StartCoroutine(Activate());
                    target_R.transform.position = pos_R[state];
                    target_L.transform.position = pos_L[state];

                    if (state == positions_R.Length - 1)
                    {
                        state = 0;
                        return;
                    }
                    else
                    {
                        state++;
                    }

                }
            }
        }
    }
    private IEnumerator Activate()
    {
        while(!target_R.activeSelf && !target_L.activeSelf)
        {
            loader_flag = true;
            yield return new WaitForSeconds(3.0f);
            target_R.SetActive(true);
            target_L.SetActive(true);
            loader_flag = false;
        }
           
    }

}
