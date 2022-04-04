using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NextTech.PayHub.Accounting.Shared;
using Xunit;

namespace NextTech.PayHub.Accounting.Domain.Tests
{
    public class AmericanExpressTests : IClassFixture<ServiceFixture>
    {
        private ServiceProvider serviceProvider;

        public AmericanExpressTests(ServiceFixture fixture)
        {
            serviceProvider = fixture.ServiceProvider;
        }

        [Fact]
        public void Given_AmericanExpress_Has_Valid_Parameter_Then_Should_Be_Create()
        {
            string cardNumber = "375991270307470";
            string cardOwner = "Larry Loe";
            string cardExpireDate = "02/2023";
            string cardCVC = "7845";             

            var actual = AmericanExpress.New(cardNumber, cardOwner, cardExpireDate, cardCVC);

            actual.CardNumber.Should().Be(cardNumber);
            actual.CardOwner.Should().Be(cardOwner);
            actual.CardExpireDate.Should().Be(cardExpireDate);
            actual.CardCVC.Should().Be(cardCVC);
            actual.ValidCardInformation.Should().BeTrue();
            actual.ValidationsErrors.Should().BeEmpty();
        }

        [Fact]
        public void Given_AmericanExpress_Has_Invalid_CardNumber_Parameter_Then_Should_Be_Validation_Error()
        {
            string cardNumber = "3759912703074701";
            string cardOwner = "Larry Loe";
            string cardExpireDate = "02/2023";
            string cardCVC = "7845";

            var actual = AmericanExpress.New(cardNumber, cardOwner, cardExpireDate, cardCVC);

            actual.CardNumber.Should().Be(cardNumber);
            actual.CardOwner.Should().Be(cardOwner);
            actual.CardExpireDate.Should().Be(cardExpireDate);
            actual.CardCVC.Should().Be(cardCVC);
            actual.ValidCardInformation.Should().BeFalse();
            actual.ValidationsErrors.Should().NotBeEmpty();
            actual.ValidationsErrors.Should().Contain(Constants.Card.Invalid_Card_Message);
        }

        [Fact]
        public void Given_AmericanExpress_Has_Invalid_CardExpireDate_Parameter_Then_Should_Be_Validation_Error()
        {
            string cardNumber = "375991270307470";
            string cardOwner = "Larry Loe";
            string cardExpireDate = "02/20231";
            string cardCVC = "7845";

            var actual = AmericanExpress.New(cardNumber, cardOwner, cardExpireDate, cardCVC);

            actual.CardNumber.Should().Be(cardNumber);
            actual.CardOwner.Should().Be(cardOwner);
            actual.CardExpireDate.Should().Be(cardExpireDate);
            actual.CardCVC.Should().Be(cardCVC);
            actual.ValidCardInformation.Should().BeFalse();
            actual.ValidationsErrors.Should().NotBeEmpty();
            actual.ValidationsErrors.Should().Contain(Constants.Card.Card_Expired_Date_Or_Invalid_Date_Message);
        }

        [Fact]
        public void Given_AmericanExpress_Has_Invalid_CardCVCNumber_Parameter_Then_Should_Be_Validation_Error()
        {
            string cardNumber = "375991270307470";
            string cardOwner = "Larry Loe";
            string cardExpireDate = "02/2024";
            string cardCVC = "17845";

            var actual = AmericanExpress.New(cardNumber, cardOwner, cardExpireDate, cardCVC);

            actual.CardNumber.Should().Be(cardNumber);
            actual.CardOwner.Should().Be(cardOwner);
            actual.CardExpireDate.Should().Be(cardExpireDate);
            actual.CardCVC.Should().Be(cardCVC);
            actual.ValidCardInformation.Should().BeFalse();
            actual.ValidationsErrors.Should().NotBeEmpty();
            actual.ValidationsErrors.Should().Contain(Constants.Card.Cvc_Number_Invalid_Message);
        }
    }
}