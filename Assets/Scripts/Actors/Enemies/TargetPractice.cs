using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPractice : Actor {
    public override void Init() {
        base.Init();
    }

    protected override void Die(){
        SimplePool.Despawn(gameObject);
    }
}
