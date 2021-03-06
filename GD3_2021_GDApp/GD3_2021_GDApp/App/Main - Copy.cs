////#define DEMO

//using GDLibrary;
//using GDLibrary.Collections;
//using GDLibrary.Components;
//using GDLibrary.Components.UI;
//using GDLibrary.Core;
//using GDLibrary.Core.Demo;
//using GDLibrary.Graphics;
//using GDLibrary.Inputs;
//using GDLibrary.Managers;
//using GDLibrary.Parameters;
//using GDLibrary.Renderers;
//using GDLibrary.Utilities;
//using JigLibX.Collision;
//using JigLibX.Geometry;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Audio;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Media;
//using System;
//using System.Collections.Generic;
//using ColorSwitchGame;

//namespace GDApp
//{
//    public class Main : Game
//    {
//        #region Fields

//        private GraphicsDeviceManager _graphics;
//        private SpriteBatch _spriteBatch;

//        /// <summary>
//        /// Stores all scenes (which means all game objects i.e. players, cameras, pickups, behaviours, controllers)
//        /// </summary>
//        private SceneManager sceneManager;

//        /// <summary>
//        /// Renders all game objects with an attached and enabled renderer
//        /// </summary>
//        private RenderManager renderManager;

//        /// <summary>
//        /// Updates and Draws all ui objects
//        /// </summary>
//        private UISceneManager uiSceneManager;

//        /// <summary>
//        /// Updates and Draws all menu objects
//        /// </summary>
//        private MyMenuManager uiMenuManager;

//        /// <summary>
//        /// Plays all 2D and 3D sounds
//        /// </summary>
//        private SoundManager soundManager;

//        private PickingManager pickingManager;

//        /// <summary>
//        /// Handles all system wide events between entities
//        /// </summary>
//        private EventDispatcher eventDispatcher;

//        /// <summary>
//        /// Applies physics to all game objects with a Collider
//        /// </summary>
//        private PhysicsManager physicsManager;

//        /// <summary>
//        /// Quick lookup for all textures used within the game
//        /// </summary>
//        private Dictionary<string, Texture2D> textureDictionary;

//        /// <summary>
//        /// Quick lookup for all fonts used within the game
//        /// </summary>
//        private ContentDictionary<SpriteFont> fontDictionary;

//        /// <summary>
//        /// Quick lookup for all models used within the game
//        /// </summary>
//        private ContentDictionary<Model> modelDictionary;

//        //temps
//        private Scene activeScene;

//        private UITextObject nameTextObj;
//        private Collider collider;

//        private Texture2D uiColor;
//        private Color cSwitch;

//        #endregion Fields

//        /// <summary>
//        /// Construct the Game object
//        /// </summary>
//        #region Constructors

//        public Main()
//        {
//            _graphics = new GraphicsDeviceManager(this);
//            Content.RootDirectory = "Content";
//        }

//        #endregion Constructors

//        /// <summary>
//        /// Set application data, input, title and scene manager
//        /// </summary>
//        #region Initialization - Scene manager, Application data, Screen, Input, Scenes, Game Objects

//        /// <summary>
//        /// Initialize engine, dictionaries, assets, level contents
//        /// </summary>
//        protected override void Initialize()
//        {
//            //Initialize color for UI
//            cSwitch = Color.Blue;
//            uiColor = Content.Load<Texture2D>("Blue_Frame");


//            //data, input, scene manager
//            InitializeEngine("ColorShift", 1024, 768);

//            //load structures that store assets (e.g. textures, sounds) or archetypes (e.g. Quad game object)
//            InitializeDictionaries();

//            //load assets into the relevant dictionary
//            LoadAssets();

//            //level with scenes and game objects
//            InitializeLevel();

//            //TODO - remove hardcoded mouse values - update Screen class
//            //centre the mouse with hardcoded value - remove later
//            Input.Mouse.Position = new Vector2(512, 384);

//            base.Initialize();
//        }

//        /// <summary>
//        /// Stores all re-used assets and archetypal game objects
//        /// </summary>
//        private void InitializeDictionaries()
//        {
//            textureDictionary = new Dictionary<string, Texture2D>();
//        }

//        /// <summary>
//        /// Load resources from file
//        /// </summary>
//        private void LoadAssets()
//        {
//            LoadTextures();
//        }

//        /// <summary>
//        /// Load texture data from file and add to the dictionary
//        /// </summary>
//        private void LoadTextures()
//        {
//            //debug
//            textureDictionary.Add("checkerboard", Content.Load<Texture2D>("Assets/Demo/Textures/checkerboard"));

//            //skybox
//            textureDictionary.Add("skybox_front", Content.Load<Texture2D>("Assets/Textures/Skybox/front"));
//            textureDictionary.Add("skybox_left", Content.Load<Texture2D>("Assets/Textures/Skybox/left"));
//            textureDictionary.Add("skybox_right", Content.Load<Texture2D>("Assets/Textures/Skybox/right"));
//            textureDictionary.Add("skybox_back", Content.Load<Texture2D>("Assets/Textures/Skybox/back"));
//            textureDictionary.Add("skybox_sky", Content.Load<Texture2D>("Assets/Textures/Skybox/sky"));

//            //ColorSwitch
//            textureDictionary.Add("BrickWall_01", Content.Load<Texture2D>("Assets/ColorSwitch/Wall/Texture/BrickWall"));
//        }

//        /// <summary>
//        /// Create a scene, add content, add to the scene manager, and load default scene
//        /// </summary>
//        private void InitializeLevel()
//        {
           
//            activeScene = new Scene("level 1");
           

//            InitializeCameras(activeScene);
            
//            InitializeSkybox(activeScene, 500);

//            // ColorSwitch Make walls + Neutral Platforms
//            ColorSwitch game = new ColorSwitch();
//            game.InitializeBounds(activeScene, Content);
//            game.InitializeNeutralPlatforms(activeScene, Content);

//            //Load platforms
//            game.InitializePlatforms(activeScene, Content);

//            sceneManager.Add(activeScene);
//            sceneManager.LoadScene("level 1");

//            //Initialize the switch at the beginning to get every object to their correct state
//            Switch();
//        }

//        /// <summary>
//        /// Set up the skybox using a QuadMesh
//        /// </summary>
//        /// <param name="level">Scene Stores all game objects for current...</param>
//        /// <param name="worldScale">float Value used to scale skybox normally 250 - 1000</param>
//        private void InitializeSkybox(Scene level, float worldScale = 500)
//        {
//            #region Archetype

//            var material = new BasicMaterial("simple diffuse");
//            material.Texture = textureDictionary["checkerboard"];
//            material.Shader = new BasicShader(Application.Content);

//            var archetypalQuad = new GameObject("quad", GameObjectType.Skybox);
//            archetypalQuad.IsStatic = false;
//            var renderer = new MeshRenderer();
//            renderer.Material = material;
//            archetypalQuad.AddComponent(renderer);
//            renderer.Mesh = new QuadMesh();

//            #endregion Archetype

//            //back
//            GameObject back = archetypalQuad.Clone() as GameObject;
//            back.Name = "skybox_back";
//            material.Texture = textureDictionary["skybox_back"];
//            back.Transform.Translate(0, 0, -worldScale / 2.0f);
//            back.Transform.Scale(worldScale, worldScale, null);
//            level.Add(back);

//            //left
//            GameObject left = archetypalQuad.Clone() as GameObject;
//            left.Name = "skybox_left";
//            material.Texture = textureDictionary["skybox_left"];
//            left.Transform.Translate(-worldScale / 2.0f, 0, 0);
//            left.Transform.Scale(worldScale, worldScale, null);
//            left.Transform.Rotate(0, 90, 0);
//            level.Add(left);

//            //right
//            GameObject right = archetypalQuad.Clone() as GameObject;
//            right.Name = "skybox_right";
//            material.Texture = textureDictionary["skybox_right"];
//            right.Transform.Translate(worldScale / 2.0f, 0, 0);
//            right.Transform.Scale(worldScale, worldScale, null);
//            right.Transform.Rotate(0, -90, 0);
//            level.Add(right);

//            //front
//            GameObject front = archetypalQuad.Clone() as GameObject;
//            front.Name = "skybox_front";
//            material.Texture = textureDictionary["skybox_front"];
//            front.Transform.Translate(0, 0, worldScale / 2.0f);
//            front.Transform.Scale(worldScale, worldScale, null);
//            front.Transform.Rotate(0, -180, 0);
//            level.Add(front);

//            //top
//            GameObject top = archetypalQuad.Clone() as GameObject;
//            top.Name = "skybox_sky";
//            material.Texture = textureDictionary["skybox_sky"];
//            top.Transform.Translate(0, worldScale / 2.0f, 0);
//            top.Transform.Scale(worldScale, worldScale, null);
//            top.Transform.Rotate(90, 0, 0);
//            level.Add(top);
//        }

//        /// <summary>
//        /// Initialize the camera(s) in our scene
//        /// </summary>
//        /// <param name="level"></param>
//        private void InitializeCameras(Scene level)
//        {
//            #region First Person Camera

//            //add camera game object
//            var camera = new GameObject("main camera", GameObjectType.Camera);

//            //set viewport
//            //var viewportLeft = new Viewport(0, 0,
//            //    _graphics.PreferredBackBufferWidth / 2,
//            //    _graphics.PreferredBackBufferHeight);

//            //add components
//            camera.AddComponent(new Camera(_graphics.GraphicsDevice.Viewport));
//            camera.AddComponent(new FirstPersonController(0.05f, 0.025f, 0.00009f));

//            //set initial position
//            camera.Transform.SetTranslation(0, 0, 15);

//            //add to level
//            level.Add(camera);

//            #endregion First Person Camera

//            #region Curve Camera

//            //add curve for camera translation
//            var translationCurve = new Curve3D(CurveLoopType.Cycle);
//            translationCurve.Add(new Vector3(0, 0, 10), 0);
//            translationCurve.Add(new Vector3(0, 5, 15), 1000);
//            translationCurve.Add(new Vector3(0, 0, 20), 2000);
//            translationCurve.Add(new Vector3(0, -5, 25), 3000);
//            translationCurve.Add(new Vector3(0, 0, 30), 4000);
//            translationCurve.Add(new Vector3(0, 0, 10), 6000);

//            //add camera game object
//            var curveCamera = new GameObject("curve camera", GameObjectType.Camera);

//            //set viewport
//            //var viewportRight = new Viewport(_graphics.PreferredBackBufferWidth / 2, 0,
//            //    _graphics.PreferredBackBufferWidth / 2,
//            //    _graphics.PreferredBackBufferHeight);

//            //add components
//            curveCamera.AddComponent(new Camera(_graphics.GraphicsDevice.Viewport));
//            curveCamera.AddComponent(new CurveBehaviour(translationCurve));
//            curveCamera.AddComponent(new FOVOnScrollController(MathHelper.ToRadians(2)));

//            //add to level
//            level.Add(curveCamera);

//            #endregion Curve Camera

//            //set theMain camera, if we dont call this then the first camera added will be the Main
//            level.SetMainCamera("main camera");

//            //allows us to scale time on all game objects that based movement on Time
//            // Time.Instance.TimeScale = 0.1f;
//        }

//        /// <summary>
//        /// Add demo game objects based on FBX vertex data
//        /// </summary>
//        /// <param name="level"></param>
//        private void InitializeModels(Scene level)
//        {
//            #region Archetype

//            var material = new BasicMaterial("model material");
//            material.Texture = Content.Load<Texture2D>("Assets/Demo/Textures/checkerboard");
//            material.Shader = new BasicShader(Application.Content);

//            var archetypalSphere = new GameObject("sphere", GameObjectType.Consumable);
//            archetypalSphere.IsStatic = false;

//            var renderer = new ModelRenderer();
//            renderer.Material = material;
//            archetypalSphere.AddComponent(renderer);
//            renderer.Model = Content.Load<Model>("Assets/Models/sphere");

//            //downsize the model a little because the sphere is quite large
//            archetypalSphere.Transform.SetScale(0.125f, 0.125f, 0.125f);

//            #endregion Archetype

//            var count = 0;
//            for (var i = -8; i <= 8; i += 2)
//            {
//                var clone = archetypalSphere.Clone() as GameObject;
//                clone.Name = $"{clone.Name} - {count++}";
//                clone.Transform.SetTranslation(-5, i, 0);
//                level.Add(clone);
//            }
//        }

//        /// <summary>
//        /// Add demo game objects based on user-defined vertices and indices
//        /// </summary>
//        /// <param name="level"></param>
//        private void InitializeCubes(Scene level)
//        {
//            #region Archetype

//            var material = new BasicMaterial("simple diffuse");
//            material.Texture = Content.Load<Texture2D>("Assets/Demo/Textures/mona lisa");
//            material.Shader = new BasicShader(Application.Content);

//            var archetypalCube = new GameObject("cube", GameObjectType.Architecture);
//            var renderer = new MeshRenderer();
//            renderer.Material = material;
//            archetypalCube.AddComponent(renderer);
//            renderer.Mesh = new CubeMesh();

//            #endregion Archetype

//            var count = 0;
//            for (var i = 1; i <= 8; i += 2)
//            {
//                var clone = archetypalCube.Clone() as GameObject;
//                clone.Name = $"{clone.Name} - {count++}";
//                clone.Transform.SetTranslation(i, 0, 0);
//                clone.Transform.SetScale(1, i, 1);
//                level.Add(clone);
//            }
//        }

//        /// <summary>
//        /// Set application data, input, title and scene manager
//        /// </summary>
//        private void InitializeEngine(string gameTitle, int width, int height)
//        {
//            //set game title
//            Window.Title = gameTitle;

//            //instanciate scene manager to store all scenes
//            sceneManager = new SceneManager(this);

//            //initialize global application data
//            Application.Main = this;
//            Application.Content = Content;
//            Application.GraphicsDevice = _graphics.GraphicsDevice; //TODO - is this necessary?
//            Application.GraphicsDeviceManager = _graphics;
//            Application.SceneManager = sceneManager;

//            //instanciate render manager to render all drawn game objects using preferred renderer (e.g. forward, backward)
//            renderManager = new RenderManager(this, new ForwardRenderer(), false);

//            //instanciate screen (singleton) and set resolution etc
//            Screen.GetInstance().Set(width, height, true, false);

//            //instanciate input components and store reference in Input for global access
//            Input.Keys = new KeyboardComponent(this);
//            Input.Mouse = new MouseComponent(this);
//            Input.Gamepad = new GamepadComponent(this);

//            //add all input components to component list so that they will be updated and/or drawn
//            //Q. what would happen is we commented out these lines?
//            Components.Add(sceneManager); //add so SceneManager::Update() will be called
//            Components.Add(renderManager); //add so RenderManager::Draw() will be called
//            Components.Add(Input.Keys);
//            Components.Add(Input.Mouse);
//            Components.Add(Input.Gamepad);
//            Components.Add(Time.GetInstance(this));

//        }

//        #endregion Initialization - Scene manager, Application data, Screen, Input, Scenes, Game Objects

//        #region Load & Unload Assets

//        protected override void LoadContent()
//        {
//            _spriteBatch = new SpriteBatch(GraphicsDevice);
//        }

//        protected override void UnloadContent()
//        {
//            base.UnloadContent();
//        }

//        #endregion Load & Unload Assets

//        #region Update & Draw

       
//        /// <summary>
//        /// Added a mouse input to detect whenever the player left click, which would trigger switch event
//        /// </summary>
//        /// <param name="gameTime"></param>
//        protected override void Update(GameTime gameTime)
//        {
//            base.Update(gameTime);

            

//            if (Input.Mouse.WasJustClicked((MouseButton.Left)))
//                Switch();
//        }

//        /// <summary>
//        /// Predicate to check if a Gameobject has a component with the IInteractable interface
//        /// </summary>
//        /// <param name="gameObject"></param>
//        /// <returns></returns>
//        static bool IsInteractable(GameObject gameObject)
//        {
//            foreach (Component component in gameObject.Components)
//            {
//                if (component is IInteractable)
//                    return true;
//            }
//            // return gO.Components is IInteractable;
//            return false;
//        }

//        /// <summary>
//        /// Added a method called Switch that will change UI color from red to blue and viceversa
//        /// </summary>
//        /// 
//        private void Switch()
//        {
            
//            if(cSwitch == Color.Blue)
//            {
//                cSwitch = Color.Red;
//                uiColor = Content.Load<Texture2D>("Red_Frame");

//                foreach (GameObject gameObject in activeScene.FindAll(IsInteractable))
//                {
//                    foreach (Component component in gameObject.Components)
//                    {
//                        if (component is IInteractable)
//                            (component as IInteractable).Switch(true);
//                    }
//                }
//            }
//            else
//            {
//                cSwitch = Color.Blue;
//                uiColor = Content.Load<Texture2D>("Blue_Frame");
//                foreach (GameObject gameObject in activeScene.FindAll(IsInteractable))
//                {
//                    foreach (Component component in gameObject.Components)
//                    {
//                        if (component is IInteractable)
//                            (component as IInteractable).Switch(false);
//                    }
//                }
//            }
//        }

//        protected override void Draw(GameTime gameTime)
//        {
//            GraphicsDevice.Clear(Color.HotPink);
            
//            base.Draw(gameTime);
            
//            // Added the UI to be drawn after all the object so its not overlapped by any object
//            _spriteBatch.Begin();
//            _spriteBatch.Draw(uiColor, new Vector2(0, 0), null, cSwitch, 0, Vector2.Zero, new Vector2(0.62f, 0.75f), SpriteEffects.None, 0);
//            _spriteBatch.End();
//        }

//        #endregion Update & Draw

//#if DEMO

//        private void InitializeEditorHelpers()
//        {
//            //a game object to record camera positions to an XML file for use in a curve later
//            var curveRecorder = new GameObject("curve recorder", GameObjectType.Editor);
//            curveRecorder.AddComponent(new GDLibrary.Editor.CurveRecorderController());
//            activeScene.Add(curveRecorder);
//        }

//        private void RunDemos()
//        {
//        #region Curve Demo

//            //var curve1D = new GDLibrary.Parameters.Curve1D(CurveLoopType.Cycle);
//            //curve1D.Add(0, 0);
//            //curve1D.Add(10, 1000);
//            //curve1D.Add(20, 2000);
//            //curve1D.Add(40, 4000);
//            //curve1D.Add(60, 6000);
//            //var value = curve1D.Evaluate(500, 2);

//        #endregion Curve Demo

//        #region Serialization Single Object Demo

//            var demoSaveLoad = new DemoSaveLoad(new Vector3(1, 2, 3), new Vector3(45, 90, -180), new Vector3(1.5f, 0.1f, 20.25f));
//            GDLibrary.Utilities.SerializationUtility.Save("DemoSingle.xml", demoSaveLoad);
//            var readSingle = GDLibrary.Utilities.SerializationUtility.Load("DemoSingle.xml",
//                typeof(DemoSaveLoad)) as DemoSaveLoad;

//        #endregion Serialization Single Object Demo

//        #region Serialization List Objects Demo

//            List<DemoSaveLoad> listDemos = new List<DemoSaveLoad>();
//            listDemos.Add(new DemoSaveLoad(new Vector3(1, 2, 3), new Vector3(45, 90, -180), new Vector3(1.5f, 0.1f, 20.25f)));
//            listDemos.Add(new DemoSaveLoad(new Vector3(10, 20, 30), new Vector3(4, 9, -18), new Vector3(15f, 1f, 202.5f)));
//            listDemos.Add(new DemoSaveLoad(new Vector3(100, 200, 300), new Vector3(145, 290, -80), new Vector3(6.5f, 1.1f, 8.05f)));

//            GDLibrary.Utilities.SerializationUtility.Save("ListDemo.xml", listDemos);
//            var readList = GDLibrary.Utilities.SerializationUtility.Load("ListDemo.xml",
//                typeof(List<DemoSaveLoad>)) as List<DemoSaveLoad>;

//        #endregion Serialization List Objects Demo
//        }

//#endif
//    }
//}