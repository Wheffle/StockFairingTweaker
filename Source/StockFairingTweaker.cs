/*
 * Author: Chase Barnes (Wheffle)
 * <altoid287@gmail.com>
 * 19 May 2015
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockFairingTweaker
{
    public class ModuleProceduralFairingTweaker : PartModule
    {
        /*
         * Current Functionality:
         * - Make number of pieces per section (nArcs) tweakable in-game
         * - Make section height (xSectionHeightMax) tweakable in-game
         * - Make edge smoothing (edgeWarp) tweakable in-game
         */

        [UI_FloatRange(minValue = 2.0f, maxValue = 6.0f, stepIncrement = 1.0f)]
        [KSPField(guiName = "Divisions", guiActiveEditor = true)]
        float tweakArcs = 2.0f;

        [UI_FloatRange(minValue = 2.0f, maxValue = 6.0f, stepIncrement = 0.1f)]
        [KSPField(guiName = "Max Height", guiActiveEditor = true)]
        float tweakSectionHeightMax = 2.0f;

        [UI_FloatRange(minValue = 0.00f, maxValue = 0.10f, stepIncrement = 0.01f)]
        [KSPField(guiName = "Edge Smooth", guiActiveEditor = true)]
        float tweakEdgeWarp = 0.00f;

        ModuleProceduralFairing pf;
        
        public override void OnStart(StartState state)
        {
            base.OnStart(state);
            
            pf = FindProceduralFairingModule();

            if (pf != null)
            {
                //set ProceduralFairingModule's tweakable fields to persistant
                pf.Fields["nArcs"].isPersistant = true;
                pf.Fields["xSectionHeightMax"].isPersistant = true;
                pf.Fields["edgeWarp"].isPersistant = true;
                
                //initial tweak bar settings
                tweakArcs = pf.nArcs;
                tweakSectionHeightMax = pf.xSectionHeightMax;
                tweakEdgeWarp = pf.edgeWarp;
                
            }
            else
            {
                PDebug.Log("StockFairingTweaker: ModuleProceduralFairing instance not found!");
                Fields["tweakArcs"].guiActiveEditor = false;
                Fields["tweakSectionHeightMax"].guiActiveEditor = false;
                Fields["tweakEdgeWarp"].guiActiveEditor = false;
            }
        }

        public void Update()
        {
            if (pf != null && HighLogic.LoadedSceneIsEditor)
            {
                pf.nArcs = (int)tweakArcs;
                pf.xSectionHeightMax = tweakSectionHeightMax;
                pf.edgeWarp = tweakEdgeWarp;
            }
        }

        private ModuleProceduralFairing FindProceduralFairingModule()
        {
            foreach (PartModule module in part.Modules)
            {
                if (module.moduleName == "ModuleProceduralFairing") return (ModuleProceduralFairing)module;
            }

            return null;
        }
    }
}

