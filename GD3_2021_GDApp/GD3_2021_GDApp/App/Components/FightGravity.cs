using GDLibrary;
using GDLibrary.Components;
using GDLibrary.Parameters;
using JigLibX.Physics;
using Microsoft.Xna.Framework;

namespace GDApp.App.Components
{
    class FightGravity : Behaviour
    {
        Collider collider;
        public override void Awake(GameObject gameObject)
        { 
            collider = gameObject.GetComponent<Collider>() as Collider;
            collider.Body.ApplyGravity = false;
            collider.Body.SetBodyInvInertia(0f,0f, 0f);

            base.Awake(gameObject);
        }

        public override void Update()
        {
            collider.Body.Velocity = gameObject.GetComponent<CurveBehaviour>().Transform.LocalTranslation;
        }

    }
}
