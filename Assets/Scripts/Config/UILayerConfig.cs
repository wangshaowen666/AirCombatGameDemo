﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILayerConfig : NormalSingleton<UILayerConfig>
{
	public static readonly Dictionary<string, UILayer> Layers = new Dictionary<string, UILayer>()
	{
		{Paths.PREFAB_START_VIEW,UILayer.BASE_UI},
		{Paths.PREFAB_SELECTED_HERO_VIEW,UILayer.BASE_UI},
		{Paths.PREFAB_STRENGTHEN_VIEW,UILayer.BASE_UI},
		{Paths.PREFAB_LOADING_VIEW,UILayer.BASE_UI},
		{Paths.PREFAB_GAME_UI_VIEW,UILayer.BASE_UI},
		{Paths.PREFAB_LEVELS_VIEW,UILayer.BASE_UI},
		{Paths.PREFAB_PAUSE_VIEW,UILayer.MIDDLE_UI},
		{Paths.PREFAB_DIALOG,UILayer.TOP_UI}
	};
}