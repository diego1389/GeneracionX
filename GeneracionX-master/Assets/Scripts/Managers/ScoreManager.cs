using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ScoreManager : MonoBehaviour
{
    public static int energy;
    public Slider EnergySlider;
    public static int EnergyToOpenDoor = 100;
    //Text text;

    void Awake ()
    {
        //text = GetComponent <Text> ();
        //EnergySlider.maxValue = EnergyToOpenDoor;
        //energy = 0;
    }


    void Update ()
    {
        EnergySlider.value = energy;
        //text.text = "Score: " + score;
    }

    //public static void UpdateEnergy(int energyValue)
    //{
    //    EnergySlider.value += energyValue;
    //}
}
