using UnityEngine;

//namespace iii_UMVR06_TPSDefenseGame_Subroutines_2 {

//    public class DetectionState : IEnemyBehaviourSystem_Base {

//        public override void EnterState(IEnemy_Base theEnemy) {
//            base.EnterState(theEnemy);
//        }

//        public override void DetectPlayer() {            
//            int numbers = Physics.SphereCastNonAlloc(
//                reference_IEnemyBaseTypeEnemy.transform.position,
//                reference_IEnemyBaseTypeEnemy.DetectDistance,
//                Vector3.forward,
//                results,
//                0,
//                reference_IEnemyBaseTypeEnemy.DetectLayerMask.value
//                );

//            for(int i = 0; i < numbers; i++) {
//                if(results[i].transform.gameObject.GetComponent<Attackable>() == null) {
//                    continue; }
//                if(Mathf.Abs(
//                    results[i].transform.position.y -
//                    reference_IEnemyBaseTypeEnemy.transform.position.y) > 
//                    reference_IEnemyBaseTypeEnemy.AcceptalbeMaxHeightOffset) {
//                    continue; }
//                if(Vector3.Angle(
//                    reference_IEnemyBaseTypeEnemy.transform.forward,
//                    results[i].transform.position -
//                    reference_IEnemyBaseTypeEnemy.transform.position) > 
//                    reference_IEnemyBaseTypeEnemy.MyVision) {
//                    continue; }
//                if(reference_IEnemyBaseTypeEnemy.Target != null) {
//                    float currentTargetDistance = Vector3.Distance(
//                        reference_IEnemyBaseTypeEnemy.transform.position, 
//                        reference_IEnemyBaseTypeEnemy.Target.transform.position);
//                    float newTargetDistance = Vector3.Distance(
//                        reference_IEnemyBaseTypeEnemy.transform.position, results[i].transform.position);
//                    if(newTargetDistance < currentTargetDistance) {     
//                        reference_IEnemyBaseTypeEnemy.Target = results[i].transform.gameObject;
//                    } 
//                } else {
//                    reference_IEnemyBaseTypeEnemy.Target = results[i].transform.gameObject;
//                }                
//            }
//        }

//        private void DetectingAndTurnBack() {
//            if(reference_IEnemyBaseTypeEnemy.Target != null &&
//                !(Vector3.Distance(reference_IEnemyBaseTypeEnemy.Target.transform.position,
//                reference_IEnemyBaseTypeEnemy.transform.position) > reference_IEnemyBaseTypeEnemy.DetectDistance)) {
//                reference_IEnemyBaseTypeEnemy.ConfuseTimer = 0;
//                reference_IEnemyBaseTypeEnemy.LookTarget();
//                return;
//            } else {
//                reference_IEnemyBaseTypeEnemy.Target = null;
//                if(reference_IEnemyBaseTypeEnemy.ConfuseTimer >= reference_IEnemyBaseTypeEnemy.ConfuseTime) {
//                    reference_IEnemyBaseTypeEnemy.LookOriginDirection();
//                } else {
//                    reference_IEnemyBaseTypeEnemy.ConfuseTimer += Time.deltaTime;
//                }
//            }
//            DetectPlayer();
//        }

//        public override void Update() {
//            base.Update();
//            DetectingAndTurnBack();
//        }

//    }

//}
