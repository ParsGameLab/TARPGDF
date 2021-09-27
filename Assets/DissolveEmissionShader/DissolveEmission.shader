Shader "DissolverShader/DissolveShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_NormalMap ("Normal Map", 2D) = "bump" {}
		_NormalStrenght ("Normal Strength", Range(0, 1.5)) = 0.5
		_DissolveMap ("Dissolve Map", 2D) = "white" {}
		_DissolveAmount ("DissolveAmount", Range(0,1)) = 0
		_DissolveColor ("DissolveColor", Color) = (1,1,1,1)
		_DissolveEmission ("DissolveEmission", Range(0,1)) = 1
		_DissolveWidth ("DissolveWidth", Range(0,0.1)) = 0.05
		_Glossiness ("Smoothness", Range(0,1)) = 0.5

	    _MetallicGlossMap("Metallic", 2D) = "white" {}
		_Metallic("Metallic", Range(0,1)) = 0.0

		_OcclusionMap("Occlusion", 2D) = "white" {}
	    _OcclusionStrength("Strength", Range(0.0, 1.0)) = 1.0
		
	}

	
	

	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200	
		Cull Off
		CGPROGRAM
		#pragma surface surf Lambert addshadow	
		#pragma target 3.0
		#pragma shader_feature _METALLICGLOSSMAP
		
		
	    

		sampler2D _MainTex;
		sampler2D _NormalMap;
		sampler2D _DissolveMap;
		sampler2D _MetallicGlossMap;
		sampler2D _OcclusionMap;
		

		struct Input {
			float2 uv_MainTex;
			float2 uv_NormalMap;
			float2 uv_DissolveMap;
			
		};

		half _DissolveAmount;
		half _NormalStrenght;
		half _Glossiness;		
		half _Metallic;
		half _Occlusion;
		half _DissolveEmission;
		half _DissolveWidth;
		fixed4 _Color;
		fixed4 _DissolveColor;		
		

		void surf (Input IN, inout SurfaceOutput o) {

			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;			
			fixed4 mask = tex2D (_DissolveMap, IN.uv_DissolveMap);
			fixed4 cSpec = tex2D (_MetallicGlossMap, IN.uv_MainTex);
			fixed4 cOccl = tex2D(_OcclusionMap, IN.uv_MainTex);

			if(mask.r < _DissolveAmount)
				discard;

			o.Albedo = c.rgb;
			
			if(mask.r < _DissolveAmount + _DissolveWidth) {
				o.Albedo = _DissolveColor;			
				o.Emission = _DissolveColor * _DissolveEmission;				
			}
			
			
			//o.Metallic = cSpec.rgb;			
			//o.Smoothness = _Glossiness * cSpec.a;
			//o.Occlusion = cOccl.rgb;
			o.Alpha = c.a;
			o.Normal = UnpackScaleNormal(tex2D(_NormalMap, IN.uv_NormalMap), _NormalStrenght);
		}
		ENDCG
	}
	FallBack "Diffuse"    
}
