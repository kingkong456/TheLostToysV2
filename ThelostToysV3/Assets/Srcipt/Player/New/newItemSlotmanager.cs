using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newItemSlotmanager : MonoBehaviour {
    //***********************
    //function i need
    //set player toy
    //use toy
    //update slot
    //***********************

    public List<Toy> m_toys = new List<Toy>();
    public List<newSlotNode> m_slot = new List<newSlotNode>();
    private int current_item = 0;

    [Header("inventory confinguration")]
    public int max_slot;
    public GameObject slot_pototype;
    public RectTransform spawn_slot_point;
    public float slot_moveDegree = 17.5f;

    //add new toy
    public void add_PlayerToy(Toy toy_i)
    {
        if (m_toys.Count < max_slot)
        {
            m_toys.Add(toy_i);
            GameObject new_slot = Instantiate(slot_pototype, spawn_slot_point.position, spawn_slot_point.rotation, transform);

            //m_slot[current_item] = new_slot.GetComponent<newSlotNode>();
            m_slot.Add(new_slot.GetComponent<newSlotNode>());
            new_slot.GetComponent<newSlotNode>().set_NewICon(toy_i.icon);

            for (int i = 0; i < (max_slot - current_item); i++)
            {
                new_slot.GetComponent<newSlotNode>().move_left(slot_moveDegree - 0.75f);
            }
            current_item++;
        }
    }

    public void remove_PlayerToy(int index_i)
    {
        if(m_toys[index_i] == null || m_slot[index_i] == null)
        {
            Debug.Log("Bu");
        }
        m_toys.RemoveAt(index_i);
        m_slot.RemoveAt(index_i);

        for (int i = 0; i < max_slot; i++)
        {
            if(m_slot[i - 1] == null)
            {
                if(i != 0)
                {
                    m_slot[i].move_left(slot_moveDegree - 0.75f);
                }
            }
        }
        
        current_item--;
    }
}
