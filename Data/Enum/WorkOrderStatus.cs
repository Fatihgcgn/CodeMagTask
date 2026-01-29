namespace Data.Enum;

public enum WorkOrderStatus
{
    Created = 1,        
    Serializing = 2,    // Serial üretimi başladı
    Serialized = 3,     // Serial üretimi tamamlandı
    Aggregating = 4,    // Koli / Paletleme
    Completed = 5,      // Bitti
    Cancelled = 9
}