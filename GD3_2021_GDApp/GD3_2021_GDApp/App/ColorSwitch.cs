using GDLibrary;
using GDApp;
using GDLibrary.Components;
using GDLibrary.Graphics;
using GDLibrary.Parameters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ColorSwitch
{
    class FirstScene
    {
        /// <summary>
        /// Add Boundry walls to level.
        /// </summary>
        /// <param name="level"></param>
        public void InitializeBounds(Scene level, ContentManager Content)
        {
            #region Archetype

            var shader = new BasicShader(Application.Content, true, true);
            var mesh = new CubeMesh();
            var material = new BasicMaterial("simple diffuse", shader, Color.White, 1, Content.Load<Texture2D>("Assets/ColorSwitch/Wall/Texture/BrickWall"));
            var wall = new GameObject("wall", GameObjectType.Architecture, true);

            #endregion Archetype

            //Roof 
            for (int i = 0; i < 3; i++)
            {
                var clone = wall.Clone() as GameObject;
                clone.Name = $"{clone.Name} - {i}";
                clone.Transform.SetTranslation(0, 40, -10 - (i * 50));
                clone.Transform.SetScale(0.5f, 50, 50);
                clone.Transform.SetRotation(0,0 , 90);
                clone.AddComponent(new MeshRenderer(mesh, material));
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
                clone.AddComponent(new MeshRenderer(mesh, material));
                level.Add(clone);
            }

            //Right wall
            for (int i = 0; i < 3; i++)
            {
                var clone = wall.Clone() as GameObject;
                clone.Name = $"{clone.Name} - {i + 6}";
                clone.Transform.SetTranslation(25, 0, -10 - (i*50));
                clone.Transform.SetScale(0.5f, 80, 50);
                clone.AddComponent(new MeshRenderer(mesh, material));
                clone.AddComponent(new MeshRenderer(mesh, material));
                level.Add(clone);
            }

            //Left wall
            for (int i = 0; i < 3; i++)
            {
                var clone = wall.Clone() as GameObject;
                clone.Name = $"{clone.Name} - {i + 9}";
                clone.Transform.SetTranslation(-25, 0, -10 - (i * 50));
                clone.Transform.SetScale(0.5f, 80, 50);
                clone.AddComponent(new MeshRenderer(mesh, material));
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

            var shader = new BasicShader(Application.Content, true, true);
            var mesh = new CubeMesh();
            var material = new BasicMaterial("simple diffuse", shader, Color.White, 1, Content.Load<Texture2D>("Assets/ColorSwitch/Wall/Texture/BrickWall"));
            var platform = new GameObject("platform", GameObjectType.Architecture, true);

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
                clone.AddComponent(new MeshRenderer(mesh, material));
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
            var switcherR = new PlatformSwitch(true);
            platformObjectRS.AddComponent(switcherR);

            //Blue stationary platform
            var materialB = new BasicMaterial("blue", shader, Color.Blue, 1, Content.Load<Texture2D>("Assets/Textures/Props/Platforms/blue"));

            var platformObjectBS = new GameObject("Platform Stationary Blue", GameObjectType.Interactable, false);
            platformObjectBS.Transform.SetScale(2, 0.5f, 2);
            var switcherB = new PlatformSwitch(false);
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

                platformObjectRSClone.AddComponent(new MeshRenderer(mesh, materialR));
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

                platformObjectBSClone.AddComponent(new MeshRenderer(mesh, materialB));
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

            platformObjectRMClone.AddComponent(new MeshRenderer(mesh, materialR));
            platformObjectRMClone.Transform.SetScale(2, 0.5f, 2);
            level.Add(platformObjectRMClone);

            //Blue Moving Platform
            var platformObjectBMClone = platformObjectBS.Clone() as GameObject;
            translationCurve = new Curve3D(CurveLoopType.Cycle);
            translationCurve.Add(new Vector3(-4, 0, -22), 0);
            translationCurve.Add(new Vector3(4, 0, -22), 2000);
            translationCurve.Add(new Vector3(-4, 0, -22), 4000);
            platformObjectBMClone.AddComponent(new CurveBehaviour(translationCurve));

            platformObjectBMClone.AddComponent(new MeshRenderer(mesh, materialR));
            platformObjectBMClone.Transform.SetScale(2, 0.5f, 2);
            level.Add(platformObjectBMClone);
        }

    }
}
