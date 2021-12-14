using GDLibrary.Components.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GDLibrary.Core;
using System.Collections.Generic;
using GDLibrary.Graphics;

namespace GDLibrary.Components.UI
{
    class SwitchUI : UIBehaviour
    {
        bool activeTexture = false;
        private UITextureObject uiTextureObject;
        private Texture2D originalDefaultTexture;

        public override void Awake()
        {
            uiTextureObject = uiObject as UITextureObject;
            originalDefaultTexture = uiTextureObject.DefaultTexture;

            EventDispatcher.Subscribe(EventCategoryType.MaterialChange, HandleEvent);
            base.Awake();
        }

        private void HandleEvent(EventData eventData)
        {
          

            if (eventData.EventActionType == EventActionType.OnMouseClick)
            {

                if (activeTexture)
                {
                    uiObject.Color = Color.Blue;
                    uiTextureObject.DefaultTexture = uiTextureObject.AlternateTexture;
                    activeTexture = false;
                }
                else
                {
                    uiObject.Color = Color.Red;
                    uiTextureObject.DefaultTexture = originalDefaultTexture;
                    activeTexture = true;
                }
            }
        }
    }
}