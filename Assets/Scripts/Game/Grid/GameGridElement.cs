using UnityEngine;

namespace Match3Linked.Game
{
    public class GameGridElement : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        public bool IsMoving { get; private set; }
        public bool IsSpawned => gameObject.activeSelf;
        
        public ColorType ColorType { get; set; }

        public Sprite Sprite
        {
            get => spriteRenderer.sprite;
            set => spriteRenderer.sprite = value;
        }
        
        public void Move(Vector3 targetPos, float time)
        {
            IsMoving = true;
            StartCoroutine(transform.MoveCoroutine(targetPos, time, () =>
            {
                IsMoving = false;
            }));
        }
        
        public void Spawn(float scaleFactor)
        {
            gameObject.SetActive(true);
            Vector2 startScale = Vector2.one * 0.1f;
            Vector2 endScale = Vector2.one * scaleFactor;
            StartCoroutine(transform.ScaleCoroutine(startScale, endScale, 0.25f));
        }
        
        public void Despawn(float scaleFactor)
        {
            Vector2 startScale = Vector2.one * scaleFactor;
            Vector2 endScale = Vector2.one * 0.1f;
            StartCoroutine(transform.ScaleCoroutine(startScale, endScale, 0.25f, () =>
            {
                gameObject.SetActive(false);
            }));
        }
    }
}