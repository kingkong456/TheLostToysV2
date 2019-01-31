using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(EnemyBehavior))]
public class fiedOfViewEditor : Editor {

    private void OnSceneGUI()
    {
        EnemyBehavior enemy = (EnemyBehavior)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(enemy.transform.position, Vector3.up, Vector3.forward, 360, enemy.view_range);

        Vector3 agnleA = enemy.diraction_from_angle(-enemy.view_angle / 2, false);
        Vector3 agnleB = enemy.diraction_from_angle(enemy.view_angle / 2, false);

        Handles.DrawLine(enemy.transform.position, enemy.transform.position + agnleA * enemy.view_range);
        Handles.DrawLine(enemy.transform.position, enemy.transform.position + agnleB * enemy.view_range);
    }

}
