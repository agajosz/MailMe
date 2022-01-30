using MailMe.Application.Fixtures.Entity;
using NUnit.Framework;

namespace MailMe.Application.UnitTests.Fixtures.Entity
{
    public class NewsletterFixtureUnitTests
    {
        [Test]
        public void GivenFixtureWhenAwayGoalsAreBiggerThanHomeGoals_WhenGettingAwayWinner_ReturnsTrue()
        {
            const bool expected = true;
            var fixture = new NewsletterFixture { AwayGoals = 3, HomeGoals = 2};

            var actual = fixture.AwayWinner;
            
            Assert.That(expected.Equals(actual));
        }
        [Test]
        public void GivenFixtureWhenAwayGoalsAreLessThanHomeGoals_WhenGettingAwayWinner_ReturnsFalse()
        {
            const bool expected = false;
            var fixture = new NewsletterFixture { AwayGoals = 2, HomeGoals = 3};

            var actual = fixture.AwayWinner;
            
            Assert.That(expected.Equals(actual));
        }
        [Test]
        public void GivenFixtureWhenAwayGoalsAreBiggerThanHomeGoals_WhenGettingHomeWinner_ReturnsFalse()
        {
            const bool expected = false;
            var fixture = new NewsletterFixture { AwayGoals = 3, HomeGoals = 2};

            var actual = fixture.HomeWinner;
            
            Assert.That(expected.Equals(actual));
        }
        [Test]
        public void GivenFixtureWhenAwayGoalsAreLessThanHomeGoals_WhenGettingHomeWinner_ReturnsTrue()
        {
            const bool expected = true;
            var fixture = new NewsletterFixture { AwayGoals = 2, HomeGoals = 3};

            var actual = fixture.HomeWinner;
            
            Assert.That(expected.Equals(actual));
        }
        [Test]
        public void GivenFixtureWhenAwayGoalsAreEqualToHomeGoals_WhenGettingHomeWinner_ReturnsFalse()
        {
            const bool expected = false;
            var fixture = new NewsletterFixture { AwayGoals = 2, HomeGoals = 2};

            var actual = fixture.HomeWinner;
            
            Assert.That(expected.Equals(actual));
        }
        [Test]
        public void GivenFixtureWhenAwayGoalsAreEqualToHomeGoals_WhenGettingAwayWinner_ReturnsFalse()
        {
            const bool expected = false;
            var fixture = new NewsletterFixture { AwayGoals = 2, HomeGoals = 2};

            var actual = fixture.AwayWinner;
            
            Assert.That(expected.Equals(actual));
        }
    }
}