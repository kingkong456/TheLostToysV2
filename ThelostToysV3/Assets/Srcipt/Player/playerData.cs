using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class playerData : MonoBehaviour
{
    [Header("Hp system")]
    public float start_hp;
    private float hp;
    public Image my_HpBar;

    [Header("Mana system")]
    public float start_mana;
    private float mana;
    public Image my_ManaBar;

    [Header("my item system")]
    public Toy[] m_toys = new Toy[3];
    public Image[] toys_images;

    public Sprite default_icon;

    private Animator m_animator;

    private void Start()
    {
        m_animator = GetComponent<Animator>();
        hp = start_hp;
        mana = start_mana;
    }

    public void set_toy_number(Toy toy, int number)
    {
        if (toy == null)
        {
            toys_images[number].sprite = default_icon;
            return;
        }

        m_animator.SetTrigger("getToy");
        toys_images[number].sprite = toy.icon;
        m_toys[number] = toy;
    }

    #region mana system

        public void use_mana(float mana_use)
        {
            Debug.Log(mana);
            mana -= mana_use;
            Debug.Log(mana);
            my_ManaBar.fillAmount = mana / start_mana;
        }

        public void add_mana(float mana_In)
        {
            if((mana + mana_In) <= start_mana)
            {
                mana += mana_In;
            }
            else if((mana + mana_In) > start_mana)
            {
                mana = start_mana;
            }

            my_ManaBar.fillAmount = mana / start_mana;
        }

        public float get_mana()
        {
            return mana;
        }

        #endregion

    #region HP System

        public void tacking_Dmg(float dmg_get)
        {
            hp -= dmg_get;
            my_HpBar.fillAmount = hp / start_hp;

            if (hp <= 0)
            {
                //die
                //end this game
            }
        }

        public void heal_hp(float heal_point)
        {
            if ((hp + heal_point) <= start_hp)
            {
                hp += heal_point;
            }
            else if ((hp + heal_point) > start_hp)
            {
                hp = start_hp;
            }

            my_HpBar.fillAmount = hp / start_hp;
        }

        #endregion
}
