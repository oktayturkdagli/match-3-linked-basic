using System.Collections;
using System.Collections.Generic;
using Match3Linked.Core;
using UnityEngine;

namespace Match3Linked.Game
{
    public class GameGridSpawning
    {
        private readonly GameGrid _grid;

        public GameGridSpawning(GameGrid grid)
        {
            _grid = grid;
        }
        
        public IEnumerator RespawnElements()
        {
            AudioManager.Instance.PlayOneShot("spawning");

            foreach (GameGridElement element in _grid.Elements)
            {
                if (!element.IsSpawned)
                {
                    var randomElementInfo = _grid.ElementInfoList.GetRandomElement();
                    element.Sprite = randomElementInfo.sprite;
                    element.ColorType = randomElementInfo.colorType;
                    element.Spawn(_grid.CellSize);
                }
            }

            yield return new WaitForSeconds(0.4f);
        }
        
        public IEnumerator Despawn(List<GameGridElement> elements)
        {
            AudioManager.Instance.PlayOneShot("despawning");

            foreach (GameGridElement gridElement in elements)
            {
                gridElement.Despawn(_grid.CellSize);
            }

            yield return new WaitForSeconds(0.4f);
            GameManager.Instance.MovesAvailable--;
            GameEvents.OnElementsDespawned.Invoke(elements.Count);
        }
    }
}