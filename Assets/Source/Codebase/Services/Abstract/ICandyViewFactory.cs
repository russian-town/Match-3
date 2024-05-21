﻿using Source.Codebase.Domain.Models;

namespace Source.Codebase.Services.Abstract
{
    public interface ICandyViewFactory
    {
        void Create(Candy candy);
    }
}