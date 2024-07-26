Shader "Toon/Basic/TwoSteps" {
	Properties {
		_Color ("Color", Vector) = (1,1,1,1)
		_HColor ("Highlight Color", Vector) = (0.8,0.8,0.8,1)
		_SColor ("Shadow Color", Vector) = (0.2,0.2,0.2,1)
		_MainTex ("Main Texture", 2D) = "white" {}
		_RampThreshold ("Ramp Threshold", Range(0.1, 1)) = 0.5
		_RampSmooth ("Ramp Smooth", Range(0, 1)) = 0.1
		_SpecColor ("Specular Color", Vector) = (0.5,0.5,0.5,1)
		_SpecSmooth ("Specular Smooth", Range(0, 1)) = 0.1
		_Shininess ("Shininess", Range(0.001, 10)) = 0.2
		_RimColor ("Rim Color", Vector) = (0.8,0.8,0.8,0.6)
		_RimThreshold ("Rim Threshold", Range(0, 1)) = 0.5
		_RimSmooth ("Rim Smooth", Range(0, 1)) = 0.1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Diffuse"
}