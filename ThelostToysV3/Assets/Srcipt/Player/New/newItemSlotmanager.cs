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
    public GameObject removeSlot_particle;

    private void Start()
    {

    }

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
              new_slot.GetComponent<newSlotNode>().move_left(slot_moveDegree);
          }
          current_item++;
      }
    }

    public void remove_PlayerToy(int index)
    {
        current_item--;

        GameObject fx = Instantiate(removeSlot_particle, m_slot[index].spawnRemovePoint.position, removeSlot_particle.transform.rotation, transform);
        Destroy(fx, 3);

        Destroy(m_slot[index].gameObject);
        m_toys.RemoveAt(index);
        m_slot.RemoveAt(index);

        for (int i = index; i < max_slot + 1; i++)
        {
            m_slot[i].move_left(slot_moveDegree);
        }
    }
}
