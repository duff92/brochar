// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/NeonShader"
{
	Properties
	{
		_Color("Color", Color) = (1, 1, 1, 1)
		_Centre("Centre", Vector) = (1.0, 0, 0)
		_Radius("Radius", Float) = 0.5
		//_Shininess("Shininess", Float) = 0
		//_EmissionMap("Emission Map", 2D) = "black"{}
		//[HDR] _EmissionColor("Emission Color", Color) = (0,0,0,1)

	}
		SubShader
	{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		LOD 300
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off

		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#define STEPS 16
#define STEP_SIZE 0.01
#define MIN_DISTANCE 0.02


#include "UnityCG.cginc"
#include "Lighting.cginc"

		float3 _Centre;
	float _Radius;
	fixed4 _Color;
	//float _Shininess;
	//sampler2D _EmissionMap;
	//fixed4 _EmissionColor;

	float3 viewDirection;

	struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
	};

	struct v2f
	{
		float3 wPos : TEXCOORD0; // World position
		float4 pos : SV_POSITION; // Clip space
	};

	float sdf_sphere(float3 p, float3 c, float r) {
		return distance(p, c) - r;
	}
	float map(float3 p, float radius) {
		return sdf_sphere(p, +_Centre, radius); // Left sphere		
	}

	float3 normal(float3 p)
	{
		const float eps = 0.01;

		return normalize
		(float3
			(map(p + float3(eps, 0, 0), _Radius) - map(p - float3(eps, 0, 0), _Radius),
				map(p + float3(0, eps, 0), _Radius) - map(p - float3(0, eps, 0), _Radius),
				map(p + float3(0, 0, eps), _Radius) - map(p - float3(0, 0, eps), _Radius)
				)
		);
	}

	fixed4 simpleLambert(fixed3 normal) {
		fixed3 lightDir = _WorldSpaceLightPos0.xyz; // Light direction
		fixed3 lightCol = _LightColor0.rgb; // Light color

		fixed NdotL = max(dot(normal, lightDir), 0);
		fixed4 c;
		c.rgb = _Color * lightCol * NdotL;
		c.a = 1;
		return c;
	}

	fixed4 neonLight(fixed3 normal, float3 vDir) {

		fixed NdotL = max(dot(normal, -1 * vDir), 0);
		fixed4 c;

		if (NdotL < 0.2) {
			c.rgb = _Color;
			c.rgb = _Color * (1 + NdotL);
			c.a = 1;




		}
		else {
			c.rgb = _Color;
			c.a = 1;
		}

		return c;

	}



	fixed4 renderSurface(float3 pos, float alpha, float3 vDir) {
		float3 n = normal(pos); // Normal to the surface


	    return neonLight(n, vDir);
	
	}

	fixed4 rayMarch(float3 position, float3 direction) {


		for (int i = 0; i < STEPS; i++) {
			float distance = map(position, _Radius);
			if (distance < MIN_DISTANCE) {
				return renderSurface(position, 1.0, direction);
			}

			position += distance * direction;
		}

		return fixed4(1, 1, 1, 0);

	}

	// Vertex Shader
	v2f vert(appdata v)
	{
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		o.wPos = mul(unity_ObjectToWorld, v.vertex).xyz;

		// Set center of spheres

		return o;
	}

	// Fragment (pixel) Shader
	fixed4 frag(v2f i) : SV_Target
	{

		float3 worldPosition = i.wPos;
		viewDirection = normalize(i.wPos - _WorldSpaceCameraPos);

		return rayMarch(worldPosition, viewDirection);
	}
		ENDCG
	}
	}
}
