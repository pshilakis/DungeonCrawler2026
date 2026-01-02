using PGS.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PGS
{
    public class GameManager : Singleton<GameManager>
    {
		[Header("System Prefab References")]
		[SerializeField] private EventSystem eventSystemPrefab;

		protected override void Awake()
		{
			base.Awake();

			//Setup Input
			CommonUtilities.AddComponentToNewGameObject<InputRelay>(this.transform, nameof(InputRelay));

			//Setup EventSystem for UI Input
			GameObject obj = GameObject.Instantiate(eventSystemPrefab.gameObject);
			obj.name = nameof(EventSystem);
			CommonUtilities.SetNewParent(obj, this.transform);
		}
    }
}
