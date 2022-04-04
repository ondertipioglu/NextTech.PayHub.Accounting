using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NextTech.PayHub.Accounting.Shared;
using Xunit;

namespace NextTech.PayHub.Accounting.Domain.Tests
{
    public class VisaTests : IClassFixture<ServiceFixture>
    {
        private ServiceProvider serviceProvider;

        public VisaTests(ServiceFixture fixture)
        {
            serviceProvider = fixture.ServiceProvider;
        }

        [Fact]
        public void Given_Visa_Has_Valid_Parameter_Then_Should_Be_Create()
        {
            string cardNumber = "4556810326147131";
            string cardOwner = "Karren Koe";
            string cardExpireDate = "07/2027";
            string cardCVC = "458";

            var actual = Visa.New(cardNumber, cardOwner, cardExpireDate, cardCVC);

            actual.CardNumber.Should().Be(cardNumber);
            actual.CardOwner.Should().Be(cardOwner);
            actual.CardExpireDate.Should().Be(cardExpireDate);
            actual.CardCVC.Should().Be(cardCVC);
            actual.ValidCardInformation.Should().BeTrue();
            actual.ValidationsErrors.Should().BeEmpty();
        }

        [Fact]
        public void Given_Visa_Has_Invalid_CardNumber_Parameter_Then_Should_Be_Validation_Error()
        {
            string cardNumber = "455681032614713111";
            string cardOwner = "Karren Koe";
            string cardExpireDate = "07/2027";
            string cardCVC = "784";

            var actual = Visa.New(cardNumber, cardOwner, cardExpireDate, cardCVC);

            actual.CardNumber.Should().Be(cardNumber);
            actual.CardOwner.Should().Be(cardOwner);
            actual.CardExpireDate.Should().Be(cardExpireDate);
            actual.CardCVC.Should().Be(cardCVC);
            actual.ValidCardInformation.Should().BeFalse();
            actual.ValidationsErrors.Should().NotBeEmpty();
            actual.ValidationsErrors.Should().Contain(Constants.Card.Invalid_Card_Message);
        }

        [Fact]
        public void Given_Visa_Has_Invalid_CardExpireDate_Parameter_Then_Should_Be_Validation_Error()
        {
            string cardNumber = "4556810326147131";
            string cardOwner = "Karren Koe";
            string cardExpireDate = "01/2017";
            string cardCVC = "785";

            var actual = Visa.New(cardNumber, cardOwner, cardExpireDate, cardCVC);

            actual.CardNumber.Should().Be(cardNumber);
            actual.CardOwner.Should().Be(cardOwner);
            actual.CardExpireDate.Should().Be(cardExpireDate);
            actual.CardCVC.Should().Be(cardCVC);
            actual.ValidCardInformation.Should().BeFalse();
            actual.ValidationsErrors.Should().NotBeEmpty();
            actual.ValidationsErrors.Should().Contain(Constants.Card.Card_Expired_Date_Or_Invalid_Date_Message);
        }

        [Fact]
        public void Given_Visa_Has_Invalid_CardCVCNumber_Parameter_Then_Should_Be_Validation_Error()
        {
            string cardNumber = "4556810326147131";
            string cardOwner = "Karren Koe";
            string cardExpireDate = "07/2027";
            string cardCVC = "178405";

            var actual = Visa.New(cardNumber, cardOwner, cardExpireDate, cardCVC);

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
