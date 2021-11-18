﻿using GDLibrary.Utilities;
using GDLibrary.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GDLibrary.Editor
{
    [DataContract]
    public sealed class CurveHelper
    {
        private static readonly int ROUND_PRECISION = 3;

        private Vector3 translation;
        private Vector3 rotation;

        [DataMember]
        public Vector3 Translation { get => translation; set => translation = value; }
        [DataMember]
        public Vector3 Rotation { get => rotation; set => rotation = value; }

        public CurveHelper(Vector3 translation, Vector3 rotation)
        {
            //round the vectors "in-place"
            translation.Round(ROUND_PRECISION);
            rotation.Round(ROUND_PRECISION);

            //reduce precision
            this.translation = translation;
            this.rotation = rotation;
        }
    }

    public class CurveRecorderController : Controller
    {
        private static readonly int DEFAULT_MIN_SIZE = 10;

        private Camera camera;
        private string fileName;
        private List<CurveHelper> keyTransforms;

        public CurveRecorderController(string fileName = "")
        {
            if (fileName.Length == 0)
                fileName = $"{GetType().Name}.xml"; //use class as name

            this.fileName = fileName;
            keyTransforms = new List<CurveHelper>(DEFAULT_MIN_SIZE);
        }

        public override void Awake()
        {
            camera = Camera.Main;
            if (camera is null)
                throw new NullReferenceException("Scene does not have a main camera");
        }

        public override void Update()
        {
            HandleInputs();
        }
        protected override void HandleInputs()
        {
            HandleMouseInput();
            HandleKeyboardInput();
        }
        protected override void HandleMouseInput()
        {
            //if we right clicked then add to list
            if (Input.Mouse.WasJustClicked(Inputs.MouseButton.Right))
                keyTransforms.Add(new CurveHelper(camera.transform.LocalTranslation,
                    camera.transform.LocalRotation));
        }

        protected override void HandleKeyboardInput()
        {
            if (Input.Keys.WasJustPressed(Keys.F1))
                keyTransforms.Clear();
            else if (Input.Keys.WasJustPressed(Keys.F2))
            {
                if (keyTransforms.Count > 1)
                    keyTransforms.RemoveAt(keyTransforms.Count - 1);
            }
            else if (Input.Keys.WasJustPressed(Keys.F5))
            {
                if (keyTransforms.Count > 0)
                    SerializationUtility.Save(fileName, keyTransforms);
            }
        }

        protected override void HandleGamepadInput()
        {
        }
    }
}