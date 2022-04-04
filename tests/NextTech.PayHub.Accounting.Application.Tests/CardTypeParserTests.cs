using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NextTech.PayHub.Accounting.Application.Helper;
using NextTech.PayHub.Accounting.Domain;
using System.Linq;
using Xunit;

namespace NextTech.PayHub.Accounting.Application.Tests
{
    public class CardTypeParserTests : IClassFixture<ServiceFixture>
    {
        private ServiceProvider serviceProvider;
        private readonly ICardTypeParser cardTypeParser;
        public CardTypeParserTests(ServiceFixture fixture)
        {
            serviceProvider = fixture.ServiceProvider;
            cardTypeParser = serviceProvider.GetService<ICardTypeParser>();
        }

        [Theory]
        [InlineData(new object[] { "341705489877326" })]
        [InlineData(new object[] { "378205123942384" })]
        [InlineData(new object[] { "349947688935917" })]
        [InlineData(new object[] { "372787700970991" })]
        [InlineData(new object[] { "340034146060742" })]
        public void Given_For_AmericanExpress_Parameter_Then_Should_Be_Math_AmericanExpress_Card(string cardNumber)
        {
            var actual = this.cardTypeParser.RegisteredCardTypes.SingleOrDefault(x => x.IsMatch(cardNumber));

            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<AmericanExpress>();
        }

        [Theory]
        [InlineData(new object[] { "5420694919132101" })]
        [InlineData(new object[] { "5417837491844107" })]
        [InlineData(new object[] { "5518258597797396" })]
        [InlineData(new object[] { "5251545145893508" })]
        [InlineData(new object[] { "5208523791607013" })]
        public void Given_For_MasterCard_Parameter_Then_Should_Be_Math_MasterCard_Card(string cardNumber)
        {
            var actual = this.cardTypeParser.RegisteredCardTypes.SingleOrDefault(x => x.IsMatch(cardNumber));

            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<MasterCard>();
        }

        [Theory]
        [InlineData(new object[] { "4929422913474421" })]
        [InlineData(new object[] { "4024007136090486" })]
        [InlineData(new object[] { "4532018264919177" })]
        [InlineData(new object[] { "4716382484285654" })]
        [InlineData(new object[] { "4929513791955125" })]
        public void Given_For_Visa_Parameter_Then_Should_Be_Math_Visa_Card(string cardNumber)
        {
            var actual = this.cardTypeParser.RegisteredCardTypes.SingleOrDefault(x => x.IsMatch(cardNumber));

            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<Visa>();
        }

        [Theory]
        [InlineData(new object[] { "3417058987726" })]
        [InlineData(new object[] { "525154514589350822" })]
        [InlineData(new object[] { "4716382484285654ss" })]
        [InlineData(new object[] { "49295955125" })]
        [InlineData(new object[] { "5251545145893508dddd" })]
        public void Given_For_Invalid_Card_Number_Then_Should_Be_Null(string cardNumber)
        {
            var actual = this.cardTypeParser.RegisteredCardTypes.SingleOrDefault(x => x.IsMatch(cardNumber));

            actual.Should().BeNull();
        }
    }
}
