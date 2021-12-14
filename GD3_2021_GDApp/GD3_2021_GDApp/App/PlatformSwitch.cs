using GDLibrary;
using GDLibrary.Components;
using GDLibrary.Core;
using GDLibrary.Graphics;
using Microsoft.Xna.Framework;

namespace GDApp
{
    /// <summary>
    /// Used to give interactable game objects the switch property of the platformer room type.
    /// </summary>
    class PlatformSwitch : Component, IInteractable
    {
        /// <summary>
        /// Tell which color this gameobject has
        /// </summary>
        bool isVisible;
        GameObject parent;
        private BasicMaterial material;
        private Vector3 originalColor;
        private float originalAlpha;

        public PlatformSwitch(bool isVisible)
        {
            this.isVisible = isVisible;
        }

        public override void Awake(GameObject gameObject)
        {
            parent = gameObject;
            material = gameObject.GetComponent<Renderer>().Material as BasicMaterial;

            if (material != null)
            {
                originalColor = material.DiffuseColor;
                originalAlpha = material.Alpha;
            }

            EventDispatcher.Subscribe(EventCategoryType.MaterialChange, HandleEvent);
            base.Awake(gameObject);
        }

        public void Switch()
        {
            if (isVisible)
            {
                isVisible = false;
                material.Alpha = 0.5f;
                //TODO - Disable Collision
            }
            else
            {
                isVisible = true;
                material.Alpha = 1f;
                //TODO - Enable Collision
            }


        }

        private void HandleEvent(EventData eventData)
        {
            Switch();
        }
    }
}
