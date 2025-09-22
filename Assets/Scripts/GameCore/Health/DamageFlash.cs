using System.Collections;
using UnityEngine;

namespace GameCore.Health
{
    public class DamageFlash : MonoBehaviour
    {
        [SerializeField] private Material _flashMaterial;
        [SerializeField] private float _flashDuration = 0.2f;
        
        private SpriteRenderer _spriteRenderer;
        private Material _originalMaterial;
        private Coroutine _flashCoroutine;

        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _originalMaterial = _spriteRenderer.material;
        }

        private void OnDisable()
        {
            _flashCoroutine = null;
            _spriteRenderer.material = _originalMaterial;
        }

        public void Flash()
        {
            if (_flashCoroutine != null)
            {
                StopCoroutine(_flashCoroutine);
            }
            _flashCoroutine = StartCoroutine(FlashCoroutine());
        }

        private IEnumerator FlashCoroutine()
        {
            _spriteRenderer.material = _flashMaterial;
            yield return new WaitForSeconds(_flashDuration);
            _spriteRenderer.material = _originalMaterial;
            _flashCoroutine = null;
        }
    }
}