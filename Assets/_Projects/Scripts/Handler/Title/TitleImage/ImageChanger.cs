using Cysharp.Threading.Tasks;
using Data.Title.TitleImage;
using DG.Tweening;
using Interface;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Handler.Title.TitleImage
{
    internal sealed class TitleImageChanger : IDisposable, IEventable
    {
        private ImageReference reference;
        private TitleImageChangeProperty property;
        private CancellationTokenSource cts;

        internal TitleImageChanger
            (ImageReference reference, TitleImageChangeProperty property)
        {
            this.reference = reference;
            this.property = property;

            this.cts = new();
        }

        public void Dispose()
        {
            reference = null;
            property = null;

            cts.Cancel();
            cts.Dispose();
            cts = null;
        }

        public void Start()
        {
            TitleImageChangePeriodically(
                Array.AsReadOnly(reference.TitleImages),
                property.ChangeIntervalSeconds,
                property.ChangeDurationSeconds,
                cts.Token
                ).Forget();
        }

        private async UniTask TitleImageChangePeriodically
            (ReadOnlyCollection<Image> images, float interval, float duration, CancellationToken ct)
        {
            if (images is null) return;
            if (images.Count <= 1) return;
            foreach (Image image in images) if (image == null) return;

            int len = images.Count;
            int i = 0;  // 表示するべき画像のインデックス

            // 最初の画像をセットアップ
            foreach (Image image in images) Hide(image);
            Show(images[i]);

            while (true)
            {
                // 一定時間待って...
                await UniTask.Delay(TimeSpan.FromSeconds(interval), cancellationToken: ct);

                // 画像切り替え
                await UniTask.WhenAll(
                    DoFade(images[i], 0, duration, ct),
                    DoFade(images[NextI(i, len)], 1, duration, ct));

                // 次の画像どうぞ！
                i = NextI(i, len);
            }

            int NextI(int index, int length) => (index + 1) % length;

            void SetAlpha(Image image, float alpha)
            {
                if (image == null) return;
                if (alpha is not (>= 0 and <= 1)) return;

                Color color = image.color;
                color.a = alpha;
                image.color = color;
            }
            void Show(Image image) => SetAlpha(image, 1);
            void Hide(Image image) => SetAlpha(image, 0);

            async UniTask DoFade(Image image, float endValue, float duration, CancellationToken ct)
            {
                if (image == null) return;
                if (endValue is not (>= 0 and <= 1)) return;
                if (duration <= 0) return;

                await image.DOFade(endValue, duration).ToUniTask(cancellationToken: ct);
            }
        }

        public void Update() { }
    }
}