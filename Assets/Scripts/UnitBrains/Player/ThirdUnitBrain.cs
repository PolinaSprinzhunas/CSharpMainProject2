using System;
using System.Collections.Generic;
using Model;
using Model.Runtime.Projectiles;
using UnityEngine;

namespace UnitBrains.Player
{
    public class ThirdUnitBrain : DefaultPlayerUnitBrain
    {
     
        public override string TargetUnitName => "Ironclad Behemoth";
        private enum UnitAction
        {
            None,
            Moving,
            Attacking
        }

        private UnitAction currentAction = UnitAction.None;
        private float lastActionTime;
        private float actionDelay = 1f;

        public void StartMove()
        {
            if (Time.time >= lastActionTime && currentAction != UnitAction.Moving)
            {
                currentAction = UnitAction.Moving;
                lastActionTime = Time.time + actionDelay;
            }
        }

        public void StartAttack()
        {
            if (Time.time >= lastActionTime && currentAction != UnitAction.Attacking)
            {
                currentAction = UnitAction.Attacking;
                lastActionTime = Time.time + actionDelay;
            }
        }

        protected override List<Vector2Int> SelectTargets()
        {
            var targets = base.SelectTargets();
            if (targets.Count > 1)
                targets.RemoveAt(1);
            return targets;
        }

        protected override List<Vector2Int> GetNextStep()
        {
            if (HasTargetsInRange())
                return new List<Vector2Int> { unit.Pos };
            else
            {
                var target = runtimeModel.RoMap.Bases[IsPlayerUnitBrain ? RuntimeModel.BotPlayerId : RuntimeModel.PlayerId];
                return new List<Vector2Int> { CalcNextStepTowards(target) };
            }
        }
    }
}



