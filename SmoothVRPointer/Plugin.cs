﻿using HarmonyLib;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using SiraUtil.Zenject;
using SmoothVRPointer.Installers;
using SmoothVRPointer.Patches;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;

namespace SmoothVRPointer
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }
        public const string HARMONY_ID = "soothvrpointer.denpadokei.com.github";
        private Harmony _harmony;

        [Init]
        /// <summary>
        /// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
        /// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
        /// Only use [Init] with one Constructor.
        /// </summary>
        public void Init(IPALogger logger, Config conf, Zenjector zenjector)
        {
            Instance = this;
            Log = logger;
            Log.Info("SmoothVRPointer initialized.");
            this._harmony = new Harmony(HARMONY_ID);
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Log.Debug("Config loaded");

            zenjector.Install<MenuInstaller>(Location.Menu);
        }

        #region BSIPA Config
        //Uncomment to use BSIPA's config
        /*
        [Init]
        public void InitWithConfig(Config conf)
        {
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Log.Debug("Config loaded");
        }
        */
        #endregion

        [OnStart]
        public void OnApplicationStart()
        {
            Log.Debug("OnApplicationStart");
            SceneManager.activeSceneChanged += this.SceneManager_activeSceneChanged;
            //new GameObject("SmoothVRPointerController").AddComponent<SmoothVRPointerController>();

        }

        private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
        {
            //OculusHelperPatch.CurrentSceneName = arg1.name;
            IVRPlatformHelperPatch.CurrentSceneName = arg1.name;
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            Log.Debug("OnApplicationQuit");
            SceneManager.activeSceneChanged -= this.SceneManager_activeSceneChanged;
        }

        [OnEnable]
        public void OnEnable()
        {
            Log.Debug("Patch");
            this._harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        [OnDisable]
        public void OnDisable()
        {
            Log.Debug("UnPatch");
            this._harmony?.UnpatchSelf();
        }
    }
}
