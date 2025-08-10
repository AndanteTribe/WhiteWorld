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
        ///
        /// </summary>
        /// <param name="keyword">最後にキーワードを表示する</param>
        /// <param name="dummyTexts">四方から流れるダミーテキスト</param>
        public async UniTask StartTextAnimationAsync(string keyword, string[] dummyTexts)
        {
            var canvasSize = _canvasRectTransform.sizeDelta;

            _keywordTextPrefab.text = keyword;
            var keywordInstance = Instantiate(_keywordTextPrefab, _canvasRectTransform);

            keywordInstance.rectTransform.anchoredPosition = Vector2.zero;
            keywordInstance.gameObject.SetActive(false);

            var dummyTextInstances = new List<TextMeshProUGUI>();
            var animationTasks = new List<UniTask>();

            for (int i = 0; i < dummyTexts.Length; i++)
            {
                var dummyString = dummyTexts[i];
                var dummyInstance = Instantiate(_dummyTextPrefab, transform);

                dummyInstance.text = dummyString;
                dummyTextInstances.Add(dummyInstance);

                dummyInstance.gameObject.SetActive(true);
                dummyInstance.fontSize = 120f;

                dummyInstance.color = new Color(dummyInstance.color.r, dummyInstance.color.g, dummyInstance.color.b, 0f);

                var rectTransform = dummyInstance.rectTransform;
                var (startPos, endPos) = GetStartEndPositionForSide(i % 4, canvasSize);
                rectTransform.anchoredPosition = startPos;

                // 位置のアニメーション
                var motionTask = LMotion.Create(startPos, endPos, 4f)
                    .WithEase(Ease.InOutSine)
                    .WithDelay(i * 0.3f)
                    .BindToAnchoredPosition(rectTransform)
                    .ToUniTask();
                animationTasks.Add(motionTask);

                // フェードアウトのアニメーション
                var fadeTask = LMotion.Create(1f, 0f, 2f) // 2秒かけてフェードアウト
                    .WithDelay(i * 0.3f + 1f) // 位置アニメーションより少し遅れて開始
                    .WithEase(Ease.InSine)
                    .BindToColorA(dummyInstance)
                    .ToUniTask();
                animationTasks.Add(fadeTask);
            }

            await UniTask.WhenAll(animationTasks);

            foreach (var dummy in dummyTextInstances)
            {
                if (dummy != null) Destroy(dummy.gameObject);
            }

            // --- キーワードのアニメーション ---
            keywordInstance.gameObject.SetActive(true);
            var textTransform = keywordInstance.transform;
            var originalColor = keywordInstance.color;

            textTransform.localScale = Vector3.one * 2f;
            keywordInstance.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

            var scaleMotion = LMotion.Create(textTransform.localScale, Vector3.one * 1.5f, 0.6f)
                .WithEase(Ease.OutBack)
                .BindToLocalScale(textTransform);

            var fadeMotion = LMotion.Create(0f, originalColor.a, 0.5f)
                .WithEase(Ease.OutQuad)
                .BindToColorA(keywordInstance);

            await UniTask.WhenAll(
                scaleMotion.ToUniTask(),
                fadeMotion.ToUniTask()
            );

            await UniTask.Delay(TimeSpan.FromSeconds(1));

            await LMotion.Create(originalColor.a, 0f, 1f)
                .BindToColorA(keywordInstance);

            Destroy(keywordInstance.gameObject);
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
