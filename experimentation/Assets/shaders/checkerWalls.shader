Shader "Custom/checkerWalls" {
	Properties {
		_Color1 ("Color1", Color) = (0,0,0.7,1)
		_Color2 ("Color2", Color) = (0, 0, 0, 1)
		_Color3 ("Color3", Color) = (0.6, 0, 0, 1)
		_Color4 ("Color4", Color) = (0.8, 0.5, 0, 1)
		_MainTex ("_MainTex", 2D) = "white" {}
		_SubTex("_SubTex", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _SubTex;

		struct Input {
			float2 uv_MainTex;
			float2 uv_SubTex;
			float3 viewDir;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color1;
		fixed4 _Color2;
		fixed4 _Color3;
		fixed4 _Color4;


		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			half mix1 = tex2D(_MainTex, IN.uv_MainTex).r;
			half mix2 = tex2D(_SubTex, IN.uv_SubTex).r;
			half mix3 = saturate(dot(normalize(IN.viewDir), o.Normal));

			fixed4 c1 = _Color1 * mix1 + _Color2 * (1 - mix1);
			fixed4 c2 = _Color3 * mix2 + _Color4 * (1 - mix2);

			fixed4 c = c1 * mix3 + c2 * (1 - mix3);

			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
