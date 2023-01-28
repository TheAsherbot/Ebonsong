using UnityEngine;
using PlayerNameSpace;

public class HealThree : Collectables
{
    public override void OnCollect()
    {
        PlayerScore playerScore = GameObject.FindObjectOfType<Player>().GetComponent<PlayerScore>();
        PlayerHealth playerHealth = playerScore.gameObject.GetComponent<PlayerHealth>();

        playerScore.AddScore(3);
        playerHealth.Heal(3);
        base.OnCollect();
    }
}
