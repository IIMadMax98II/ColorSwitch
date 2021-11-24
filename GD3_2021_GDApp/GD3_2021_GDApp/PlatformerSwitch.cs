using System;
using System.Collections.Generic;
using System.Text;
using GDLibrary;
using GDLibrary.Components;

namespace GDApp
{
    /// <summary>
    /// Used to give interactable game objects the switch property of the platformer room type.
    /// </summary>
    class PlatformerSwitch : Component, IInteractable
    {
        /// <summary>
        /// 
        /// </summary>
        bool isRed;

        public PlatformerSwitch(bool isRed)
        {
            this.isRed = isRed;
        }

        public void Switch(bool isRed)
        {
            if (isRed == this.isRed)
                GameObject.GetComponent<MeshRenderer>().Material.Alpha = 1f;
                //TODO - Enable Collision
            else if(isRed != this.isRed)
                GetComponent<MeshRenderer>().Material.Alpha = 0.5f;
                //TODO - Disable Collision
        }
    }
}
