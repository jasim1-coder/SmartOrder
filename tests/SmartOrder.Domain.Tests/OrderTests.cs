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

        [Fact]
        public void Cannot_Pay_Cancelled_Order()
        {
            var order = Order.Create();
            order.Cancel("Customer changed mind");

            Assert.Throws<InvalidOperationException>(() =>
                order.MarkAsPaid());
        }

        [Fact]
        public void Cannot_Pay_Order_Twice()
        {
            var order = Order.Create();
            order.MarkAsPaid();

            Assert.Throws<InvalidOperationException>(() =>
                order.MarkAsPaid());
        }



    }
}
