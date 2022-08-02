using HT13.Models;

var africanElephantSpecies = new Species("Eukaryotes", "Animalia", "Chordata", "Mammalia", "Proboscidea", "Elephantidae", "Loxodonta", "L. africana");
var elephantDorie = new Animal("Dorie", africanElephantSpecies);
var redEaredSliderSpecies = new Species("Eukaryotes", "Animalia", "Chordata", "Reptilia", "Testudines", "Emydidae", "Trachemys", "T. scripta");
var natashkaCherepashka = new Animal("Natashka", redEaredSliderSpecies);
var catspecies = new Species("Eukaryotes", "Animalia", "Chordata", "Mammalia", "Carnivora", "Felidae", "Felis", "F. catus");
var kitTheCat = new Animal("Kit", catspecies);
var animals = new List<Animal> {elephantDorie, natashkaCherepashka, kitTheCat };
Console.WriteLine(string.Join("\n", animals));