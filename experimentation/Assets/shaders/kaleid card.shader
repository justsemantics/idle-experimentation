Shader "Custom/kaleid card" {
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
		// Physically based Standard lighting model, and enable shadows on all light types
#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
#pragma target 3.0

		static const float PI = 3.14159265f;

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

	void surf(Input IN, inout SurfaceOutputStandard o) {
		// Albedo comes from a texture tinted by color
		float2 uv = IN.uv_MainTex;

		uv.x = fmod(uv.x, 1);
		uv.y = fmod(uv.y, 1);

		uv.x -= 0.5;
		uv.y -= 0.5;

		float x, y;


		if (uv.y < uv.x && uv.y > -uv.x) {
			x = uv.x;
			y = uv.y;
		}

		else if (uv.y < uv.x && uv.y < -uv.x) {
			x = -uv.y;
			y = uv.x;
		}

		else if (uv.y > uv.x && uv.y < -uv.x) {
			x = -uv.x;
			y = -uv.y;
		}

		else if (uv.y > uv.x && uv.y > -uv.x) {
			x = uv.y;
			y = -uv.x;
		}

		uv.x = x + 0.5 + _offX;
		uv.y = y + 0.5 + _offY;

		/*float radius = uv.x;
		float angle = uv.y * 2 * PI;

		uv.x = (radius * sin(angle)) / 2 + 0.5;
		uv.y = radius * cos(angle) / 2 + 0.5;*/
		fixed4 c = tex2D(_MainTex, uv);
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
