using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace PGS
{
    public class PlayerTurnSelectView : GameView<PlayerTurnSelectState>
    {
        [SerializeField] private PlayerInfoCard playerInfoCardPrefab;
        [SerializeField] private Transform playerPortraitContainer;

        public async UniTask SetPlayerTurnCards(IReadOnlyList<CharacterData> characterDataList)
        {
            foreach (CharacterData character in characterDataList)
            {
                PlayerInfoCard card = Instantiate(playerInfoCardPrefab, playerPortraitContainer);
                card.Setup(character);
            }
        }
    }
}
