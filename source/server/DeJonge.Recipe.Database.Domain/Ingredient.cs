namespace DeJonge.Recipe.Database.Domain
{
    public class Ingredient
    {
        public string Name { get; set; } = null!;

        public string Quantity { get; set; } = null!;

        public bool Optional { get; set; }
    }
}
