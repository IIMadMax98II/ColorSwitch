using GDLibrary;
using GDLibrary.Components;
using GDLibrary.Core;

namespace GDApp
{
    public class MyHeroCollider : CharacterCollider
    {
        public MyHeroCollider(float accelerationRate, float decelerationRate,
       bool isHandlingCollision = true, bool isTrigger = false)
            : base(accelerationRate, decelerationRate, isHandlingCollision, isTrigger)
        {
        }

        protected override void HandleResponse(GameObject parentGameObject)
        {
            if (parentGameObject.GameObjectType == GameObjectType.Consumable)
            {
                System.Diagnostics.Debug.WriteLine(parentGameObject?.Name);

                object[] parameters = { parentGameObject };
                EventDispatcher.Raise(new EventData(EventCategoryType.GameObject,
                    EventActionType.OnRemoveObject, parameters));

                object[] parameters1 = { "health", 1 };
                EventDispatcher.Raise(new EventData(EventCategoryType.UI,
                    EventActionType.OnHealthDelta, parameters1));

                object[] parameters2 = { "sword" };
                EventDispatcher.Raise(new EventData(EventCategoryType.Inventory,
                    EventActionType.OnAddInventory, parameters2));

                // EventDispatcher.Raise(new EventData(EventCategoryType.Inventory,
                //  EventActionType.OnAddInventory, parameters1));
            }
            //Teleport the player to the beginnig of the level
            else if (parentGameObject.GameObjectType == GameObjectType.OOB)
            {
                foreach (Component component in this.gameObject.Components)
                    {
                    if (component is ColorSwitchFPC)
                    {
                        //(component as ColorSwitchFPC).characterBody.DisableBody();
                        //this.body.MoveTo(new Vector3(0, 2, 5), Matrix.Identity);
                        // this.transform.SetTranslation(0, 2, 5);
                        //(component as ColorSwitchFPC).characterBody.MoveTo(new Vector3(0, 2, 5), Matrix.Identity);


                        //(component as ColorSwitchFPC).characterBody.CollisionSkin.ApplyLocalTransform(new JigLibX.Math.Transform(-SetMass(2), Matrix.Identity));
                        //(component as ColorSwitchFPC).characterBody.EnableBody();
                    }
                        
                }
                //this.body.DisableBody();
                //this.body.MoveTo(Vector3.Zero, Matrix.Identity);
                //this.body.CollisionSkin.ApplyLocalTransform(new JigLibX.Math.Transform(-SetMass(2), Matrix.Identity));
                //this.body.EnableBody();

            }

            base.HandleResponse(parentGameObject);
        }
    }
}