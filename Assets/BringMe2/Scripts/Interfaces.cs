using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RummageBattle {
    public interface IDamageable {
        void ApplyDamage(int ammount);
    }

    public interface IEliminable {
        void Eliminate();
    }

    public interface IFreezeable {
        void Freeze();
        void UnFreeze();
    }

    public interface IMoveable {
        void SetPosition(Vector3 position);
        void SetRotation(Quaternion rotation);
    }
}