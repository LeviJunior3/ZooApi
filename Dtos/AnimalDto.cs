using System.Text.Json.Serialization;

namespace ZooApi.Dtos
{
    public class AnimalDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        [JsonPropertyName("dataNascimento")]
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateTime DataNascimento { get; set; }
        public string Especie { get; set; }
        public string Habitat { get; set; }
        public string PaisDeOrigem { get; set; }

        public List<int> CuidadoIds { get; set; } = new();
    }
}
