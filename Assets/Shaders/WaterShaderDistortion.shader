Shader "SetSail/WaterShaderDistortion" 
{
	Properties
	{
		//_NAME_OF_VARIABLE("UNTIY_WINDOW_CONTROLS_NAME",VARIABLE_TYPE) = DEFAULT_VALUE
		_Intensity("Intensity", Range(0, 0.1)) = 0
		_Tint("Tint", Color) = (0.5,0.5,1,0.3)
		_DistortionTexture ("Distortion Texture", 2D) = "white" {}

	}


	SubShader{
		GrabPass{ "_GrabTexture" }
		Tags {"Queue"="Transparent" "RenderType"="Transparent" }
		Blend One Zero
		Lighting Off
		Fog { Mode Off }
		ZWrite Off
		LOD 200
		Cull [_CullMode]
		Pass{
 
			CGPROGRAM

			#pragma vertex vertFunction
			#pragma fragment fragFunction
			#include "UnityCG.cginc"



			struct vertexData {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 grabPos : TEXCOORD1;
			};



			sampler2D _GrabTexture;
			float _Intensity;
			float4 _Tint;

			sampler2D _DistortionTexture;
			float4 _DistortionTexture_ST; // Needed for TRANSFORM_TEX(____, _DistortionTexture)

			float2 v_diffuseUV;
			float2 v_diffuseUVS;

			v2f vertFunction(vertexData v) {
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.grabPos = ComputeGrabScreenPos(o.pos);
            	o.uv = v.uv; 
				return o;
			}

			float4 fragFunction(v2f o) : COLOR{				
				v_diffuseUV = o.uv*50 +(_Time) * _Intensity;
		 		v_diffuseUVS = o.uv*50 - (_Time*2.1)  * _Intensity;

				float4 diffuse = tex2D(_DistortionTexture, v_diffuseUV);
				float4 diffuseS = tex2D(_DistortionTexture, v_diffuseUVS);
	    		diffuse = lerp(diffuse, diffuseS, 0.5);


				o.grabPos +=diffuse;

				float4 projCoord = UNITY_PROJ_COORD(o.grabPos);
				float4 color = tex2Dproj(_GrabTexture, projCoord);

				if((diffuse.r >= 0.4 && diffuse.r <= 1)){
		            diffuse.rgb = 1;
		        }else{
		            //_Tint
		            diffuse.rgb =  _Tint.rgb;
		        }
				color = lerp(color, diffuse,0.3);
				return color * _Tint;
			}

			ENDCG
		}
	}
FallBack "Diffuse"

}
