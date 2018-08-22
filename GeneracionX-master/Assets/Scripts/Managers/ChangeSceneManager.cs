using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChangeSceneManager : MonoBehaviour {

    GameObject _door;
    static bool doorIsOpen;
    // Use this for initialization
    void Start () {
        _door = GameObject.FindGameObjectWithTag("Door");
        doorIsOpen = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level02");
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void checkDoor(int score)
    {
        if (score >= ScoreManager.EnergyToOpenDoor && !doorIsOpen)
        {
            Animator doorAnimator;
            BoxCollider doorCollider;
            AudioSource doorSound;
            if (_door != null)
            {
                doorAnimator = _door.GetComponentInChildren<Animator>();
                doorCollider = _door.GetComponentInChildren<BoxCollider>();
                doorSound = _door.GetComponentInChildren<AudioSource>();
                if (doorAnimator != null && doorCollider != null && doorSound != null)
                {
                    doorAnimator.SetTrigger("OpenDoor");
                    doorCollider.isTrigger = true;
                    doorSound.Play();
                    doorIsOpen = true;
                }
            }
        }
    }
}
