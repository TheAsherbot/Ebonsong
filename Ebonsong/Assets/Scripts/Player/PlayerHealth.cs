using UnityEngine;

namespace PlayerNameSpace
{
    [RequireComponent(typeof(Player))]
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int maleHPMax = 10;
        [SerializeField] private int femaleHPMax = 15;

        private int lives = 3;
        private int maleHP;
        private int femaleHP;

        private Player player;

        private void Awake()
        {
            player = GetComponent<Player>();
        }

        private void Start()
        {
            maleHP = maleHPMax;
            femaleHP = femaleHPMax;
        }

        public void Heal(int healAmount)
        {
            if (player.IsMale)
            {
                maleHP += healAmount;
                if (maleHP > maleHPMax)
                {
                    maleHP = maleHPMax;
                }
            }
            else
            {
                femaleHP += healAmount;
                if (femaleHP > femaleHPMax)
                {
                    femaleHP = femaleHPMax;
                }
            }
        }

        public void Damage(int damageAmount)
        {
            if (player.IsMale)
            {
                maleHP -= damageAmount;
                if (maleHP <= 0) 
                {
                    lives--;
                }
            }
            else
            {
                femaleHP -= damageAmount;
                if (femaleHP <= 0)
                {
                    lives--;
                }
            }

            if (lives <= 0)
            {
                GameManager.Instance.GameOver();
            }
        }

        public void AddLives(int addAmount)
        {
            lives += addAmount;
        }

    }
}
