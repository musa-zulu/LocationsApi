namespace LocationsApi.Domain.Entities
{
    public class Category
    {       
        public int CategoryId { get; set; }
        public string DataId { get; set; }
        public string Name { get; set; }
        public string PluralName { get; set; }
        public string ShortName { get; set; }
        public string IconPrefix { get; set; }
        public string IconSuffic { get; set; }
        public bool Primary { get; set; }
    }
}
