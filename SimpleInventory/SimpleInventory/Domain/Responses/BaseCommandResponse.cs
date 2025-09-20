namespace SimpleInventory.Domain.Responses
{
    public class BaseCommandResponse
    {
        public int Id { get; set; }

        public string Entity {  get; set; }

        public string Message { get; set; }
    }
}
