Shader "GpuDoodle"{
	Properties{
		_Color("Tint", Color) = (0, 0, 0, 1)
		_MainTex("Texture", 2D) = "white" {}
		_Metallic("Metallic", Range(0,1)) = 0.0

		// Add properties
		_DoodleMaxOffset("Doodle Max Offset", vector) = (0.005, 0.005, 0, 0)
		_DoodleFrameTime("Doodle Frame Time", Float) = 0.2
		_DoodleFrameCount("Doodle Frame Count", Int) = 24
		_DoodleNoiseScale("Doodle Noise Scale", vector) = (35, 35, 1, 1)
	}

	SubShader{
		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent"}

		 Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		CGPROGRAM
		#pragma surface surf Lambert vertex:vert nofog nolightmap nodynlightmap keepalpha noinstancing
		#pragma multi_compile_local _ PIXELSNAP_ON
		#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
		#include "UnitySprites.cginc"
					#include "UnityCG.cginc"
			// Add helper file
			#include "UtilsCG.cginc" 

		struct Input
		{
			float2 uv_MainTex;
			fixed4 color;
		};

		void vert(inout appdata_full v, out Input o)
		{
			v.vertex = UnityFlipSprite(v.vertex, _Flip);

			#if defined(PIXELSNAP_ON)
			v.vertex = UnityPixelSnap(v.vertex);
			#endif

			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.color = v.color * _Color * _RendererColor;
		}
		// Add identifiers
		float2 _DoodleMaxOffset;  // - How far the UV can be distorted
		float _DoodleFrameTime;   // - How long does a frame last
		int _DoodleFrameCount;    // - How many frames per animation
		float2 _DoodleNoiseScale; // - How noisy should the effect be

		void surf(Input IN, inout SurfaceOutput o)
		{
			float2 offset = 0.0;
			offset = DoodleTextureOffset(IN.uv_MainTex, _DoodleMaxOffset, _Time.y, _DoodleFrameTime, _DoodleFrameCount, _DoodleNoiseScale);

			fixed4 c = SampleSpriteTexture(IN.uv_MainTex + offset) * IN.color;
			o.Albedo = c.rgb * c.a;
			o.Alpha = c.a;
		}
		ENDCG
		

	}

Fallback "Transparent/VertexLit"
}