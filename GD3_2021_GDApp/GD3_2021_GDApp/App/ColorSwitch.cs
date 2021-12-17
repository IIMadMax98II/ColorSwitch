﻿using GDLibrary;
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
            for (int i = 0; i < 2; i++)
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
            for(int i = 0; i < 3; i++)
            {
                var platformObjectRSClone = platformObjectRS.Clone() as GameObject;

                switch (i) {
                    case 0: platformObjectRSClone.Transform.SetTranslation(3, 0, -2);
                        break;
                    case 1: platformObjectRSClone.Transform.SetTranslation(-3, 0, -8);
                        break;
                    case 2: platformObjectRSClone.Transform.SetTranslation(3, 0, -14);
                        break;
                }

                platformObjectRSClone.Transform.SetScale(3, 0.5f, 3);

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
            for (int i = 0; i < 3; i++)
            {
                var platformObjectBSClone = platformObjectBS.Clone() as GameObject;

                switch (i)
                {
                    case 0:
                        platformObjectBSClone.Transform.SetTranslation(-3, 0, -2);
                        break;
                    case 1:
                        platformObjectBSClone.Transform.SetTranslation(3, 0, -8);
                        break;
                    case 2:
                        platformObjectBSClone.Transform.SetTranslation(-3, 0, -14);
                        break;
                }

                platformObjectBSClone.AddComponent(new MeshRenderer(mesh, materialB));
                platformObjectBSClone.Transform.SetScale(3, 0.5f, 3);

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

            //Moving Platform
            Curve3D translationCurve;

            //Red Moving Platform
            var platformObjectRMClone = platformObjectRS.Clone() as GameObject;
            platformObjectRMClone.Name = "Clone - Platform Moving Red";
            platformObjectRMClone.Transform.SetTranslation(4, 0, -26);

            //Add Translation Curve
            translationCurve = new Curve3D(CurveLoopType.Cycle);
            translationCurve.Add(new Vector3(2, 0, 0), 0 );
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
        }

    }
}
