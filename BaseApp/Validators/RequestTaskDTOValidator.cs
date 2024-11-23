using BaseApp.DTO;
using FluentValidation;

namespace BaseApp.Validators
{
    public class RequestTaskDTOValidator : AbstractValidator<RequestTaskDTO>
    {

        public RequestTaskDTOValidator()
        {
            RuleFor(a => a.Title)
                .MinimumLength(2)
                .MaximumLength(100)
                .WithMessage("Task's name must be from 2 to 100 characters");

            RuleFor(a => a.Description)
               .MinimumLength(2)
               .MaximumLength(200)
               .WithMessage("Task's name must be from 2 to 200 characters");

            RuleFor(a => a.Note)
                .MinimumLength(2)
                .WithMessage("Task's note must be 2 characters minimum");

            RuleFor(a => a.StartDate)
                .GreaterThan(DateTime.Now)
                .LessThan(a => a.EndDate)
                .WithMessage("Start date must be greater than current date and less than end date");

        }
    }
}
