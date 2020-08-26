Shader "Custom/HologramTex" {
	Properties {		
		_MainTex("Main Texture", 2D) = "white" {}
		_RimColor ("Color", Color) = (1,1,1,1)
		_RimPower("Rim Power", Range(0.1,8)) = 2
	}
	SubShader {
			Tags{"Queue" = "Transparent"}
		
		Pass {
			Blend SrcAlpha OneMinusSrcAlpha
			Zwrite on
			ColorMask 0
		}
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert alpha:fade
		
		struct Input {
			float3 viewDir;
			float2 uv_MainTex;
		};
		
		sampler2D _MainTex;
		fixed4 _RimColor;
		half _RimPower;

		void surf (Input IN, inout SurfaceOutput o) {
			half rim = saturate(dot(normalize(IN.viewDir), o.Normal));
			o.Emission = _RimColor.rgb * pow(rim, _RimPower) * 10;
			o.Alpha = pow(rim, _RimPower);
			
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			half4 output_col = c;
			half3 transparent_diff = c.xyz - fixed4(0,0,0,1).xyz;
			half transparent_diff_squared = dot(transparent_diff,transparent_diff);
			if(transparent_diff_squared < 0.1)
                 discard;
			o.Albedo = output_col.rgb;

		}
		ENDCG
	}
	FallBack "Diffuse"
}
