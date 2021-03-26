namespace fooddelivery.Models.Enums
{
    public enum DeliveryStatus
    {
        Open = 0,
        InProgress = 1,
        Ready = 2,
        OutForDelivery = 3,
        Delivered = 4,
        NotDelivered = -2,
        Canceled = -1
    }
}