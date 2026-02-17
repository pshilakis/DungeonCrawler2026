using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace PGS.Utilities
{
    public static class RuntimeSaveUtilities
    {
		#region General Directories
		private const string EXTENSION = ".es3";
		#endregion

		#region Game-specific directories
		private const string SAVED_GAMES_FOLDER = "Saved Games/";
		#endregion

		#region File names
		public const string CHARACTER_FILE = "characters";
		#endregion

		private static string BuildFullFilePath(string id, string fileName)
		{
			string saveFolder = SAVED_GAMES_FOLDER + AddSlash(id);
			string file = fileName + EXTENSION;
			return saveFolder + file;
		}

		private static string AddSlash(string str)
		{
			return str + "/"; 
		}

		public static string[] GetListOfSavedGameIDs()
		{
			return ES3.GetDirectories(SAVED_GAMES_FOLDER);
		}

		public static void SaveCharacters(string id, List<CharacterData> data, bool log = false)
		{
			string path = BuildFullFilePath(id, CHARACTER_FILE);
			ES3.Save(CHARACTER_FILE, data, path);
#if UNITY_EDITOR
			if (log)
			{
				LogSave(path);
			}
#endif
		}

		public static List<CharacterData> LoadCharacters(string id)
		{
			string path = BuildFullFilePath(id, CHARACTER_FILE);
			bool isValid = ES3.FileExists(path);
			return isValid ? ES3.Load<List<CharacterData>>(CHARACTER_FILE, path) : null;
		}

		private static void LogSave(string path)
		{
			Debug.Log($"Data saved to {Application.persistentDataPath}/{path}");
		}
	}
}
