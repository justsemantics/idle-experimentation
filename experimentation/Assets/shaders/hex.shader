Shader "Custom/hex" {
	Properties{
		_Color1("Color", Color) = (0,1,0,1)
		_Color2("Color2", Color) = (1, 0, 0, 1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
	_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_offX("x offset", float) = 0.0
		_offY("y offset", float) = 0.0
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
// Upgrade NOTE: excluded shader from OpenGL ES 2.0 because it uses non-square matrices
#pragma exclude_renderers gles
		// Physically based Standard lighting model, and enable shadows on all light types
#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
#pragma target 3.0

	static const float PI = 3.14159265f;
	static const float deg60 = tan(PI / 3);

	sampler2D _MainTex;

	struct Input {
		float2 uv_MainTex;
	};

	half _Glossiness;
	half _Metallic;
	fixed4 _Color1;
	fixed4 _Color2;
	float _offX;
	float _offY;

		float a1(float x) {
			return -deg60 * x - 1;
		}
		float a2(float x) {
			return -deg60 * x;
		}
		float a3(float x) {
			return -deg60 * x + 1;
		}

		float b1(float x) {
			return deg60 * x + 1;
		}
		float b2(float x) {
			return deg60 * x;
		}
		float b3(float x) {
			return deg60 * x - 1;
		}
	void surf(Input IN, inout SurfaceOutputStandard o) {
		// Albedo comes from a texture tinted by color
		float2 uv = IN.uv_MainTex;

		uv.x = fmod(uv.x, 1);
		uv.y = fmod(uv.y, 1);

		uv.x -= 0.5;
		uv.y -= 0.5;

		float3 coords = { uv.x, uv.y, 1 };
		float angle = 0;

		if (coords.y > a2(coords.x) & coords.y > b2(coords.x)) {
			angle = 5 * PI / 3;
		}
		if (coords.y > 0 & coords.y < b1(coords.x) & coords.y < a2(coords.x)) {
			angle = 4 * PI / 3;
		}
		if (coords.y < 0 & coords.y > a1(coords.x) & coords.y > b2(coords.x)) {
			angle = PI;
		}
		if (coords.y < b2(coords.x) & coords.y < a2(coords.x)) {
			angle = 2 * PI / 3;
		}
		if (coords.y < 0 & coords.y > a2(coords.x) & coords.y > b3(coords.x)) {
			angle = PI / 3;
		}

		float3x2 rotationMatrix = {
			cos(angle), sin(angle),
			-sin(angle), cos(angle),
			0, 0
		};

		float2 newCoords = mul(coords, rotationMatrix);

		uv.x = newCoords.x + 0.5 + _offX;
		uv.y = newCoords.y + 0.5 + _offY;
		fixed4 c = tex2D(_MainTex, uv).r * _Color1 + tex2D(_MainTex, uv).b * _Color2;
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
