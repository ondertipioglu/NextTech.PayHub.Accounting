using AutoMapper;
using MediatR;
using NextTech.Core.MediatR.Queries;
using NextTech.PayHub.Accounting.Application.Helper;
using NextTech.PayHub.Accounting.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NextTech.PayHub.Accounting.Application
{
    public class AuthorizeCardQueryHandler : IQueryHandler<AuthorizeCardQuery, AuthorizeCardQueryResponse>
    {
        private readonly ICardRepository cardRepository;
        private readonly ICardTypeParser cardTypeParser;

        public AuthorizeCardQueryHandler(ICardTypeParser cardTypeParser, ICardRepository cardRepository)
        {
            this.cardTypeParser = cardTypeParser;
            this.cardRepository = cardRepository;
        }

        public async Task<AuthorizeCardQueryResponse> Handle(AuthorizeCardQuery request, CancellationToken cancellationToken)
        {
            var response = new AuthorizeCardQueryResponse();

            var card = this.cardTypeParser.RegisteredCardTypes.SingleOrDefault(x => x.IsMatch(request.CardNumber));

            if (card is null) throw new CardInvalidException(Constants.Card.Invalid_Card_Message);

            var cardObject = card.Initialize(request.CardNumber, request.CardOwner, request.CardExpireDate, request.CvcNumber);

            if (cardObject.ValidCardInformation)
            {
                var cardInDb = await cardRepository.GetAsync(request.CardNumber, request.CardOwner, request.CardExpireDate, request.CvcNumber, cancellationToken);

                if (cardInDb is null) throw new AccountNotFound(Constants.Card.Invalid_Account_Information_Message); ;

                response.CartType = cardObject.GetType().ToString();
                return response;
            }
            else
            {
                var validationResultMessage = cardObject.ValidationsErrors?.Aggregate((a, b) => a + ", " + b);
                throw new CardInvalidException($"Validation Error(s): " + validationResultMessage);
            }
        }
    }
}
