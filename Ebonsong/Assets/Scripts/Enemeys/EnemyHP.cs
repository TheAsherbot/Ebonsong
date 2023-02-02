using UnityEngine;
using UnityEngine.UI;

namespace Enemies
{
    public class EnemyHP : MonoBehaviour
    {
        [SerializeField] private int maxHP = 5;
        [SerializeField] private Slider hpSlider;

        private int hp;

        private void Start()
        {
            hpSlider.maxValue = maxHP;
            SetHP(maxHP);
        }

        public void Damage(int damageAmount)
        {
            hp -= damageAmount;
            SetSlider();
            if (hp <= 0)
            {
                Destroy(this.gameObject);
            }
        }

        public void SetHP(int hp)
        {
            this.hp = hp;
            SetSlider();
            if (hp <= 0)
            {
                Destroy(this.gameObject);
            }
        }

        private void SetSlider()
        {
            hpSlider.value = hp;
        }
    }
}
