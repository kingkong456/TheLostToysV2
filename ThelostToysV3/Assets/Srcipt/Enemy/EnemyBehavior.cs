using System.Collections;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
    public float view_range;
    [Range(0, 360)]
    public float view_angle;

    public Transform target;

    public float hight_player_checker;

    [Header("Attack")]
    public float attack_power;

    public void find_target()
    {
        GameObject[] target_in_scene = GameObject.FindGameObjectsWithTag("Player");
        float shost_distance = Mathf.Infinity;
        Transform nearest_player = null;
        Transform target_in_range;

        foreach (GameObject player in target_in_scene)
        {
            float distance_to_player = Vector3.Distance(transform.position, player.transform.position);
            if (distance_to_player <= shost_distance)
            {
                shost_distance = distance_to_player;
                nearest_player = player.transform;
            }
        }

        GameObject[] Fakers = GameObject.FindGameObjectsWithTag("Faker");
        foreach (GameObject faker in Fakers)
        {
            float distance = Vector3.Distance(transform.position, faker.transform.position);

            if (distance < shost_distance)
            {
                shost_distance = distance;
                nearest_player = faker.transform;
            }
        }

        if (shost_distance <= view_range)
        {
            target_in_range = nearest_player;
        }
        else
        {
            target_in_range = null;
        }

        if (target_in_range != null)
        {
            Vector3 dir_target = (target_in_range.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, dir_target) < view_angle / 2)
            {
                RaycastHit hit;

                if(Physics.Raycast(transform.position + new Vector3(0, hight_player_checker, 0), dir_target, out hit, shost_distance))
                {
                    if (hit.transform.tag == "Player")
                    {
                        target = target_in_range;
                    }
                }
            }
        }
        else
        {
            target = null;
        }
    }

    public void face_to_target()
    {
        if (target == null)
        {
            return;
        }

        Vector3 dir = (target.position - transform.position).normalized;
        Quaternion look_rotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        transform.rotation = look_rotation;
    }


    public Vector3 diraction_from_angle(float angle_degrees, bool angle_in_grobal)
    {
        if(!angle_in_grobal)
        {
            angle_degrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angle_degrees * Mathf.Deg2Rad), 0, Mathf.Cos(angle_degrees * Mathf.Deg2Rad));
    }

}
