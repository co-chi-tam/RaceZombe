using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CMapManager : MonoBehaviour {

		[SerializeField]	private GameObject[] m_CarSpawnPoints;
		[SerializeField]	private GameObject[] m_ZombieSpawnPoints;
		[SerializeField]	private GameObject[] m_EnemySpawnPoints;

		public GameObject[] GetCarSpawnPoints() {
			return this.m_CarSpawnPoints;
		}

		public GameObject[] GetZombieSpawnPoints() {
			return this.m_ZombieSpawnPoints;
		}

		public GameObject[] GetEnemySpawnPoints() {
			return this.m_EnemySpawnPoints;
		}

	}
}
