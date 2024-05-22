using System;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using Source.Codebase.Domain;
using Source.Codebase.Domain.Constants;
using Source.Codebase.Domain.Models;
using Source.Codebase.Presentation;
using Source.Codebase.Services.Abstract;
using UnityEngine;

namespace Source.Codebase.Controllers.Presenters
{
    public class CandyPresenter : IPresenter
    {
        private readonly CandyView _view;
        private readonly Candy _candy;
        private readonly IPositionConverter _positionConverter;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ICandyService _candyService;
        private readonly float _moveDuration;

        private CancellationTokenSource _cancellationTokenSource;

        public CandyPresenter(CandyView view, Candy candy, IPositionConverter positionConverter)
        {
            _view = view;
            _candy = candy;
            _positionConverter = positionConverter;

            _moveDuration = GameConstants.CandyMoveDuration;
        }

        public void Enable()
        {
            _cancellationTokenSource = new();

            _candy.PositionChanged += SetViewPosition;
            _candy.Destroyed += OnCandyDestroyed;

            SetViewPosition();
        }

        public void Disable()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();

            _candy.PositionChanged -= SetViewPosition;
            _candy.Destroyed -= OnCandyDestroyed;

            _view.transform.DOKill();
            _view.transform.localScale = Vector3.one;
        }

        private async void SetViewPosition()
        {
            Vector3 newPosition = _positionConverter.GetWorldFromBoardPosition(_candy.BoardPosition);
            _view.transform.DOMove(newPosition, _moveDuration);
            await Task.Delay(TimeSpan.FromSeconds(_moveDuration));
        }

        private async void OnCandyDestroyed(Candy candy)
        {
            try
            {
                await _view.PlayExplosion(_cancellationTokenSource.Token);
                _view.Destroy();
            }
            catch (TaskCanceledException exception)
            {
            }
        }
    }
}