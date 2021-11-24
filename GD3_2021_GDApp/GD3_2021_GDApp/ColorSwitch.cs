using GDLibrary;
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
        public void InitializeBounds(Scene level, ContentManager Content)
        {
            #region Archetype

            var material = new BasicMaterial("simple diffuse");
            material.Texture = Content.Load<Texture2D>("Assets/ColorSwitch/Wall/Texture/BrickWall");
            material.Shader = new BasicShader(Application.Content);

            var Wall = new GameObject("cube", GameObjectType.Architecture);
            var renderer = new MeshRenderer();
            renderer.Material = material;
            Wall.AddComponent(renderer);
            renderer.Mesh = new CubeMesh();

            #endregion Archetype

            //Roof
            for (int i = 0; i < 3; i++)
            {
                var clone = Wall.Clone() as GameObject;
                clone.Name = $"{clone.Name} - {1}";
                clone.Transform.SetTranslation(0, 40, -10 - (i * 50));
                clone.Transform.SetScale(0.5f, 50, 50);
                clone.Transform.SetRotation(0,0 , 90);
                level.Add(clone);
            }

            //Floor
            for (int i = 0; i < 3; i++)
            {
                var clone = Wall.Clone() as GameObject;
                clone.Name = $"{clone.Name} - {1}";
                clone.Transform.SetTranslation(0, -40, -10 - (i * 50));
                clone.Transform.SetScale(0.5f, 50, 50);
                clone.Transform.SetRotation(0, 0, 90);
                level.Add(clone);
            }

            //Right wall
            for (int i = 0; i < 3; i++)
            {
                var clone = Wall.Clone() as GameObject;
                clone.Name = $"{clone.Name} - {1}";
                clone.Transform.SetTranslation(25, 0, -10 - (i*50));
                clone.Transform.SetScale(0.5f, 80, 50);
                level.Add(clone);
            }

            //Left wall
            for (int i = 0; i < 3; i++)
            {
                var clone = Wall.Clone() as GameObject;
                clone.Name = $"{clone.Name} - {1}";
                clone.Transform.SetTranslation(-25, 0, -10 - (i * 50));
                clone.Transform.SetScale(0.5f, 80, 50);
                level.Add(clone);
            }

        }

        public void InitializeNeutralPlatforms(Scene level, ContentManager Content)
        {
            #region Archetype

            var material = new BasicMaterial("model material");
            material.Texture = Content.Load<Texture2D>("Assets/Demo/Textures/checkerboard");
            material.Shader = new BasicShader(Application.Content);

            var archetypalSphere = new GameObject("sphere", GameObjectType.Consumable);
            archetypalSphere.IsStatic = false;

            var renderer = new ModelRenderer();
            renderer.Material = material;
            archetypalSphere.AddComponent(renderer);
            renderer.Model = Content.Load<Model>("Assets/Models/sphere");

            //downsize the model a little because the sphere is quite large
            archetypalSphere.Transform.SetScale(0.125f, 0.125f, 0.125f);

            #endregion Archetype

            var count = 0;
            for (var i = -8; i <= 8; i += 2)
            {
                var clone = archetypalSphere.Clone() as GameObject;
                clone.Name = $"{clone.Name} - {count++}";
                clone.Transform.SetTranslation(-5, i, 0);
                level.Add(clone);
            }
        }
    }
}
