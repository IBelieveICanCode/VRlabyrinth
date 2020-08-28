Shader "Custom/Hole" {
	Properties {
		_RimColor ("Color", Color) = (1,1,1,1)
		_RimPower("Rim Power", Range(0.5,8)) = 2
		//_MainTex ("Diffuse", 2D) = "white" {}
	}
	SubShader {
		Tags { "Queue"="Geometry-1" }

		ColorMask 0
		ZWrite off
		Stencil
		{
			Ref 1
			Comp always
			Pass replace
		}
		
		CGPROGRAM
		#pragma surface surf Lambert alpha:fade


		//sampler2D _MainTex;

		struct Input {
			float3 viewDir;
		};

		fixed4 _RimColor;
		half _RimPower;

		void surf (Input IN, inout SurfaceOutput o) {
			half rim = 1 - saturate(dot(normalize(IN.viewDir), o.Normal));
			o.Emission = _RimColor.rgb * pow(rim, _RimPower) * 10;
			o.Alpha = pow(rim, _RimPower);
		}
		ENDCG
	}
	FallBack "Diffuse"
}
