namespace Day8_hw.Models
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public int YearOfProduction { get; set; }
        public int Price { get; set; }
        public string BodyType { get; set; }
        public double EngineVolume { get; set; }
        public bool IsClearedInKazakhstan { get; set; }
        public string Comment { get; set; }
    }
}
