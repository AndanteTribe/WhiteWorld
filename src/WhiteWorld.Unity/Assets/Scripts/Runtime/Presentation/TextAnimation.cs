using System;
using System.Collections.Generic;
using LitMotion;
using TMPro;
using UnityEngine;
using Cysharp.Threading.Tasks;
using LitMotion.Extensions;
using Random = UnityEngine.Random;

namespace WhiteWorld.Presentation.Runtime
{
    public class TextAnimator : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _keywordTextPrefab;

        [SerializeField]
        private TextMeshProUGUI _dummyTextPrefab;

        [SerializeField]
        private RectTransform _canvasRectTransform;

        /// <summary>
        /// テキストアニメーションを開始する
        /// </summary>
        /// <param name="keyword">最後にキーワードを表示する</param>
        /// <param name="dummyTexts">四方から流れるダミーテキスト</param>
        public async UniTask StartTextAnimationAsync(string keyword, string[] dummyTexts)
        {
            var cancellationToken = this.GetCancellationTokenOnDestroy();
            var canvasSize = _canvasRectTransform.sizeDelta;

            _keywordTextPrefab.text = keyword;

            var keywordInstance = await InstantiateAsync(_keywordTextPrefab, _canvasRectTransform)
                .WithCancellation(cancellationToken: cancellationToken );

            keywordInstance[0].rectTransform.anchoredPosition = Vector2.zero;
            keywordInstance[0].gameObject.SetActive(false);

            var dummyTextInstances = new List<TextMeshProUGUI>();
            var animationTasks = new List<UniTask>();

            var dummyInstance = await InstantiateAsync(_dummyTextPrefab, dummyTexts.Length, _canvasRectTransform)
                .WithCancellation(cancellationToken: cancellationToken);

            for (int i = 0; i < dummyTexts.Length; i++)
            {
                var dummyString = dummyTexts[i];

                dummyInstance[i].text = dummyString;
                dummyTextInstances.Add(dummyInstance[i]);

                dummyInstance[i].gameObject.SetActive(true);
                dummyInstance[i].fontSize = 120f;

                dummyInstance[i].color = new Color(1, 1, 1, 0f);

                var rectTransform =  dummyInstance[i].rectTransform;
                var (startPos, endPos) = GetStartEndPositionForSide(i % 4, canvasSize);
                rectTransform.anchoredPosition = startPos;

                // 位置のアニメーション
                var motionTask = LMotion.Create(startPos, endPos, 4f)
                    .WithEase(Ease.InOutSine)
                    .WithDelay(i * 0.3f)
                    .BindToAnchoredPosition(rectTransform)
                    .ToUniTask(cancellationToken);
                animationTasks.Add(motionTask);

                // フェードアウトのアニメーション
                var fadeTask = LMotion.Create(1f, 0f, 2f) // 2秒かけてフェードアウト
                    .WithDelay(i * 0.3f + 1f) // 位置アニメーションより少し遅れて開始
                    .WithEase(Ease.InSine)
                    .BindToColorA( dummyInstance[i])
                    .ToUniTask(cancellationToken);
                animationTasks.Add(fadeTask);
            }

            await UniTask.WhenAll(animationTasks);

            foreach (var dummy in dummyTextInstances)
            {
                if (dummy != null) Destroy(dummy.gameObject);
            }

            // --- キーワードのアニメーション ---
            keywordInstance[0].gameObject.SetActive(true);
            var textTransform = keywordInstance[0].transform;
            var originalColor = keywordInstance[0].color;

            textTransform.localScale = Vector3.one * 2f;
            keywordInstance[0].color = new Color(1, 1, 1, 0f);

            var scaleMotion = LMotion.Create(textTransform.localScale, Vector3.one * 1.5f, 0.6f)
                .WithEase(Ease.OutBack)
                .BindToLocalScale(textTransform);

            var fadeMotion = LMotion.Create(0f, originalColor.a, 0.5f)
                .WithEase(Ease.OutQuad)
                .BindToColorA(keywordInstance[0]);

            await UniTask.WhenAll(
                scaleMotion.ToUniTask(cancellationToken),
                fadeMotion.ToUniTask(cancellationToken)
            );

            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancellationToken);

            await LMotion.Create(originalColor.a, 0f, 1f)
                .BindToColorA(keywordInstance[0])
                .ToUniTask(cancellationToken);

            Destroy(keywordInstance[0].gameObject);
            keywordInstance = null;
        }

        /// <summary>
        /// ダミーテキストが流れる方向を決める
        /// </summary>
        /// <param name="side"></param>
        /// <param name="canvasSize"></param>
        /// <returns></returns>
        private (Vector2 start, Vector2 end) GetStartEndPositionForSide(int side, Vector2 canvasSize)
        {
            var margin = 150f;
            Vector2 start, end;
            float randomPos;

            switch (side)
            {
                case 0: // 右から左へ
                    randomPos = Random.Range(-canvasSize.y / 2, canvasSize.y / 2);
                    start = new Vector2(canvasSize.x / 2 + margin, randomPos);
                    end = new Vector2(-canvasSize.x / 2 - margin, randomPos + Random.Range(-100, 100));
                    break;
                case 1: // 下から上へ
                    randomPos = Random.Range(-canvasSize.x / 2, canvasSize.x / 2);
                    start = new Vector2(randomPos, -canvasSize.y / 2 - margin);
                    end = new Vector2(randomPos + Random.Range(-100, 100), canvasSize.y / 2 + margin);
                    break;
                case 2: // 左から右へ
                    randomPos = Random.Range(-canvasSize.y / 2, canvasSize.y / 2);
                    start = new Vector2(-canvasSize.x / 2 - margin, randomPos);
                    end = new Vector2(canvasSize.x / 2 + margin, randomPos + Random.Range(-100, 100));
                    break;
                default: // 上から下へ (case 3)
                    randomPos = Random.Range(-canvasSize.y / 2, canvasSize.y / 2);
                    start = new Vector2(randomPos, canvasSize.y / 2 + margin);
                    end = new Vector2(randomPos + Random.Range(-100, 100), -canvasSize.y / 2 - margin);
                    break;
            }
            return (start, end);
        }
    }
}
