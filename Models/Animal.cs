﻿namespace ZooApi.Models
{
    public class Animal
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Especie { get; set; }

        public string Habitat { get; set; }

        public string PaisDeOrigem { get; set; }

        public ICollection<AnimalCuidado> AnimaisCuidados { get; set; } = new List<AnimalCuidado>();
    }
}
