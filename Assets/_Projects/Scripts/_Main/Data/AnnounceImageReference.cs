using Cysharp.Threading.Tasks;
using DG.Tweening;
using Interface;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Data
{
    [Serializable]
    internal sealed class AnnounceImageReference : IReference
    {
        [SerializeField]
        private Image image;

        [SerializeField, Tooltip("3, 2, 1, GO, END の順")]
        private Sprite[] sprites;

        internal async UniTask CountDown(CancellationToken ct)
        {
            size = 2;

            await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: true, cancellationToken: ct);
            sprite = sprites[0];
            isActive = true;
            await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: true, cancellationToken: ct);
            sprite = sprites[1];
            await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: true, cancellationToken: ct);
            sprite = sprites[2];
            await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: true, cancellationToken: ct);
            sprite = sprites[3];
            await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: true, cancellationToken: ct);

            isActive = false;
        }

        internal async UniTask GameEnded(CancellationToken ct)
        {
            float endSize = 2;
            float duration = 0.5f;
            Ease ease = Ease.OutQuad;

            size = 0;
            sprite = sprites[4];
            isActive = true;

            await DOTween.To(() => size, x => size = x, endSize, duration).SetEase(ease).ToUniTask(cancellationToken: ct);
            await UniTask.Delay(TimeSpan.FromSeconds(3), ignoreTimeScale: true, cancellationToken: ct);
        }

        private bool isActive
        {
            set { if (image != null) image.gameObject.SetActive(value); }
        }

        private float size
        {
            get => image == null ? 0 : image.rectTransform.localScale.x;
            set { if (image != null) image.rectTransform.localScale = new(value, value, 1); }
        }

        private Sprite sprite
        {
            set { if (image != null) image.sprite = value; }
        }

        public void Dispose()
        {
            image = null;
        }
    }
}