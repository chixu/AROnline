using System;
using System.Collections.Generic;
using UnityEngine;
 
/**
 * @author zeh fernando
 * @modify MemoryC_2017-02-05
 */
class StatusBar {
 
	public static void Show(bool v = true){
		UnityEditor.PlayerSettings.iOS.statusBarStyle = UnityEditor.iOSStatusBarStyle.Default;
		#if UNITY_IOS
			
		#endif
		#if UNITY_ANDROID
		AndroidStatusBar.statusBarState = v?AndroidStatusBar.States.Visible : AndroidStatusBar.States.Hidden;
		#endif
	}
}