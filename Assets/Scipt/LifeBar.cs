using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LifeBar : MonoBehaviour
{


    public Slider healthSlider;
    public Image lifeState;
    private Player player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (player != null)
        {
            float normalizedLife = player.GetNormalizedLife(); 
            healthSlider.value = normalizedLife;
            lifeState.fillAmount = normalizedLife;
        }
    }
}
