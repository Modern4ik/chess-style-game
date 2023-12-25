using GameLogic.Units;
using System.Threading.Tasks;
using UnityEngine;

namespace View
{
    public class UnitView : MonoBehaviour, IUnitView
    {
        public const string isFighting = "isFighting";
        public const string isMoving = "isMoving";

        private float _targetX;
        private float _targetY;
        private float _reduceSpeed = 2f;
        private bool isMovingProcess = false;
        private bool isFightingProcess = false;

        [SerializeField] private Animator unitAnimator;

        private void Update()
        {
            if (isMovingProcess)
            {
                this.transform.position = new Vector2(Mathf.MoveTowards(this.transform.position.x, _targetX, _reduceSpeed * Time.deltaTime),
                                             Mathf.MoveTowards(this.transform.position.y, _targetY, _reduceSpeed * Time.deltaTime));
            }
        }

        public async Task MoveAnimation(int moveToX, int moveToY)
        {
            _targetX = moveToX;
            _targetY = moveToY;

            bool isEqualsValues = false;
            isMovingProcess = true;
            unitAnimator.SetBool(isMoving, isMovingProcess);

            while (!isEqualsValues)
            {
                isEqualsValues = this.transform.position.x == moveToX && this.transform.position.y == moveToY;
                await Task.Delay(25);
            }

            isMovingProcess = false;
            unitAnimator.SetBool(isMoving, isMovingProcess);
        }
        public async Task FightAnimation(BaseUnit defendingUnit)
        {
            this.StartFight();
            defendingUnit.getUnitView().StartFight();

            while (isFightingProcess)
            {
                isFightingProcess = unitAnimator.GetBool(isFighting);
                await Task.Delay(25);
            }
        }
        public void StartFight()
        {
            isFightingProcess = true;
            this.unitAnimator.SetBool(isFighting, isFightingProcess);
        }

        public void DisableFightAnimation()
        {
            unitAnimator.SetBool(isFighting, false);
        }

        public void Destroy() => Destroy(gameObject);
        public void SetPosition(Vector2 position) => this.transform.position = position;


    }
}