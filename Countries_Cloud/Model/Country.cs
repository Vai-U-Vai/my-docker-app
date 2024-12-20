using System.ComponentModel.DataAnnotations;

namespace Countries_Cloud.Model
{
    public class Country
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
       
        [Required]
        [StringLength(2)]
        public string Alpha2Code { get; set; } = string.Empty;
        public string Alpha3Code { get; set; } = string.Empty;
        public string NumericCode { get; set; } = string.Empty;
        public Country() { }
        public Country(string name, string alpha2Code, string alpha3Code, string numericCode)
        {            
            Name = name;
            Alpha2Code = alpha2Code;
            Alpha3Code = alpha3Code;
            NumericCode = numericCode;
        }

        public override string ToString()
        {
            return $"{Id} - {Name}; " +
                $"Alpha-2: {Alpha2Code}; " +
                $"Alpha-3: {Alpha3Code}; " +
                $"Numeric: {NumericCode}";
        }

    }
}
