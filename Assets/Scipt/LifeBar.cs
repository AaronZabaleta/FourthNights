using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LifeBar : MonoBehaviour
{


    public Image lifeState;
    private Player player;
    private float _maxLife;

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
}
