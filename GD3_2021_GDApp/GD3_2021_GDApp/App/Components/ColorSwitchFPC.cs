﻿using GDLibrary;
using GDLibrary.Components;
using GDLibrary.Core;
using GDLibrary.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace GDApp
{
    /// <summary>
    /// Adds collidable 1st person controller to camera using keyboard and mouse input
    /// </summary>
    public class ColorSwitchFPC : FirstPersonController
    {
        #region Statics

        private static readonly float DEFAULT_JUMP_HEIGHT = 5;

        #endregion Statics

        #region Fields

        private CharacterCollider characterCollider;
        private Character characterBody;

        //temp vars
        private Vector3 restrictedLook, restrictedRight;

        private float jumpHeight;

        #endregion Fields

        #region Contructors

        public ColorSwitchFPC(float jumpHeight, float moveSpeed, float strafeSpeed, float rotationSpeed)
        : this(jumpHeight, moveSpeed, strafeSpeed, rotationSpeed * Vector2.One)
        {
        }

        public ColorSwitchFPC(float jumpHeight, float moveSpeed, float strafeSpeed, Vector2 rotationSpeed)
        : base(moveSpeed, strafeSpeed, rotationSpeed, true)
        {
            this.jumpHeight = jumpHeight > 0 ? jumpHeight : DEFAULT_JUMP_HEIGHT;
        }

        #endregion Contructors

        public override void Awake(GameObject gameObject)
        {
            //get the collider attached to the game object for this controller
            characterCollider = gameObject.GetComponent<Collider>() as CharacterCollider;
            //get the body so that we can change its position when keys
            characterBody = characterCollider.Body as Character;
            base.Awake(gameObject);
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

        protected override void HandleKeyboardInput()
        {
            if (characterBody == null)
                throw new NullReferenceException("No body to move with this controller. You need to add the collider component before this controller!");

            HandleMove();
            HandleStrafe();
            HandleJump();
        }

        private void HandleMove()
        {
            if (Input.Keys.IsPressed(Keys.W))//&& Input.Keys.IsPressed(Keys.LeftControl))
            {
                restrictedLook = transform.Up; //we use Up instead of Forward
                restrictedLook.Y = 0;
                characterBody.Velocity -= moveSpeed * restrictedLook * Time.Instance.DeltaTimeMs;
            }
            else if (Input.Keys.IsPressed(Keys.S))
            {
                restrictedLook = transform.Up;
                restrictedLook.Y = 0;
                characterBody.Velocity += moveSpeed * restrictedLook * Time.Instance.DeltaTimeMs;
            }
            else
            {
                characterBody.DesiredVelocity = Vector3.Zero;
            }
        }

        private void HandleStrafe()
        {
            if (Input.Keys.IsPressed(Keys.A))
            {
                restrictedRight = transform.Right;
                restrictedRight.Y = 0;
                characterBody.Velocity -= strafeSpeed * restrictedRight * Time.Instance.DeltaTimeMs;
            }
            else if (Input.Keys.IsPressed(Keys.D))
            {
                restrictedRight = transform.Right;
                restrictedRight.Y = 0;
                characterBody.Velocity += strafeSpeed * restrictedRight * Time.Instance.DeltaTimeMs;
            }
            else
            {
                characterBody.DesiredVelocity = Vector3.Zero;
            }
        }

        protected override void HandleMouseInput()
        {
            if (Input.Mouse.WasJustClicked(MouseButton.Left))
                EventDispatcher.Raise(new EventData(EventCategoryType.MaterialChange, EventActionType.OnMouseClick));

            base.HandleMouseInput();
        }
        private void HandleJump()
        {
            if (Input.Keys.IsPressed(Keys.Space))
                characterBody.DoJump(jumpHeight);
        }

        #region Unused

        protected override void HandleGamepadInput()
        {
        }

        #endregion Unused
    }
}