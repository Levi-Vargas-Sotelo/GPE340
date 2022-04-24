using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Health healthToTrack;
    [SerializeField]
    private Image fill;
    [SerializeField]
    private Vector3 trackingOffset;
    [SerializeField]
    public bool isTracking;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(healthToTrack != null)
        {
            healthText.text = string.Format("Health: " + healthToTrack.healthValue);

            fill.fillAmount = healthToTrack.healthValue / healthToTrack.maxHealthValue;

            if(isTracking)
            {
                gameObject.transform.position = healthToTrack.gameObject.transform.position + trackingOffset;
                if(healthToTrack.healthValue <= 0)
                {
                    DestroyBar();
                }
            }
        }
    }

    public void SetTarget(Health theHealth)
    {
        healthToTrack = theHealth;
    }

    public void DestroyBar()
    {
        Destroy(gameObject);
    }
}
