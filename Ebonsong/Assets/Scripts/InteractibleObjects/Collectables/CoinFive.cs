using UnityEngine;
using PlayerNameSpace;

public class CoinFive : Collectables
{
    public override void OnCollect()
    {
        PlayerScore playerScore = GameObject.FindObjectOfType<Player>().GetComponent<PlayerScore>();

        playerScore.AddCoins(5);
        base.OnCollect();
    }
}
