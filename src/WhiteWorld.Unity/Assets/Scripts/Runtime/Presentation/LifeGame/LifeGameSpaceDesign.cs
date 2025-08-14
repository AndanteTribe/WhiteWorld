using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using WhiteWorld.Domain.Entity;
using WhiteWorld.Domain.LifeGame;
using Space = WhiteWorld.Domain.Entity.Space;

namespace WhiteWorld.Presentation.LifeGame
{
    public class LifeGameSpaceDesign : MonoBehaviour, ISpaceMoveController, ISpaceTelevisionController
    {
        [SerializeField]
        private SpacePoint[] _spacePoints;
        [SerializeField]
        private Transform _player;

        private int _currentPos;
        private AudioController _audioController;

        [Inject]
        public void Initialize(AudioController audioController) => _audioController = audioController;

        async UniTask<Space> ISpaceMoveController.MoveAsync(SpaceAmount amount, CancellationToken cancellationToken)
        {
            // 最初にプレイヤーのy座標を（変えないので）取得しておく.
            var currentY = _player.position.y;
            var absAmount = Math.Abs((int)amount);

            // amountマス進む.
            // もし途中でテレビマスがあったら止まる（強制）.
            // プレイヤーの座標を移動するマスの場所まで移動.
            for (var i = 1; i <= absAmount; i++)
            {
                // 実際の移動量
                var realAmount = (int)amount >= 0 ? i : -i;
                var spacePoint = _spacePoints[_currentPos + realAmount];
                // テレビマスに到達したら、そこで止まる
                if (spacePoint.Space == Space.Television)
                {
                    _currentPos += realAmount;
                    return spacePoint.Space;
                }

                var nextPos = spacePoint.transform.position;
                nextPos.y = currentY; // Y座標は変えない
                _player.position = nextPos;
                _audioController.PlaySE(2).Forget(); //  移動効果音を鳴らす
                await UniTask.WaitForSeconds(1, cancellationToken: cancellationToken);
            }
            // テレビマスに到達しなかった場合、amount分進む
            return _spacePoints[_currentPos += (int)amount].Space;
        }

        UniTask ISpaceTelevisionController.ExecuteAsync(CancellationToken cancellationToken)
        {
            var space = _spacePoints[_currentPos];
            space.TVController.StartTVAnimation();
            return space.TVController.WaitForAnimationPreEndAsync(cancellationToken);
        }
    }
}