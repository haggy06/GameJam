//RealToon V5.0.5
//MJQStudioWorks
//2019

Shader "RealToon/Version 5/Lite/Fade Transparency Multi" {
    Properties {

		[Enum(Off,2,On,0)] _DoubleSided("Double Sided", int) = 2

        _MainTex ("Texture", 2D) = "white" {}
        _OverrideTex1("Texture", 2D) = "black" {}
        _OverrideTex2("Texture", 2D) = "black" {}
        _OverrideTex3("Texture", 2D) = "black" {}
        [HDR] _MainColor ("Main Color", Color) = (0.7843137,0.7843137,0.7843137,1)
     
		[Toggle] _MVCOL ("Mix Vertex Color", Float ) = 0 

		[Toggle] _MCIALO ("Main Color In Ambient Light Only", Float ) = 0


	    [Toggle] _L_SLO("Scenes Light Only", Float) = 0
	    [Toggle] _L_UCL("Use Custome Light", Float) = 0
        [HDR] _CustomColor ("Custome Light Color", Color) = (1,1,1,1)
		[HDR] _HighlightColor ("Highlight Color", Color) = (1,1,1,1)
        _HighlightColorPower ("Highlight Color Power", Float ) = 1

    	[Toggle] _HighlightOverrideUse("Highlight Override Use", Float) = 0
    	
    	
        _HighlightOverrideTex ("Texture", 2D) = "white" {}
        _HighlightOverrideColorPower ("Highlight Override Color Power", Float ) = 0
        _Opacity ("Opacity", Range(0, 1)) = 1
        [Toggle] _AffectShadow ("Affect Shadow", Float ) = 1
		_TransparentThreshold ("Transparent Threshold", Float ) = 0
		_MaskTransparency1("Mask Transparency 1", 2D) = "white" {}
		_MaskTransparency2("Mask Transparency 2", 2D) = "white" {}
		_MaskTransparency3("Mask Transparency 3", 2D) = "white" {}
      //  _MaskTransparency4("Mask Transparency 4", 2D) = "white" {}
	

        _OutlineWidth ("Width", Float ) = 0.5
        _OutlineWidthControl ("Width Control", 2D) = "white" {}

        [Enum(Normal,0,Origin,1)] _OutlineExtrudeMethod("Outline Extrude Method", int) = 0

        _ReduceOutlineBackFace ("Reduce Outline Backface", Range(0, 1)) = 0

        _OutlineOffset ("Outline Offset", Vector) = (0,0,0)
        _OutlineZPostionInCamera ("Outline Z Position In Camera", Float) = 0

        [Enum(Off,1,On,0)] _DoubleSidedOutline("Double Sided Outline", int) = 1

        [HDR] _OutlineColor ("Color", Color) = (0,0,0,1)

        [Toggle] _MixMainTexToOutline ("Mix Main Texture To Outline", Float ) = 0

        _NoisyOutlineIntensity ("Noisy Outline Intensity", Range(0, 1)) = 0
        [Toggle] _DynamicNoisyOutline ("Dynamic Noisy Outline", Float ) = 0
        [Toggle] _LightAffectOutlineColor ("Light Affect Outline Color", Float ) = 0

        [Toggle] _OutlineWidthAffectedByViewDistance ("Outline Width Affected By View Distance", Float ) = 0
        _FarDistanceMaxWidth ("Far Distance Max Width", Float ) = 1

        [Toggle] _VertexColorRedAffectOutlineWitdh ("Vertex Color Red Affect Outline Witdh", Float ) = 0



        _Glossiness ("Glossiness", Range(0, 1)) = 0.6
        _GlossSoftness ("Softness", Range(0, 1)) = 0
        [HDR] _GlossColor ("Color", Color) = (1,1,1,1)
        _GlossColorPower ("Color Power", Float ) = 10
        _MaskGloss ("Mask Gloss", 2D) = "white" {}

        


		_DirectionalLightIntensity ("Directional Light Intensity", Float ) = 0
		_PointSpotlightIntensity ("Point and Spot Light Intensity", Float ) = 0.1
		_LightFalloffSoftness ("Light Falloff Softness", Range(0, 1)) = 1

   

       

        _RimLightUnfill ("Unfill", Float ) = 1.5
        _RimLightSoftness ("Softness", Range(0, 1)) = 1
        [HDR] _RimLightColor ("Color", Color) = (1,1,1,1)
        _RimLightColorPower ("Color Power", Float ) = 10
        [Toggle] _LightAffectRimLightColor ("Light Affect Rim Light Color", Float ) = 0
        [Toggle] _RimLightInLight ("Rim Light In Light", Float ) = 1
        [Toggle] _RimLightXHold ("Rim Light PosX Hold", Float ) = 0
		_RefVal ("ID", int ) = 0
        [Enum(None,8,A,0,B,2)] _Oper("Set 1", int) = 8
        [Enum(None,8,A,6,B,7)] _Compa("Set 2", int) = 8



        [Toggle] _L_F_O ("Outline", Float ) = 0

		[Toggle] _L_F_GLO ("Gloss", Float ) = 0
		
		
		
		
		
		
		[Toggle] _L_F_UOAL ("Use Old Ambient Light", Float ) = 0
		
		
		[Toggle] _L_F_RL ("Rim Light", Float ) = 0
		[Enum(On,1,Off,0)] _ZWrite("ZWrite", int) = 0

    }

    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
   
         Pass {
            Name "Outline"
            Tags {
                    "LightMode" = "Always"
            }
            
            Cull Off

            Stencil {
                Ref[_RefVal]
                Comp [_Compa]
                Pass [_Oper]
                Fail [_Oper]
            }
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
           // #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog

            //#pragma multi_compile_instancing

            #pragma only_renderers d3d9 d3d11 vulkan glcore gles3 metal xboxone ps4 wiiu switch
            #pragma target 3.0

     
     uniform float _L_F_O;
                uniform float4 _LightColor0;

                   uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
   
            uniform sampler2D _MaskTransparency1; uniform float4 _MaskTransparency1_ST;
            uniform sampler2D _MaskTransparency2; uniform float4 _MaskTransparency2_ST;
            uniform sampler2D _MaskTransparency3; uniform float4 _MaskTransparency3_ST;
           // uniform sampler2D _MaskTransparency4; uniform float4 _MaskTransparency4_ST;
                uniform half _Opacity;
                uniform fixed _TexturePatternStyle;

                uniform half _OutlineWidth;
                uniform sampler2D _OutlineWidthControl; uniform float4 _OutlineWidthControl_ST;
                uniform float3 _OEM;
                uniform int _OutlineExtrudeMethod;
                uniform half3 _OutlineOffset;
                uniform half _OutlineZPostionInCamera;
                uniform half4 _OutlineColor;
                uniform half _MixMainTexToOutline;
                uniform half _NoisyOutlineIntensity;
                uniform fixed _DynamicNoisyOutline;
                uniform fixed _LightAffectOutlineColor;
                uniform fixed _OutlineWidthAffectedByViewDistance;
                uniform half _FarDistanceMaxWidth;
                uniform fixed _VertexColorRedAffectOutlineWitdh;

                uniform half _ReduceOutlineBackFace;
                uniform half _TransparentThreshold;

       

            struct VertexInput 
            {

                float4 vertex : POSITION;

            

                    float3 normal : NORMAL;
                    float2 texcoord0 : TEXCOORD0;
                    float4 vertexColor : COLOR;
                    UNITY_VERTEX_INPUT_INSTANCE_ID

              

            };

            struct VertexOutput 
            {

                float4 pos : SV_POSITION;

              

                    float2 uv0 : TEXCOORD0;
                    float4 posWorld : TEXCOORD4;
                    float3 normalDir : TEXCOORD3;
                    float4 vertexColor : COLOR;
                    float4 projPos : TEXCOORD1;
                    UNITY_FOG_COORDS(2)
                
               

            };

            VertexOutput vert (VertexInput v) 
            {

                VertexOutput o = (VertexOutput)0;

                if(_L_F_O == 1){

                    UNITY_SETUP_INSTANCE_ID (v);

                    o.uv0 = v.texcoord0;
                    o.vertexColor = v.vertexColor;

                    o.normalDir = UnityObjectToWorldNormal(v.normal);
                    o.posWorld = mul(unity_ObjectToWorld, v.vertex);

                    float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                    half RTD_OB_VP_CAL = distance(objPos.rgb,_WorldSpaceCameraPos);

                    half RTD_OL_VCRAOW_OO = lerp( _OutlineWidth, (_OutlineWidth*(1.0 - o.vertexColor.r)), _VertexColorRedAffectOutlineWitdh );
                    half RTD_OL_OLWABVD_OO = lerp( RTD_OL_VCRAOW_OO, ( clamp(RTD_OL_VCRAOW_OO*RTD_OB_VP_CAL, RTD_OL_VCRAOW_OO, _FarDistanceMaxWidth) ), _OutlineWidthAffectedByViewDistance );
                    half4 _OutlineWidthControl_var = tex2Dlod(_OutlineWidthControl,float4(TRANSFORM_TEX(o.uv0, _OutlineWidthControl),0.0,0));

                    float4 node_8661 = _Time;
                    float node_4658_ang = node_8661.g;
                    float node_4658_spd = 0.002;
                    float node_4658_cos = cos(node_4658_spd*node_4658_ang);
                    float node_4658_sin = sin(node_4658_spd*node_4658_ang);
                    float2 node_4658_piv = float2(0.5,0.5);
                    half2 node_4658 = (mul(o.uv0-node_4658_piv,float2x2( node_4658_cos, -node_4658_sin, node_4658_sin, node_4658_cos))+node_4658_piv);

                    half2 RTD_OL_DNOL_OO = lerp( o.uv0, node_4658, _DynamicNoisyOutline );
                    half2 node_6616 = RTD_OL_DNOL_OO;
                    float2 node_9863_skew = node_6616 + 0.2127+node_6616.x*0.3713*node_6616.y;
                    float2 node_9863_rnd = 4.789*sin(489.123*(node_9863_skew));
                    half node_9863 = frac(node_9863_rnd.x*node_9863_rnd.y*(1+node_9863_skew.x));

                    _OEM = lerp(v.normal,normalize(v.vertex),_OutlineExtrudeMethod);

                    half RTD_OL = ( RTD_OL_OLWABVD_OO*0.01 )*_OutlineWidthControl_var.r*lerp(1.0,node_9863,_NoisyOutlineIntensity);
                    o.pos = UnityObjectToClipPos( float4((v.vertex.xyz + _OutlineOffset.xyz * 0.01) + _OEM * RTD_OL,1) );


                    #if defined(SHADER_API_GLCORE) || defined(SHADER_API_GLES) || defined(SHADER_API_GLES3)
                        o.pos.z = o.pos.z + _OutlineZPostionInCamera * 0.0005;
                    #else
                        o.pos.z = o.pos.z - _OutlineZPostionInCamera * 0.0005;
                    #endif
                
                    UNITY_TRANSFER_FOG(o,o.pos);
                    o.projPos = ComputeScreenPos (o.pos);
                    COMPUTE_EYEDEPTH(o.projPos.z);

                }

                return o;

            }

            float4 frag(VertexOutput i) : COLOR 
            {

            if(_L_F_O == 1){

                i.normalDir = normalize(i.normalDir);
                 float3 viewDirection;   
             
                viewDirection = normalize(float3(i.posWorld.x,_WorldSpaceCameraPos.y,_WorldSpaceCameraPos.z) - i.posWorld.xyz);
          
                float3 normalDirection = i.normalDir;

                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float3 lightColor = _LightColor0.rgb;

                half RTD_OB_VP_CAL = distance(objPos.rgb,_WorldSpaceCameraPos);
                half2 RTD_VD_Cal = (float2((sceneUVs.x * 2 - 1)*(_ScreenParams.r/_ScreenParams.g), sceneUVs.y * 2 - 1).rg*RTD_OB_VP_CAL);

                half2 RTD_TC_TP_OO = lerp( i.uv0, RTD_VD_Cal, _TexturePatternStyle );
                half2 node_4408 = RTD_TC_TP_OO;
              
                half4 _MainTex_var =
                    tex2D(_MainTex, TRANSFORM_TEX(node_4408, _MainTex));
               

                half4 _MaskTransparency_var1 = tex2D(_MaskTransparency1, TRANSFORM_TEX(i.uv0, _MaskTransparency1));
                half4 _MaskTransparency_var2 = tex2D(_MaskTransparency2, TRANSFORM_TEX(i.uv0, _MaskTransparency2));
                half4 _MaskTransparency_var3 = tex2D(_MaskTransparency3, TRANSFORM_TEX(i.uv0, _MaskTransparency3));
              //  half4 _MaskTransparency_var4 = tex2D(_MaskTransparency4, TRANSFORM_TEX(i.uv0, _MaskTransparency4));
                half _MaskTransparency_var = _MaskTransparency_var1.r * _MaskTransparency_var2.r*_MaskTransparency_var3.r;

                half RTD_TRAN_MAS = (smoothstep(clamp(-20,1,_TransparentThreshold),1,_MainTex_var.a) *_MaskTransparency_var);
                half RTD_TRAN_OPA_Sli = lerp( RTD_TRAN_MAS, smoothstep(clamp(-20,1,_TransparentThreshold) , 1, _MainTex_var.a)  ,_Opacity);
                half RTD_TRAN = saturate(( 0.74 > 0.5 ? (1.0-(1.0-2.0*(0.74-0.5))*(1.0-RTD_TRAN_OPA_Sli)) : (2.0*0.74*RTD_TRAN_OPA_Sli) ));
                clip(RTD_TRAN - 0.5);

                float oc = (1.0-dot(normalDirection, viewDirection));
                clip(smoothstep( lerp(4,1.7,_ReduceOutlineBackFace), 0, oc ) - 0.5);

                float node_3413 = 0.0;
                half3 RTD_OL_LAOC_OO = lerp( lerp(_OutlineColor.rgb,_OutlineColor.rgb * _MainTex_var, _MixMainTexToOutline) , lerp(float3(node_3413,node_3413,node_3413), lerp(_OutlineColor.rgb,_OutlineColor.rgb * _MainTex_var, _MixMainTexToOutline) ,_LightColor0.rgb), _LightAffectOutlineColor );

                fixed4 finalRGBA = fixed4(RTD_OL_LAOC_OO,0);
				finalRGBA.a = _OutlineColor.a;
				UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;

            }

                return 0;

            

            }

            ENDCG

        }

        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }

			Cull [_DoubleSided]
            Blend SrcAlpha OneMinusSrcAlpha, OneMinusDstAlpha One
            ZWrite [_ZWrite]
        
			Stencil {
            	Ref[_RefVal]
            	Comp [_Compa]
            	Pass [_Oper]
            	Fail [_Oper]
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase novertexlights
            #pragma multi_compile_fog
            
			//#pragma multi_compile_instancing

            #pragma only_renderers d3d9 d3d11 vulkan glcore gles3 gles metal xboxone ps4 wiiu switch
            #pragma target 3.0
            
	
	
 
		
			
	
	
			

	
	uniform float _L_F_RL;
	uniform float _L_F_UOAL;
    uniform float _L_F_GLO;

		            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
                    uniform sampler2D _OverrideTex1; uniform float4 _OverrideTex1_ST;
                    uniform sampler2D _OverrideTex2; uniform float4 _OverrideTex2_ST;
                    uniform sampler2D _OverrideTex3; uniform float4 _OverrideTex3_ST;
    
            uniform half4 _MainColor;
			uniform half _MVCOL;
			uniform fixed _MCIALO;

			uniform half _HighlightColorPower;
			uniform half4 _HighlightColor;

            uniform float _HighlightOverrideUse;
            uniform half _HighlightOverrideColorPower;
			uniform sampler2D _HighlightOverrideTex; uniform float4 _HighlightOverrideTex_ST;

	

            uniform half _Opacity;
			uniform fixed _AffectShadow;
			uniform half _TransparentThreshold;
			uniform sampler2D _MaskTransparency1; uniform float4 _MaskTransparency1_ST;
			uniform sampler2D _MaskTransparency2; uniform float4 _MaskTransparency2_ST;
			uniform sampler2D _MaskTransparency3; uniform float4 _MaskTransparency3_ST;
          //  uniform sampler2D _MaskTransparency4; uniform float4 _MaskTransparency4_ST;
            uniform float _L_SLO;
            uniform float _L_UCL;
		    uniform half4 _CustomColor;
        
		
				uniform half _Glossiness;
				uniform half _GlossSoftness;
				uniform half4 _GlossColor;
				uniform half _GlossColorPower;
				uniform sampler2D _MaskGloss; uniform float4 _MaskGloss_ST;
	

	

     

		         
			uniform half _DirectionalLightIntensity;
       


	

	
				uniform half _RimLightUnfill;
				uniform half _RimLightSoftness; 
				uniform fixed _LightAffectRimLightColor;
				uniform half4 _RimLightColor;
				uniform half _RimLightColorPower;
			    uniform fixed _RimLightInLight;
                uniform fixed _RimLightXHold;
	

            half3 AL_GI()
			{
				return ShadeSH9(float4(0,0,0,1));
            }
            
            struct VertexInput 
			{

                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
      
                float4 vertexColor : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID

            };

            struct VertexOutput 
			{

                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
         
        
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 vertexColor : COLOR;
                float4 projPos : TEXCOORD5;
                UNITY_FOG_COORDS(6)

            };

            VertexOutput vert (VertexInput v) 
			{

                VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID (v);

                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;

                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);

                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);

                o.pos = UnityObjectToClipPos( v.vertex );
                 
                   

                UNITY_TRANSFER_FOG(o,o.pos);

                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);

                return o;

            }

            float4 frag(VertexOutput i) : COLOR 
			{
				    if(_L_SLO==1)
                    _MCIALO = 0;

		
                float3 normalLocal = half3(0,0,1);
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
				float2 sceneUVs = (i.projPos.xy / i.projPos.w);

                i.normalDir = normalize(i.normalDir);
           
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
              //  float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                 float3 viewDirection;   
                   if(_L_F_RL == 1){
                if(_RimLightXHold == 1){
                viewDirection = normalize(float3(i.posWorld.x,_WorldSpaceCameraPos.y,_WorldSpaceCameraPos.z) - i.posWorld.xyz);
                }else{
                viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                }
                }
                else{
                   viewDirection = normalize(float3(i.posWorld.x,_WorldSpaceCameraPos.y,_WorldSpaceCameraPos.z) - i.posWorld.xyz);
                }
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform ));

				half RTL_OB_VP_CAL = distance(objPos.rgb,_WorldSpaceCameraPos);
				half2 RTL_VD_CAL = (float2((sceneUVs.x * 2 - 1)*(_ScreenParams.r/_ScreenParams.g), sceneUVs.y * 2 - 1).rg*RTL_OB_VP_CAL);

			

                half MCapOutP = 1; 
           
            
		
              //  half4 _MainTex_var2 = tex2D(_OverrideTex,TRANSFORM_TEX(i.uv0, _OverrideTex))-(mask2);
           //   half4 _MainTex_var = half4((tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex))-mask).rgb+_MainTex_var2.rgb,1);

                    half4 mainTexture = tex2D(_MainTex, i.uv0.xy);
                    half4 overlayTexture1 = tex2D(_OverrideTex1, i.uv0.xy);
                    half4 overlayTexture2 = tex2D(_OverrideTex2, i.uv0.xy);
                    half4 overlayTexture3 = tex2D(_OverrideTex3, i.uv0.xy);
                  //  half4 overlayTexture = overlayTexture3/ overlayTexture2 / overlayTexture1;
                      half4 _MainTex_var = mainTexture;

                      _MainTex_var.rgb = lerp(_MainTex_var, overlayTexture1, overlayTexture1.a).rgb;
                      _MainTex_var.rgb = lerp(_MainTex_var, overlayTexture2, overlayTexture2.a).rgb;
                      _MainTex_var.rgb = lerp(_MainTex_var, overlayTexture3, overlayTexture3.a).rgb;
				half3 _RTL_MVCOL = lerp(1, i.vertexColor, _MVCOL);
				half3 RTL_TEX_COL = (_MainTex_var.rgb*_MainColor.rgb*MCapOutP*_RTL_MVCOL);
				
				half4 _MaskTransparency_var1 = tex2D(_MaskTransparency1, TRANSFORM_TEX(i.uv0, _MaskTransparency1));
				half4 _MaskTransparency_var2 = tex2D(_MaskTransparency2, TRANSFORM_TEX(i.uv0, _MaskTransparency2));
				half4 _MaskTransparency_var3 = tex2D(_MaskTransparency3, TRANSFORM_TEX(i.uv0, _MaskTransparency3));
              //  half4 _MaskTransparency_var4 = tex2D(_MaskTransparency4, TRANSFORM_TEX(i.uv0, _MaskTransparency4));
				half _MaskTransparency_var = _MaskTransparency_var1.r * _MaskTransparency_var2.r*_MaskTransparency_var3.r;
                half node_829 = lerp(( smoothstep(clamp(-20,1,_TransparentThreshold) , 1, _MainTex_var.a) * _MaskTransparency_var), smoothstep(clamp(-20,1,_TransparentThreshold) , 1, _MainTex_var.a) ,_Opacity);
                half RTL_TRAN_AS_OO = lerp( 1.0, 0.74, _AffectShadow );
                half RTL_TRAN_OC = saturate( ( RTL_TRAN_AS_OO > 0.5 ? ( 1.0 - (1.0-2.0 * (RTL_TRAN_AS_OO-0.5) ) * ( 1.0 - ( node_829 * RTL_TRAN_AS_OO ) ) ) : ( 2.0 * RTL_TRAN_AS_OO * (node_829 * RTL_TRAN_AS_OO) ) )  );
                clip(RTL_TRAN_OC - 0.5);

				float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor;
                        if(_L_UCL == 1){
                        lightColor = _CustomColor.rgb;
                        }else{
                         lightColor = _LightColor0.rgb;
                        }
                //float3 lightColor = _LightColor0.rgb;
                //float3 lightColor = float3(1,1,1);

                float3 halfDirection = normalize(viewDirection+lightDirection);

                float attenuation = 1;

	

					half3 RTL_SON = normalDirection;
					half3 RTL_SON_CHE_1 = 1;

			
            half3 RTL_UAL = 0;
            half3 RTL_GI = 0;
            half3 RTL_UOAL = 0;
                if(_L_F_UOAL == 1){
                    
                    if(_L_SLO==0)
                    RTL_UAL = UNITY_LIGHTMODEL_AMBIENT.rgb;
                    RTL_UOAL = RTL_UAL;
                    }else{
                    if(_L_SLO==1)
                    RTL_GI = AL_GI();
                    RTL_UOAL = RTL_GI;
                    }

			

					half3 RTL_SCT = 1;

				

			

					half RTL_PT = 1;

			

				half3 RTL_OSC = 1;
                half3 RTL_HL;
                if(_L_UCL == 0){
                RTL_HL = (_HighlightColor.rgb*_HighlightColorPower+_DirectionalLightIntensity);
                	
                }else{
                		
                RTL_HL= 1;
                }
				//RTL_HL*= tex2D(_MainTex, i.uv0.xy).r*_HighlightOverrideColorPower;
				
				half RTL_LVLC = saturate( dot( lightColor.rgb , float3(0.3,0.59,0.11) ) );
				half3 RTL_MCIALO = lerp(RTL_TEX_COL , lerp(RTL_TEX_COL , _MainTex_var.rgb * MCapOutP * 0.7 , clamp((RTL_LVLC*1),0,1) ) , _MCIALO );
				
			     half3 RTL_GLO = RTL_HL;

		if(_L_F_GLO == 1){
						float RTL_GLO_MAIN_SOF_Sil = lerp(0.1,1.0,_GlossSoftness);
						half RTL_NDOTH = max(0,dot(halfDirection,normalDirection));
						half RTL_GLO_MAIN = smoothstep( 0.1, RTL_GLO_MAIN_SOF_Sil, pow(RTL_NDOTH,exp2(lerp(-2,15,_Glossiness))) );

						half3 RTL_GT = RTL_GLO_MAIN;

					

					half4 _MaskGloss_var = tex2D(_MaskGloss,TRANSFORM_TEX(i.uv0, _MaskGloss));

					half3 RTL_GLO_MAS = lerp(RTL_HL,lerp(RTL_HL,(_GlossColor.rgb*_GlossColorPower),RTL_GT),_MaskGloss_var.r);
					RTL_GLO = RTL_GLO_MAS;
                    }
			
					


		
        half3 RTL_RL_CHE_1;
				   half3 RTL_RL_MAIN;
                if(_L_F_RL == 1){
                    float node_4353 = 0.0;
					float node_3687 = 0.0;

					half3 RTL_RL_LARL_OO = lerp( _RimLightColor.rgb, lerp(float3(node_3687,node_3687,node_3687),_RimLightColor.rgb,lightColor.rgb), _LightAffectRimLightColor );
					half RTL_RL_S_Sli = lerp(1.71,0.29,_RimLightSoftness);
					RTL_RL_MAIN = lerp(float3(node_4353,node_4353,node_4353),(RTL_RL_LARL_OO*_RimLightColorPower),smoothstep( 1.71, RTL_RL_S_Sli, pow(1.0-max(0,dot(normalDirection, viewDirection)),(1.0 - _RimLightUnfill)) ));
					half3 RTL_RL_IL_OO = lerp(RTL_GLO,(RTL_GLO+RTL_RL_MAIN),_RimLightInLight);
					RTL_RL_CHE_1 = RTL_RL_IL_OO;
                    }else{
				

					RTL_RL_CHE_1 = RTL_GLO;
                    }
				

	

					half3 RTL_CLD = lightDirection;

		

				half3 RTL_ST_SS_AVD_OO = viewDirection;
				half RTL_NDOTL = 0.5*dot(RTL_ST_SS_AVD_OO,RTL_SON)+0.5;

		            
					half3 RTL_ST = 1;

			
            
					half RTL_SS = attenuation;

			
				
				half3 RTL_FR_OFF_OTHERS = (lerp( RTL_TEX_COL , _MainTex_var.rgb , _MCIALO) * lerp(((RTL_OSC*RTL_SCT*RTL_PT)*RTL_LVLC),(RTL_RL_CHE_1*RTL_ST*RTL_SON_CHE_1*lightColor.rgb),RTL_SS));

			            
					half3 RTL_FR = RTL_FR_OFF_OTHERS;

			
               
                    half3 RTL_SL = RTL_UOAL;
                    half3 RTL_SL_CHE_1 = RTL_FR;
                    half3 RTL_RL;
				if(_L_F_RL == 1){
					half3 RTL_RL_ON = lerp((RTL_SL_CHE_1+RTL_RL_MAIN),RTL_SL_CHE_1,_RimLightInLight);
					RTL_RL = RTL_RL_ON;
                    }else{
				

					RTL_RL = RTL_SL_CHE_1;
                    }
			

				float3 emissive = (RTL_MCIALO*RTL_SL);
						if(_HighlightOverrideUse == 1){
					float time = (_Time.y*0.5f)%2;
					if(time>1)
					{
						time = 2-time;
					}
			        half4 _HighlightOverrideTex_var = tex2D(_HighlightOverrideTex,TRANSFORM_TEX(i.uv0,_HighlightOverrideTex));
				    half3 RTL_GLO_MAS = lerp(emissive,_HighlightOverrideTex_var.rgb*_HighlightColor.rgb *(_HighlightOverrideColorPower*lerp(0,1,time)),_HighlightOverrideTex_var.r);
                //half3 result =	 _HighlightOverrideTex_var.rgb *_HighlightOverrideColorPower;
					emissive += RTL_GLO_MAS;
					}
				float3 finalColor = emissive + RTL_RL;

                half RTL_TRAN_O = node_829;

                fixed4 finalRGBA = fixed4(finalColor,RTL_TRAN_O);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;

            }

            ENDCG

        }

        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
			Cull [_DoubleSided]
            Blend One One
            ZWrite [_ZWrite]
			            
			Stencil {
            	Ref[_RefVal]
            	Comp [_Compa]
            	Pass [_Oper]
            	Fail [_Oper]
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            
            #pragma multi_compile_fwdadd novertexlights
            #pragma multi_compile_fog
			//#pragma multi_compile_instancing
            #pragma only_renderers d3d9 d3d11 vulkan glcore gles3 gles metal ps4 wiiu switch
			#pragma target 3.0
            
	
	

	
		
	
			
			
			
		
			
		   uniform float _L_F_RL;
    uniform float _L_F_UOAL;
    uniform float _L_F_GLO;
		

            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
          
  
            uniform half4 _MainColor;
			uniform half _MVCOL;
			uniform fixed _MCIALO;
			uniform half _HighlightColorPower;
			uniform half4 _HighlightColor;


                uniform half _HighlightOverrideColorPower;
			uniform sampler2D _HighlightOverrideTex; uniform float4 _HighlightOverrideTex_ST;

            uniform half _Opacity;
			uniform fixed _AffectShadow;
			uniform half _TransparentThreshold;
			uniform sampler2D _MaskTransparency1; uniform float4 _MaskTransparency1_ST;
			uniform sampler2D _MaskTransparency2; uniform float4 _MaskTransparency2_ST;
			uniform sampler2D _MaskTransparency3; uniform float4 _MaskTransparency3_ST;
         //   uniform sampler2D _MaskTransparency3; uniform float4 _MaskTransparency3_ST;
            uniform float _L_SLO;
                 uniform float _L_UCL;
        uniform half4 _CustomColor;
           
		
				uniform half _Glossiness;
				uniform half _GlossSoftness;
				uniform half4 _GlossColor;
				uniform half _GlossColorPower;
				uniform sampler2D _MaskGloss; uniform float4 _MaskGloss_ST;
         


	

	
			
	

		         
		

			uniform half _PointSpotlightIntensity;
			uniform half _LightFalloffSoftness;
       
	           
	

		
				uniform half _RimLightUnfill;
				uniform half _RimLightSoftness; 
				uniform fixed _LightAffectRimLightColor;
				uniform half4 _RimLightColor;
				uniform half _RimLightColorPower;
				uniform fixed _RimLightInLight;
                uniform fixed _RimLightXHold;
	

            half3 AL_GI()
			{
				return ShadeSH9(float4(0,0,0,1));
            }


            struct VertexInput
			{

                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID

            };

            struct VertexOutput 
			{

                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 vertexColor : COLOR;
                float4 projPos : TEXCOORD5;
                LIGHTING_COORDS(6,7)
                UNITY_FOG_COORDS(8)

            };

            VertexOutput vert (VertexInput v) 
			{

                VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID (v);

                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;

                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);

                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );

                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);

                COMPUTE_EYEDEPTH(o.projPos.z);
                TRANSFER_VERTEX_TO_FRAGMENT(o)

                return o;

            }

            float4 frag(VertexOutput i) : COLOR 
			{
				if(_L_SLO==1)
				_MCIALO = 0;

				float3 normalLocal = half3(0,0,1);

                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
				float2 sceneUVs = (i.projPos.xy / i.projPos.w);

                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                       float3 viewDirection;   
                    if(_L_F_RL == 1){
                if(_RimLightXHold == 1){
                viewDirection = normalize(float3(i.posWorld.x,_WorldSpaceCameraPos.y,_WorldSpaceCameraPos.z) - i.posWorld.xyz);
                }else{
                viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                }
                }else{
                   viewDirection = normalize(float3(i.posWorld.x,_WorldSpaceCameraPos.y,_WorldSpaceCameraPos.z) - i.posWorld.xyz);
                }
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform ));

				half RTL_OB_VP_CAL = distance(objPos.rgb,_WorldSpaceCameraPos);
				half2 RTL_VD_CAL = (float2((sceneUVs.x * 2 - 1)*(_ScreenParams.r/_ScreenParams.g), sceneUVs.y * 2 - 1).rg*RTL_OB_VP_CAL);

                    half MCapOutP = 1;
				half4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
				half3 _RTL_MVCOL = lerp(1, i.vertexColor, _MVCOL); 
				half3 RTL_TEX_COL = (_MainTex_var.rgb*_MainColor.rgb*MCapOutP*_RTL_MVCOL);
				
				half4 _MaskTransparency_var1 = tex2D(_MaskTransparency1, TRANSFORM_TEX(i.uv0, _MaskTransparency1));
				half4 _MaskTransparency_var2 = tex2D(_MaskTransparency2, TRANSFORM_TEX(i.uv0, _MaskTransparency2));
				half4 _MaskTransparency_var3 = tex2D(_MaskTransparency3, TRANSFORM_TEX(i.uv0, _MaskTransparency3));
             //   half4 _MaskTransparency_var4 = tex2D(_MaskTransparency4, TRANSFORM_TEX(i.uv0, _MaskTransparency4));
				half _MaskTransparency_var = _MaskTransparency_var1.r * _MaskTransparency_var2.r*_MaskTransparency_var3.r;

			
                half node_829 = lerp(( smoothstep(clamp(-20,1,_TransparentThreshold) , 1, _MainTex_var.a) * _MaskTransparency_var), smoothstep(clamp(-20,1,_TransparentThreshold) , 1, _MainTex_var.a) ,_Opacity);
                half RTL_TRAN_AS_OO = lerp( 1.0, 0.74, _AffectShadow );
                half RTL_TRAN_OC = saturate( ( RTL_TRAN_AS_OO > 0.5 ? ( 1.0 - (1.0-2.0 * (RTL_TRAN_AS_OO-0.5) ) * ( 1.0 - ( node_829 * RTL_TRAN_AS_OO ) ) ) : ( 2.0 * RTL_TRAN_AS_OO * (node_829 * RTL_TRAN_AS_OO) ) )  );
                clip(RTL_TRAN_OC - 0.5);

				float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                               float3 lightColor;
                        if(_L_UCL == 1){
                        lightColor = _CustomColor.rgb;
                        }else{
                         lightColor = _LightColor0.rgb;
                        }
                float3 halfDirection = normalize(viewDirection+lightDirection);

				fixed lightfo = 0;
                half3 RTL_RL_MAIN;
				#ifdef POINT
					unityShadowCoord3 lightCoord = mul(unity_WorldToLight, unityShadowCoord4(i.posWorld.xyz, 1)).xyz; 
					lightfo = tex2D(_LightTexture0, dot(lightCoord, lightCoord).rr).r;
				#else
					lightfo;
				#endif
				#ifdef POINT_COOKIE
					#if !defined(UNITY_HALF_PRECISION_FRAGMENT_SHADER_REGISTERS)
					#define DLCOO(input, worldPos) unityShadowCoord3 lightCoord = mul(unity_WorldToLight, unityShadowCoord4(worldPos, 1)).xyz
				#else
					#define DLCOO(input, worldPos) unityShadowCoord3 lightCoord = i._LightCoord
				#endif
					DLCOO(i, i.posWorld.xyz);
					lightfo = tex2D(_LightTextureB0, dot(lightCoord, lightCoord).rr).r * texCUBE(_LightTexture0, lightCoord).w;
				#else
					lightfo;
				#endif
				#ifdef DIRECTIONAL
					lightfo = UNITY_SHADOW_ATTENUATION(i, i.posWorld.xyz);
				#endif
				#ifdef SPOT
               
					#if !defined(UNITY_HALF_PRECISION_FRAGMENT_SHADER_REGISTERS)
				  #define DLCOO(input, worldPos) unityShadowCoord4 lightCoord = mul(unity_WorldToLight, unityShadowCoord4(worldPos, 1))
				#else
					#define DLCOO(input, worldPos) unityShadowCoord4 lightCoord = input._LightCoord
				#endif
					DLCOO(i, i.posWorld.xyz);
					lightfo = (lightCoord.z > 0) * tex2D(_LightTexture0, lightCoord.xy / lightCoord.w + 0.5).w * tex2D(_LightTextureB0, dot(lightCoord, lightCoord).xx).r;
				#else
					lightfo;
				#endif

				fixed attenuation = 1; 
				fixed lightfos = smoothstep(0, _LightFalloffSoftness ,lightfo);

			

					half3 RTL_SON = normalDirection;
					half3 RTL_SON_CHE_1 = 1;

			           half3 RTL_UAL = 0;
            half3 RTL_GI = 0;
            half3 RTL_UOAL = 0;

  if(_L_F_UOAL == 1){
                    
                    if(_L_SLO==0)
                    RTL_UAL = UNITY_LIGHTMODEL_AMBIENT.rgb;
                    RTL_UOAL = RTL_UAL;
                    }else{
                    if(_L_SLO==0)
                    RTL_GI = AL_GI();
                    RTL_UOAL = RTL_GI;
                    }
               


					half3 RTL_SCT = 1;

				

		

					half RTL_PT = 1;

			

				half3 RTL_OSC =1;
                half3 RTL_HL;
                if(_L_UCL == 0){
				 RTL_HL = (_HighlightColor.rgb*_HighlightColorPower+_PointSpotlightIntensity);
                }else{
                	
                RTL_HL= 1;
                }
				
				
				half RTL_LVLC = saturate( dot( lightColor.rgb , float3(0.3,0.59,0.11) ) );
				half3 RTL_MCIALO = lerp(RTL_TEX_COL , lerp(RTL_TEX_COL , _MainTex_var.rgb * MCapOutP * 0.7 , clamp((RTL_LVLC*1),0,1) ) , _MCIALO ); 
                    half3 RTL_GLO = RTL_HL;
				if(_L_F_GLO == 1){
			

						float RTL_GLO_MAIN_SOF_Sil = lerp(0.1,1.0,_GlossSoftness);
						half RTL_NDOTH = max(0,dot(halfDirection,normalDirection));
						half RTL_GLO_MAIN = smoothstep( 0.1, RTL_GLO_MAIN_SOF_Sil, pow(RTL_NDOTH,exp2(lerp(-2,15,_Glossiness))) );

						half3 RTL_GT = RTL_GLO_MAIN;

					

					half4 _MaskGloss_var = tex2D(_MaskGloss,TRANSFORM_TEX(i.uv0, _MaskGloss));

					half3 RTL_GLO_MAS = lerp(RTL_HL,lerp(RTL_HL,(_GlossColor.rgb*_GlossColorPower),RTL_GT),_MaskGloss_var.r);
					RTL_GLO = RTL_GLO_MAS;

				}

				
                half3 RTL_RL_CHE_1;
                
				if(_L_F_RL == 1){

					float node_4353 = 0.0;
					float node_3687 = 0.0;

					half3 RTL_RL_LARL_OO = lerp( _RimLightColor.rgb, lerp(float3(node_3687,node_3687,node_3687),_RimLightColor.rgb,lightColor.rgb), _LightAffectRimLightColor );
					half RTL_RL_S_Sli = lerp(1.71,0.29,_RimLightSoftness);
					RTL_RL_MAIN = lerp(float3(node_4353,node_4353,node_4353),(RTL_RL_LARL_OO*_RimLightColorPower),smoothstep( 1.71, RTL_RL_S_Sli, pow(1.0-max(0,dot(normalDirection, viewDirection)),(1.0 - _RimLightUnfill)) ));
					half3 RTL_RL_IL_OO = lerp(RTL_GLO,(RTL_GLO+RTL_RL_MAIN),_RimLightInLight);
					RTL_RL_CHE_1 = RTL_RL_IL_OO;
                    }
				else{

				    RTL_RL_CHE_1 = RTL_GLO;
                    }
				

	

					half3 RTL_CLD = lightDirection;

		

				half3 RTL_ST_SS_AVD_OO = viewDirection;
				half RTL_NDOTL = 0.5*dot(RTL_ST_SS_AVD_OO,RTL_SON)+0.5;

		

					half3 RTL_ST = 1;

			

			

					half RTL_SS = attenuation;

			

				
				half3 RTL_FR_OFF_OTHERS = (lerp( RTL_TEX_COL , _MainTex_var.rgb , _MCIALO) * (RTL_RL_CHE_1*RTL_ST*RTL_SON_CHE_1*lightColor.rgb));

		

					half3 RTL_FR = RTL_FR_OFF_OTHERS;

		
             
                    half3 RTL_SL = RTL_UOAL;
                    half3 RTL_SL_CHE_1 = RTL_FR;
                    half3 RTL_RL;
				if(_L_F_RL == 1){

					half3 RTL_RL_ON = lerp((RTL_SL_CHE_1+RTL_RL_MAIN),RTL_SL_CHE_1,_RimLightInLight);
					RTL_RL = RTL_RL_ON;
                    }
				else{

					RTL_RL = RTL_SL_CHE_1;
                    }


				
				float3 emissive = (RTL_MCIALO+RTL_RL) * lightfos;
				float3 finalColor = (emissive);

                half RTL_TRAN_O = node_829;

                fixed4 finalRGBA = fixed4(finalColor,RTL_TRAN_O);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;

            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull [_DoubleSided]
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            //#pragma multi_compile_shadowcaster
            #pragma multi_compile_fog

			//#pragma multi_compile_instancing

            #pragma only_renderers d3d9 d3d11 vulkan glcore gles3 gles metal xboxone ps4 wiiu switch
            #pragma target 3.0

            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;

         
		uniform sampler2D _MaskTransparency1; uniform float4 _MaskTransparency1_ST;
		uniform sampler2D _MaskTransparency2; uniform float4 _MaskTransparency2_ST;
		uniform sampler2D _MaskTransparency3; uniform float4 _MaskTransparency3_ST;
       // uniform sampler2D _MaskTransparency4; uniform float4 _MaskTransparency4_ST;
            uniform half _Opacity;
            uniform fixed _AffectShadow;

			uniform half _TransparentThreshold;

            struct VertexInput 
			{

                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID

            };

            struct VertexOutput 
			{

                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;

            };

            VertexOutput vert (VertexInput v) 
			{

                VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID (v);
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;

            }

            float4 frag(VertexOutput i) : COLOR 
			{

        half4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));

				half4 _MaskTransparency_var1 = tex2D(_MaskTransparency1, TRANSFORM_TEX(i.uv0, _MaskTransparency1));
				half4 _MaskTransparency_var2 = tex2D(_MaskTransparency2, TRANSFORM_TEX(i.uv0, _MaskTransparency2));
				half4 _MaskTransparency_var3 = tex2D(_MaskTransparency3, TRANSFORM_TEX(i.uv0, _MaskTransparency3));
             //   half4 _MaskTransparency_var4 = tex2D(_MaskTransparency4, TRANSFORM_TEX(i.uv0, _MaskTransparency4));
				half _MaskTransparency_var = _MaskTransparency_var1.r * _MaskTransparency_var2.r*_MaskTransparency_var3.r;

				half node_829 = lerp(( smoothstep(clamp(-20,1,_TransparentThreshold),1,_MainTex_var.a) *_MaskTransparency_var), smoothstep(clamp(-20,1,_TransparentThreshold) , 1, _MainTex_var.a) ,_Opacity);
				half RTL_TRAN_AS_OO = lerp( 1.0, 0.74, _AffectShadow );
				half RTL_TRAN_OC = saturate(( RTL_TRAN_AS_OO > 0.5 ? (1.0-(1.0-2.0*(RTL_TRAN_AS_OO-0.5))*(1.0-(node_829*RTL_TRAN_AS_OO))) : (2.0*RTL_TRAN_AS_OO*(node_829*RTL_TRAN_AS_OO)) ));
                clip(RTL_TRAN_OC - 0.5);

				SHADOW_CASTER_FRAGMENT(i)

            }

            ENDCG

        }

    }

	CustomEditor "RealToonShaderGUI_Lite"

}