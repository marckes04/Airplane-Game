using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUDController : MonoBehaviour
{
    private GameObject UI_Hud;

    private Text scoreText;
    private Slider fuelSlider;

    private bool canCountScore;

    private float scoreCount;
    private int scoreValue;

    private float fuelValue = 100f;
    private float fuel_Spend_Treshold = 1f;

    PlayerController playercontroller;



    void Start()
    {
        UI_Hud = GameObject.Find("HUD");

        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        fuelSlider = GameObject.Find("Fuel").GetComponent<Slider>();

        UI_Hud.SetActive(false);

        playercontroller = GameObject.FindWithTag("Player1").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        ScoreAndFuel();
    }

    public void ActivateHUD(bool Active)
    {
        canCountScore = true;
        UI_Hud.SetActive(Active);
    }

    void ScoreAndFuel()
    {
        if (canCountScore)
        {
            fuelValue -= fuel_Spend_Treshold * Time.deltaTime;
            fuelSlider.value = fuelValue / 100f;

            scoreCount += 5f * Time.deltaTime;
            scoreValue = (int)scoreCount;
            scoreText.text = "Score: " + scoreValue;

            if(fuelValue <= 0f)
            {
                playercontroller.PlayerCrashed();
                canCountScore = false;
            }
        }
    }

    public void FuelCollected()
    {
        fuelValue += 30f;

        if (fuelValue > 100)
            fuelValue = 100;

    }

    public void IncreaseScore()
    {
        scoreCount += 50;
    }

}
