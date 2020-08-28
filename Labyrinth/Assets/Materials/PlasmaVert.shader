Shader "Custom/PlasmaVert"
{
	Properties
	{
	  _Tint("Colour Tint", Color) = (1,1,1,1)
      _Speed("Speed", Range(1,100)) = 10
      _Scale1("Scale 1", Range(0.1,10)) = 2
      _Scale2("Scale 2", Range(0.1,10)) = 2
      _Scale3("Scale 3", Range(0.1,10)) = 2
      _Scale4("Scale 4", Range(0.1,10)) = 2
	}
	SubShader
	{
		Pass
		{
			CGPROGRAM		
			#pragma vertex vert
			#pragma fragment frag
		
			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 color: COLOR;
			};

			float4 _Tint;
			float _Speed;
			float _Scale1;
			float _Scale2;
			float _Scale3;
			float _Scale4;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				const float PI = 3.14159265;
				float t = _Time.x * _Speed;
          
				//vertical
				float c = sin(v.vertex.x * _Scale1 + t);

				//horizontal
				c += sin(v.vertex.z * _Scale2 + t);

				//diagonal
				c += sin(_Scale3*(v.vertex.x*sin(t/2.0) + v.vertex.z*cos(t/3))+t);

				//circular
				float c1 = pow(v.vertex.x + 0.5 * sin(t/5),2);
				float c2 = pow(v.vertex.z + 0.5 * cos(t/3),2);
				c += sin(sqrt(_Scale4*(c1 + c2)+1+t));
			
				o.color.r = sin(c/4.0*PI);
				o.color.g = sin(c/4.0*PI + 2*PI/4);
				o.color.b = sin(c/4.0*PI + 4*PI/4);
			
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{			
				fixed4 col = i.color * _Tint;
				return col;
			}
			ENDCG		
		}		
	}
}