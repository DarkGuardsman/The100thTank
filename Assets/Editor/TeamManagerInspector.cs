using UnityEngine;
using UnityEditor;
using BuiltBroken.Team;
using System.Collections;

[CustomEditor(typeof(TeamManager))]
public class TeamManagerInspector : Editor
{
	public override void OnInspectorGUI ()
	{
		TeamManager manager = (TeamManager)target;

		if (manager.data != null) {
			for (int i = 0; i < manager.data.Length; i++) {
				if (manager.data [i] == null) {
					manager.data [i] = new TeamData ();
					manager.data [i].name = "Team " + (i + 1);
				}

				manager.data [i].name = EditorGUILayout.TextField ("Name", manager.data [i].name);
				manager.data [i].primary = EditorGUILayout.ColorField ("Primary", manager.data [i].primary);
				manager.data [i].secondary = EditorGUILayout.ColorField ("Secondary", manager.data [i].secondary);
				if (GUILayout.Button ("Remove index " + i)) {
					manager.RemoveTeam (i);
					break;
				}
			}
		}
		if (GUILayout.Button ("AddTeam")) {
			manager.ExpandTeamArray ();
		}
	}
}
