// Upgrade NOTE: replaced '_LightMatrix0' with 'unity_WorldToLight'

// Upgrade NOTE: replaced '_LightMatrix0' with 'unity_WorldToLight'

// Upgrade NOTE: replaced '_LightMatrix0' with 'unity_WorldToLight'

// Upgrade NOTE: replaced '_LightMatrix0' with 'unity_WorldToLight'

Shader "Custom/iceShader3"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_ScatterTex ("Scatter Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			sampler2D _CameraDepthTexture;
			float4x4 unity_WorldToLight;
			struct a2v
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float2 dist : TEXCOORD0;
				float4 hpos : SV_POSITION;
			};



			v2f vert (a2v IN, 
					uniform float4x4 modelView,
					uniform float grow
			)
			{
				v2f OUT;
				float4 P = IN.vertex;
				P.xyz += IN.normal * grow; // scale vertex along normal
				OUT.hpos = mul(UNITY_MATRIX_MVP, P);
				OUT.dist = IN.uv;
				return OUT;
			}
			
			sampler2D _MainTex;
			sampler2D _ScatterTex;

			float trace(float3 P
				//uniform float4x4  lightTexMatrix, // to light texture space
				//uniform sampler2D lightDepthTex
				)
			{
				// transform point into light texture space

				float4 texCoord = mul(unity_WorldToLight, float4(P, 1.0));

				// get distance from light at entry point

				float d_i = tex2D(_CameraDepthTexture, texCoord.xyw);

				// transform position to light space

				float4 Plight = mul(unity_WorldToLight, float4(P, 1.0));

				// distance of this pixel from light (exit)

				float d_o = length(Plight);

				// calculate depth

				float s = d_o; //- d_i;
				return s;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				//get the depth
				float si = trace(i.hpos);// , lightTexMatrix, lightDepthTex);
				//fixed4 col = tex2D(_MainTex, i.hpos);
				// just invert the colors
				//col = 1 - col;
				return tex2D(_ScatterTex, float2(0, si));
			}
			ENDCG
		}
	}
}
