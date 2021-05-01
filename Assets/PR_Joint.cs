using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum mJointType
{
    HandRight, HandLeft, Head, AnkleRight, AnkleLeft
};

public enum mJointColor
{
    Pink, Yellow, Green, Blue, White
};
public class PR_Joint : MonoBehaviour
{
    
    public Transform mesh;

    public mJointType jointType;
    public mJointColor jointColor;

  
    private void Start()
    {
        #region Color Switcher
        Color colorDefined = Color.black;
        switch (jointColor.ToString())
        {
            case "Pink":
                colorDefined = Color.magenta;
                break;
            case "Yellow":
                colorDefined = Color.yellow;
                break;
            case "Green":
                colorDefined = Color.green;
                break;
            case "Blue":
                colorDefined = Color.blue;
                break;
            case "White":
                colorDefined = Color.white;
                break;
        }

        var targetRenderer = GetComponentInChildren<Renderer>();
        targetRenderer.material.SetColor("_Color", colorDefined);

        #endregion
    }
    // Update is called once per frame
    private void Update()
    {
        mesh.position = Vector3.Lerp(mesh.position, transform.position, Time.deltaTime * 15.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (!collision.gameObject.CompareTag(jointType.ToString()))
            return;
        collision.gameObject.SetActive(false);

    }
}
