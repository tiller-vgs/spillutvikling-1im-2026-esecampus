using UnityEngine;

public class HeartsUI : MonoBehaviour
{  
    private Transform[] hearts;
    private Player_health health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //children = GetComponentsInChildren<Transform>(true);
        hearts = GameObject.FindGameObjectWithTag("Player_health").GetComponentsInChildren<Transform>(true);
        var player = (GameObject)GameObject.FindGameObjectWithTag("Player");
        Player_health[] healths = player.GetComponents<Player_health>();
        this.health = healths[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (hearts[1] != null)
        {
            if (health.PlayerHealth == 2)
            {
                hearts[1].gameObject.SetActive(false);
            }
        }
        
    }
}
