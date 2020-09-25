using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RummageBattle {
    public interface IDamageable<T> {
        void ApplyDamage(T ammount);
        T GetHealth();
    }

    public interface IEliminable {
        void Eliminate();
    }

    public interface IFreezeable {
        void Freeze();
        void UnFreeze();
    }

    public interface ITeleportable {
        void SetPosition(Vector3 position);
        void SetRotation(Quaternion rotation);
    }

    public interface IInteractable {
        void Interact();
    }

    public interface IDroppable {
        void Drop();
    }
}