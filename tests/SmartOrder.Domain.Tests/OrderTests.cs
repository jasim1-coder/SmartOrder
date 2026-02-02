using SmartOrder.Domain.ValueObjects;


namespace SmartOrder.Domain.Tests
{
    public class OrderTests
    {
        [Fact]
        public void Cannot_Cancel_Paid_Order()
        {
            var order = new Order();
            order.AddItem(Guid.NewGuid(), new Money(100, "USD"), 1);
            order.MarkAsPaid();

            Assert.Throws<InvalidOperationException>(() =>
            order.Cancel("Customer requested"));
        }
    }
}
