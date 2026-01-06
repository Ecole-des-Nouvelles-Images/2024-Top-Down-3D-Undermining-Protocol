using System.Collections;
using UnityEngine;

namespace Elias.Scripts
{
    public class ArrowAnim : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private float _fadeDuration = 3f;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            StartCoroutine(FadeLoop());
        }

        private IEnumerator FadeLoop()
        {
            while (true)
            {
                float elapsedTime = 0f;
                Color color = _spriteRenderer.color;
                color.a = 0f;
                _spriteRenderer.color = color;

                while (elapsedTime < _fadeDuration / 2f)
                {
                    color.a = Mathf.Lerp(0f, 1f, elapsedTime / (_fadeDuration / 2f));
                    _spriteRenderer.color = color;
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                elapsedTime = 0f;
                while (elapsedTime < _fadeDuration / 2f)
                {
                    color.a = Mathf.Lerp(1f, 0f, elapsedTime / (_fadeDuration / 2f));
                    _spriteRenderer.color = color;
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
            }
        }
    }
}