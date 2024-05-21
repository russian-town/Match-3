using System;
using System.Threading.Tasks;
using DG.Tweening;
using Source.Codebase.Domain;
using Source.Codebase.Domain.Models;
using Source.Codebase.Presentation;
using Source.Codebase.Services;
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

        public CandyPresenter(CandyView view, Candy candy, IPositionConverter positionConverter)
        {
            _view = view;
            _candy = candy;
            _positionConverter = positionConverter;
        }

        public void Enable()
        {
            _candy.PositionChanged += SetViewPosition;
            _candy.Destroyed += OnCandyDestroyed;

            SetViewPosition();
        }

        public void Disable()
        {
            _candy.PositionChanged -= SetViewPosition;
            _candy.Destroyed -= OnCandyDestroyed;

            _view.transform.DOKill();
        }

        private async void SetViewPosition()
        {
            Vector3 newPosition = _positionConverter.GetWorldFromBoardPosition(_candy.BoardPosition);
            _view.transform.DOMove(newPosition, 0.3f);
            await Task.Delay(TimeSpan.FromSeconds(0.3f));

        }

        private async void OnCandyDestroyed(Candy candy)
        {
            _view.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.OutSine);
            await Task.Delay(TimeSpan.FromSeconds(0.3f));
            
            _view.Destroy();
        }
    }
}