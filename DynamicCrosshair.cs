using UnityEngine;
using System.Collections;

public class DynamicCrosshair : MonoBehaviour
{
    static public float spread = 0;

    public const int SHOOT_SPREAD = 20;
    public const int JUMP_SPREAD = 40;
    public const int WALK_SPREAD = 10;
    public const int RUN_SPREAD = 30;

    public GameObject crosshair;

    GameObject topLeft;
    GameObject topRight;
    GameObject bottomLeft;
    GameObject bottomRight;

    float initialYPosition;
    float initialXPosition;

    void Start()
    {
        topLeft = crosshair.transform.Find("TopLeft").gameObject;
        bottomLeft = crosshair.transform.Find("BottomLeft").gameObject;
        topRight = crosshair.transform.Find("TopRight").gameObject;
        bottomRight = crosshair.transform.Find("BottomRight").gameObject;

        initialYPosition = topLeft.GetComponent<RectTransform>().localPosition.y;
        initialXPosition = topLeft.GetComponent<RectTransform>().localPosition.x;
    }

    void Update()
    {
        if (spread != 0)
        {
            topLeft.GetComponent<RectTransform>().localPosition = new Vector3(initialXPosition + -spread, initialYPosition + spread, 0);
            bottomLeft.GetComponent<RectTransform>().localPosition = new Vector3(initialXPosition + -spread, -(initialYPosition + spread), 0);
            topRight.GetComponent<RectTransform>().localPosition = new Vector3(-(initialXPosition + -spread), initialYPosition + spread, 0);
            bottomRight.GetComponent<RectTransform>().localPosition = new Vector3(-(initialXPosition + -spread), -(initialYPosition + spread), 0);
            spread -= 2f;
        }
    }

}
