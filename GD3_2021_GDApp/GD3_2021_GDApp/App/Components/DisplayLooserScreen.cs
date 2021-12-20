using GDLibrary;
using GDLibrary.Components;
using GDLibrary.Components.UI;
using GDLibrary.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace GDApp.App.Components
{
    class DisplayLooserScreen: UIBehaviour
    {
        public override void Awake()
        {
            EventDispatcher.Subscribe(EventCategoryType.LooseMenu, HandleEvent);
            base.Awake();
        }

        private void HandleEvent(EventData eventData)
        {

        }
    }
}
