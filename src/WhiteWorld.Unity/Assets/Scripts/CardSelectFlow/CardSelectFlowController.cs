using System;
using System.Threading;
using CardSelectFlow.Interface;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
/// <summary>
/// カード選択フロウの状態を管理するクラス
/// </summary>
public class CardSelectFlowController : ICardSelectFlowController
{
    /// <summary>
    /// フロウが始まったときに発火されるイベント
    /// </summary>
    public Observable<Unit> OnStartCardSelectFlow => _onStartCardSelectFlow;
    /// <summary>
    /// カードを出現させる処理が始まったときに発火されるイベント
    /// </summary>
    public Observable<Unit> OnStartAppearCardFlow => _onStartAppearCardFlow;
    /// <summary>
    /// カードを出現させる処理が終わったときに発火されるイベント
    /// </summary>
    public Observable<Unit> OnEndAppearCardFlow => _onEndAppearCardFlow;
    /// <summary>
    /// カードが選択されたときに発火されるイベント
    /// </summary>
    public Observable<Unit> OnCardSelected => _onCardSelected;
    /// <summary>
    /// カードが消えるアニメーションが始まった時に発火されるイベント
    /// </summary>
    public Observable<Unit> OnStartCardDisappearAnimation => _onStartCardDisappearAnimation;
    /// <summary>
    /// カードが消えるアニメーションが終わったときに発火されるイベント
    /// </summary>
    public Observable<Unit> OnEndCardDisappearAnimation => _onEndCardDisappearAnimation;
    /// <summary>
    /// フロウが終わったときに発火されるイベント
    /// </summary>
    public Observable<Unit> OnEndCardSelectFlow => _onEndCardSelectFlow;

    private readonly Subject<Unit> _onStartCardSelectFlow = new();
    private readonly Subject<Unit> _onStartAppearCardFlow = new();
    private readonly Subject<Unit> _onEndAppearCardFlow = new();
    private readonly Subject<Unit> _onCardSelected = new();
    private readonly Subject<Unit> _onStartCardDisappearAnimation = new();
    private readonly Subject<Unit> _onEndCardDisappearAnimation = new();
    private readonly Subject<Unit> _onEndCardSelectFlow = new();

    //TODO Inject
    private ICardStateController _stateController;

    private async UniTask Flow()
    {
        //トークン発行
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        //スタートを通知
        _onStartCardSelectFlow.OnNext(Unit.Default);

        //出現アニメーションの終了を待つ
        await WaitCardAppearFlow(token);

        //プレイヤーの選択を待つ
        await WaitCardSelected(token);

        //消えるアニメーションを待つ
        await WaitCardDisAppearFlow(token);

        //終了
        _onEndCardSelectFlow.OnNext(Unit.Default);
    }

    private async UniTask WaitCardAppearFlow(CancellationToken token)
    {
        //出現処理の開始を通知
        _onStartAppearCardFlow.OnNext(Unit.Default);

        await _stateController.OnEndCardAppear
            .FirstAsync(token);

        //出現処理の終了を通知
        _onEndAppearCardFlow.OnNext(Unit.Default);
    }

    private async UniTask WaitCardSelected(CancellationToken token)
    {
        await UniTask.Delay(100);

        //選択が完了したことを通知
        //TODO これうまくやって選択されたカードを保持したいね
        _onCardSelected.OnNext(Unit.Default);
    }

    private async UniTask WaitCardDisAppearFlow(CancellationToken token)
    {
        //カードを非表示にするアニメーションの開始を通知
        _onStartCardDisappearAnimation.OnNext(Unit.Default);

        //TODO
        await UniTask.Delay(100);

        //カードを非表示にするアニメーションの終了を通知
        _onEndCardDisappearAnimation.OnNext(Unit.Default);
    }
}
