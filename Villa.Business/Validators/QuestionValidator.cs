using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Villa.Entity.Entities;

namespace Villa.Business.Validators
{
    public class QuestionValidator : AbstractValidator<Quest>
    {
        public QuestionValidator()
        {
            RuleFor(x => x.Question).NotEmpty().WithMessage("Soru alanı boş geçilemez");
            RuleFor(x => x.Question).MaximumLength(500).WithMessage("Soru alanı en fazla 500 karakter olabilir");
            RuleFor(x => x.Question).MinimumLength(5).WithMessage("Soru alanı en az 5 karakter olabilir");
            RuleFor(x => x.Answer).NotEmpty().WithMessage("Cevap alanı boş geçilemez");
            RuleFor(x => x.Answer).MaximumLength(500).WithMessage("Cevap alanı en fazla 500 karakter olabilir");
        }
    }
}
