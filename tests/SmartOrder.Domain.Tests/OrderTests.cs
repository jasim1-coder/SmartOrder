using SmartOrder.Domain.Aggregates;
using SmartOrder.Domain.ValueObjects;


namespace SmartOrder.Domain.Tests
{
    public class OrderTests
    {
        [Fact]
        public void Cannot_Cancel_Paid_Order()
        {
            var order = Order.Create();
            order.AddItem(Guid.NewGuid(), new Money(100, "USD"), 1);
            order.MarkAsPaid();

            Assert.Throws<InvalidOperationException>(() =>
            order.Cancel("Customer requested"));
        }

        [Fact]
        public void Adding_Item_To_Cancelled_Order_Should_Fail()
        {
            var order = Order.Create();
            order.Cancel("Customer cancelled");

            Assert.Throws<InvalidOperationException>(() =>
                order.AddItem(
                    Guid.NewGuid(),
                    new Money(50, "USD"),
                    1
                ));
        }

    }
}
