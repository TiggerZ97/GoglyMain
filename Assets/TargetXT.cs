using UnityEditor;
using UnityEngine;
using System.Collections;

// Creates an instance of a primitive depending on the option selected by the user.
public class TargetXT : MonoBehaviour
{
    #region Target Type
    public enum TargetType
    {
        Head, HandLeft, HandRight, FootLeft, FootRight
    };
#endregion

    #region Target Color
    public enum TargetColor
    {
        Pink, Yellow, Green, Blue, White
    };
    #endregion

    //Public variables
    public mJointColor targetColor;
    public mJointType targetType;

    private void Start()
    {
        #region Color Switcher
        Color colorDefined = Color.black;
        switch(targetColor.ToString())
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

        var targetRenderer = GetComponent<Renderer>();
        targetRenderer.material.SetColor("_Color", colorDefined);

        #endregion


        #region Type Switcher
        gameObject.tag = targetType.ToString();
        #endregion

    }
}