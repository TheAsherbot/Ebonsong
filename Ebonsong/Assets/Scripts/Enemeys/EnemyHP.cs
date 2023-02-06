using System;
using UnityEngine;
using UnityEngine.UI;

namespace Enemies
{
    public class EnemyHP : MonoBehaviour
    {
        public class OnHealthChanged
        {
            public int hp;
        }
        public event EventHandler<OnHealthChanged> onHealthChanged;

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
            if (damageAmount > 0)
            {
                hp -= damageAmount;
                if (hp <= 0)
                {
                    Destroy(this.gameObject);
                }
                SetSlider();
                TriggerOnHealthChanged();
            }
        }

        public void Heal(int healAmount)
        {
            if (healAmount < 0)
            {
                hp += healAmount;
                SetSlider();
                TriggerOnHealthChanged();
            }
        }

        public void SetHP(int hp)
        {
            if (hp <= 0)
            {
                Destroy(this.gameObject);
            }
            this.hp = hp;
            TriggerOnHealthChanged();
            SetSlider();
        }

        private void SetSlider()
        {
            hpSlider.value = hp;
        }

        private void TriggerOnHealthChanged()
        {
            if (onHealthChanged != null) onHealthChanged(this, new OnHealthChanged
            {
                hp = hp
            });
        }

    }
}
