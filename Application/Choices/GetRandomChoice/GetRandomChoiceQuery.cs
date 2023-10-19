using Application.Choices.Common;
using MediatR;

namespace Application.Choices.GetRandomChoice;

public record GetRandomChoiceQuery : IRequest<ChoiceResponse>;