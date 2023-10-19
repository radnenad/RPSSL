using Application.Abstractions;
using Application.Choices.Common;
using MediatR;

namespace Application.Choices.GetRandomChoice;

public class GetRandomChoiceQueryHandler : IRequestHandler<GetRandomChoiceQuery, ChoiceResponse>
{
    private readonly IRandomChoiceService _randomChoiceService;

    public GetRandomChoiceQueryHandler(IRandomChoiceService randomChoiceService)
    {
        _randomChoiceService = randomChoiceService;
    }

    public async Task<ChoiceResponse> Handle(GetRandomChoiceQuery request, CancellationToken cancellationToken)
    {
        var choice = await _randomChoiceService.GetRandomChoice();
        var response = new ChoiceResponse(choice.Id, choice.Name);
        return response;
    }
}