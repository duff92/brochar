// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/JulKuleShader"
{
	Properties
	{
		_Color("Color", Color) = (1, 1, 1, 1)
		_Centre("Centre", Vector) = (1.0, 0, 0)
		_Radius("Radius", Float) = 0.5
		_SpecularLightColor("Specular Color", Color) = (1, 1, 1, 1)
		_Specular("Specular Intensity", Float) = 1.0
	    _Cube ("Cube Map", CUBE) = "" {}



	}
		SubShader
	{
		Tags{ "Queue" = "overlay+4" "RenderType" = "overlay" "Lightmode" = "ForwardBase" }
		LOD 200
		Blend SrcAlpha OneMinusSrcAlpha


		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile PORTRAIT_ON PORTRAIT_OFF

#define STEPS 64
#define STEP_SIZE 0.01
#define MIN_DISTANCE 0.02


#include "UnityCG.cginc"
#include "Lighting.cginc"

		float3 _Centre;
	float _Radius;
	fixed4 _Color;
	float3 viewDirection;
	fixed4 _SpecularLightColor;
	float _Specular;
	float _Atten;
	samplerCUBE _Cube;


	struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
		float3 normal : NORMAL;
	};

	struct v2f
	{
		float4 pos : SV_POSITION; // Clip space
		float3 wPos : TEXCOORD0; // World position
		float3 nDir : TEXCOORD1;
		float2 screenpos: TEXCOORD2; // Screen position

	};

	float sdf_sphere(float3 p, float3 c, float r) {
		return distance(p, c) - r;
	}
	float map(float3 p) {
		return sdf_sphere(p, _Centre, _Radius); // Left sphere
	}

	float3 normal(float3 p)
	{
		const float eps = 0.01;

		return normalize
		(float3
			(map(p + float3(eps, 0, 0)) - map(p - float3(eps, 0, 0)),
				map(p + float3(0, eps, 0)) - map(p - float3(0, eps, 0)),
				map(p + float3(0, 0, eps)) - map(p - float3(0, 0, eps))
				)
		);
	}

	float4 blinnPhong(float3 normal, float2 scp) {


		float3 am = _Color * 0.05; // Ambient color
		
		float3 lDir = _WorldSpaceLightPos0.xyz;
		float3 dif = _Color * (saturate(dot(lDir, normal))); // Diffuse color

		float3 rDir = reflect(lDir, normal); // Reflection direction
		
		//float4 texCoord = texCUBE(_Cube, normal); // Cubemap
	


		float3 hw = normalize(lDir +  viewDirection);
		float spec = pow(saturate(dot(normal, hw)), 12.0);

		float3 sp = _Specular * spec; // Specular color

		// texCoord +
		return float4(am + dif + sp, 1.0f);
		

	}


	fixed4 renderSurface(float3 pos, float alpha, float3 normal, float2 screenPos) {

		return blinnPhong(normal, screenPos);
	}


	// Vertex Shader
	v2f vert(appdata v)
	{

		
		float4x4 modelMatrixInverse = unity_WorldToObject;

		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		o.wPos = mul(unity_ObjectToWorld, v.vertex).xyz;
		o.nDir = normalize(mul(float4(v.normal, 0.0), unity_WorldToObject).xyz);
		o.screenpos = ComputeScreenPos(o.pos);

		return o;
	}

	// Fragment (pixel) Shader
	fixed4 frag(v2f i) : SV_Target
	{

		float3 worldPosition = i.wPos;
		float2 screenPos = i.screenpos;
		viewDirection = normalize(i.wPos - _WorldSpaceCameraPos);

		return renderSurface(i.wPos, 1.0, normalize(i.nDir), screenPos);
	}
		ENDCG
	}
	}
}
