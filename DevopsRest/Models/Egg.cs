namespace DevopsRest.Models
{
    public class Egg
    {
        public int Id { get; set; }
        private string _name;
        private double _price;

        public string Name
        {
            get => _name;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Must not be null");
                }
                if (value.Length < 1)
                {
                    throw new ArgumentOutOfRangeException("Must be atleast one char");
                }
                _name = value;
            }
        }
        public double Price
        {
            get => _price;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("the price cannot be negativ");

                }
                _price = value;
            }
        }

        public Egg(int id, string name, double price) {
            Id = id;
            Name = name;
            Price = price;
        }
        public Egg(string name, double price) {

            Name = name;
            Price = price;
        }
        //Default value when no data is given 
        public Egg() {
            Id = 0;
            Name = "Unknown";
            Price = 19.23;
        }

        public override string ToString() {
            return $"The Egg {Id}, {Name} costs {Price}";
        }

    }
}
