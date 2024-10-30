using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Main.Handler
{
    internal sealed class HammerTrigger : MonoBehaviour
    {
        private void Start()
        {
            this.OnTriggerEnterAsObservable()
                .Where(collider => collider.tag.Contains("Cake"))
                .Subscribe(collider =>
                {
                    if (collider.TryGetComponent(out CakeKnockbacker cakeScript))
                    {
                        cakeScript.Hit();
                    }
                })
                .AddTo(this);
        }
    }
}

