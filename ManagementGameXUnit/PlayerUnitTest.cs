using ManagementGameCore;
using Moq;

namespace ManagementGameXUnit
{
    public class PlayerUnitTest
    {
        [Fact]
        public void MonthlyExpenses_Default_ReturnPositiveAccount()
        {
            // Arrange
            Player player = new Player();
            int expenses = 0;
            player.Factories.ForEach(f =>
            {
                expenses += f.Automated ? 1500 : 1000;
            });
            expenses += player.Materials * 300;
            expenses += player.Products * 500;

            Money expectedAccount = new(10000 - expenses, 0);

            // Act
            player.MonthlyExpenses();

            // Assert
            Assert.True(player.Account > 0);
            Assert.Equal(expectedAccount, player.Account);
        }

        [Fact]
        public void MonthlyExpenses_Default_ReturnNegativeAccount()
        {
            Player player = new Player();

            for (int i = 0; i < 3; i++)
                player.MonthlyExpenses();

            Assert.True(player.Account < 0);
        }

        [Fact]
        public void MonthlyExpenses_Default_PlayerMonthlyExpensesNotify()
        {
            Player player = new Player();

            var monthlyExpensesEvent = Assert.Raises<PlayerEventArgs>(
                h => Player.PlayerMonthlyExpensesNotify += h,
                h => Player.PlayerMonthlyExpensesNotify -= h,
                () => player.MonthlyExpenses());

            Assert.NotNull(monthlyExpensesEvent);
            Assert.Equal(player, monthlyExpensesEvent.Sender);
            //Assert.Equal(new PlayerEventArgs(player), monthlyExpensesEvent.Arguments);
        }

        public void MaterialAuction_RequestResponseInfo_ReceiveMaterials()
        {
            Player player = new();

            var mockBanker = new Mock<Banker>();
            var mockPlatform = new Mock<IPlayerPlatform>();
        }
        
    }
}