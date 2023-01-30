using UnityEngine;
using TMPro;

namespace PlayerNameSpace
{
    [RequireComponent(typeof(Player))]
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int maleHPMax = 10;
        [SerializeField] private int femaleHPMax = 15;
        [SerializeField] private TextMeshProUGUI maleHPText;
        [SerializeField] private TextMeshProUGUI femaleHPText;

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
            SetHpText();
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
            SetHpText();
        }

        public void Damage(int damageAmount)
        {
            if (player.IsMale)
            {
                maleHP -= damageAmount;
                if (maleHP <= 0) 
                {
                    GameManager.Instance.playerLives--;
                    GameManager.Instance.RestartLevel();
                }
            }
            else
            {
                femaleHP -= damageAmount;
                if (femaleHP <= 0)
                {
                    GameManager.Instance.playerLives--;
                    GameManager.Instance.RestartLevel();
                }
            }

            if (GameManager.Instance.playerLives <= 0)
            {
                GameManager.Instance.GameOver();
            }

            SetHpText();
        }

        public void AddLives(int addAmount)
        {
            GameManager.Instance.playerLives += addAmount;
        }

        private void SetHpText()
        {
            maleHPText.text = "" + maleHP + " / " + maleHPMax;
            femaleHPText.text = "" + femaleHP + " / " + femaleHPMax;
        }

    }
}
