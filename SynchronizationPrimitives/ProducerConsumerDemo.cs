using System.Collections.Concurrent;

public class ProducerConsumerDemo : IDisposable
{
    private readonly ConcurrentQueue<int> _queue = new();

    private readonly AutoResetEvent _signal = new(false);

    private int _processedCount;

    public async Task RunAsync()
    {
        Console.WriteLine();
        Console.WriteLine("=== PRODUCER-CONSUMER DEMO ===");

        // Запускаем потребителя.
        // восстановить

        // Запускаем трёх производителей.
        // Produce(1, 5)
        // Produce(2, 5)
        // Produce(3, 5)
        // восстановить

        // Ждём завершения производителей.
        // восстановить

        // Специальное значение означает завершение работы.
        _queue.Enqueue(-1);

        _signal.Set();

        // Ждём завершения потребителя.
        // восстановить

        Console.WriteLine();
        Console.WriteLine($"Обработано элементов: {_processedCount}");
    }

    private void Produce(int producerId, int count)
    {
        for (int i = 1; i <= count; i++)
        {
            int value = producerId * 100 + i;

            _queue.Enqueue(value);

            Console.WriteLine(
                $"Производитель {producerId}: создал {value}");

            // Сообщаем потребителю о появлении данных через _signal.
            // восстановить

            Thread.Sleep(Random.Shared.Next(100, 400));
        }

        Console.WriteLine(
            $"Производитель {producerId}: завершил работу");
    }

    private void Consume()
    {
        while (true)
        {
            // Ждём сигнала о появлении данных.
            // восстановить

            // Забираем все доступные элементы.
            while (_queue.TryDequeue(out int value))
            {
                // -1 — специальный сигнал завершения.
                if (value == -1)
                {
                    Console.WriteLine("Потребитель завершает работу.");
                    return;
                }

                Console.WriteLine(
                    $"Потребитель обработал: {value}");

                Interlocked.Increment(ref _processedCount);

                Thread.Sleep(200);
            }
        }
    }

    public void Dispose()
    {
        _signal.Dispose();
    }
}