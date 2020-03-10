using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerData playerData;
    public RectTransform lifeUI;

    private float lifeRatio;

    private float lifeValue;
    public float life
    {
        get { return lifeValue; }
        set { lifeValue = value; lifeUI.sizeDelta = new Vector2(lifeValue * lifeRatio, lifeUI.sizeDelta.y); }
    }

    private float lifeMaxValue;
    public float lifeMax
    {
        get { return lifeMaxValue; }
        set { lifeMaxValue = value; lifeRatio = lifeUI.parent.GetComponent<RectTransform>().rect.width / value; }
    }


    // Start is called before the first frame update
    void OnEnable()
    {
        lifeMax = playerData.lifeMax;
        life = playerData.life;
    }

    float t = 0f;
    // Update is called once per frame
    void Update()
    {
    }

    public void PlayerHealthUpdate()
    {
        lifeMax = playerData.lifeMax;
        life = playerData.life;
    }
}
