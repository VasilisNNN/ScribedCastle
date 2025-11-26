using System;
using UnityEngine;

public class ColorAndMaterial : MonoBehaviour
{
    private SpriteRenderer SR;
    private Transform _transform;
    private Color StartColor;
    private Player pl;
    private SpriteRenderer ChildSPRT;

    private Animator Anim;
    public float Alpha;

    public Material StartMaterial { get; set; }
    public float AplhaColor { get; set; }

    private StatsControll Stats;
    private Constructor Constr;
    private float minimalalpha;

    private Material StunMaterial;
    private Material WhiteMaterial;

    public void Awake()
    {
        WhiteMaterial = Resources.Load<Material>("Materials/DamageLight");

        StunMaterial = Resources.Load<Material>("Materials/DoodleHorizontal");
        Anim = GetComponent<Animator>();
        Stats = GetComponent<StatsControll>();
        SR = GetComponent<SpriteRenderer>();
        pl = InitializeObjects.PL;
        Constr = InitializeObjects.Constr;
        AplhaColor = 1;
        _transform = transform;
        if (SR != null)
        {
            StartMaterial = SR.material;

            StartColor = new Color(1,1,1,1);
        }


        print("Initialize color " + name);
    }

   
    void SetColorToSPRT(Material material)
    {
        if (SR == null) return;
        
            if (material != null)
                SR.material = material;


        Vector3 TargetPos = pl.MainCamera.transform.position;

    

        if (Stats.BuildedStructure  && _transform.parent ==null)
        {

            if (_transform.position.y > TargetPos.y-2)
            {
                float Camdepth = ((_transform.position.y - 2) - (pl.MainCamera.transform.position.y - 2)) / 4;
                float Mousedepth = (_transform.position.y - pl.MainCamera.ScreenToWorldPoint(pl.IM.MousePosition).y) / 20;
       
                float colordepth = Camdepth + Mousedepth;

                if(Constr.DistanceFade)
                colordepth = Mathf.Clamp(colordepth, 0f, 0.25f);
                else 
                colordepth = 0;

                SR.color = new Color(StartColor.r - colordepth / 1.5f, StartColor.g - colordepth / 1.1f, StartColor.b - colordepth, Mathf.Lerp(SR.color.a, Alpha, Time.deltaTime * 10));
                for (int i = 0; i < _transform.childCount; i++)
                    SetChildColor(material, i, new Color(StartColor.r - colordepth / 1.5f, StartColor.g - colordepth / 1.1f, StartColor.b - colordepth ));

            }
            else

            {
                SR.color = new Color(StartColor.r, StartColor.g, StartColor.b, Mathf.Lerp(SR.color.a, Alpha, Time.deltaTime * 10));
                for (int i = 0; i < _transform.childCount; i++)
                    SetChildColor(material, i, StartColor);

            }
            
        }
        else
        {
            for (int i = 0; i < _transform.childCount; i++)
                SetChildColor(material, i, _transform.GetChild(i).GetComponent<SpriteRenderer>().color);

            SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, Mathf.Lerp(SR.color.a, Alpha, Time.deltaTime * 10));
        }


        
    }


    void SetChildColor(Material material, int i, Color color)
    {
        if (_transform.GetChild(i).GetComponent<SpriteRenderer>() == null) return;
            ChildSPRT = _transform.GetChild(i).GetComponent<SpriteRenderer>();

        if (ChildSPRT == null || _transform.GetChild(i).GetComponent<Blinking>() != null) return;

        if (material != null)
            ChildSPRT.material = material;

        if (_transform.GetChild(i).name == "Base") return;
        
        ChildSPRT.color = new Color(color.r, color.g, color.b, Alpha);

        for (int j = 0; j < ChildSPRT.transform.childCount; j++)
        {
            if (ChildSPRT.transform.GetChild(j).GetComponent<SpriteRenderer>() != null)
                ChildSPRT.transform.GetChild(j).GetComponent<SpriteRenderer>().color = ChildSPRT.color;
        }

    }




    public void SetColorAndMaterial(float alpha, Material material)
    {
        Alpha = alpha;
        SetColorToSPRT(material);
       
        if (!Stats.BuildedStructure) return;
  
        if (Mathf.Abs(_transform.localScale.x)<1)
            _transform.localScale = new Vector3(
            _transform.localScale.x + Time.deltaTime,
            _transform.localScale.y ,
            _transform.localScale.z );
        
        if (Mathf.Abs(_transform.localScale.y) < 1)
            _transform.localScale = new Vector3(
                _transform.localScale.x ,
                _transform.localScale.y + Time.deltaTime,
                _transform.localScale.z );


        if ( _transform.localScale.x < -1)
        {
            _transform.localScale = new Vector3(-1, _transform.localScale.y, 1);
        }

        if (_transform.localScale.y > 1 || _transform.localScale.x > 1)
        {
            _transform.localScale = new Vector3(1, 1, 1);
            for (int i = 0; i < _transform.childCount; i++)
            {
                _transform.GetChild(i).name = _transform.GetChild(i).name.Replace("(Clone)", null);
                _transform.GetChild(i).localScale = _transform.localScale;
                if (Anim != null)
                {
                    Anim.enabled = true;
                    Anim.applyRootMotion = true;

                    Anim.Rebind();
                    Anim.Update(0f);
                }
            }
           
        }


    }

    public void ObjectColorAlpha()
    {
        minimalalpha = 0.3f;
  

        if (pl.inv.GetItemInDatabase(Stats.DatabaseID).Structure && Constr.Building && Stats.transform.parent == null)
        {


            float alpha = MathF.Abs(Stats.transform.position.x - Constr.transform.position.x) / 10 - 1;
            alpha = Mathf.Clamp(alpha, 0.1f, 1);

            if (Stats.transform.position.y < Constr.transform.position.y)
            {
                if (Constr.AlphaBuildingFade)
                   SetColorAndMaterial(alpha, StartMaterial);
                else SetColorAndMaterial(1, StartMaterial);

                return;

            }
            else
            if (Stats.transform.position.y > Constr.transform.position.y + 1)
            {
                if (Constr.AlphaBuildingFade)
                   SetColorAndMaterial(alpha, StartMaterial);
                else
                    SetColorAndMaterial(1, StartMaterial);

                return;
            }
            else
            {
                SetColorAndMaterial(1, StartMaterial);
                return;
            }





        }


        if (!Constr.Building)
            SetColorAndMaterial(1, StartMaterial);

        if (Stats.InvisTimer - 0.8f > Time.fixedTime)
        {

            if (!Stats.Stunned)
            {
                SetColorAndMaterial(0.5f, WhiteMaterial);
            }
            else
                SetColorAndMaterial(1, StunMaterial);

            return;
        }



        if (Stats.InvisTimer > Time.fixedTime && Stats.InvisTimer > Time.fixedTime + 0.05f)
        {

            AplhaColor = 0.5f;
            SetColorAndMaterial(0.5f, StartMaterial);
            return;
        }

        if (Stats.InvisTimer > Time.fixedTime && Stats.InvisTimer < Time.fixedTime + 0.05f)
        {
    
            AplhaColor = 1;
            SetColorAndMaterial(1, StartMaterial);
            return;
        }

        if (Stats.ReduceAlphaOnColl)
        {
            // print("ReduceAlphaOnColl " + FO.name);

            if (pl.coll_obj.Contains(Stats.gameObject) || (Mathf.Abs(pl.transform.position.x - Stats.transform.position.x) < 1 && Mathf.Abs(pl.transform.position.y - _transform.position.y) < 1))
            {
                if (AplhaColor > minimalalpha && Stats.name != "Base")
                    AplhaColor -= 1 * Time.deltaTime * 3;

                SetColorAndMaterial(AplhaColor, StartMaterial);
            }
            else
            {

                if (AplhaColor < 1)
                {
                    if (AplhaColor > 0.9f) AplhaColor = 1;
                    SetColorAndMaterial(AplhaColor, StartMaterial);
                    AplhaColor += 1 * Time.deltaTime;
                }


            }
        }
        else
        {
            if (AplhaColor < 1)
                    
            {
                AplhaColor += 1 * Time.deltaTime;
                if (AplhaColor > 0.9f) AplhaColor = 1;
                SetColorAndMaterial(AplhaColor, StartMaterial);
            }
        }


        if (Stats.Stunned)
        {
            SetColorAndMaterial(AplhaColor, StunMaterial);
            return;
        }



        if (!pl.coll_obj.Contains(gameObject) && Stats.NewMaterialOnColl != null)
        {
            Stats.StartColl = false;
            SetColorAndMaterial(AplhaColor, StartMaterial);
            return;
        }



        if (!Stats.StartColl)
        {

            Stats.CollMaterialTimer = Time.fixedTime + 0.5f;
            Stats.StartColl = true;

        }




        if (Stats.CollMaterialTimer > Time.fixedTime)
        {
            if (Stats.CollMaterialTimer > Time.fixedTime + 0.05f)
                SetColorAndMaterial(AplhaColor, Stats.NewMaterialOnColl);
            else SetColorAndMaterial(AplhaColor, StartMaterial);
        }


    }


}
