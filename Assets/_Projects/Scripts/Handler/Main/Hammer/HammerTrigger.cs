using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Handler.Main.Cake;

namespace Handler.Main.Hammer
{
    internal sealed class HammerTrigger : MonoBehaviour
    {
        private void Start()
        {
            this.OnTriggerEnterAsObservable()
                .Where(collider => collider.tag.Contains("Cake"))
                .Subscribe(collider =>
                {
                    if (collider.TryGetComponent(out CakeKnockback cakeScript))
                    {
                        cakeScript.Hit();
                    }
                })
                .AddTo(this);
        }
    }
}

