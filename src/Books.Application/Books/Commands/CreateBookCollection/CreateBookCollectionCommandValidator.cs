using Books.Application.Books.Commands.CreateBook;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Books.Commands.CreateBookCollection
{
    public class CreateBookCollectionCommandValidator : AbstractValidator<CreateBookCollectionCommand>
    {
        public CreateBookCollectionCommandValidator()
        {
            RuleFor(x => x.BookCollection).NotNull().ForEach(b =>
            {
                b.NotNull();
                b.SetValidator(new CreateBookCommandValidator());
            });
        }
    }
}
