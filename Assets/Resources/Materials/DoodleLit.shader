Shader "Custom/DoodleLit"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

			// Add properties
_DoodleMaxOffset("Doodle Max Offset", vector) = (0.005, 0.005, 0, 0)
_DoodleFrameTime("Doodle Frame Time", Float) = 0.2
_DoodleFrameCount("Doodle Frame Count", Int) = 24
_DoodleNoiseScale("Doodle Noise Scale", vector) = (35, 35, 1, 1)
    }
    SubShader
    {
 Tags{ "RenderType" = "Transparent" "Queue" = "Transparent"}
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows
			
				#include "UnityCG.cginc"
			// Add helper file
			#include "UtilsCG.cginc" 
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
		

			struct appdata {
			float4 vertex : POSITION;
			float4 color    : COLOR;
			float2 uv : TEXCOORD0;
			//half metallic : METALIC;
			//float4 albedo: ALBEDO;
	     	};


        struct Input
        {
            float2 uv_MainTex;
			float4 color    : COLOR;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

		// Add identifiers
		float2 _DoodleMaxOffset;  // - How far the UV can be distorted
		float _DoodleFrameTime;   // - How long does a frame last
		int _DoodleFrameCount;    // - How many frames per animation
		float2 _DoodleNoiseScale; // - How noisy should the effect be

        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			float2 offset = 0.0;
			offset = DoodleTextureOffset(IN.uv_MainTex, _DoodleMaxOffset, _Time.y, _DoodleFrameTime, _DoodleFrameCount, _DoodleNoiseScale);

            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex + offset) * _Color;
			c *= IN.color;
            o.Albedo = c;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha *= c.a;
			
        }
        ENDCG
    }
    FallBack "Diffuse"
}
