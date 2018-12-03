// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33776,y:32627,varname:node_3138,prsc:2|custl-7622-OUT,olwid-9911-OUT,olcol-3142-RGB;n:type:ShaderForge.SFN_Color,id:7241,x:32010,y:32250,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2d,id:6980,x:31908,y:32362,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_6980,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:7405,x:32480,y:32348,varname:node_7405,prsc:2|A-7241-RGB,B-6980-RGB;n:type:ShaderForge.SFN_Slider,id:9978,x:32561,y:33057,ptovrint:False,ptlb:outlineWidth,ptin:_outlineWidth,varname:node_9978,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.07560637,max:1;n:type:ShaderForge.SFN_Color,id:3142,x:32435,y:33244,ptovrint:False,ptlb:OutlineColor,ptin:_OutlineColor,varname:node_3142,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.1544118,c2:0.1544118,c3:0.1544118,c4:1;n:type:ShaderForge.SFN_SwitchProperty,id:9911,x:33019,y:33264,ptovrint:False,ptlb:outlineSwitch,ptin:_outlineSwitch,varname:node_9911,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-5940-OUT,B-9978-OUT;n:type:ShaderForge.SFN_Vector1,id:5940,x:32772,y:33339,varname:node_5940,prsc:2,v1:0;n:type:ShaderForge.SFN_Dot,id:1780,x:31699,y:33025,varname:node_1780,prsc:2,dt:0|A-108-OUT,B-238-OUT;n:type:ShaderForge.SFN_NormalVector,id:108,x:31493,y:32869,prsc:2,pt:False;n:type:ShaderForge.SFN_LightVector,id:238,x:31512,y:33172,varname:node_238,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7622,x:32601,y:32815,varname:node_7622,prsc:2|A-9219-OUT,B-6804-RGB,C-7381-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:7028,x:32118,y:33111,varname:node_7028,prsc:2;n:type:ShaderForge.SFN_Posterize,id:6597,x:31986,y:32990,varname:node_6597,prsc:2|IN-7859-OUT,STPS-1040-OUT;n:type:ShaderForge.SFN_Vector1,id:1040,x:31882,y:33238,varname:node_1040,prsc:2,v1:2;n:type:ShaderForge.SFN_Lerp,id:9219,x:32365,y:32725,varname:node_9219,prsc:2|A-5816-OUT,B-7405-OUT,T-9271-OUT;n:type:ShaderForge.SFN_Color,id:6511,x:31729,y:32594,ptovrint:False,ptlb:shadowColor,ptin:_shadowColor,varname:node_6511,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:5816,x:31968,y:32594,varname:node_5816,prsc:2|A-7405-OUT,B-6511-RGB;n:type:ShaderForge.SFN_LightColor,id:6804,x:32286,y:33023,varname:node_6804,prsc:2;n:type:ShaderForge.SFN_Clamp01,id:9271,x:32118,y:32893,varname:node_9271,prsc:2|IN-6597-OUT;n:type:ShaderForge.SFN_RemapRange,id:7859,x:31836,y:32865,varname:node_7859,prsc:2,frmn:0,frmx:1,tomn:0,tomx:2|IN-1780-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:7381,x:31493,y:32645,varname:node_7381,prsc:2;proporder:7241-6980-9978-3142-9911-6511;pass:END;sub:END;*/

Shader "Shader Forge/elUnlitDapena" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("MainTex", 2D) = "white" {}
        _outlineWidth ("outlineWidth", Range(0, 1)) = 0.07560637
        _OutlineColor ("OutlineColor", Color) = (0.1544118,0.1544118,0.1544118,1)
        [MaterialToggle] _outlineSwitch ("outlineSwitch", Float ) = 0
        _shadowColor ("shadowColor", Color) = (0.5,0.5,0.5,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "Outline"
            Tags {
            }
            Cull Front
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float _outlineWidth;
            uniform float4 _OutlineColor;
            uniform fixed _outlineSwitch;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( float4(v.vertex.xyz + v.normal*lerp( 0.0, _outlineWidth, _outlineSwitch ),1) );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                return fixed4(_OutlineColor.rgb,0);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _Color;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _shadowColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 node_7405 = (_Color.rgb*_MainTex_var.rgb);
                float node_1040 = 2.0;
                float3 finalColor = (lerp((node_7405*_shadowColor.rgb),node_7405,saturate(floor((dot(i.normalDir,lightDirection)*2.0+0.0) * node_1040) / (node_1040 - 1)))*_LightColor0.rgb*attenuation);
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _Color;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _shadowColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 node_7405 = (_Color.rgb*_MainTex_var.rgb);
                float node_1040 = 2.0;
                float3 finalColor = (lerp((node_7405*_shadowColor.rgb),node_7405,saturate(floor((dot(i.normalDir,lightDirection)*2.0+0.0) * node_1040) / (node_1040 - 1)))*_LightColor0.rgb*attenuation);
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
