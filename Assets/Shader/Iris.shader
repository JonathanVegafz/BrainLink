﻿Shader "Jona/Iris"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_tex("tex", 2D) = "white" {}
		_param ("Param.", Float) = 0.5
		_aspect ("Camara Aspect", Float) = 1
		_offset ("Offset", Vector) = (0,0,0,0)
		_lit ("Lit", Float) = 1
	}

	SubShader{
		pass {
			CGPROGRAM
				#pragma vertex vert_img
				#pragma fragment frag
				#include "UnityCG.cginc"

				uniform sampler2D _MainTex;
				uniform sampler2D _tex;
				uniform float _param;
				uniform float _aspect;
				uniform float _lit;
				uniform float4 _offset;
				

				float4 frag(v2f_img i) : COLOR{
					float4 color = tex2D(_MainTex, i.uv);

					i.uv.x *= _aspect;
					i.uv *= _param;
					i.uv.x -= _aspect * _param * 0.5 - 0.5;
					i.uv.y -= _param * 0.5 - 0.5;

					float4 color2 = tex2D(_tex, i.uv) * _lit;


					return lerp(color2, color, color2.r);
				}

			ENDCG
		}
	}

}