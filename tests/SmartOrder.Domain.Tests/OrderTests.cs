using SmartOrder.Domain.Aggregates;
using SmartOrder.Domain.ValueObjects;


namespace SmartOrder.Domain.Tests
{
    public class OrderTests
    {
        [Fact]
        public void Cannot_Cancel_Paid_Order()
        {
            var customerId = Guid.NewGuid();
            var order = Order.Create(customerId);
            order.AddItem(Guid.NewGuid(), new Money(100, "USD"), 1);
            order.MarkAsPaid();

            Assert.Throws<InvalidOperationException>(() =>
            order.Cancel("Customer requested"));
        }

        [Fact]
        public void Adding_Item_To_Cancelled_Order_Should_Fail()
        {
            var customerId = Guid.NewGuid();
            var order = Order.Create(customerId);
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
            var customerId = Guid.NewGuid();
            var order = Order.Create(customerId);
            order.Cancel("Customer changed mind");

            Assert.Throws<InvalidOperationException>(() =>
                order.MarkAsPaid());
        }

        [Fact]
        public void Cannot_Pay_Order_Twice()
        {
            var customerId = Guid.NewGuid();
            var order = Order.Create(customerId);
            order.MarkAsPaid();

            Assert.Throws<InvalidOperationException>(() =>
                order.MarkAsPaid());
        }

        [Fact]
        public void Cannot_Cancel_Paid_Orders()
        {
            var customerId = Guid.NewGuid();
            var order = Order.Create(customerId);
            order.MarkAsPaid();

            Assert.Throws<InvalidOperationException>(() =>
                order.Cancel("Customer request"));
        }

        [Fact]
        public void Cannot_Cancel_Order_Twice()
        {
            var customerId = Guid.NewGuid();
            var order = Order.Create(customerId);
            order.Cancel("Changed mind");

            Assert.Throws<InvalidOperationException>(() =>
                order.Cancel("Second attempt"));
        }

    }
}
