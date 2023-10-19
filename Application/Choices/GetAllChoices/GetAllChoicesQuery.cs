using Application.Choices.Common;
using MediatR;

namespace Application.Choices.GetAllChoices;

public record GetAllChoicesQuery : IRequest<IEnumerable<ChoiceResponse>>;

