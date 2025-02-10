using System;

public interface IGameEvent
{
    // Event'in oluşturulma zamanını takip etmek için
    DateTime TimeStamp { get; }
    // Event'in benzersiz tanımlayıcısı
    Guid EventId { get; }
} 