using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePooling : MonoBehaviour
{
    public GameObject particle;
    private List<GameObject> pool;
    public Transform poolTransform;
    public Transform activesTransform;

    public int nb = 1000;

    private static ParticlePooling instance;

    public static ParticlePooling Instance { get { return instance; } }


    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;

        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        pool = new List<GameObject>(nb);
        for (int i = 0; i < nb; i++)
        {
            GameObject par = GameObject.Instantiate(particle, poolTransform);
            par.SetActive(false);
            pool.Add(par);
        }
    }

    public GameObject instantiate(Transform transform, NEAT.Person part, bool damaging = false)
    {
        GameObject res = null;
        if(pool.Count > 0)
        {
            res = pool[pool.Count - 1];
            pool.RemoveAt(pool.Count - 1);
            res.transform.parent = activesTransform;
            res.transform.position = transform.position;
            res.transform.rotation = transform.rotation;
            res.GetComponent<Particle>().weapon = part;
            res.GetComponent<Particle>().pool = this;

            res.GetComponent<Particle>().damaging = damaging;
            res.GetComponent<Particle>().enabled = true;
            res.SetActive(true);
        }
        return res;
    }

    public void destroy(GameObject particle)
    {
        particle.GetComponent<Particle>().enabled = false;
        particle.GetComponent<Particle>().weapon = null;
        particle.SetActive(false);
        particle.transform.parent = poolTransform;
        pool.Add(particle);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
