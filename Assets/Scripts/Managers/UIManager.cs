using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PlayerData playerData;

    public RectTransform lifeUI;
    public Text lifeText;
    public List<Image> weaponsInv = new List<Image>();

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

    private int chosenW = 0;


    // Start is called before the first frame update
    void OnEnable()
    {
        lifeMax = playerData.lifeMax;
        life = playerData.life;
        lifeText.text = life + "/" + lifeMax;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayerHealthUpdate()
    {
        lifeMax = playerData.lifeMax;
        life = playerData.life;
        lifeText.text = life + "/" + lifeMax;
    }

    public void ChangeWeapon(int n)
    {
        weaponsInv[chosenW].color = Color.white;
        chosenW = n;
        weaponsInv[n].color = Color.red;
    }
}
