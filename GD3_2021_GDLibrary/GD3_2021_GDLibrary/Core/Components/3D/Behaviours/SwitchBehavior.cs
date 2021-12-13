using GDLibrary;
using GDLibrary.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace GDLibrary.Components
{
    class SwitchBehavior : Behaviour
    {
        private Color cSwitch;
        private Texture2D uiColor;
        private Scene activeScene;
        private ContentManager Content;
     
        public SwitchBehavior(ContentManager Content, Scene activeScene)
        {
            this.activeScene = activeScene;
            this.Content = Content;

            //Initialize color for UI
            this.cSwitch = Color.Blue;
            this.uiColor = Content.Load<Texture2D>("Blue_Frame");
        }
        protected override void OnEnabled()
        {
            base.OnEnabled();
        }
        protected override void OnDisabled()
        {
            base.OnDisabled();
        }
        public void Switch()
        {

            /// <summary>
            /// Predicate to check if a Gameobject has a component with the IInteractable interface
            /// </summary>
            /// <param name="gameObject"></param>
            /// <returns></returns>
            static bool IsInteractable(GameObject gameObject)
            {
                foreach (Component component in gameObject.Components)
                {
                    if (component is IInteractable)
                        return true;
                }
                // return gO.Components is IInteractable;
                return false;
            }

            if (cSwitch == Color.Blue)
            {
                cSwitch = Color.Red;
                uiColor = Content.Load<Texture2D>("Red_Frame");

                foreach (GameObject gameObject in activeScene.FindAll(IsInteractable))
                {
                    foreach (Component component in gameObject.Components)
                    {
                        if (component is IInteractable)
                            (component as IInteractable).Switch(true);
                    }
                }
            }
            else
            {
                cSwitch = Color.Blue;
                uiColor = Content.Load<Texture2D>("Blue_Frame");
                foreach (GameObject gameObject in activeScene.FindAll(IsInteractable))
                {
                    foreach (Component component in gameObject.Components)
                    {
                        if (component is IInteractable)
                            (component as IInteractable).Switch(false);
                    }
                }
            }
        }
    }
}
