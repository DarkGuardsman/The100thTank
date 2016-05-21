using System;
using UnityEngine;
using System.Collections.Generic;

namespace BuiltBroken.Team
{
	/// <summary>
	/// Keeps track of the different teams in the scene
	/// </summary>
	public class TeamManager : MonoBehaviour
	{
		public TeamData[] data;

		/// <summary>
		/// Expands the team array by one slot
		/// </summary>
		public void ExpandTeamArray ()
		{
			ExpandTeamArray (1);
		}

		/// <summary>
		/// Expands the team array by x slots
		/// </summary>
		public void ExpandTeamArray (int size)
		{
			if (data == null) {
				data = new TeamData[1];
			} else {
				TeamData[] temp_data = data;
				data = new TeamData[temp_data.Length + size];
				for (int i = 0; i < temp_data.Length; i++) {
					data [i] = temp_data [i];
				}
			}
		}

		/// <summary>
		/// Removes the team index and compacts the array
		/// </summary>
		/// <param name="v">Index of the team to remove</param>
		public void RemoveTeam (int v)
		{
			if (data.Length <= 1) {
				data = null;
			} else {
				data [v] = null;
				TeamData[] temp_data = new TeamData[data.Length - 1];
				int b = 0;
				for (int i = 0; i < data.Length; i++) {
					//Skips over the index that was set to null, also skips over any other null values
					if (data [i] != null) {
						temp_data [b] = data [i];
						b++;
					}
				}
				data = temp_data;
			}
		}
	}
}

