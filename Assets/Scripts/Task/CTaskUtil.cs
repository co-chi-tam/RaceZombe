using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CTaskUtil {

		public static string HOST 						= "HOST";
		public static string VERSION 					= "VERSION";
		public static string GAME_FIRST_LAUNCH 			= "GAME_FIRST_LAUNCH";
		public static string GAME_FIRST_TIME 			= "GAME_FIRST_TIME";
		public static string GAME_RESOURCE_COMPLETED	= "GAME_RESOURCE_COMPLETED";
		public static string GAME_SOUND_VOLUME 			= "GAME_SOUND_VOLUME";
		public static string PLAYER_ENERGY 				= "PLAYER_ENERGY";
		public static string PLAYER_MAX_ENERGY			= "PLAYER_MAX_ENERGY";
		public static string PLAYER_ENEGY_SAVE_TIMER 	= "PLAYER_ENEGY_SAVE_TIMER";
		public static string PLAYER_SELECTED_MISSION 	= "PLAYER_SELECTED_MISSION";
		public static string PLAYER_SELECTED_CAR 		= "PLAYER_SELECTED_CAR";

		public static Dictionary<string, object> REFERENCES = new Dictionary<string, object> () { 
			{ HOST, 					"https://tamco-tinygame.rhcloud.com" },
			{ VERSION, 					"v.1.0.0" },
			{ GAME_FIRST_LAUNCH, 		false },
			{ GAME_FIRST_TIME, 			DateTime.UtcNow.Ticks.ToString() },
			{ GAME_RESOURCE_COMPLETED, 	false },
			{ GAME_SOUND_VOLUME, 		1f },
			{ PLAYER_ENERGY, 			new CPlayerEnergy() },
			{ PLAYER_MAX_ENERGY, 		10 },
			{ PLAYER_ENEGY_SAVE_TIMER, 	DateTime.UtcNow.Ticks },
			{ PLAYER_SELECTED_MISSION, 	new CGameModeData() },
			{ PLAYER_SELECTED_CAR, 		new CCarData() }
		};

		public static object Get(string name) {
			return REFERENCES [name];
		}

		public static T Get<T>(string name) {
			var value = REFERENCES [name];
			var convert = Convert.ChangeType (value, typeof(T));
			return (T)convert;
		}

		public static void Set(string name, object value) {
			REFERENCES [name] = value;
		}

	}

}
