using UnityEngine;
using PlayerNameSpace;

public class Heal : Collectables
{
    public override void OnCollect()
    {
        PlayerScore playerScore = GameObject.FindObjectOfType<Player>().GetComponent<PlayerScore>();
        PlayerHealth playerHealth = playerScore.gameObject.GetComponent<PlayerHealth>();

        playerScore.AddScore(1);
        playerHealth.Heal(1);
        base.OnCollect();
    }
}
