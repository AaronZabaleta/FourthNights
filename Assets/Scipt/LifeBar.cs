using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LifeBar : MonoBehaviour
{


    public Image lifeState;
    private Player player;
    private float _maxLife;
    public Slider healthSlider;
    public float healthValue = 100f;
    public float decreaseRate = 1f;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        _maxLife = player.lifeState;


    }

    // Update is called once per frame
    void Update()
    {

        lifeState.fillAmount = player.lifeState / _maxLife;

    }

    public void DecreaseHealth(float amount)
    {
        healthValue = Mathf.Clamp(healthValue - amount * Time.deltaTime, 0f, 100f);
        healthSlider.value = healthValue;
    }

    public void ResetHealth()
    {
        healthValue = 100f;
        healthSlider.value = healthValue;
    }
}
