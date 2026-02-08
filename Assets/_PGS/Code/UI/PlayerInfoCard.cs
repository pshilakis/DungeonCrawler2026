using UnityEngine;
using TMPro;
using PGS.UI;
using Cysharp.Threading.Tasks;

namespace PGS
{
    public class PlayerInfoCard : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameLabel;
        [SerializeField] private TextMeshProUGUI turnLabel;
        [SerializeField] private ButtonHandler button;

        public async UniTask Setup(CharacterData character)
        {
            nameLabel.text = character.Name;
        }

    }
}
