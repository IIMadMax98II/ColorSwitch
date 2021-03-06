using GDLibrary.Core;
using GDLibrary.Graphics;
using JigLibX.Physics;
using Microsoft.Xna.Framework;

namespace GDLibrary.Components
{
    class SwitchPlatform : Behaviour
    {
        /// <summary>
        /// Tell which color this gameobject has
        /// </summary>
        bool isVisible;
        GameObject parent;
        private BasicMaterial material;
        private Body body;
        private Vector3 originalColor;
        private float originalAlpha;

        public SwitchPlatform(bool isVisible)
        {
            this.isVisible = isVisible;
        }

        public override void Awake(GameObject gameObject)
        {
            parent = gameObject;
            material = gameObject.GetComponent<Renderer>().Material as BasicMaterial;
            body = gameObject.GetComponent<Collider>().Body;
            

            if (material != null)
            {
                originalColor = material.DiffuseColor;
                originalAlpha = material.Alpha;
            }

            EventDispatcher.Subscribe(EventCategoryType.MaterialChange, HandleEvent);
            

            Switch();

            base.Awake(gameObject);
        }

        public void Switch()
        {
            if (isVisible)
            {
                isVisible = false;
                material.Alpha = 0.1f;

                //Disable Collision
                body.DisableBody();
                
            }
            else
            {
                isVisible = true;
                material.Alpha = 1f;

                //Enable Collision
                body.EnableBody();
            }


        }

        private void HandleEvent(EventData eventData)
        {
            Switch();
        }
        

    }
}

//static bool IsInteractable(GameObject gameObject)
//{
//    foreach (Component component in gameObject.Components)
//    {
//        if (component is IInteractable)
//            return true;
//    }
//    // return gO.Components is IInteractable;
//    return false;
//}

//foreach (GameObject gameObject in activeScene.FindAll(IsInteractable))
//          {
//              foreach (Component component in gameObject.Components)
//              {
//                  if (component is IInteractable)
//                      (component as IInteractable).Switch(true);
//              }
//          }

// other color (blue)

//     foreach (GameObject gameObject in activeScene.FindAll(IsInteractable))
//                {
//                    foreach (Component component in gameObject.Components)
//                    {
//                        if (component is IInteractable)
//                            (component as IInteractable).Switch(false);
//}
//}
