using GDLibrary;
using GDApp;
using GDLibrary.Components;
using GDLibrary.Graphics;
using GDLibrary.Parameters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using JigLibX.Collision;
using GDApp.App.Components;

namespace ColorSwitch
{
    class FirstScene 
    {
        /// <summary>
        /// Add neutral platforms to level.
        /// </summary>
        /// <param name="level"></param>
        public void InitializeNeutralPlatforms(Scene level, ContentManager Content)
        {
            #region Archetype

            var shader = new BasicShader(Application.Content, true, true);
            var mesh = new CubeMesh();
            var material = new BasicMaterial("simple diffuse", shader, 
                            Color.White, 1, 
                            Content.Load<Texture2D>("Assets/ColorSwitch/Wall/Texture/BrickWall"));

            var platform = new GameObject("Neutral Platform", GameObjectType.Ground, true);

            #endregion Archetype

            //Main Platform
            for (int i = 0; i <= 8; i++)
            {
                var clone = platform.Clone() as GameObject;
                clone.Name = $"{clone.Name} - {1}";

                switch (i)
                {
                    case 0:
                        clone.Transform.SetTranslation(0, -2, 5);
                        clone.Transform.SetScale(10, 5, 5);
                        break;
                    case 1:
                        clone.Transform.SetTranslation(0, -2, -20);
                        clone.Transform.SetScale(10, 5, 2.5f);
                        break;
                    case 2:
                        clone.Transform.SetTranslation(0, 0, -40);
                        clone.Transform.SetScale(12, 12, 8);
                        break;
                    case 3:
                        clone.Transform.SetTranslation(0, -12, -75);
                        clone.Transform.SetScale(5, 5, 5);
                        break;
                    case 4:
                        clone.Transform.SetTranslation(0, -15, -120);
                        clone.Transform.SetScale(12, 12, 8);
                        break;
                    case 5:
                        clone.Transform.SetTranslation(0, -12, -200);
                        clone.Transform.SetScale(8, 6, 10);
                        break;
                    case 6:
                        clone.Transform.SetTranslation(0, -5, -208);
                        clone.Transform.SetScale(16, 16, 8);
                        break;
                    case 7:
                        clone.Transform.SetTranslation(6, -10, -200);
                        clone.Transform.SetScale(5, 10, 10);
                        break;
                    case 8:
                        clone.Transform.SetTranslation(-6, -10, -200);
                        clone.Transform.SetScale(5,10, 10);
                        break;
                }
                clone.AddComponent(new MeshRenderer(mesh, material));


                //add Collision Surface(s)
                var collider = new Collider();

                clone.AddComponent(collider);
                collider.AddPrimitive(new JigLibX.Geometry.Box(
                    clone.Transform.LocalTranslation, 
                    clone.Transform.LocalRotation,
                    clone.Transform.LocalScale), 
                    new MaterialProperties(0.8f, 0.8f, 0.7f));
                collider.Enable(true, 1);

                // Add to Scene Manager
                level.Add(clone);
            }

        }

        /// <summary>
        /// Add Platforms to our demo.
        /// </summary>
        /// <param name="level"></param>
        public void InitializePlatforms(Scene level, ContentManager Content)
        {
            //For now we can make the level here, we can create a seperate file/class for clarity and simplicity later

            //Initialize all platform types
            var shader = new BasicShader(Application.Content, true, true);
            var mesh = new CubeMesh();

            //Red stationary platform
            var materialR = new BasicMaterial("red", shader, Color.Red, 1, Content.Load<Texture2D>("Assets/Textures/Props/Platforms/red"));
            
            var platformObjectRS = new GameObject("Platform Stationary Red", GameObjectType.Interactable, false);
            platformObjectRS.Transform.SetScale(2, 0.5f, 2);
            

            //Blue stationary platform
            var materialB = new BasicMaterial("blue", shader, Color.Blue, 1, Content.Load<Texture2D>("Assets/Textures/Props/Platforms/blue"));

            var platformObjectBS = new GameObject("Platform Stationary Blue", GameObjectType.Interactable, false);
            platformObjectBS.Transform.SetScale(3, 0.5f, 3);

            //Create Platform clone Red
            for(int i = 0; i <= 14; i++)
            {
                var platformObjectRSClone = platformObjectRS.Clone() as GameObject;

                switch (i) {
                    case 0: 
                        platformObjectRSClone.Transform.SetTranslation(3, 0, -2);
                        platformObjectRSClone.Transform.SetScale(3, 0.5f, 3);
                        break;
                    case 1: 
                        platformObjectRSClone.Transform.SetTranslation(-3, 0, -8);
                        platformObjectRSClone.Transform.SetScale(3, 0.5f, 3);
                        break;
                    case 2: 
                        platformObjectRSClone.Transform.SetTranslation(3, 0, -14);
                        platformObjectRSClone.Transform.SetScale(3, 0.5f, 3);
                        break;
                    case 3:
                        platformObjectRSClone.Transform.SetTranslation(0, 1.5f, -24);
                        platformObjectRSClone.Transform.SetScale(12, 1, 3);
                        break;
                    case 4:
                        platformObjectRSClone.Transform.SetTranslation(0, 4.5f, -32);
                        platformObjectRSClone.Transform.SetScale(12, 1, 3);
                        break;
                    case 5:
                        platformObjectRSClone.Transform.SetTranslation(8, -10, -65);
                        platformObjectRSClone.Transform.SetScale(5, 0.5f, 5);
                        break;
                    case 6:
                        platformObjectRSClone.Transform.SetTranslation(8, -10, -85);
                        platformObjectRSClone.Transform.SetScale(5, 0.5f, 5);
                        break;
                    case 7:
                        platformObjectRSClone.Transform.SetTranslation(2, -10, -100);
                        platformObjectRSClone.Transform.SetScale(2, 0.5f, 20);
                        break;
                    case 8:
                        platformObjectRSClone.Transform.SetTranslation(4, -10, -130);
                        platformObjectRSClone.Transform.SetScale(2, 0.5f, 2);
                        break;
                    case 9:
                        platformObjectRSClone.Transform.SetTranslation(4, -10, -140);
                        platformObjectRSClone.Transform.SetScale(2, 0.5f, 2);
                        break;
                    case 10:
                        platformObjectRSClone.Transform.SetTranslation(4, -10, -150);
                        platformObjectRSClone.Transform.SetScale(2, 0.5f, 2);
                        break;
                    case 11:
                        platformObjectRSClone.Transform.SetTranslation(-4, -10, -160);
                        platformObjectRSClone.Transform.SetScale(2, 0.5f, 2);
                        break;
                    case 12:
                        platformObjectRSClone.Transform.SetTranslation(-4, -10, -170);
                        platformObjectRSClone.Transform.SetScale(2, 0.5f, 2);
                        break;
                    case 13:
                        platformObjectRSClone.Transform.SetTranslation(-4, -10, -180);
                        platformObjectRSClone.Transform.SetScale(2, 0.5f, 2);
                        break;
                    case 14:
                        platformObjectRSClone.Transform.SetTranslation(-4, -10, -190);
                        platformObjectRSClone.Transform.SetScale(2, 0.5f, 2);
                        break;
                }

                

                platformObjectRSClone.AddComponent(new MeshRenderer(mesh, materialR));

                //add Collision Surface(s)
                var collider = new Collider();

                platformObjectRSClone.AddComponent(collider);
                collider.AddPrimitive(new JigLibX.Geometry.Box(
                    platformObjectRSClone.Transform.LocalTranslation,
                    platformObjectRSClone.Transform.LocalRotation,
                    platformObjectRSClone.Transform.LocalScale),
                    new MaterialProperties(0.8f, 0.8f, 0.7f));
                collider.Enable(true, 1);

                //Adds the switch method
                platformObjectRSClone.AddComponent(new SwitchPlatform(true));
                
               
                level.Add(platformObjectRSClone);
            }

            //Create Platform clone Blue
            for (int i = 0; i <= 13 ; i++)
            {
                var platformObjectBSClone = platformObjectBS.Clone() as GameObject;

                switch (i)
                {
                    case 0:
                        platformObjectBSClone.Transform.SetTranslation(-3, 0, -2);
                        platformObjectBSClone.Transform.SetScale(3, 0.5f, 3);
                        break;
                    case 1:
                        platformObjectBSClone.Transform.SetTranslation(3, 0, -8);
                        platformObjectBSClone.Transform.SetScale(3, 0.5f, 3);
                        break;
                    case 2:
                        platformObjectBSClone.Transform.SetTranslation(-3, 0, -14);
                        platformObjectBSClone.Transform.SetScale(3, 0.5f, 3);
                        break;
                    case 3:
                        platformObjectBSClone.Transform.SetTranslation(0, 3, -28);
                        platformObjectBSClone.Transform.SetScale(12, 1, 3);
                        break;
                    case 4:
                        platformObjectBSClone.Transform.SetTranslation(-8, -10, -65);
                        platformObjectBSClone.Transform.SetScale(5, 0.5f, 5);
                        break;
                    case 5:
                        platformObjectBSClone.Transform.SetTranslation(-8, -10, -85);
                        platformObjectBSClone.Transform.SetScale(5, 0.5f, 5);
                        break;
                    case 6:
                        platformObjectBSClone.Transform.SetTranslation(-2, -10, -100);
                        platformObjectBSClone.Transform.SetScale(2, 0.5f, 20);
                        break;
                    case 7:
                        platformObjectBSClone.Transform.SetTranslation(-4, -10, -130);
                        platformObjectBSClone.Transform.SetScale(2, 0.5f, 2);
                        break;
                    case 8:
                        platformObjectBSClone.Transform.SetTranslation(-4, -10, -140);
                        platformObjectBSClone.Transform.SetScale(2, 0.5f, 2);
                        break;
                    case 9:
                        platformObjectBSClone.Transform.SetTranslation(-4, -10, -150);
                        platformObjectBSClone.Transform.SetScale(2, 0.5f, 2);
                        break;
                    case 10:
                        platformObjectBSClone.Transform.SetTranslation(4, -10, -160);
                        platformObjectBSClone.Transform.SetScale(2, 0.5f, 2);
                        break;
                    case 11:
                        platformObjectBSClone.Transform.SetTranslation(4, -10, -170);
                        platformObjectBSClone.Transform.SetScale(2, 0.5f, 2);
                        break;
                    case 12:
                        platformObjectBSClone.Transform.SetTranslation(4, -10, -180);
                        platformObjectBSClone.Transform.SetScale(2, 0.5f, 2);
                        break;
                    case 13:
                        platformObjectBSClone.Transform.SetTranslation(4, -10, -190);
                        platformObjectBSClone.Transform.SetScale(2, 0.5f, 2);
                        break;
                }

                platformObjectBSClone.AddComponent(new MeshRenderer(mesh, materialB));

                //add Collision Surface(s)
                var collider = new Collider();

                platformObjectBSClone.AddComponent(collider);
                collider.AddPrimitive(new JigLibX.Geometry.Box(
                    platformObjectBSClone.Transform.LocalTranslation,
                    platformObjectBSClone.Transform.LocalRotation,
                    platformObjectBSClone.Transform.LocalScale),
                    new MaterialProperties(0.8f, 0.8f, 0.7f));
                collider.Enable(true, 1);

                //Adds the switch method
                platformObjectBSClone.AddComponent(new SwitchPlatform(false));

   
                level.Add(platformObjectBSClone);
            }

            //Physic system broken and its too late to make this ones into the game
            #region MovingPlatforms
            /*
            //Moving Platform
            Curve3D translationCurve;

            //Red Moving Platform
            var platformObjectRMClone = platformObjectRS.Clone() as GameObject;
            platformObjectRMClone.Name = "Clone - Platform Moving Red";
            platformObjectRMClone.Transform.SetTranslation(4, 0, -26);

            //Add Translation Curve
            translationCurve = new Curve3D(CurveLoopType.Cycle);
            translationCurve.Add(new Vector3(2, 0, 0), 0);
            translationCurve.Add(new Vector3(-2, 0, 0), 4000);

            platformObjectRMClone.AddComponent(new CurveBehaviour(translationCurve));

            platformObjectRMClone.AddComponent(new MeshRenderer(mesh, materialR));
            platformObjectRMClone.Transform.SetScale(4, 0.5f, 4);

            //add Collision Surface(s)
            var colliderRM = new Collider();

            platformObjectRMClone.AddComponent(colliderRM);
            colliderRM.AddPrimitive(new JigLibX.Geometry.Box(
                platformObjectRMClone.Transform.LocalTranslation,
                platformObjectRMClone.Transform.LocalRotation,
                platformObjectRMClone.Transform.LocalScale),
                new MaterialProperties(0f, 0f, 0f));
            colliderRM.Enable(false, 1);

            //Adds the switch method
            platformObjectRMClone.AddComponent(new SwitchPlatform(true));
            platformObjectRMClone.AddComponent(new FightGravity());

            level.Add(platformObjectRMClone);

            //Blue Moving Platform
            var platformObjectBMClone = platformObjectBS.Clone() as GameObject;
            platformObjectBMClone.Name = "Clone - Platform Moving Blue";
            platformObjectBMClone.Transform.SetTranslation(-4, 0, -32);

            //Add Translation Curve
            translationCurve = new Curve3D(CurveLoopType.Cycle);
            translationCurve.Add(new Vector3(2, 0, 0), 0);
            translationCurve.Add(new Vector3(-2, 0, 0), 20000);
            translationCurve.Add(new Vector3(2, 0, 0), 30000);

            platformObjectBMClone.AddComponent(new CurveBehaviour(translationCurve));
            platformObjectBMClone.AddComponent(new MeshRenderer(mesh, materialB));
            platformObjectBMClone.Transform.SetScale(4, 0.5f, 4);

            //add Collision Surface(s)
            var colliderBM = new Collider();

            platformObjectBMClone.AddComponent(colliderBM);
            colliderBM.AddPrimitive(new JigLibX.Geometry.Box(
                platformObjectBMClone.Transform.LocalTranslation,
                platformObjectBMClone.Transform.LocalRotation,
                platformObjectBMClone.Transform.LocalScale),
                new MaterialProperties(0.8f, 0.8f, 0.7f));
            colliderBM.Enable(false, 1);

            //Adds the switch method
            platformObjectBMClone.AddComponent(new SwitchPlatform(false));
            platformObjectBMClone.AddComponent(new FightGravity());

            level.Add(platformObjectBMClone); 
             */
            #endregion

        }

    }
}
