﻿using GDLibrary;
using GDApp;
using GDLibrary.Components;
using GDLibrary.Core;
using GDLibrary.Graphics;
using GDLibrary.Inputs;
using GDLibrary.Managers;
using GDLibrary.Parameters;
using GDLibrary.Renderers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ColorSwitchGame
{
    class ColorSwitch
    {
        /// <summary>
        /// Add Boundry walls to level.
        /// </summary>
        /// <param name="level"></param>
        public void InitializeBounds(Scene level, ContentManager Content)
        {
            #region Archetype

            var material = new BasicMaterial("simple diffuse");
            material.Texture = Content.Load<Texture2D>("Assets/ColorSwitch/Wall/Texture/BrickWall");
            material.Shader = new BasicShader(Application.Content);

            var wall = new GameObject("wall", GameObjectType.Architecture);
            var renderer = new MeshRenderer();
            renderer.Material = material;
            wall.AddComponent(renderer);
            renderer.Mesh = new CubeMesh();

            #endregion Archetype

            //Roof 
            for (int i = 0; i < 3; i++)
            {
                var clone = wall.Clone() as GameObject;
                clone.Name = $"{clone.Name} - {i}";
                clone.Transform.SetTranslation(0, 40, -10 - (i * 50));
                clone.Transform.SetScale(0.5f, 50, 50);
                clone.Transform.SetRotation(0,0 , 90);
                level.Add(clone);
            }

            //Floor
            for (int i = 0; i < 3; i++)
            {
                var clone = wall.Clone() as GameObject;
                clone.Name = $"{clone.Name} - {i + 3}";
                clone.Transform.SetTranslation(0, -40, -10 - (i * 50));
                clone.Transform.SetScale(0.5f, 50, 50);
                clone.Transform.SetRotation(0, 0, 90);
                level.Add(clone);
            }

            //Right wall
            for (int i = 0; i < 3; i++)
            {
                var clone = wall.Clone() as GameObject;
                clone.Name = $"{clone.Name} - {i + 6}";
                clone.Transform.SetTranslation(25, 0, -10 - (i*50));
                clone.Transform.SetScale(0.5f, 80, 50);
                level.Add(clone);
            }

            //Left wall
            for (int i = 0; i < 3; i++)
            {
                var clone = wall.Clone() as GameObject;
                clone.Name = $"{clone.Name} - {i + 9}";
                clone.Transform.SetTranslation(-25, 0, -10 - (i * 50));
                clone.Transform.SetScale(0.5f, 80, 50);
                level.Add(clone);
            }

        }

        /// <summary>
        /// Add neutral platforms to level.
        /// </summary>
        /// <param name="level"></param>
        public void InitializeNeutralPlatforms(Scene level, ContentManager Content)
        {
            #region Archetype

            var material = new BasicMaterial("simple diffuse");
            material.Texture = Content.Load<Texture2D>("Assets/ColorSwitch/Wall/Texture/BrickWall");
            material.Shader = new BasicShader(Application.Content);

            var platform = new GameObject("platform", GameObjectType.Architecture);
            var renderer = new MeshRenderer();
            renderer.Material = material;
            platform.AddComponent(renderer);
            renderer.Mesh = new CubeMesh();

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
                        clone.Transform.SetTranslation(0, -2, -14);
                        clone.Transform.SetScale(10, 5, 2.5f);
                        break;
                }
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

            //Red stationary platform
            var rendererRS = new MeshRenderer();
            rendererRS.Mesh = new CubeMesh();
            var materialRS = new BasicMaterial("red");
            materialRS.Texture = Content.Load<Texture2D>("red");
            materialRS.Shader = new BasicShader(Application.Content);
            rendererRS.Material = materialRS;

            var platformObjectRS = new GameObject("Platform Stationary Red", GameObjectType.Interactable);
            platformObjectRS.Transform.SetScale(2, 0.5f, 2);
            platformObjectRS.AddComponent(rendererRS);
            var switcherR = new PlatformerSwitch(true);
            platformObjectRS.AddComponent(switcherR);

            //Blue stationary platform
            var rendererBS = new MeshRenderer();
            rendererBS.Mesh = new CubeMesh();
            var materialBS = new BasicMaterial("blue");
            materialBS.Texture = Content.Load<Texture2D>("blue");
            materialBS.Shader = new BasicShader(Application.Content);
            rendererBS.Material = materialBS;

            var platformObjectBS = new GameObject("Platform Stationary Blue", GameObjectType.Interactable);
            platformObjectBS.Transform.SetScale(2, 0.5f, 2);
            platformObjectBS.AddComponent(rendererBS);
            var switcherB = new PlatformerSwitch(false);
            platformObjectBS.AddComponent(switcherB);

            //Create Platform clone Red
            for(int i = 0; i < 3; i++)
            {
                var platformObjectRSClone = platformObjectRS.Clone() as GameObject;

                switch (i) {
                    case 0: platformObjectRSClone.Transform.SetTranslation(2, 0, 0);
                        break;
                    case 1: platformObjectRSClone.Transform.SetTranslation(-2, 0, -4);
                        break;
                    case 2: platformObjectRSClone.Transform.SetTranslation(2, 0, -8);
                        break;
                }

                platformObjectRSClone.Transform.SetScale(2, 0.5f, 2);
                level.Add(platformObjectRSClone);
            }

            //Create Platform clone Blue
            for (int i = 0; i < 3; i++)
            {
                var platformObjectBSClone = platformObjectBS.Clone() as GameObject;

                switch (i)
                {
                    case 0:
                        platformObjectBSClone.Transform.SetTranslation(-2, 0, 0);
                        break;
                    case 1:
                        platformObjectBSClone.Transform.SetTranslation(2, 0, -4);
                        break;
                    case 2:
                        platformObjectBSClone.Transform.SetTranslation(-2, 0, -8);
                        break;
                }

                platformObjectBSClone.Transform.SetScale(2, 0.5f, 2);
                level.Add(platformObjectBSClone);
            }

            //Moving Platform
            Curve3D translationCurve;

            //Red Moving Platform
            var platformObjectRMClone = platformObjectRS.Clone() as GameObject;
            translationCurve = new Curve3D(CurveLoopType.Cycle);
            translationCurve.Add(new Vector3(4, 0, -18), 0);
            translationCurve.Add(new Vector3(-4, 0, -18), 2000);
            translationCurve.Add(new Vector3(4, 0, -18), 4000);
            platformObjectRMClone.AddComponent(new CurveBehaviour(translationCurve));

            platformObjectRMClone.Transform.SetScale(2, 0.5f, 2);
            level.Add(platformObjectRMClone);

            //Blue Moving Platform
            var platformObjectBMClone = platformObjectBS.Clone() as GameObject;
            translationCurve = new Curve3D(CurveLoopType.Cycle);
            translationCurve.Add(new Vector3(-4, 0, -22), 0);
            translationCurve.Add(new Vector3(4, 0, -22), 2000);
            translationCurve.Add(new Vector3(-4, 0, -22), 4000);
            platformObjectBMClone.AddComponent(new CurveBehaviour(translationCurve));

            platformObjectBMClone.Transform.SetScale(2, 0.5f, 2);
            level.Add(platformObjectBMClone);
        }

    }
}
