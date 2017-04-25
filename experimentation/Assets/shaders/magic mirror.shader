Shader "Custom/magic mirror" {
	Properties {
		
	}
	SubShader {
		Tags{ "Queue" = "Geometry-1" }  // Write to the stencil buffer before drawing any geometry to the screen
		ColorMask 0 // Don't write to any colour channels
		ZWrite Off // Don't write to the Depth buffer
		LOD 200

		// Write the value 1 to the stencil buffer
		Stencil
	{
		Ref 1
		Comp Always
		Pass Replace
	}
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};



		void surf (Input IN, inout SurfaceOutputStandard o) {
			o.Alpha = 0;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
