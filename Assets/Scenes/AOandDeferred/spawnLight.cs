using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnLight : MonoBehaviour
{
    public GameObject light;
    //List<Transform> listT = new List<Transform>(50);
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 50; ++i)
        {
            GameObject gb = GameObject.Instantiate(light,transform,true);
            gb.GetComponent<lightSpawned>().i = i;
            //listT.Add(gb.transform);
        }
    }

    float t = 0f;
    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        transform.rotation = Quaternion.Euler(new Vector3(0f, t*30f, 0f));

        /*for (int i = 0; i < 50; ++i)
        {
            listT[i].localPosition = new Vector3(Mathf.Sin((t+i)/20f*Mathf.PI)*(i%10)/2f * (Mathf.Sin((i/10)/5f*2f*Mathf.PI) + 1f), 0f, (i%10) / 2f * Mathf.Cos((i / 10)/5f*2 * Mathf.PI));
            //listT[i].localPosition = new Vector3(i, 0f, 0f);
        }*/
    }
}
