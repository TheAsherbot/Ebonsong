using UnityEngine;

namespace PlayerNameSpace
{
    [RequireComponent(typeof(Player))]
    public class PlayerScore : MonoBehaviour
    {
        [HideInInspector] public int coinAmount;
        [HideInInspector] public int score;

        public void AddCoins(int coinAmount)
        {
            this.coinAmount += coinAmount;
            AddScore(coinAmount);
        }

        public void AddScore(int scoreAmount)
        {
            score += coinAmount;
        }
    }
}
