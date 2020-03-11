using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PlayerController playerController;

    public RectTransform lifeUI;
    public Text lifeText;
    public List<RawImage> weaponsInv = new List<RawImage>();

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
        lifeMax = playerController.playerData.lifeMax;
        life = playerController.playerData.life;
        lifeText.text = life + "/" + lifeMax;

        for (int i = 0; i < playerController.weapon.Count; i++)
        {
            if (playerController.weapon[i].network == null)
                playerController.weapon[i].buildModel();

            weaponsInv[i].texture = NN.ImageFromNet.imageFromNet(playerController.weapon[i].network);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayerHealthUpdate()
    {
        lifeMax = playerController.playerData.lifeMax;
        life = playerController.playerData.life;
        lifeText.text = life + "/" + lifeMax;
    }

    public void ChangeWeapon(int n)
    {
        weaponsInv[chosenW].color = Color.white;
        chosenW = n;
        weaponsInv[n].color = Color.red;
    }

    public void NewWeapon()
    {
        if (playerController.weapon[chosenW].network == null)
            playerController.weapon[chosenW].buildModel();

        weaponsInv[chosenW].texture = NN.ImageFromNet.imageFromNet(playerController.weapon[chosenW].network);
    }
}
