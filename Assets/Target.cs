using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    //In this class the target behaviour is defined

    // -- We using placeholder behaviour for first approach (BubbleBehaviour)
    #region BubbleBehaviour

    private Vector3 MovDir = Vector3.zero;
    private Coroutine CurrentChanger = null;

    private void OnEnable()
    {
        CurrentChanger = StartCoroutine(DirChanger());
    }
    private void OnDisable()
    {
        StopCoroutine(CurrentChanger);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        //Return to Bubble Manager
    }

    void Update()
    {
        //Movement
        transform.position += MovDir * Time.deltaTime * 0.5f;

    }
    private IEnumerator DirChanger()
    {
        while(gameObject.activeSelf)
        {
            MovDir = new Vector2(Random.Range(0, 100) * 0.01f, Random.Range(0, 100) * 0.01f);
            yield return new WaitForSeconds(3.0f);
        }
    }

    #endregion
}
