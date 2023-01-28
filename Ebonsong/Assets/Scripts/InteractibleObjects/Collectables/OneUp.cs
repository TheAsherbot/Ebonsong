using UnityEngine;
using PlayerNameSpace;

public class OneUp : Collectables
{
    public override void OnCollect()
    {
        PlayerScore playerScore = GameObject.FindObjectOfType<Player>().GetComponent<PlayerScore>();
        PlayerHealth playerHealth = playerScore.gameObject.GetComponent<PlayerHealth>();

        playerScore.AddScore(10);
        playerHealth.AddLives(1);
        base.OnCollect();
    }
}
