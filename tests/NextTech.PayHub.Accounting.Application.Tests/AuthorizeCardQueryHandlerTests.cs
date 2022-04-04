using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NextTech.PayHub.Accounting.Application.Helper;
using NextTech.PayHub.Accounting.Domain;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NextTech.PayHub.Accounting.Application.Tests
{
    public class AuthorizeCardQueryHandlerTests : IClassFixture<ServiceFixture>
    {
        Mock<ICardRepository> cardRepositoryMock = new Mock<ICardRepository>();
        private readonly ICardTypeParser cardTypeParser;

        readonly IMediator mediator;
        private ServiceProvider serviceProvider;
        public AuthorizeCardQueryHandlerTests(ServiceFixture fixture)
        {
            serviceProvider = fixture.ServiceProvider;
            mediator = serviceProvider.GetService<IMediator>();
            cardTypeParser = serviceProvider.GetService<ICardTypeParser>();
        }

        [Theory]
        [MemberData(nameof(ValidAmericanExpressCards))]
        public async Task Given_AmericanExpress_Has_Valid_Parameter_Then_Should_Be_Return_CardType(string cardNumber, string cardOwner, string cardExpireDate, string cardCVC)
        {
            var card = AmericanExpress.New(cardNumber, cardOwner, cardExpireDate, cardCVC);
            cardRepositoryMock.Setup(st => st.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), default)).Returns(Task.FromResult<Card>(card));

            var command = AuthorizeCardQuery.Create(cardOwner, cardNumber, cardExpireDate, cardCVC);

            var commandHandler = new AuthorizeCardQueryHandler(cardTypeParser, cardRepositoryMock.Object);

            var actual = await commandHandler.Handle(command, CancellationToken.None);

            var expectedType = new AmericanExpress();

            actual.CartType.Should().NotBeNull();
            actual.CartType.Should().Be(expectedType.GetType().ToString());         

            cardRepositoryMock.Verify(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(ValidMasterCards))]
        public async Task Given_MasterCard_Has_Valid_Parameter_Then_Should_Be_Return_CardType(string cardNumber, string cardOwner, string cardExpireDate, string cardCVC)
        {
            var card = MasterCard.New(cardNumber, cardOwner, cardExpireDate, cardCVC);
            cardRepositoryMock.Setup(st => st.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), default)).Returns(Task.FromResult<Card>(card));

            var command = AuthorizeCardQuery.Create(cardOwner, cardNumber, cardExpireDate, cardCVC);

            var commandHandler = new AuthorizeCardQueryHandler(cardTypeParser, cardRepositoryMock.Object);

            var actual = await commandHandler.Handle(command, CancellationToken.None);

            var expectedType = new MasterCard();

            actual.CartType.Should().NotBeNull();
            actual.CartType.Should().Be(expectedType.GetType().ToString());

            await Task.CompletedTask;

            cardRepositoryMock.Verify(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(ValidVisaCards))]
        public async Task Given_Visa_Has_Valid_Parameter_Then_Should_Be_Return_CardType(string cardNumber, string cardOwner, string cardExpireDate, string cardCVC)
        {
            var card = Visa.New(cardNumber, cardOwner, cardExpireDate, cardCVC);
            cardRepositoryMock.Setup(st => st.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), default)).Returns(Task.FromResult<Card>(card));

            var command = AuthorizeCardQuery.Create(cardOwner, cardNumber, cardExpireDate, cardCVC);

            var commandHandler = new AuthorizeCardQueryHandler(cardTypeParser, cardRepositoryMock.Object);

            var actual = await commandHandler.Handle(command, CancellationToken.None);

            var expectedType = new Visa();

            actual.CartType.Should().NotBeNull();
            actual.CartType.Should().Be(expectedType.GetType().ToString());

            await Task.CompletedTask;

            cardRepositoryMock.Verify(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(InvalidCardNumber))]
        public async Task Given_Invalid_Parameter_Then_Should_Be_Throw_CardInvalidException(string cardNumber, string cardOwner, string cardExpireDate, string cardCVC)
        {
            var card = AmericanExpress.New(cardNumber, cardOwner, cardExpireDate, cardCVC);

            var command = AuthorizeCardQuery.Create(cardOwner, cardNumber, cardExpireDate, cardCVC);

            var commandHandler = new AuthorizeCardQueryHandler(cardTypeParser, cardRepositoryMock.Object);

            Func<Task> actual = async () => await commandHandler.Handle(command, CancellationToken.None);

            await actual.Should().ThrowAsync<CardInvalidException>();
        }

        public static IEnumerable<object[]> ValidAmericanExpressCards => new List<object[]>()
        {
            new object[] { "341473328272163", "John Doe", "06/2024","1258" },
            new object[] { "343899323972191", "Gordon Norman", "01/2025", "8597" },
            new object[] { "347425294152997", "Marta Moe", "06/2025", "528" },
            new object[] {"345742344802146", "Larry Loe", "02/2023", "785" },
            new object[] {"343876755249611", "Karren Koe", "01/2023", "4526" },
        };

        public static IEnumerable<object[]> ValidMasterCards => new List<object[]>()
        {
            new object[] { "5398018211981551", "John Doe", "06/2024","128" },
            new object[] { "5506721639486074", "Gordon Norman", "01/2025", "857" },
            new object[] { "5281822974099000", "Marta Moe", "06/2025", "528" },
            new object[] { "5555568530940190", "Larry Loe", "02/2023", "785" },
            new object[] { "5357581544207027", "Karren Koe", "01/2023", "456" },
        };

        public static IEnumerable<object[]> ValidVisaCards => new List<object[]>()
        {
            new object[] { "4024007141221050", "John Doe", "06/2024","128" },
            new object[] { "4532415641025530", "Gordon Norman", "01/2025", "857" },
            new object[] { "4916585548763858", "Marta Moe", "06/2025", "528" },
            new object[] { "4716033485844656", "Larry Loe", "02/2023", "785" },
            new object[] { "4886080874056420", "Karren Koe", "01/2023", "456" },
        };

        public static IEnumerable<object[]> InvalidCardNumber => new List<object[]>()
        {
            new object[] { "", "John Doe", "06/2024","128" },
            new object[] { "45321564125530", "Gordon Norman", "01/2025", "857" },
            new object[] { "491658554876385832", "Marta Moe", "06/2025", "528" },
            new object[] { "1603348544656", "Larry Loe", "02/2023", "785" },
            new object[] { "486080875620", "Karren Koe", "01/2023", "456" },
        };
    }
}