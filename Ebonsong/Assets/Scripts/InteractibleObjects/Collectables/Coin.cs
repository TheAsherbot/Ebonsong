using UnityEngine;
using PlayerNameSpace;

public class Coin : Collectables
{
    public override void OnCollect()
    {
        PlayerScore playerScore = GameObject.FindObjectOfType<Player>().GetComponent<PlayerScore>();

        playerScore.AddCoins(1);
        base.OnCollect();
    }
}
