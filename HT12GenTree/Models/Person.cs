namespace HT12GenTree.Models
{
    internal abstract class Person
    {
        public const int MINIMAL_AGE_FOR_PARENTIONG = 14; 
        private Date _birthDate;
        private string? _name;
        private Date? _death;

        public Person(string? name, Date birthDate)
        {
            Name = name;
            _birthDate = birthDate ?? throw new ArgumentNullException(nameof(birthDate));
        }

        public Date BirthDate
        {
            get => _birthDate;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (_death != null && value > _death)
                {
                    throw new ArgumentException("The day of death is earlier than the day of birth", nameof(value));
                }

                _birthDate = value;
            }
        }
        public Date? Death
        {
            get => _death;
            set
            {
                if (value != null && value < _birthDate)
                {
                    throw new ArgumentException("The day of death is earlier than the day of birth", nameof(value));
                }

                _death = value;
            }
        }

        public string? Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _name = null;
                }
                else
                {
                    _name = value.Trim();
                }
            }
        }

        public override string ToString() => $"Name - {(_name ?? "Null or empty")}. BirthDay - {_birthDate}. Death - {(_death == null ? "No data" : _death.ToString())}. ";
    }
}
