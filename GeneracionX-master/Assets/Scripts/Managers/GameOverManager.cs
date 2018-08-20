using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth PlayerHealth;
    public float RestartDelay = 5f;

    Animator anim;
    float restartTimer;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (PlayerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");
            restartTimer += Time.deltaTime;
            if(restartTimer >= RestartDelay)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Level01");
            }
        }
    }
}
