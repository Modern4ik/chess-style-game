using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero1 : BaseHero
{
    private Health health = new Health(2);

    public override Health getHealth()
    {
        return health;
    }

    public override void ReceiveDamage(int damage)
    {
        health.GetDamage(1);
    }

    private MovePattern _movePattern = new MovePattern(
        new List<Coordinate>() {
            new Coordinate(0, 1)
        }
      );

    public override MovePattern movePattern()
    {
        return _movePattern;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
