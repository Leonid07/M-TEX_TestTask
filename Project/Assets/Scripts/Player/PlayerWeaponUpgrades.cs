using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class PlayerWeaponUpgrades : MonoBehaviour
{
    [Inject] Menu menu;
    [Inject] PlayerShooting playerShooting;

    void Start()
    {        
        this.OnTriggerEnterAsObservable()
            .Where(other => other.CompareTag("Bonus"))
            .Subscribe(other =>
            {
                Bonus bonus = other.GetComponent<Bonus>();
                switch (bonus.typeBonus)
                {
                    case Bonus.TypeBonus.PowerUp:
                        LevelUpForPlayer(bonus.levelUpForWeapons);
                        break;
                    case Bonus.TypeBonus.ScoreBoost:
                        ActiveBonusBoostScore(bonus.NumberOfPoints);
                        break;
                }
                Destroy(other.gameObject);
            })
            .AddTo(this);
    }

    void ActiveBonusBoostScore(int count)
    {
        menu.AccountChange(count);
    }

    void LevelUpForPlayer(int levelCount)
    {
        playerShooting.levelWeapons += levelCount;
    }
}