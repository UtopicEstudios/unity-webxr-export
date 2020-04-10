using UnityEditor;

namespace WebXR.Editor
{
	public static class Builder
	{
		private const string TemplateFolderName = "WebXR~";
		[MenuItem("Build/All")]
		public static void BuildAll()
		{
			BuildPackage();
			BuildDesertSample();
		}

		[MenuItem("Build/Package")]
		public static void BuildPackage()
		{
			AssetDatabase.ExportPackage(new[] { "Assets/WebXR", "Assets/WebGLTemplates/" + TemplateFolderName }, "WebXR-Assets.unitypackage", ExportPackageOptions.Recurse);
		}

		[MenuItem("Build/Desert Sample")]
		public static void BuildDesertSample()
		{
#if !UNITY_2018_4_OR_NEWER
			// There is no explicit api for setting the template as of 2018.4
			PlayerSettings.SetPropertyString("template", "PROJECT:" + TemplateFolderName, BuildTargetGroup.WebGL);
#else
			PlayerSettings.WebGL.template = TemplateFolderName;
#endif
			BuildPipeline.BuildPlayer(new BuildPlayerOptions
			{
				target = BuildTarget.WebGL,
				locationPathName = "Build",
				scenes = new[] { "Assets/WebXR/Samples/Desert/WebXR.unity" },
			});
		}
	}
}
