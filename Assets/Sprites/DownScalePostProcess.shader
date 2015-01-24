Shader "Custom/DownScalePostProcess" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_RenderTex("Render tex", 2D) = "white" {}
		_Offset("Offset", Float) = 0
	}
	CGINCLUDE
	#include "UnityCG.cginc"
	struct v2f {
		float4 pos : SV_POSITION;
		half2 uv : TEXCOORD0;
	};
	sampler2D _MainTex;
	sampler2D _RenderTex;
	float _Offset;

	v2f vert( appdata_img v ) {
		v2f o; 
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		o.uv = v.texcoord;
		return o;
	}
	half4 frag(v2f i) : SV_Target {
		half4 color = tex2D(_RenderTex, i.uv * float2(1, _ScreenParams.y / _ScreenParams.x));
		return color;
	}
	ENDCG
	SubShader {
		 Pass {
			  ZTest Always Cull Off ZWrite Off
			  Fog { Mode off }      

			  CGPROGRAM
			  #pragma fragmentoption ARB_precision_hint_fastest
			  #pragma vertex vert
			  #pragma fragment frag
			  ENDCG
		  }
	}
	Fallback off
}