using System.Threading.Tasks;
using UnityEngine;

public class UnitView : MonoBehaviour, IUnitView
{
    public static UnitView Instance;

    private float _targetX;
    private float _targetY;
    private float _reduceSpeed = 2f;
    private bool isMoving = false;
    private Transform unitTransform;
    private Animator unitAnimator;
    private Animator defendUnit;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (isMoving)
        {
            unitTransform.position = new Vector2(Mathf.MoveTowards(unitTransform.position.x, _targetX, _reduceSpeed * Time.deltaTime),
                                        Mathf.MoveTowards(unitTransform.position.y, _targetY, _reduceSpeed * Time.deltaTime));
        }
    }

    public async Task StartMoveAnimation(BaseUnit unit, int moveToX, int moveToY)
    {
        _targetX = moveToX;
        _targetY = moveToY;

        unitTransform = unit.getUnityObject().GetTransform();
        unitAnimator = unitTransform.GetComponent<Animator>();

        bool isEqualsValues = false;
        isMoving = true;
        unitAnimator.SetBool("isMoving", isMoving);

        while (!isEqualsValues)
        {
            isEqualsValues = unitTransform.position.x == moveToX && unitTransform.position.y == moveToY;
            await Task.Delay(25);
        }

        isMoving = false;
        unitAnimator.SetBool("isMoving", isMoving);
    }
    public async Task StartFightAnimation(BaseUnit attackingunit, BaseUnit defendingUnit)
    {   
        unitAnimator = attackingunit.getUnityObject().GetTransform().GetComponent<Animator>();
        defendUnit = defendingUnit.getUnityObject().GetTransform().GetComponent<Animator>();

        bool isAnimPlaying = true;
        unitAnimator.SetBool("isFighting", isAnimPlaying);
        defendUnit.SetBool("isFighting", isAnimPlaying);

        while (isAnimPlaying)
        {
            isAnimPlaying = unitAnimator.GetBool("isFighting");
            await Task.Delay(25);
        }
    }

    public void DisableFightAnimation()
    {
        unitAnimator.SetBool("isFighting", false);
    }

}