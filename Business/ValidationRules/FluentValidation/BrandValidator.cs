using System;
using System.Collections.Generic;
using System.Text;
using Entity.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class BrandValidator : AbstractValidator<Brand>
    {
        public BrandValidator()
        {
            RuleFor(b => b.Name).NotEmpty();
            RuleFor(b => b.Name).MinimumLength(2);
            //RuleFor(b => b.Name).Must(StartWithA).WithMessage("Marka adı A harfi ile başlamalı!");
        }

        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
