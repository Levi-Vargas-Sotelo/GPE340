using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private float respawnTime;
    [SerializeField]
    private Health targetHealth;
    [SerializeField]
    private Renderer rend;
    [SerializeField]
    private BoxCollider coll;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Defeat()
    {
        rend.enabled = false;
        coll.enabled = false;
        StartCoroutine(RespawnTarget());
    }

    IEnumerator RespawnTarget()
    {
        yield return new WaitForSeconds(respawnTime);
        rend.enabled = true;
        coll.enabled = true;
        targetHealth.FullHeal();
    }
}
