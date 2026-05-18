using UnityEngine;

public abstract class AchievementsBase : MonoBehaviour
{
    public abstract void Init();
    public abstract void SetAch(string n);
    public abstract void AchievementsManager();

    public abstract void MainUpdate();
    public MenuCustom _Menu;
    public SaveLoad SL;
    public Constructor Constr;
    public ItemDatabase itemDatabase;
    public float OnBoardTimer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
