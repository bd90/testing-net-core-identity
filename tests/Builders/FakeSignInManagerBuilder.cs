﻿using System;
using Moq;
using tests.Helpers;

namespace tests.Builders
{
    public class FakeSignInManagerBuilder
    {
        private Mock<FakeSignInManager> _mock = new Mock<FakeSignInManager>();
        
        public FakeSignInManagerBuilder With(Action<Mock<FakeSignInManager>> mock)
        {
            mock(_mock);
            return this;
        }

        public Mock<FakeSignInManager> Build()
        {
            return _mock;
        }
    }
}