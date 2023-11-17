using System.Threading.Tasks;
using UnityEngine;

public class UnitView : MonoBehaviour, IUnitView
{
    public static UnitView Instance;
    public const string isFighting = "isFighting";
    public const string isMoving = "isMoving";

    private float _targetX;
    private float _targetY;
    private float _reduceSpeed = 2f;
    private bool isStartMoving = false;
    private IUnityObject unitToAnimate;
    private Animator unitAnimator;
    private Animator defendUnitAnimator;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (isStartMoving)
        {
            unitToAnimate.SetPosition(new Vector2(Mathf.MoveTowards(unitToAnimate.GetCoordX(), _targetX, _reduceSpeed * Time.deltaTime),
                                        Mathf.MoveTowards(unitToAnimate.GetCoordY(), _targetY, _reduceSpeed * Time.deltaTime)));
        }
    }

    public async Task StartMoveAnimation(BaseUnit unit, int moveToX, int moveToY)
    {
        _targetX = moveToX;
        _targetY = moveToY;

        unitToAnimate = unit.getUnityObject();
        unitAnimator = unitToAnimate.GetAnimator();

        bool isEqualsValues = false;
        isStartMoving = true;
        unitAnimator.SetBool(isMoving, isStartMoving);

        while (!isEqualsValues)
        {
            isEqualsValues = unitToAnimate.GetCoordX() == moveToX && unitToAnimate.GetCoordY() == moveToY;
            await Task.Delay(25);
        }

        isStartMoving = false;
        unitAnimator.SetBool(isMoving, isStartMoving);
    }
    public async Task StartFightAnimation(BaseUnit attackingunit, BaseUnit defendingUnit)
    {
        unitAnimator = attackingunit.getUnityObject().GetAnimator();
        defendUnitAnimator = defendingUnit.getUnityObject().GetAnimator();

        bool isAnimPlaying = true;
        unitAnimator.SetBool(isFighting, isAnimPlaying);
        defendUnitAnimator.SetBool(isFighting, isAnimPlaying);

        while (isAnimPlaying)
        {
            isAnimPlaying = unitAnimator.GetBool(isFighting);
            await Task.Delay(25);
        }
    }

    public void DisableFightAnimation()
    {
        unitAnimator.SetBool(isFighting, false);
    }
}